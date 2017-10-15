﻿// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IpTviewr.UiServices.Discovery.Logos
{
    [Serializable]
    [DebuggerStepThrough]
    [XmlRoot(ElementName ="PackedLogos", Namespace = XmlPackedLogosRoot.Namespace)]
    public class XmlPackedLogosRoot
    {
        public const string Namespace = "http://movistartv.alphacentaury.org/schema/2017:PackedLogos";

        [XmlElement("Logo")]
        public PackedLogo[] Logos
        {
            get;
            set;
        } // Logos
    } // class XmlPackedLogosRoot
} // namespace
