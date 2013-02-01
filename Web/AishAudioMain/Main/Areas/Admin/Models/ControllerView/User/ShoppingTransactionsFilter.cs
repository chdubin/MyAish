using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Main.Areas.Admin.Models.ControllerView.User
{
    public class ShoppingTransactionsFilter
    {
        public enum TransactionStateEnum
        {
            AllTransactions = 0,
            AllSuccessfulMonthlyTransactions,
            AllSuccessfulPurchaseTransactions,
            AllUnsuccessfulMonthlyTransactions
        }

        public Guid user_id { get; set; }

        [DisplayName("From:")]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = "MM/dd/yyyy")]
        public DateTime? fsincedata { get; set; }

        [DisplayName("To:")]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = "MM/dd/yyyy")]
        public DateTime? fbeforedata { get; set; }

        [DisplayName("Transaction state")]
        [DefaultValue(TransactionStateEnum.AllTransactions)]
        public TransactionStateEnum ftranstate { get; set; }

        public SelectList TransactionState { get; set; }


        public void Initialize()
        {
            var plans = new List<KeyValuePair<TransactionStateEnum, string>>(new[]{
                new KeyValuePair<TransactionStateEnum,string>(TransactionStateEnum.AllTransactions,"All transactions"),
                new KeyValuePair<TransactionStateEnum,string>(TransactionStateEnum.AllSuccessfulMonthlyTransactions,"All successful monthly transactions"),
                new KeyValuePair<TransactionStateEnum,string>(TransactionStateEnum.AllSuccessfulPurchaseTransactions,"All successful purchase transactions"),
                new KeyValuePair<TransactionStateEnum,string>(TransactionStateEnum.AllUnsuccessfulMonthlyTransactions,"All unsuccessful monthly transactions")
            });

            TransactionState = new SelectList(plans, "Key", "Value", ftranstate);

        }
    }
}