using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainCommon
{
	public enum ShoppingTransactionStateEnum : short
	{
		Begint=0,
		Prepaid=1,
		Complete=2,
		Rollback=3
	}
}
