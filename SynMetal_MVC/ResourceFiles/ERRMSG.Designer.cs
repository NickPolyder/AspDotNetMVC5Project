﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ERRMSG {
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
    public class ERRMSG {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ERRMSG() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SynMetal_MVC.ResourceFiles.ERRMSG", typeof(ERRMSG).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ο νέος κωδικός πρόσβασης δεν είναι ίδιος με τον κωδικό επιβεβαίωσης.
        /// </summary>
        public static string NewPassComp {
            get {
                return ResourceManager.GetString("NewPassComp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to .
        /// </summary>
        public static string PassWord {
            get {
                return ResourceManager.GetString("PassWord", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Η επιβεβαίωση κωδικού πρέπει να είναι ίδια με τον κωδικό.
        /// </summary>
        public static string PassWordCon {
            get {
                return ResourceManager.GetString("PassWordCon", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to To πεδίο {0} πρέπει να είναι τουλάχιστον {2} χαρακτήρες.
        /// </summary>
        public static string StringLength {
            get {
                return ResourceManager.GetString("StringLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Το πεδίο {0} πρέπει να είναι μεταξύ {1} και {2}.
        /// </summary>
        public static string StringMinMax {
            get {
                return ResourceManager.GetString("StringMinMax", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Το πεδίο {0} πρέπει να έχει σύμβολα αριθμούς και τα &apos;-&apos;,&apos;_&apos;.
        /// </summary>
        public static string StringRegex {
            get {
                return ResourceManager.GetString("StringRegex", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Μη συμβατό όνομα χρήστη.
        /// </summary>
        public static string UserName {
            get {
                return ResourceManager.GetString("UserName", resourceCulture);
            }
        }
    }
}