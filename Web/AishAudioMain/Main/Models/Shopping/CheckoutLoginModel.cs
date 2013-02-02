using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Main.Models.Shopping
{
    public class CheckoutLoginModel
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public string UserName { get; set; }


        public CheckoutLoginModel()
        {

        }
    }
}