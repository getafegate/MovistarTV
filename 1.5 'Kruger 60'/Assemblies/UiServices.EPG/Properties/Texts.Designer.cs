﻿// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

namespace IpTviewr.UiServices.EPG.Properties {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("IpTviewr.UiServices.EPG.Properties.Texts", typeof(Texts).Assembly);
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
        ///   Looks up a localized string similar to Loading program information....
        /// </summary>
        internal static string EpgDataLoading {
            get {
                return ResourceManager.GetString("EpgDataLoading", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Loading EPG data....
        /// </summary>
        internal static string EpgDataLoadingList {
            get {
                return ResourceManager.GetString("EpgDataLoadingList", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No data.
        /// </summary>
        internal static string EpgNoData {
            get {
                return ResourceManager.GetString("EpgNoData", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Genre not available.
        /// </summary>
        internal static string EpgNoGenre {
            get {
                return ResourceManager.GetString("EpgNoGenre", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Program information is not available.
        /// </summary>
        internal static string EpgNoInformation {
            get {
                return ResourceManager.GetString("EpgNoInformation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Not available.
        /// </summary>
        internal static string EpgNoInformationShort {
            get {
                return ResourceManager.GetString("EpgNoInformationShort", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Parental rating not available.
        /// </summary>
        internal static string EpgNoParentalRating {
            get {
                return ResourceManager.GetString("EpgNoParentalRating", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to After.
        /// </summary>
        internal static string EpgProgramAfterCaption {
            get {
                return ResourceManager.GetString("EpgProgramAfterCaption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Before.
        /// </summary>
        internal static string EpgProgramBeforeCaption {
            get {
                return ResourceManager.GetString("EpgProgramBeforeCaption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Now.
        /// </summary>
        internal static string EpgProgramNowCaption {
            get {
                return ResourceManager.GetString("EpgProgramNowCaption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Then.
        /// </summary>
        internal static string EpgProgramThenCaption {
            get {
                return ResourceManager.GetString("EpgProgramThenCaption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There was an error retrieving the list of programs.
        /// </summary>
        internal static string ObtainingListException {
            get {
                return ResourceManager.GetString("ObtainingListException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ended {0} ago.
        /// </summary>
        internal static string ProgramEnded {
            get {
                return ResourceManager.GetString("ProgramEnded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Started {0} ago.
        /// </summary>
        internal static string ProgramStarted {
            get {
                return ResourceManager.GetString("ProgramStarted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Will start in {0}.
        /// </summary>
        internal static string ProgramWillStart {
            get {
                return ResourceManager.GetString("ProgramWillStart", resourceCulture);
            }
        }
    }
}
