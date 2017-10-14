﻿// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpTViewr.Internal.Logos
{
    [Flags]
    public enum Quality
    {
        None = 0x00,
        SD = 0x01,
        HD = 0x02,
    } // Quality

    class Package
    {
        public string Filename;
        public Size Size;
        public Image Image;
        public Graphics G;
        public int MaxWidth;
        public int PosX, PosY;
#if DEBUG
        public long FileSizes;
#endif
    } // class Package
} // namespace