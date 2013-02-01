using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainCommon
{
    public enum ShoppingTransactionTypeEnum
    {
        NULL,

        /// <summary>
        /// For authorize card transactions
        /// </summary>
        authorize,
        authorize_monthlyfee,
        authorize_monthlyfee_purchase,
        authorize_purchase,

        /// <summary>
        /// For monthlyfee subscribe plan transactions
        /// </summary>
        monthlyfee,
        monthlyfee_purchase,

        /// <summary>
        /// User purchase transactions
        /// </summary>
        purchase,
        /// <summary>
        /// Manual transactions
        /// </summary>
        manual,
        /// <summary>
        /// Monthly subscribe purchase on daemon transactions
        /// </summary>
        monthly,
    }
}
