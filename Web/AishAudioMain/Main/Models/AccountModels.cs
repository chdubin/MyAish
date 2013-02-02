using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Main.Common.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Main.Models.Base;

namespace Main.Models
{
    public class ChangePasswordModel : ValidateModel
    {        
        [DataType(DataType.Password)]
        [DisplayName("Current password")]
        public string OldPassword { get; set; }

        public override bool IsValidateEmail { get { return false; } }
    }

    public class LogOnModel
    {
        [Required]
        [DisplayName("User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        [DisplayName("Remember me?")]
        public bool RememberMe { get; set; }

        public string EmailError { get; set; }
    }

    //[PropertiesMustMatch("Password", "ConfirmPassword", ErrorMessage = "The password and confirmation password do not match.")]
    //public class RegisterModel
    //{
    //    [Required]
    //    [DisplayName("User name")]
    //    public string UserName { get; set; }

    //    [Required]
    //    [DataType(DataType.EmailAddress)]
    //    [DisplayName("Email address")]
    //    public string Email { get; set; }

    //    [Required]
    //    [ValidatePasswordLength]
    //    [DataType(DataType.Password)]
    //    [DisplayName("Password")]
    //    public string Password { get; set; }

    //    [Required]
    //    [DataType(DataType.Password)]
    //    [DisplayName("Confirm password")]
    //    public string ConfirmPassword { get; set; }
    //}

}