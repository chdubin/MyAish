using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.Security;
using System.ComponentModel;
using MainCommon;

namespace Main.Areas.Admin.Models.Portal
{
    public class EditModel
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(512)]
        public string Name { get; set; }

        public long PortalID { get; set; }

        [DataType(DataType.Text)]
        public string ApplicationName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string ThemeName { get; set; }

        public DateTime CreateDate { get; set; }

        public bool Active { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Aliases { get; set; }

        public bool AuthorizedOnly { get; set; }

        public bool AllowBuyFiles { get; set; }

        public bool AllowBuyProducts { get; set; }

        public bool AllowRegister { get; set; }

        public bool AllowAuthorize { get; set; }

        public string PasswordProtection { get; set; }

        public EditModel()
        {}

        public EditModel(MainEntity.Models.Portal.PortalEntity portal)
        {
            PortalID = portal.portalID;
            ApplicationName = portal.applicationName;
            ThemeName = portal.themeName;

            Name = portal.EntityItem.title;
            CreateDate = portal.EntityItem.createDate;
            Active = portal.EntityItem.active;

            AuthorizedOnly = portal.authorizedOnly;
            AllowBuyFiles = portal.allowBuyFiles;
            AllowBuyProducts = portal.allowBuyProducts;
            AllowRegister = portal.allowRegister;
            AllowAuthorize = portal.allowRegister;
            PasswordProtection = portal.passwordProtection;

            Aliases = string.Join(", ", portal.PortalAlias.Select(a => a.alias).ToArray());
        }
    }
}