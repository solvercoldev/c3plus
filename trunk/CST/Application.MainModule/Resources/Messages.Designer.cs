﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.235
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Application.MainModule.Resources {
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
    internal class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Application.MainModule.Resources.Messages", typeof(Messages).Assembly);
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
        ///   Looks up a localized string similar to Dependency  for this object is a null reference.
        /// </summary>
        internal static string exception_DependenciesAreNotInitialized {
            get {
                return ResourceManager.GetString("exception_DependenciesAreNotInitialized", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot perform transfer, invalid account numbers or one account is locked.
        /// </summary>
        internal static string exception_InvalidAccountsForTransfer {
            get {
                return ResourceManager.GetString("exception_InvalidAccountsForTransfer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid page count.
        /// </summary>
        internal static string exception_InvalidPageCount {
            get {
                return ResourceManager.GetString("exception_InvalidPageCount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid page Index.
        /// </summary>
        internal static string exception_InvalidPageIndex {
            get {
                return ResourceManager.GetString("exception_InvalidPageIndex", resourceCulture);
            }
        }
    }
}
