using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MainEntity.Interfaces;
using System.Web.Security;
using MyPartnerKinoBL.Extension;
using MainEntity;

namespace MainBL
{
    public class AccountMembershipService : BaseBO, IMembershipService
    {
        public AccountMembershipService(string connection_name)
            : base(connection_name)
        {
        }

        public bool ValidateUser(string userName, string password, MembershipProvider provider)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be null or empty.", "password");

            return provider.ValidateUser(userName, password);
        }

        public bool ChangePassword(string userName, string oldPassword, string password, MembershipProvider provider)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(oldPassword)) throw new ArgumentException("Value cannot be null or empty.", "oldPassword");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be null or empty.", "newPassword");

            // The underlying ChangePassword() will throw an exception rather
            // than return false in certain failure scenarios.
            try
            {
                MembershipUser currentUser = provider.GetUser(userName, true);
                return currentUser.ChangePassword(oldPassword, password);
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (MembershipPasswordException)
            {
                return false;
            }
        }
    }
}
