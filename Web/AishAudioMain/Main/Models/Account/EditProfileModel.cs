using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Main.Common.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Main.Models.Base;

namespace Main.Models.Account
{    
    public class EditProfileModel : ValidateModel
    {
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        public override bool IsValidatePassword { get { return false; } }
        public override bool IsValidateConfirmPassword { get { return false; } }
       

        public Guid UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }


        [Required(ErrorMessage = "User Name Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Postal Address Required")]
        public string PostalAddress1 { get; set; }
        public string City1 { get; set; }
        [Required(ErrorMessage = "State Required")]
        public string State1 { get; set; }
        [Required(ErrorMessage = "Country Required")]
        public string Country1 { get; set; }


        [Required(ErrorMessage = "Incorrect Postal Code.")]
        public string PostalCode1 { get; set; }
        [Required(ErrorMessage = "Incorrect Phone.")]
        public string Phone1 { get; set; }
        public string DayPhone1 { get; set; }

        public string CreditCardID { get; private set; }
        public DateTime? CreditCardExpiration { get; private set; }


        public EditProfileModel()
        {

        }

        public EditProfileModel(Guid user_id, string first_name, string last_name, string email, string user_name,
            string credit_card_id, DateTime? credit_card_expiration)
        {
            FirstName = first_name;
            LastName = last_name;
            Email = email;
            UserID = user_id;
            UserName = user_name;

            CreditCardID = credit_card_id;
            CreditCardExpiration = credit_card_expiration;
        }

    }
}