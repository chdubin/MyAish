<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Main.Areas.Admin.Models.User.EditPlanDates>" %>
    <ul>
            <li>
                <div>
                    <label class="desc" for="EndDate">Charge day:</label>
                    <div class="editor-field">
                        <%= Html.TextBox("ChargeDay" + Model.UserID, Model.ChargeDay.HasValue ? Model.ChargeDay.Value.ToString() : "", new { @class = "field text small" })%>
                    </div>
                </div>
            </li>
            <li>
                <div>
                    <label class="desc" for="StartDate">Start Date:</label>
                    <div class="editor-field">
                        <%= Html.TextBox("StartDate" + Model.UserID, (Model.StartDate != null ? Model.StartDate.Value.ToString("MM.dd.yyyy") : DateTime.Now.ToString("MM.dd.yyyy")).Replace(".", "/"), new { @class = "field text small datepicker" })%>
                    </div>
                </div>
            </li>
            <li>
                <div>
                    <label class="desc" for="EndDate">Paid Through:</label>
                    <div class="editor-field">
                        <%= Html.TextBox("EndDate" + Model.UserID, Model.EndDate != null ? Model.EndDate.Value.ToString("MM.dd.yyyy").Replace(".", "/") : "", new { @class = "field text small datepicker" })%>
                    </div>
                </div>
            </li>
    </ul>


<br />

<div style="margin-top: 30px;margin-bottom:30px;">
    <%= Ajax.ActionLink2("<span class='ui-icon ui-icon-circle-check'></span>Change dates", "UpdatePlanDates", null,
        new AjaxOptions()
        {
            HttpMethod="POST",
            OnBegin = "function(e){ OnBeginEditPlanDates(e, '" + Model.UserID + "'); }",
            OnSuccess = "function(){ OnSuccessEditPlanDates('" + Model.UserID + "'); }",
            UpdateTargetId = "UpdatePlanDates" + Model.UserID
        },
        new { @class = "btn ui-state-default" })%>
    
    <a href="javascript:void(0);" class="btn ui-state-default" onclick="CloseDialog('EditPlanDates<%= Model.UserID %>');"><span class="ui-icon ui-icon-circle-close"></span>Cancel</a>
</div>



