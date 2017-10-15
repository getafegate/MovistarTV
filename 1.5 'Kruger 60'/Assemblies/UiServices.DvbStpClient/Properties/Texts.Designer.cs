﻿// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

namespace IpTviewr.UiServices.DvbStpClient.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Texts {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Texts() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("IpTviewr.UiServices.DvbStpClient.Properties.Texts", typeof(Texts).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please wait. Canceling download....
        /// </summary>
        internal static string CancellingDownloadOperation {
            get {
                return ResourceManager.GetString("CancellingDownloadOperation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Data reception is complete.
        /// </summary>
        internal static string CompletedDownloadDataReception {
            get {
                return ResourceManager.GetString("CompletedDownloadDataReception", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0:N0} of {1:N0}.
        /// </summary>
        internal static string DownloadFragmentProgressFormat {
            get {
                return ResourceManager.GetString("DownloadFragmentProgressFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Other, non related data.
        /// </summary>
        internal static string DownloadOtherData {
            get {
                return ResourceManager.GetString("DownloadOtherData", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0:P1}.
        /// </summary>
        internal static string DownloadSegmentProgressFormat {
            get {
                return ResourceManager.GetString("DownloadSegmentProgressFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to HELP!.
        /// </summary>
        internal static string HandleExceptionHelpButton {
            get {
                return ResourceManager.GetString("HandleExceptionHelpButton", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error has ocurred while fetching data.
        /// </summary>
        internal static string HelperExceptionCaption {
            get {
                return ResourceManager.GetString("HelperExceptionCaption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error has ocurred while fetching data.
        /// </summary>
        internal static string HelperExceptionText {
            get {
                return ResourceManager.GetString("HelperExceptionText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Request cancelled.
        /// </summary>
        internal static string HelperUserCancelledCaption {
            get {
                return ResourceManager.GetString("HelperUserCancelledCaption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The download of the data has been cancelled by the user..
        /// </summary>
        internal static string HelperUserCancelledText {
            get {
                return ResourceManager.GetString("HelperUserCancelledText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The received data can&apos;t be successfully parsed and extracted.
        ///Payload Id: 0x{0:X2}. Data type: {1}..
        /// </summary>
        internal static string ParsePayloadException {
            get {
                return ResourceManager.GetString("ParsePayloadException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {\rtf1\ansi\ansicpg1252\deff0\deflang3082{\fonttbl{\f0\fnil\fcharset0 Calibri;}{\f1\fnil\fcharset2 Symbol;}}
        ///{\colortbl ;\red0\green0\blue255;}
        ///{\*\generator Msftedit 5.41.21.2510;}\viewkind4\uc1\pard\sl240\slmult1\qc\lang10\f0\fs22{\pict\wmetafile8\picw1270\pich1270\picwgoal720\pichgoal720 
        ///010009000003ca0d00000000a10d000000000400000003010800050000000b0200000000050000
        ///000c0230003000030000001e0004000000070104000400000007010400a10d0000410b2000cc00
        ///30003000000000003000300000000000280000003000000030000000 [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string RtfTroubleshootingGuide {
            get {
                return ResourceManager.GetString("RtfTroubleshootingGuide", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}
        ///
        ///Unable to receive data from movistar+ servers due to connectivity issues. (Reason: {1})
        ///Power-cycle your router and try again in a few minutes.
        ///
        ///If the problem persists, please click “HELP!” for guidance on how to troubleshoot connectivity issues..
        /// </summary>
        internal static string SocketException {
            get {
                return ResourceManager.GetString("SocketException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}
        ///
        ///Data reception from movistar+ servers took too much time. This might be an indication of poor network performance or WiFi reception.
        ///Power-cycle your router and try again in a few minutes.
        ///
        ///If the problem persists, please click “HELP!” for guidance on how to troubleshoot connectivity issues..
        /// </summary>
        internal static string TimeoutException {
            get {
                return ResourceManager.GetString("TimeoutException", resourceCulture);
            }
        }
    }
}
