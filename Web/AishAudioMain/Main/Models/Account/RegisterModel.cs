﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Main.Common.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Main.Models.Base;

namespace Main.Models.Account
{
    public class RegisterModel : ValidateModel
    {
        public override bool IsValidateConfirmPassword { get { return false; } }
        public override bool IsValidatePassword { get { return false; } }

        public RegisterModel()
        {

        }

        public RegisterModel(string email)
        {
            this.Email = email;
        }
    }
}