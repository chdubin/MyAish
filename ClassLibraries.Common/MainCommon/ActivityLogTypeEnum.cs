using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainCommon
{
	public enum ActivityLogTypeEnum:short
	{
		SubscribeToNewsOnEmail = 1,
		SpecialOfferRegisterEmail = 2,
		
		DownloadClass = 4,
		StreamingClass = 3,
		StreamingFreeClass = 5,
        StreamingFullFreeClass = 6,
	}
}
