﻿using Etsi.Ts102034.v010501.XmlSerialization.PackageDiscovery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IpTviewr.UiServices.Discovery
{
    public class UiServicesLogicalNumbers
    {
        UiBroadcastDiscovery UiDiscovery;
        PackageDiscoveryRoot PackageDiscovery;
        string DefaultDomain;
        bool HighDefinitionPriority;

        private class LogicalNumberChannels
        {
            public ServicesCount SD = new ServicesCount();
            public ServicesCount HD = new ServicesCount();

            public void Add(UiBroadcastService uiService)
            {
                var servicesCount = (IsHighDefinitionTv(uiService)) ? HD : SD;
                servicesCount.Add(uiService);
            } // Add

            public IEnumerable<UiBroadcastService> GetSortedServices(bool highDefinitionPriority)
            {
                var list1 = highDefinitionPriority ? HD.GetSortedServices() : SD.GetSortedServices();
                var list2 = highDefinitionPriority ? SD.GetSortedServices() : HD.GetSortedServices();

                return list1.Concat(list2);
            } // GetSortedServices
        } // LogicalNumberChannels

        private class ServicesCount
        {
            private IDictionary<UiBroadcastService, int> Services = new Dictionary<UiBroadcastService, int>();

            public void Add(UiBroadcastService uiService)
            {
                int count;

                if (Services.TryGetValue(uiService, out count))
                {
                    Services[uiService] = count + 1;
                }
                else
                {
                    count = 1;
                    Services.Add(uiService, count);
                } // if-else
            } // Add

            public IEnumerable<UiBroadcastService> GetSortedServices()
            {
                var q = from entry in Services
                        orderby entry.Value descending
                        select entry.Key;

                return q;
            } // GetSortedServices
        } // ServicesCount

        public static void ClearLogicalNumbers(UiBroadcastDiscovery uiDiscovery)
        {
            foreach (var service in uiDiscovery.Services)
            {
                service.ServiceLogicalNumber = null;
            } // foreach service
        } // ClearLogicalNumbers

        public static void AssignLogicalNumbers(UiBroadcastDiscovery uiDiscovery, PackageDiscoveryRoot packageDiscovery, string defaultDomain, bool highDefinitionPriority = true)
        {
            var aux = new UiServicesLogicalNumbers(uiDiscovery, packageDiscovery, defaultDomain, highDefinitionPriority);
            aux.Assign();
        } // AssignLogicalNumbers

        public static bool IsHighDefinitionTv(UiBroadcastService uiService)
        {
            // TODO: get this information in a content provider agnostic way
            // Create IContentProvider and a default implementation
            // Specific details of movistar+ will be provided by an overriden version of the default implementation

            // movistar+ specific
            if (uiService.IsHighDefinitionTv) return true;
            if (uiService.ServiceType == "1") return false; // 1 == SD TV MPEG-2
            if (uiService.ServiceType == "22")
            {
                if (uiService.ReplacementService != null) return true;
            } // if

            return false;
        } // IsHighDefinitionTv

        private UiServicesLogicalNumbers (UiBroadcastDiscovery uiDiscovery, PackageDiscoveryRoot packageDiscovery, string defaultDomain, bool highDefinitionPriority, bool clear = false)
        {
            UiDiscovery = uiDiscovery;
            PackageDiscovery = packageDiscovery;
            DefaultDomain = defaultDomain;
            HighDefinitionPriority = highDefinitionPriority;
        } // constructor

        private void Assign()
        {
            var packages = PackageDiscovery.GetPackages();

            // step 1: create list of logical numbers and the associated channels
            var numbers = GetNumbersList(packages);

            // step 2: count channels
            var cumulativeNumbers = GetCumulativeNumbers(numbers);

            // step 3: assign number to channels
            AssignNumbers(cumulativeNumbers);

            // step 4: assign number to unreferenced channels
            AssignUnreferenced();
        } // Assign

        private Dictionary<string, IList<UiBroadcastService>> GetNumbersList(IEnumerable<Package> packages)
        {
            var numbers = new Dictionary<string, IList<UiBroadcastService>>();

            foreach (var package in packages)
            {
                foreach (var service in package.Services)
                {
                    if (service.TextualIdentifiers == null) continue; // no services assigned to logical number

                    foreach (var textualIdentifier in service.TextualIdentifiers)
                    {
                        IList<UiBroadcastService> listUiServices;

                        var uiServiceKey = UiBroadcastService.GetKey(textualIdentifier, DefaultDomain);
                        var uiService = UiDiscovery.TryGetService(uiServiceKey);
                        if (uiService == null) continue; // service not found in global list of services

                        // get list of services assigned to logical number, or create if not found
                        if (!numbers.TryGetValue(service.LogicalChannelNumber, out listUiServices))
                        {
                            listUiServices = new List<UiBroadcastService>();
                            numbers.Add(service.LogicalChannelNumber, listUiServices);
                        } // if

                        listUiServices.Add(uiService);
                    } /// foreach textualIdentifier
                } // foreach service
            } // foreach package

            return numbers;
        } // GetNumbersList

        private Dictionary<string, LogicalNumberChannels> GetCumulativeNumbers(Dictionary<string, IList<UiBroadcastService>> numbers)
        {
            var cumulativeNumbers = new Dictionary<string, LogicalNumberChannels>(numbers.Count);

            foreach (var entry in numbers)
            {
                var temp = new LogicalNumberChannels();
                cumulativeNumbers.Add(entry.Key, temp);
                foreach (var uiService in entry.Value)
                {
                    temp.Add(uiService);
                } // foreach
            } // foreach entry

            return cumulativeNumbers;
        } // GetCumulativeNumbers

        private void AssignNumbers(Dictionary<string, LogicalNumberChannels> cumulativeNumbers)
        {
            // TODO: feature request: give priority to user-overriden logical numbers, treat them as assigned by packges, or ignore them

            var numbers = new Dictionary<string, int>(UiDiscovery.Services.Count);

            foreach (var entry in cumulativeNumbers)
            {
                string logicalNumber;
                int number;

                // format logical number
                if (int.TryParse(entry.Key, out number))
                {
                    logicalNumber = string.Format("{0:000}", number);
                }
                else
                {
                    logicalNumber = entry.Key;
                } // if-else

                // assign numbers, in order of priority
                var uiServices = entry.Value.GetSortedServices(HighDefinitionPriority);
                foreach (var uiService in uiServices)
                {
                    if (uiService == null) continue;
                    AssignNumber(uiService, logicalNumber, numbers);
                } // foreach
            } // foreach entry
        }// AssignNumbers

        private void AssignNumber(UiBroadcastService uiService, string logicalNumber, IDictionary<string, int> numbers)
        {
            string serviceLogicalNumber;
            int count;

            if (!numbers.TryGetValue(logicalNumber, out count))
            {
                // number not assigned; assign direct logical number
                serviceLogicalNumber = logicalNumber;
                uiService.ServiceLogicalNumber = serviceLogicalNumber;
                count = 0;
            }
            else
            {
                // number already assigned
                var prefix = IsHighDefinitionTv(uiService) ? "H" : "S";
                serviceLogicalNumber = prefix + logicalNumber;

                // avoid duplicated H/S numbers
                if (!numbers.TryGetValue(serviceLogicalNumber, out count))
                {
                    // H/S number not assigned; assign H/S logical number
                    count = 0;
                    uiService.ServiceLogicalNumber = serviceLogicalNumber;
                }
                else
                {
                    // H/S number already assign; create unique H/S number by adding a sufix leter
                    uiService.ServiceLogicalNumber = string.Format("{0}{1}", serviceLogicalNumber, (char)('a' + (count - 1)));
                } // if-else
            } // if-else

            // increase count
            numbers[serviceLogicalNumber] = count + 1;
        } // AssignNumber

        private void AssignUnreferenced()
        {
            var noNumber = 1;
            foreach (var uiService in UiDiscovery.Services)
            {
                if (uiService.ServiceLogicalNumber == null)
                {
                    uiService.ServiceLogicalNumber = string.Format("X{0:000}", noNumber++);
                    continue;
                } // if
            } // foreach uiService
        } // AssignUnreferenced
    } // class UiServicesLogicalNumbers
} // namespace
