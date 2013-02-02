using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace MainEntity.Interfaces
{
    public interface IMembershipService
    {
        bool ValidateUser(string userName, string password, MembershipProvider provider);
        //MembershipCreateStatus CreateUser(string userName, string password, string email, long current_portal_id, MembershipProvider provider);
        bool ChangePassword(string userName, string oldPassword, string password, MembershipProvider provider);
    }
}
