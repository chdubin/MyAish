using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MainEntity.Models.Portal;
using MainCommon;
using BLToolkit.Data.Linq;

namespace MainEntity
{
    public partial class PortalDbManager
    {
        #region Select

        public IQueryable<PortalEntity> GetPortals(bool with_only_active, bool with_only_nondeleted)
        {
            var rval = from p in this.PortalEntities
                       where p.EntityItem.typeID == (int)EntityItemTypeEnum.PortalRoot
                       select new PortalEntity()
                       {
                           applicationName = p.applicationName,
                           portalID = p.portalID,
                           themeName = p.themeName,

                           allowAuthorize = p.allowAuthorize,
                           allowBuyFiles = p.allowBuyFiles,
                           allowBuyProducts = p.allowBuyProducts,
                           allowRegister =p.allowRegister,
                           authorizedOnly = p.authorizedOnly,
                           passwordProtection = p.passwordProtection,

                           EntityItem = p.EntityItem
                       };
            if (with_only_active)
                rval = rval.Where(p => p.EntityItem.active);
            if (with_only_nondeleted)
                rval = rval.Where(p => !p.EntityItem.deleted);

            return rval;
        }

        public IQueryable<PortalAlias> GetAliases(long[] from_portal_ids)
        {
            var rval = from a in this.PortalAliases
                       where from_portal_ids.Contains(a.portalID)
                       select a;
            return rval;
        }
        
        public IQueryable<PortalAlias> GetAliases()
        {
            var rval = from a in this.PortalAliases
                       where a.EntityItem.typeID == (int)EntityItemTypeEnum.PortalRoot
                             && a.EntityItem.active && !a.EntityItem.deleted
                       select new PortalAlias()
                       {
                           alias = a.alias,
                           portalAliasID = a.portalAliasID,
                           portalID = a.portalID,

                           PortalEntity = new PortalEntity()
                           {
                               allowAuthorize = a.PortalEntity.allowAuthorize,
                               allowBuyFiles = a.PortalEntity.allowBuyFiles,
                               allowBuyProducts = a.PortalEntity.allowBuyProducts,
                               allowRegister = a.PortalEntity.allowRegister,
                               applicationName = a.PortalEntity.applicationName,
                               authorizedOnly = a.PortalEntity.authorizedOnly,
                               passwordProtection = a.PortalEntity.passwordProtection,
                               portalID = a.PortalEntity.portalID,
                               themeName = a.PortalEntity.themeName, 
                               Title = a.PortalEntity.EntityItem.title
                           }
                       };
            return rval;
        }

        #endregion


        #region Update

        public int UpdatePortal(long portal_id, string application_name, string theme,
            bool authorized_only, bool allow_authorize, bool allow_register, bool allow_buy_files, bool allow_buy_products, string password_protection)
        {
            return this.PortalEntities.Where(p => p.portalID == portal_id).
                Update(p => new PortalEntity()
                {
                    applicationName=application_name,
                    themeName = theme,
                    authorizedOnly = authorized_only,
                    allowAuthorize = allow_authorize,
                    allowRegister = allow_register,
                    allowBuyFiles = allow_buy_files,
                    allowBuyProducts = allow_buy_products,
                    passwordProtection = password_protection
                });
        }

        #endregion


        #region Insert

        public int InsertPortalAlias(long portal_id, string alias)
        {
            return this.PortalAliases.Insert(() => new PortalAlias()
            {
                portalID = portal_id,
                alias = alias
            });
        }

        public int InsertPortal(long entity_id, string application_name, string theme,
            bool authorized_only, bool allow_authorize, bool allow_register, bool allow_buy_files, bool allow_buy_products, string password_protection)
        {
            return this.PortalEntities.Insert(() => new PortalEntity()
            {
                portalID = entity_id,
                applicationName = application_name,
                themeName = theme,
                authorizedOnly = authorized_only,
                allowAuthorize = allow_authorize,
                allowRegister = allow_register,
                allowBuyFiles = allow_buy_files,
                allowBuyProducts = allow_buy_products,
                passwordProtection = password_protection
            });
        }

        #endregion


        #region Delete

        public int DeleteAliases(long from_portal_id)
        {
            return this.PortalAliases.Where(a => a.portalID == from_portal_id).Delete();
        }

        #endregion
    }
}
