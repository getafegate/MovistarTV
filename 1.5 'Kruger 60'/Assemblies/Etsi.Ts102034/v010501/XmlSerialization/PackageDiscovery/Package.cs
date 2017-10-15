﻿// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

using Etsi.Ts102034.v010501.XmlSerialization.BroadcastDiscovery;
using Etsi.Ts102034.v010501.XmlSerialization.Common;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Etsi.Ts102034.v010501.XmlSerialization.PackageDiscovery
{
    [GeneratedCode("myxsdtool", "0.0.0.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "urn:dvb:metadata:iptv:sdns:2012-1")]
    public partial class Package
    {
        [XmlElement("PackageName")]
        public MultilingualText[] Name;

        [XmlElement("CountryAvailability")]
        public CountryAvailability[] CountryAvailability;

        [XmlElement("PackageDescription")]
        public DescriptionLocation[] Description;

        [XmlElement("Service")]
        public List<PackagedService> Services;

        [XmlElement("PackageReference")]
        public PackageReference[] References;

        [XmlElement("PackageAvailability")]
        public ServiceAvailability[] PackageAvailability;

        [XmlAttribute]
        public string Id;

        [XmlAttribute]
        [DefaultValue(true)]
        public bool Visible;

        public Package()
        {
            this.Visible = true;
        } // default constructor
    } // class Package
} // namespace
