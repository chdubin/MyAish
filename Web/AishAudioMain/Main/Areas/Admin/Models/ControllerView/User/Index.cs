using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Main.Areas.Admin.Models.User;

namespace Main.Areas.Admin.Models.ControllerView.User
{
    public class Index
    {
        public UserItem[] UserItems;

        public Index(UserItem[] user_items)
        {
            this.UserItems = user_items;
        }
    }
}