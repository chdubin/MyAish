using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MainEntity.Models.Portal;

namespace MainEntity.Interfaces
{
    public interface IPortalService
    {
        PortalEntity[] GetPortals(bool with_only_active, bool with_only_nondeleted, bool include_aliases);

        PortalEntity GetPortal(long from_portal_id, bool with_only_active, bool with_only_nondeleted);

        PortalAlias[] GetAliases();

        void Update(long portal_id, string name, string application_name, string theme, string[] aliases, bool active,
            bool authorized_only, bool allow_authorize, bool allow_register, bool allow_buy_files, bool allow_buy_products, string password_protection, Action<string> update_password_protection);

        long InsertPortal(string name, string application_name, string theme, string[] aliases, bool active, long root_entity_id, Guid creator_id,
            bool authorized_only, bool allow_authorize, bool allow_register, bool allow_buy_files, bool allow_buy_products, string password_protection, Action<long> insert_password_protection);

        int DeletePortal(long portal_id);
    }
}
