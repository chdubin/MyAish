using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyPartnerKinoBL.Extension;
using MainEntity.Interfaces;
using MainEntity;
using System.Web.Security;

namespace MainBL
{
    public class PortalService : BaseBO, IPortalService
    {
        public PortalService(string connection_name)
            : base(connection_name)
        {
        }


        public MainEntity.Models.Portal.PortalEntity[] GetPortals(bool with_only_active, bool with_only_nondeleted, bool include_aliases)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (PortalDbManager context) =>
                {
                    var rval = context.GetPortals(with_only_active, with_only_nondeleted).OrderBy(p => p.portalID).ToArray();
                    if (include_aliases)
                    {
                        var ids = rval.Select(p => p.portalID).ToArray();
                        var aliases = context.GetAliases(ids).OrderBy(a => a.portalAliasID).ToArray();
                        foreach (var portal in rval)
                            portal.PortalAlias.AddRange(aliases.Where(a => a.portalID == portal.portalID));
                    }
                    return rval;
                });
        }

        public MainEntity.Models.Portal.PortalEntity GetPortal(long from_portal_id, bool with_only_active, bool with_only_nondeleted)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (PortalDbManager context) =>
                {
                    var portal = context.GetPortals(with_only_active, with_only_nondeleted).Where(p => p.portalID == from_portal_id).Single();
                    var aliases = context.GetAliases(new long[] { portal.portalID }).OrderBy(a => a.portalAliasID).ToArray();
                    portal.PortalAlias.AddRange(aliases);

                    return portal;
                });
        }


        public MainEntity.Models.Portal.PortalAlias[] GetAliases()
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot, 
                (PortalDbManager context) => context.GetAliases().ToArray());

        }


        #region Update

        public void Update(long portal_id, string name, string application_name, string theme, string[] aliases, bool active,
            bool authorized_only, bool allow_authorize, bool allow_register, bool allow_buy_files, bool allow_buy_products, string password_protection, Action<string> update_password_protection)
        {
            this.Exec(System.Data.IsolationLevel.ReadCommitted,
                (PortalDbManager context) => {
                    var prevPasswordPortection = context.GetPortals(false, true).Where(p => p.portalID == portal_id).Select(p => p.passwordProtection).Single();
                    if (!string.IsNullOrEmpty(password_protection))
                    {
                        authorized_only = true;
                        allow_authorize = true;
                        allow_register = false;
                    }

                    context.UpdateEntityItem(portal_id, name, active);
                    context.UpdatePortal(portal_id, application_name, theme,
                        authorized_only, allow_authorize, allow_register, allow_buy_files, allow_buy_products, password_protection);
                    context.DeleteAliases(portal_id);
                    foreach (var alias in aliases) context.InsertPortalAlias(portal_id, alias);

                    update_password_protection(prevPasswordPortection);
                });
        }

        #endregion


        #region Insert

        public long InsertPortal(string name, string application_name, string theme, string[] aliases, bool active, long root_entity_id, Guid creator_id,
            bool authorized_only, bool allow_authorize, bool allow_register, bool allow_buy_files, bool allow_buy_products, string password_protection, Action<long> insert_password_protection)
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted,
                (PortalDbManager context) =>
                {
                    if (!string.IsNullOrEmpty(password_protection))
                    {
                        authorized_only = true;
                        allow_authorize = true;
                        allow_register = false;
                    }

                    var createDate = DateTime.Now.ToUniversalTime();
                    var entityID = EntityItemService.InsertEnityItem(name, 0, active, MainCommon.EntityItemTypeEnum.PortalRoot,
                        creator_id, root_entity_id, root_entity_id, createDate, context);
                    context.InsertPortal(entityID, application_name, theme,
                        authorized_only, allow_authorize, allow_register, allow_buy_files, allow_buy_products, password_protection);
                    foreach (var alias in aliases) context.InsertPortalAlias(entityID, alias);

                    insert_password_protection(entityID);

                    return entityID;
                });
        }

        #endregion


        #region Delete

        public int DeletePortal(long portal_id)
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted,
                (PortalDbManager context) => EntityItemService.DeleteEnityItem(portal_id, context));
        }

        #endregion
    }
}
