﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SteamShortcut {
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
    internal class Localization {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Localization() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SteamShortcut.Localization", typeof(Localization).Assembly);
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
        ///   Looks up a localized string similar to Add Windows context menu item to executable files?.
        /// </summary>
        internal static string ContextMenu_AddQuestion {
            get {
                return ResourceManager.GetString("ContextMenu_AddQuestion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Add to Steam.
        /// </summary>
        internal static string ContextMenu_Name {
            get {
                return ResourceManager.GetString("ContextMenu_Name", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Remove Windows context menu item?.
        /// </summary>
        internal static string ContextMenu_RemoveQuestion {
            get {
                return ResourceManager.GetString("ContextMenu_RemoveQuestion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Confirm.
        /// </summary>
        internal static string MessageBox_Confirm {
            get {
                return ResourceManager.GetString("MessageBox_Confirm", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to restart Steam process.
        /// </summary>
        internal static string ShortcutController_Invoke_Failed_to_restart_Steam_process {
            get {
                return ResourceManager.GetString("ShortcutController_Invoke_Failed_to_restart_Steam_process", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Steam Shortcut Error.
        /// </summary>
        internal static string SteamShortcut_Add_Steam_Shortcut_Error {
            get {
                return ResourceManager.GetString("SteamShortcut_Add_Steam_Shortcut_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Can&apos;t find steam paths. Is Steam installed?.
        /// </summary>
        internal static string SteamShortcut_PathsNotInitialized {
            get {
                return ResourceManager.GetString("SteamShortcut_PathsNotInitialized", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Restart steam required.
        /// </summary>
        internal static string SteamShortcut_RestartSteamCaption {
            get {
                return ResourceManager.GetString("SteamShortcut_RestartSteamCaption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to In order for the application to appear in the Steam library, you must restart Steam. Restart Steam now?.
        /// </summary>
        internal static string SteamShortcut_RestartSteamQuestion {
            get {
                return ResourceManager.GetString("SteamShortcut_RestartSteamQuestion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select Steam user.
        /// </summary>
        internal static string SteamUserDialog_Show_Select_Steam_user {
            get {
                return ResourceManager.GetString("SteamUserDialog_Show_Select_Steam_user", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User.
        /// </summary>
        internal static string SteamUserDialog_Show_User {
            get {
                return ResourceManager.GetString("SteamUserDialog_Show_User", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to unknown.
        /// </summary>
        internal static string SteamUserDialog_UnknownUser {
            get {
                return ResourceManager.GetString("SteamUserDialog_UnknownUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to add executable.
        /// </summary>
        internal static string VDF_AddExecutableError {
            get {
                return ResourceManager.GetString("VDF_AddExecutableError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to read shortcuts.vdf. Does a shortcuts.vdf file exist? Try to add a non-steam game to steam first.
        /// </summary>
        internal static string VDF_ReadError {
            get {
                return ResourceManager.GetString("VDF_ReadError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to write shortcuts.vdf.
        /// </summary>
        internal static string VDF_WriteError {
            get {
                return ResourceManager.GetString("VDF_WriteError", resourceCulture);
            }
        }
    }
}
