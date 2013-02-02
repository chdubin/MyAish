using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace Main.Models.Base
{
    public class ValidateModel : IDataErrorInfo
    {
        #region Public properties

        [DataType(DataType.Password)]
        public virtual string Password { get; set; }
        
        [DataType(DataType.Password)]
        public virtual string ConfirmPassword { get; set; }        
        [DataType(DataType.EmailAddress)]
        public virtual string Email { get; set; }


        public virtual bool IsValidatePassword { get { return true;} }
        public virtual bool IsValidateConfirmPassword { get { return true; } }
        public virtual bool IsValidateEmail { get { return true; } }
		public virtual bool IsRequiredEmail { get { return true; } }

        #endregion

        public string Error { get; set; }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Email")
                {
                    if (IsValidateEmail)
                    {
						if (String.IsNullOrEmpty(Email) || Email.Trim().Length == 0)
                            return IsRequiredEmail ? "The Email field is required. Please re-enter and submit again." : null;

                        string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                              @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                              @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                        Regex re = new Regex(strRegex);
                        if (!re.IsMatch(Email))
                            return "The Email field format is not correct. Please re-enter and submit again.";
                    }
                }
                else if (columnName == "Password")
                {
                    if (IsValidatePassword)
                    {
                        if (String.IsNullOrEmpty(Password) || Password.Trim().Length == 0)
                            return "The Password field is required. Please re-enter and submit again.";
                        if (Password.Length < System.Web.Security.Membership.MinRequiredPasswordLength)
                            return "Password must be at least 6 characters long";
                    }
                }
                else if (columnName == "ConfirmPassword")
                {
                    if (IsValidateConfirmPassword)
                    {
                        if (String.IsNullOrEmpty(ConfirmPassword) || ConfirmPassword.Trim().Length == 0)
                            return "The Confirm Password field is required. Please re-enter and submit again.";
                        if (!ConfirmPassword.Equals(Password))
                            return "Confirm password does not match password";
                    }
                }

                return null;
            }
        }

    }
}