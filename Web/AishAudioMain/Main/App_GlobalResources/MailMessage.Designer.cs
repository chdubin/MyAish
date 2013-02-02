//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.261
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option or rebuild the Visual Studio project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Web.Application.StronglyTypedResourceProxyBuilder", "10.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class MailMessage {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal MailMessage() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Resources.MailMessage", global::System.Reflection.Assembly.Load("App_GlobalResources"));
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
        ///   Looks up a localized string similar to {skipjack_log}.
        /// </summary>
        internal static string ChargeOnSkipjackIsDeclined {
            get {
                return ResourceManager.GetString("ChargeOnSkipjackIsDeclined", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Skipjack Transaction Log – Declined.
        /// </summary>
        internal static string ChargeOnSkipjackIsDeclined_subject {
            get {
                return ResourceManager.GetString("ChargeOnSkipjackIsDeclined_subject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {skipjack_log}.
        /// </summary>
        internal static string ChargeOnSkipjackIsSuccess {
            get {
                return ResourceManager.GetString("ChargeOnSkipjackIsSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Skipjack Transaction Log – Success: ${amount}.
        /// </summary>
        internal static string ChargeOnSkipjackIsSuccess_subject {
            get {
                return ResourceManager.GetString("ChargeOnSkipjackIsSuccess_subject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Dear {0},
        ///
        ///Your profile has been recently changed. Please contact us immediately if you did not request or make this change.                                                                                                               {1}
        ///
        ///Thank you..
        /// </summary>
        internal static string EditProfileMessageBody {
            get {
                return ResourceManager.GetString("EditProfileMessageBody", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Aish edit profile.
        /// </summary>
        internal static string EditProfileMessageSubject {
            get {
                return ResourceManager.GetString("EditProfileMessageSubject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Dear {0} {1},
        ///
        ///Here is your Aishaudio.com login information.
        ///
        ///Username: {2}
        ///Password: {3}.
        /// </summary>
        internal static string ForgotPasswordBody {
            get {
                return ResourceManager.GetString("ForgotPasswordBody", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to AishAudio Lost Passwords.
        /// </summary>
        internal static string ForgotPasswordSubject {
            get {
                return ResourceManager.GetString("ForgotPasswordSubject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to cdubin@aish.com.
        /// </summary>
        internal static string FromMailAddress {
            get {
                return ResourceManager.GetString("FromMailAddress", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Transaction Log:
        ///
        ///UserID: {user_id}
        ///TransID: {transaction_id}
        ///sjOrderID: {transaction_id}
        ///Amount: ${amount}
        ///
        ///
        ///{skipjack_log}
        ///.
        /// </summary>
        internal static string NoResponseFromTheSkipjack {
            get {
                return ResourceManager.GetString("NoResponseFromTheSkipjack", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Skipjack no response from Server.
        /// </summary>
        internal static string NoResponseFromTheSkipjack_subject {
            get {
                return ResourceManager.GetString("NoResponseFromTheSkipjack_subject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Membership Cancelled:
        ///Name: {full_name}
        ///Username: {user_name}
        ///Email: {email}.
        /// </summary>
        internal static string OnMonthlyMembershipCancellation {
            get {
                return ResourceManager.GetString("OnMonthlyMembershipCancellation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to AishAudio Cancellation.
        /// </summary>
        internal static string OnMonthlyMembershipCancellation_subject {
            get {
                return ResourceManager.GetString("OnMonthlyMembershipCancellation_subject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Dear {email}
        ///
        ///After clicking on the following link to verify your email address, you will
        ///be taken to your two free downloads.
        ///
        ///{link}
        ///
        ///If the link above does not work for any reason, you may log in at
        ///Aishaudio.com using the following login information:
        ///
        ///Username: {email}
        ///Password: {password}
        ///
        ///Once you login and select a permanent password, you will be able to download
        ///your free classes. Please note: passwords are case sensitive..
        /// </summary>
        internal static string OnRegister2FreeDownloadsUser {
            get {
                return ResourceManager.GetString("OnRegister2FreeDownloadsUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Aish registration.
        /// </summary>
        internal static string OnRegister2FreeDownloadsUser_subject {
            get {
                return ResourceManager.GetString("OnRegister2FreeDownloadsUser_subject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Dear {full_name}:
        ///
        ///Thank you for joining aishaudio.com, your 24/6 source for Jewish digital
        ///audio classes. Your email address, {email} will serve as your
        ///username whenever you log in.
        ///
        ///Your password for your account {email} has been set to “{password}”.
        ///
        ///Upon clicking on the the following link which will complete your
        ///Aishaudio.com registration, you will be given an opportunity to choose
        ///a permanent password. If the link below does not work for any reason, you
        ///may complete the registration proces [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string OnRegisterFreeUser {
            get {
                return ResourceManager.GetString("OnRegisterFreeUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Dear {full_name}:
        ///
        ///Thank you for joining aishaudio.com, your 24/6 source for Jewish digital
        ///audio classes. Your email address, {email} will serve as your
        ///username whenever you log in.
        ///
        ///Your password for your account {email} has been set to “{password}”.
        ///
        ///Every week, we’ll send you an e-update of new or free on-line listening
        ///classes.
        ///
        ///The easiest way to unsubscribe is to hit reply to this e-mail and write
        ///unsubscribe in the subject line.
        ///
        ///Thank you for joining. We hope that aishaudio.com wisdo [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string OnRegisterFreeUserWithoutEmailValidate {
            get {
                return ResourceManager.GetString("OnRegisterFreeUserWithoutEmailValidate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Aish registration.
        /// </summary>
        internal static string OnRegisterFreeUserWithoutEmailValidate_subject {
            get {
                return ResourceManager.GetString("OnRegisterFreeUserWithoutEmailValidate_subject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Aish registration.
        /// </summary>
        internal static string OnRegisterFreeUser_subject {
            get {
                return ResourceManager.GetString("OnRegisterFreeUser_subject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Dear {full_name}:
        ///
        ///Thank you for joining aishaudio.com, your 24/6 source for Jewish digital audio classes. When you wish to access your account, please use the following login information:
        ///
        ///Username: {email}
        ///Password: {password}
        ///
        ///Every week, we&apos;ll send you an e-update to let you know about new releases, timely classes, and interesting promotions. The update lets you link directly to the class of your choice for free listening or downloading.
        ///
        ///The easiest way to unsubscribe is to hit reply to this e [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string OnRegisterMonthlyUser {
            get {
                return ResourceManager.GetString("OnRegisterMonthlyUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Aish registration.
        /// </summary>
        internal static string OnRegisterMonthlyUser_subject {
            get {
                return ResourceManager.GetString("OnRegisterMonthlyUser_subject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {full_name}
        ///
        ///Your credit card has been changed.
        ///
        ///If you have any questions please reply to this email, or call 1 800 VOICES 3 to speak with one of our representatives..
        /// </summary>
        internal static string OnSomeoneChangeCreditCard {
            get {
                return ResourceManager.GetString("OnSomeoneChangeCreditCard", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Aishaudio Change Credit Card.
        /// </summary>
        internal static string OnSomeoneChangeCreditCard_subject {
            get {
                return ResourceManager.GetString("OnSomeoneChangeCreditCard_subject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An online AishAudio.com order was submitted.
        ///
        ///User ID: {user_id}
        ///Name: {full_name}
        ///Email: {email}
        ///-----------------------
        ///Product #: NA10093
        ///Title: One month membership
        ///Type: otherItem
        ///Qty: 1
        ///Price: ${price}
        ///Item Total: ${price}
        ///-----------------------
        ///
        ///Cart Subtotal: ${price}
        ///Shipping Cost: $0.00
        ///Taxes: $0.00
        ///Final Total: ${price}.
        /// </summary>
        internal static string OnSomeoneJoiningAsNewMonthlyMember {
            get {
                return ResourceManager.GetString("OnSomeoneJoiningAsNewMonthlyMember", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to New Monthly Aishaudio Account.
        /// </summary>
        internal static string OnSomeoneJoiningAsNewMonthlyMember_subject {
            get {
                return ResourceManager.GetString("OnSomeoneJoiningAsNewMonthlyMember_subject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An online AishAudio.com order was submitted.
        ///
        ///User ID: {user_id}
        ///Name: {full_name}
        ///Email: {email}
        ///-----------------------
        ///Product #: NA10093
        ///Title: One month membership
        ///Type: otherItem
        ///Qty: 1
        ///Price: ${price}
        ///Item Total: ${price}
        ///-----------------------
        ///
        ///Cart Subtotal: ${price}
        ///Shipping Cost: $0.00
        ///Taxes: $0.00
        ///Final Total: ${price}.
        /// </summary>
        internal static string OnSomeoneJoiningAsUpgradeMonthlyMember {
            get {
                return ResourceManager.GetString("OnSomeoneJoiningAsUpgradeMonthlyMember", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to New Monthly Aishaudio Account Upgrade.
        /// </summary>
        internal static string OnSomeoneJoiningAsUpgradeMonthlyMember_subject {
            get {
                return ResourceManager.GetString("OnSomeoneJoiningAsUpgradeMonthlyMember_subject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {full_name}
        ///
        ///Your credit card has been charged ${charged} for the month beginning {date}.
        ///
        ///If you have any questions please reply to this email, or call 1 800 VOICES 3 to speak with one of our representatives..
        /// </summary>
        internal static string OnSomeoneUpdatingTheirCreditCard {
            get {
                return ResourceManager.GetString("OnSomeoneUpdatingTheirCreditCard", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to AishAudio Notification.
        /// </summary>
        internal static string OnSomeoneUpdatingTheirCreditCard_subject {
            get {
                return ResourceManager.GetString("OnSomeoneUpdatingTheirCreditCard_subject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Dear {full_name},
        ///
        ///This is confirmation of your recent AishAudio.com purchase.
        ///
        ///Your order number is {order_id}.
        ///
        ///ORDER ITEMS:
        ///
        ///{products_info}
        ///-----------------------
        ///
        ///Cart Subtotal: ${sub_total}
        ///Shipping Cost: ${shipping}
        ///Taxes: ${taxes}
        ///Final Total: ${final_total}
        ///
        ///Your order will be shipped to:
        ///
        ///Shipping Address:
        ///{shipping_address}
        ///
        ///You can review the status of your order at any time
        ///by visiting the My Account section of Aishaudio.com.
        ///Please allow up to two weeks for items coming [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string OnUserBuyForMany {
            get {
                return ResourceManager.GetString("OnUserBuyForMany", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Aishaudio Prepaid Purchase.
        /// </summary>
        internal static string OnUserBuyForMany_subject {
            get {
                return ResourceManager.GetString("OnUserBuyForMany_subject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Shipping Address:
        ///{shipping_address}
        ///
        ///-----------------------
        ///
        ///User ID: {user_id}
        ///Name: {full_name}
        ///Email: {email}
        ///
        ///-----------------------
        ///
        ///{shopping_products}
        ///
        ///-----------------------
        ///
        ///{total}.
        /// </summary>
        internal static string PrepaidSaleGoesThroughForExistAccount {
            get {
                return ResourceManager.GetString("PrepaidSaleGoesThroughForExistAccount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Aishaudio Prepaid Purchase (Upgrade).
        /// </summary>
        internal static string PrepaidSaleGoesThroughForExistAccount_subject {
            get {
                return ResourceManager.GetString("PrepaidSaleGoesThroughForExistAccount_subject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Shipping Address:
        ///{shipping_address}
        ///
        ///-----------------------
        ///
        ///User ID: {user_id}
        ///Name: {full_name}
        ///Email: {email}
        ///
        ///-----------------------
        ///
        ///{shopping_products}
        ///
        ///-----------------------
        ///
        ///{total}.
        /// </summary>
        internal static string PrepaidSaleGoesThroughForNewUser {
            get {
                return ResourceManager.GetString("PrepaidSaleGoesThroughForNewUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Aishaudio Prepaid Purchase (New User).
        /// </summary>
        internal static string PrepaidSaleGoesThroughForNewUser_subject {
            get {
                return ResourceManager.GetString("PrepaidSaleGoesThroughForNewUser_subject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {transaction_log}.
        /// </summary>
        internal static string RegularSalesIsMade {
            get {
                return ResourceManager.GetString("RegularSalesIsMade", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Aishaudio.com Purchase.
        /// </summary>
        internal static string RegularSalesIsMade_subject {
            get {
                return ResourceManager.GetString("RegularSalesIsMade_subject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {skipjack_log}.
        /// </summary>
        internal static string SkipjackFailsBecauseNoResponseInGivenTime {
            get {
                return ResourceManager.GetString("SkipjackFailsBecauseNoResponseInGivenTime", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Skipjack Transaction Log - Failed.
        /// </summary>
        internal static string SkipjackFailsBecauseNoResponseInGivenTime_subject {
            get {
                return ResourceManager.GetString("SkipjackFailsBecauseNoResponseInGivenTime_subject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to cdubin@aishaudio.com.
        /// </summary>
        internal static string String1 {
            get {
                return ResourceManager.GetString("String1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to website-info@aishaudio.com.
        /// </summary>
        internal static string String2 {
            get {
                return ResourceManager.GetString("String2", resourceCulture);
            }
        }
    }
}