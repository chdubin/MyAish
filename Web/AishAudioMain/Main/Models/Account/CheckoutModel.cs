using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Main.Models.Base;
using MainEntity.Models.Shopping;

namespace Main.Models.Account
{
    public class CheckoutModel : ValidateModel
    {
        //[Required(ErrorMessage = "The First Name field is required. Please re-enter and submit again.")]
        public string FirstName1 { get; set; }
        //[Required(ErrorMessage = "The Last Name field is required. Please re-enter and submit again.")]
        public string LastName1 { get; set; }

        public string PostalAddress1 { get; set; }
        public string City1 { get; set; }
        public string PostalCode1 { get; set; }
        public string Country1 { get; set; }
        //[RegularExpression(@"^(?:(?:\+?1\s*(?:[.-]\s*)?)?(?:(\s*([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9])\s*)|([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\s*(?:[.-]\s*)?)?([2-9]1[02-9]|[2-9][02-9]1|[2-9][02-9]{2})\s*(?:[.-]\s*)?([0-9]{4})$", ErrorMessage = "The Phone field format is invalid. Please re-enter and submit again.")]
        public string Phone1 { get; set; }
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

        public bool Authorized { get; set; }
        public decimal Balance { get; set; }

        public MainEntity.Models.Shopping.ShoppingTransactionInfo TransactionInfo { get; set; }

        /*
        public Dictionary<MainEntity.Models.Class.ProductEntity, int> ProductsList1 { get; set; }
        public Dictionary<MainEntity.Models.Class.ProductEntity, int> ProductsList2 { get; set; }
        public MainEntity.Models.User.SubscribePlanEntity CartSubscribe { get; set; }
        public MainEntity.Models.User.SubscribePlanEntity BaseSubscribe { get; set; }
        public int UnitsPurchasesCount { get; set; }
        public bool IsEmptyShoppingCart { get; set; }

        public long UnitsID { get; set; }
        public int Units { get; set; }

        public int UnitsBeforePurchase { get; set; }
         * */

        public MainEntity.Models.User.Membership Membership
        {
            get
            {
                return null;
            }
            set
            {
                if (value != null)
                {
                    FirstName1 = value.firstName;
                    LastName1 = value.lastName;
                    PostalAddress1 = value.postalAdderss;
                    City1 = value.city;
                    PostalCode1 = value.postalCode;
                    Country1 = value.country;
                    Phone1 = value.phone;
                    DayPhone1 = value.dayPhone;
                    State1 = value.state;
                    Description1 = value.Description;

                    Authorized = true;
                }
                else
                    Authorized = false;

            }
        }

        public CheckoutModel()
        {

        }

        public CheckoutModel(EnterCCInfoModel model)
        {
            FirstName1 = model.FirstName1;
            LastName1 = model.LastName1;
            FirstName2 = model.FirstName2;
            LastName2 = model.LastName2;

            Email = model.Email;
            Password = model.Password;

            PostalAddress1 = model.PostalAddress1;
            City1 = model.City1;
            State1 = model.State1;
            Description1 = model.Description1;
            PostalCode1 = model.PostalCode1;
            Country1 = model.Country1;
            Phone1 = model.Phone1;
            DayPhone1 = model.DayPhone1;

            PostalAddress2 = model.PostalAddress2;
            City2 = model.City2;
            State2 = model.State2;
            Description2 = model.Description2;
            PostalCode2 = model.PostalCode2;
            Country2 = model.Country2;
            Phone2 = model.Phone2;
            DayPhone2 = model.DayPhone2;

            Authorized = model.Authorized;
        }

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
        public override bool IsValidateConfirmPassword
        {
            get
            {
                return !Authorized;
            }
        }
        /*
        public CheckoutModel(Dictionary<MainEntity.Models.Class.ProductEntity, int> products_list, MainEntity.Models.User.SubscribePlanEntity cart_subscribe,
            MainEntity.Models.User.SubscribePlanEntity base_subscribe, long units_id, int units, int balance)
        {
            this.CartSubscribe = cart_subscribe;
            this.BaseSubscribe = base_subscribe;
            this.UnitsBeforePurchase = balance;
            //int productUnitsSum = 0;
            //int freeUnitsOnSubscribe = 0;


            if (this.CartSubscribe != null || this.BaseSubscribe != null)
            {
                this.ProductsList1 = products_list.Where(p => p.Key.price2 == null || p.Key.price2 == 0).ToDictionary(item => item.Key, item => item.Value);
                this.ProductsList2 = products_list.Where(p => p.Key.price2 != null && p.Key.price2 != 0).ToDictionary(item => item.Key, item => item.Value);

                //productUnitsSum = Convert.ToInt32(products_list.Where(p => p.Key.price2 != null && p.Key.price2 != 0).Select(w => w.Key.price2).Sum() ?? 0);

                //if(this.CartSubscribe != null)
                //    freeUnitsOnSubscribe = this.CartSubscribe.freeUnitsOnSubscribe;

                this.UnitsPurchasesCount = Convert.ToInt32(this.ProductsList2.Select(p => p.Key.price2 * p.Value).Sum() ?? 0);
            }
            else
            {
                this.ProductsList1 = products_list;                
                this.ProductsList2 = new Dictionary<MainEntity.Models.Class.ProductEntity, int>();

                this.UnitsPurchasesCount = 0;
            }

            //this.Units = Math.Max(units, productUnitsSum - (balance + freeUnitsOnSubscribe));

            this.UnitsID = units_id;
            this.Units = units;


            this.IsEmptyShoppingCart = (ProductsList1.Count == 0 && ProductsList2.Count == 0 && this.CartSubscribe == null && units_id == 0);
        }
        */

    }
}