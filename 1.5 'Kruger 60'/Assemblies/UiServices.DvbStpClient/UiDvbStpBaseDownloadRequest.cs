﻿// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;

namespace IpTviewr.UiServices.DvbStpClient
{
    public abstract class UiDvbStpBaseDownloadRequest
    {
        public UiDvbStpBaseDownloadRequest()
        {
            ReceiveDatagramTimeout = 10000;
            NoDataTimeout = 45000;
            DialogCloseDelay = 500;
        } // constructor

        public int ReceiveDatagramTimeout // in milliseconds
        {
            get;
            set;
        } // ReceiveDatagramTimeout

        public int NoDataTimeout // in milliseconds
        {
            get;
            set;
        } // NoDataTimeout

        public IPAddress MulticastAddress
        {
            get;
            set;
        } // MulticastAddress

        public int MulticastPort
        {
            get;
            set;
        } // MulticastPort

        public string Description
        {
            get;
            set;
        } // Description

        public string DescriptionParsing
        {
            get;
            set;
        } // DescriptionParsing

        public bool AllowXmlExtraWhitespace
        {
            get;
            set;
        } // AllowXmlExtraWhitespace

        public Func<string, string> XmlNamespaceReplacer
        {
            get;
            set;
        } // XmlNamespaceReplacer

        [DefaultValue(500)]
        public int DialogCloseDelay
        {
            get;
            set;
        } // DialogCloseDelay
    } // abstract class UiDvbStpBaseDownloadRequest
} // namespace
