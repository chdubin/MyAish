﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.235
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Main.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("~/upload_temp")]
        public string UploadTemporaryPath {
            get {
                return ((string)(this["UploadTemporaryPath"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public int UnitsRate {
            get {
                return ((int)(this["UnitsRate"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("26")]
        public int MaxUnitsOnSubscribe {
            get {
                return ((int)(this["MaxUnitsOnSubscribe"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("30")]
        public int SubscriberDiscount {
            get {
                return ((int)(this["SubscriberDiscount"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string S3AmazonKeyPrefix {
            get {
                return ((string)(this["S3AmazonKeyPrefix"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("aaprod")]
        public string S3AmazonBucketName {
            get {
                return ((string)(this["S3AmazonBucketName"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("000284794688")]
        public string SJSerialNumber {
            get {
                return ((string)(this["SJSerialNumber"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("999988887777")]
        public string SJDeveloperSerialNumber {
            get {
                return ((string)(this["SJDeveloperSerialNumber"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://developer.skipjackic.com/scripts/evolvcc.dll?AuthorizeAPI")]
        public string SJAuthorizeServiceUrl {
            get {
                return ((string)(this["SJAuthorizeServiceUrl"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://developer.skipjackic.com/scripts/evolvcc.dll?SJAPI_TransactionChangeStatu" +
            "sRequest")]
        public string SJChangeStatusUrl {
            get {
                return ((string)(this["SJChangeStatusUrl"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://developer.skipjackic.com/scripts/evolvcc.dll?SJAPI_TransactionStatusReque" +
            "st")]
        public string SJGetStatusUrl {
            get {
                return ((string)(this["SJGetStatusUrl"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("bopohaa@gmail.com")]
        public string AdminEmailSubscribeNotification {
            get {
                return ((string)(this["AdminEmailSubscribeNotification"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("AKIAI7W7A74XORGA2NBA")]
        public string S3AmazonAccessKey {
            get {
                return ((string)(this["S3AmazonAccessKey"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("eH8SQhfXebeMoEuMnq7xGugZIQywVtCYOv728YN6")]
        public string S3AmazonSecretKey {
            get {
                return ((string)(this["S3AmazonSecretKey"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("office@aishaudio.com")]
        public string FromEmailNotification {
            get {
                return ((string)(this["FromEmailNotification"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("PasswordProtection")]
        public string PasswordProtectionAppName {
            get {
                return ((string)(this["PasswordProtectionAppName"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Branch_password_protection_access_{0}")]
        public string PasswordProtectionUserName {
            get {
                return ((string)(this["PasswordProtectionUserName"]));
            }
        }
    }
}
