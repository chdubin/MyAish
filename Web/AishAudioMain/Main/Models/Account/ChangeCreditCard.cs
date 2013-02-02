using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Main.Models.Account
{
    public class ChangeCreditCard
    {
        public Guid UserID { get; set; }

        [DisplayName("Last 4 digits:")]
        public string CurrentMembershipCartID { get; set; }
        [DisplayName("Expiration:")]
        public DateTime? CurrentExpirationDate { get; set; }

        [Required()]
        public string CreditCard { get; set; }
        [Required(ErrorMessage = "The Credit Card # field is required. Please re-enter and submit again.")]

        [DisplayName("Credit Card Number:")]
        public string CreditCardNumber { get; set; }
        [Required(ErrorMessage = "The Credit Card Expiration Month field is required. Please re-enter and submit again.")]
        public int ExpirationDateMonth { get; set; }

        [DisplayName("Expiration Date:")]
        [Required(ErrorMessage = "The Credit Card Expiration Year field is required. Please re-enter and submit again.")]
        public int ExpirationDateYear { get; set; }

        [DisplayName("Email:")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public virtual string Email { get; set; }

        [DisplayName("First name:")]
        [Required(ErrorMessage = "The First Name field is required. Please re-enter and submit again.")]
        public string FirstName { get; set; }

        [DisplayName("Last name:")]
        [Required(ErrorMessage = "The Last Name field is required. Please re-enter and submit again.")]
        public string LastName { get; set; }

        [DisplayName("Postal Address:")]
        [Required(ErrorMessage = "The Postal Address field is required. Please re-enter and submit again.")]
        public string PostalAddress { get; set; }

        [DisplayName("City:")]
        [Required(ErrorMessage = "The City field is required. Please re-enter and submit again.")]
        public string City { get; set; }

        [DisplayName("State or Province:")]
        [Required(ErrorMessage = "The State field is required. Please re-enter and submit again.")]
        public string State { get; set; }

        [DisplayName("Zip or Postal Code:")]
        [Required(ErrorMessage = "The Zip or Postal Code field is required. Please re-enter and submit again.")]
        public string PostalCode { get; set; }

        [DisplayName("Country:")]
        [Required(ErrorMessage = "The Country field is required. Please re-enter and submit again.")]
        public string Country { get; set; }

        [DisplayName("Phone:")]
        //[RegularExpression(@"^(?:(?:\+?1\s*(?:[.-]\s*)?)?(?:(\s*([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9])\s*)|([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\s*(?:[.-]\s*)?)?([2-9]1[02-9]|[2-9][02-9]1|[2-9][02-9]{2})\s*(?:[.-]\s*)?([0-9]{4})$",
        //    ErrorMessage = "The Phone field format is invalid. Please re-enter and submit again.")]
        public string Phone { get; set; }


    }
}