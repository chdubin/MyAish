using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Main.Common.Attributes;
using Main.Common;
using MainEntity.Interfaces;
using Main.Areas.Admin.Models.Common;
using MainCommon;
using Main.Areas.Admin.Models.ControllerView.Transaction;

namespace Main.Areas.Admin.Controllers
{
    public class TransactionController : Controller
    {
        private IShoppingService _shoppingService;
        private IActivityLogService _activityLogService;

        public TransactionController(IShoppingService shopping_service, IActivityLogService activity_log_service)
        {
            _shoppingService = shopping_service;
            _activityLogService = activity_log_service;
        }

        [PortalAuthorize(Roles = UserRoles.SUPERUSER_ROLE)]
        public ActionResult Index()
        {
            return View();
        }

        [PortalAuthorize(Roles = UserRoles.SUPERUSER_ROLE)]
        public ActionResult ReportMothlyFees(Main.Areas.Admin.Models.ControllerView.Transaction.ShoppingTransactionsFilter filter, bool unsuccessful = false, int ps = 100, int p = 1)
        {
            short[] tranState = unsuccessful ?
                new[] { (short)ShoppingTransactionStateEnum.Rollback } :
                new[] { (short)ShoppingTransactionStateEnum.Prepaid, (short)ShoppingTransactionStateEnum.Complete };
            var tranType = new[] { 
                ShoppingTransactionTypeEnum.monthlyfee.ToString(), ShoppingTransactionTypeEnum.authorize_monthlyfee.ToString(),
                ShoppingTransactionTypeEnum.authorize_monthlyfee_purchase.ToString(), ShoppingTransactionTypeEnum.monthlyfee_purchase.ToString() };

            int recordsCount = _shoppingService.GetShoppingTransactionsCnt(filter.fsincedata, filter.fbeforedata, tranState, tranType);
            var model = _shoppingService.GetShoppingTransactions((p - 1) * ps, ps, filter.fsincedata, filter.fbeforedata, tranState, tranType);

            this.ViewData["Pager"] = new PagingData(Request.Path, Request.QueryString, recordsCount, ps, p, 1, 100);
            this.ViewData["Title"] = "Monthly Fees";
            this.ViewData["Filter"] = filter;

            if (unsuccessful)
            {
                var activity = _activityLogService.GetLastActivity(model.Select(t => t.UserId).ToArray(), new[] { ActivityLogTypeEnum.DownloadClass, ActivityLogTypeEnum.StreamingClass, ActivityLogTypeEnum.StreamingFreeClass, ActivityLogTypeEnum.StreamingFullFreeClass });
                this.ViewData["Activity"] = activity;
            }

            return unsuccessful ? View("UnsuccessfulTransactionList", model) : View("SuccessfulTransactionReport", model);
        }

        [PortalAuthorize(Roles = UserRoles.SUPERUSER_ROLE)]
        public ActionResult ReportPurchases(Main.Areas.Admin.Models.ControllerView.Transaction.ShoppingTransactionsFilter filter, int ps = 100, int p = 1)
        {
            var tranState = new[] { (short)ShoppingTransactionStateEnum.Prepaid, (short)ShoppingTransactionStateEnum.Complete };
            var tranType = new[] { 
                ShoppingTransactionTypeEnum.purchase.ToString(),
                ShoppingTransactionTypeEnum.authorize_monthlyfee_purchase.ToString(), ShoppingTransactionTypeEnum.monthlyfee_purchase.ToString() };

            int recordsCount = _shoppingService.GetShoppingTransactionsCnt(filter.fsincedata, filter.fbeforedata, tranState, tranType);
            var model = _shoppingService.GetShoppingTransactions((p - 1) * ps, ps, filter.fsincedata, filter.fbeforedata, tranState, tranType);

            this.ViewData["Pager"] = new PagingData(Request.Path, Request.QueryString, recordsCount, ps, p, 1, 100);
            this.ViewData["Title"] = "Purchases";
            this.ViewData["Filter"] = filter;

            return View("SuccessfulTransactionReport", model);

        }

        [PortalAuthorize(Roles = UserRoles.SUPERUSER_ROLE)]
        public ActionResult AllTransactions(Main.Areas.Admin.Models.ControllerView.Transaction.ShoppingTransactionsFilter filter, int ps = 100, int p = 1)
        {
            int recordsCount = _shoppingService.GetShoppingTransactionsCnt(filter.fsincedata, filter.fbeforedata, null, null);
            var model = _shoppingService.GetShoppingTransactions((p - 1) * ps, ps, filter.fsincedata, filter.fbeforedata, null, null);

            this.ViewData["Pager"] = new PagingData(Request.Path, Request.QueryString, recordsCount, ps, p, 1, 100);
            this.ViewData["Title"] = "All";
            this.ViewData["Filter"] = filter;

            return View("AllTransactions", model);
        }

        [PortalAuthorize(Roles = UserRoles.SUPERUSER_ROLE)]
        public ActionResult TransactionDetail(long transaction_id)
        {
            TransactionDetail model = null;
            base.ViewData["TransactioID"] = transaction_id;

            var transaction = _shoppingService.GetShoppingTransaction(transaction_id);
            if (transaction != null)
            {
                var orders = _shoppingService.GetOrders(transaction.UserId, transaction_id);
                var addresess = _shoppingService.GetShoppingAddresses(new[] { transaction_id });

                model = new TransactionDetail(transaction, orders, addresess);
            }
            else model = new TransactionDetail();

            return View(model);
        }
    }
}
