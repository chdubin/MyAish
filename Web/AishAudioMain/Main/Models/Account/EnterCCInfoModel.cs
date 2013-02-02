using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Main.Models.Base;
using System.ComponentModel;

namespace Main.Models.Account
{
    [ShippingAddressCustomValidation(ErrorMessage = "Please check the Shipping Address")]
    [StateCustomValidation(ErrorMessage = "The State field is required. Please re-enter and submit again.")]
    public class EnterCCInfoModel : ValidateModel
    {
        [Required]
        public string CreditCard { get; set; }
        [Required(ErrorMessage = "The Credit Card # field is required. Please re-enter and submit again.")]
        public string CreditCardNumber { get; set; }
        [Required(ErrorMessage = "The Credit Card Expiration Year field is required. Please re-enter and submit again.")]
        public int ExpirationDateYear { get; set; }
        [Required(ErrorMessage = "The Credit Card Expiration Month field is required. Please re-enter and submit again.")]
        public int ExpirationDateMonth { get; set; }

        public bool CreditCardValidated { get; set; }

        public bool Authorized { get; set; }

        public new string this[string column_name]
        {
            get
            {
                string rval = null;

                if (column_name == "FirstName1" && !Authorized)
                    rval = String.IsNullOrEmpty(FirstName1) || FirstName1.Trim().Length == 0 ? "The First Name field is required. Please re-enter and submit again." : null;
                else if (column_name == "LastName1" && !Authorized)
                    rval = String.IsNullOrEmpty(LastName1) || LastName1.Trim().Length == 0 ? "The Last Name field is required. Please re-enter and submit again." : null;
                else
                    rval = base[column_name];


                return rval;
            }
        }

        public override bool IsRequiredEmail
        {
            get
            {
                return !Authorized;
            }
        }
        public override bool IsValidateEmail
        {
            get
            {
                return !Authorized;
            }
        }
        public override bool IsValidatePassword
        {
            get
            {
                return !Authorized;
            }
        }
        public override bool IsValidateConfirmPassword { get { return false; } }

        [Required(ErrorMessage = "The First Name field is required. Please re-enter and submit again.")]
        public string FirstName1 { get; set; }
        [Required(ErrorMessage = "The Last Name field is required. Please re-enter and submit again.")]
        public string LastName1 { get; set; }

        [Required(ErrorMessage = "The Postal Address field is required. Please re-enter and submit again.")]
        public string PostalAddress1 { get; set; }
        [Required(ErrorMessage = "The Zip or Postal Code field is required. Please re-enter and submit again.")]
        public string PostalCode1 { get; set; }
        [Required(ErrorMessage = "The Country field is required. Please re-enter and submit again.")]
        public string Country1 { get; set; }
        [Required(ErrorMessage = "The Phone field is required. Please re-enter and submit again.")]
        //[RegularExpression(@"^(?:(?:\+?1\s*(?:[.-]\s*)?)?(?:(\s*([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9])\s*)|([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\s*(?:[.-]\s*)?)?([2-9]1[02-9]|[2-9][02-9]1|[2-9][02-9]{2})\s*(?:[.-]\s*)?([0-9]{4})$", ErrorMessage = "The Phone field format is invalid. Please re-enter and submit again.")]
        public string Phone1 { get; set; }
        [Required(ErrorMessage = "The City field is required. Please re-enter and submit again.")]
        public string City1 { get; set; }
        public string DayPhone1 { get; set; }
        public string State1 { get; set; }
        public string Description1 { get; set; }

        public string FirstName2 { get; set; }
        public string LastName2 { get; set; }
        public string PostalAddress2 { get; set; }
        public string City2 { get; set; }
        public string State2 { get; set; }
        public string Description2 { get; set; }
        public string PostalCode2 { get; set; }
        public string Country2 { get; set; }
        public string Phone2 { get; set; }
        public string DayPhone2 { get; set; }

        public bool UseStandaloneShippingAddress { get; set; }


        //public Dictionary<MainEntity.Models.Class.ProductEntity, int> ProductsList1 { get; set; }
        //public Dictionary<MainEntity.Models.Class.ProductEntity, int> ProductsList2 { get; set; }
        //public MainEntity.Models.User.SubscribePlanEntity CartSubscribe { get; set; }
        //public MainEntity.Models.User.SubscribePlanEntity BaseSubscribe { get; set; }
        //public int UnitsPurchasesCount { get; set; }        

        //public int Units { get; set; }
        //public int UnitsBeforePurchase { get; set; }
        //public bool IsEmptyShoppingCart { get; set; }

        public EnterCCInfoModel()
        {

        }

        public EnterCCInfoModel(CheckoutModel checkout_model)
        {
            this.FirstName1 = checkout_model.FirstName1;
            this.LastName1 = checkout_model.LastName1;
            this.FirstName2 = checkout_model.FirstName2;
            this.LastName2 = checkout_model.LastName2;

            this.Email = checkout_model.Email;
            this.Password = checkout_model.Password;

            this.PostalAddress1 = checkout_model.PostalAddress1;
            this.City1 = checkout_model.City1;
            this.State1 = checkout_model.State1;
            this.Description1 = checkout_model.Description1;
            this.PostalCode1 = checkout_model.PostalCode1;
            this.Country1 = checkout_model.Country1;
            this.Phone1 = checkout_model.Phone1;
            this.DayPhone1 = checkout_model.DayPhone1;

            this.PostalAddress2 = checkout_model.PostalAddress2;
            this.City2 = checkout_model.City2;
            this.State2 = checkout_model.State2;
            this.Description2 = checkout_model.Description2;
            this.PostalCode2 = checkout_model.PostalCode2;
            this.Country2 = checkout_model.Country2;
            this.Phone2 = checkout_model.Phone2;
            this.DayPhone2 = checkout_model.DayPhone2;

            this.Authorized = checkout_model.Authorized;

            //this.ProductsList1 = checkout_model.ProductsList1;
            //this.ProductsList2 = checkout_model.ProductsList2;
            //this.CartSubscribe = checkout_model.CartSubscribe;
            //this.BaseSubscribe = checkout_model.BaseSubscribe;
            //this.UnitsPurchasesCount = checkout_model.UnitsPurchasesCount;
            //this.IsEmptyShoppingCart = checkout_model.IsEmptyShoppingCart;
            //this.Units = checkout_model.Units;
            //this.UnitsBeforePurchase = checkout_model.UnitsBeforePurchase;

        }

        [AttributeUsage(AttributeTargets.Class)]
        private class StateCustomValidation : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                EnterCCInfoModel model = (EnterCCInfoModel)value;
                if ((model.Country1 == "US" || model.Country1 == "CA") && model.State1 == null)
                    return false;
                else
                    return true;
            }
        }

        [AttributeUsage(AttributeTargets.Class)]
        private class ShippingAddressCustomValidation : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                EnterCCInfoModel model = (EnterCCInfoModel)value;
                if (model.UseStandaloneShippingAddress)
                {
                    //state
                    if ((model.Country2 == "US" || model.Country2 == "CA") && model.State2 == null)
                        return false;
                    if (model.City2 == null || model.City2 == "")
                        return false;
                    if (model.Country2 == null || model.Country2 == "")
                        return false;
                    if (model.FirstName2 == null || model.FirstName2 == "")
                        return false;
                    if (model.LastName2 == null || model.LastName2 == "")
                        return false;
                    if (model.Phone2 == null || model.Phone2 == "")
                        return false;
                    if (model.PostalAddress2 == null || model.PostalAddress2 == "")
                        return false;
                    if (model.PostalCode2 == null || model.PostalCode2 == "")
                        return false;
                }

                return true;
            }
        }
    }
}
