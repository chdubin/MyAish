using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MainCommon;
using MainEntity.Models.Shopping;

namespace MainEntity
{
	public partial class ShoppingDbManager
	{
		public IQueryable<Shopping> SelectShopping(Guid user_id, long from_entity_id, short[] states)
		{
			return from sh in this.Shopping
				   where sh.UserId == user_id
					   && sh.entityID == from_entity_id
					   && states.Contains(sh.shoppingStateID)
					   && !sh.deleted
				   select sh;
		}

	}
}
