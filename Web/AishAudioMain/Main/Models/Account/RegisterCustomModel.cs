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
    public class RegisterCustomModel : ValidateModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public override bool IsValidatePassword { get { return false; } }
        public override bool IsValidateConfirmPassword { get { return false; } }


        public RegisterCustomModel()
        {

        }

        public RegisterCustomModel(string first_name, string last_name, string email)
        {
            this.FirstName = first_name;
            this.LastName = last_name;
            this.Email = email;
        }
    }
}