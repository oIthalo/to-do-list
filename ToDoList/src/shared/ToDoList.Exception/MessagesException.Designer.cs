﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ToDoList.Exception {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class MessagesException {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal MessagesException() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ToDoList.Exception.MessagesException", typeof(MessagesException).Assembly);
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
        ///   Looks up a localized string similar to The email cannot be empty..
        /// </summary>
        public static string EMAIL_EMPTY {
            get {
                return ResourceManager.GetString("EMAIL_EMPTY", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The email is not valid..
        /// </summary>
        public static string EMAIL_INVALID {
            get {
                return ResourceManager.GetString("EMAIL_INVALID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The password is not valid..
        /// </summary>
        public static string INVALID_PASSWORD {
            get {
                return ResourceManager.GetString("INVALID_PASSWORD", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Login invalid..
        /// </summary>
        public static string LOGIN_INVALID {
            get {
                return ResourceManager.GetString("LOGIN_INVALID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The token cannot be null or empty..
        /// </summary>
        public static string NO_TOKEN {
            get {
                return ResourceManager.GetString("NO_TOKEN", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You cannot set the password equals the old password..
        /// </summary>
        public static string PASSWORD_ALREADY_USED {
            get {
                return ResourceManager.GetString("PASSWORD_ALREADY_USED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The password cannot be empty..
        /// </summary>
        public static string PASSWORD_EMPTY {
            get {
                return ResourceManager.GetString("PASSWORD_EMPTY", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The password must be 8 or more characters long..
        /// </summary>
        public static string SHORT_PASSWORD {
            get {
                return ResourceManager.GetString("SHORT_PASSWORD", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error unknown..
        /// </summary>
        public static string UNKNOWN_ERROR {
            get {
                return ResourceManager.GetString("UNKNOWN_ERROR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The username cannot be empty..
        /// </summary>
        public static string USER_NAME_EMPTY {
            get {
                return ResourceManager.GetString("USER_NAME_EMPTY", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You don&apos;t have permission to access this resouce..
        /// </summary>
        public static string USER_WITHOUT_PERMISSION {
            get {
                return ResourceManager.GetString("USER_WITHOUT_PERMISSION", resourceCulture);
            }
        }
    }
}
