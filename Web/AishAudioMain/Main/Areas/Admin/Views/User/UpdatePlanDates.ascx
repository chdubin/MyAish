<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Main.Areas.Admin.Models.User.EditPlanDates>" %>
<%if (Model.StartDate != null)
  { %><div>
          Last Charge Date:
          <%= Model.StartDate.Value.ToString("MM/dd/yyyy hh:mm:ss")%></div>
<%} %>
<%if (Model.ChargeDay != null)
  { %><div>
          Charge Day:
          <%= Model.ChargeDay%></div>
<%} %>
<%if (Model.EndDate != null)
  { %><div>
          Charge date:<%= Model.EndDate%></div>
<%} %>
<div>
    <%=Ajax.ActionLink("Edit Charge Info", "EditPlanDates", new { user_id = Model.UserID, start_subscribe_date = Model.StartDate, end_subscribe_date = Model.EndDate, charge_day = Model.ChargeDay },
                                                                                                              new AjaxOptions() { HttpMethod = "Get", UpdateTargetId = "EditPlanDates" + Model.UserID, OnSuccess = "function(){CreateAndOpenDialog('EditPlanDates" + Model.UserID + "');}" })%>
</div>
