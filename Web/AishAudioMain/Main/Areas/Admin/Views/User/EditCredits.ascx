<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Main.Areas.Admin.Models.User.EditCredits>" %>


<div>
  <label class="desc" for="Credits">Units:</label>
  <div class="editor-field">
        <%= Html.TextBox("Credits", Model.Credits.ToString("N0"), new { @class = "field text full", id = "Credits" + Model.UserID })%>
  </div>
</div>


<br />

<div style="margin-top: 30px;margin-bottom:30px;">
    <%= Ajax.ActionLink2("<span class='ui-icon ui-icon-circle-check'></span>Change units", "EditCredits", null,
        new AjaxOptions()
        {
            HttpMethod = "Get",
            InsertionMode = InsertionMode.Replace,
            OnBegin = "function(e){ OnBeginEditCredits(e, '" + Model.UserID + "'); }",
            OnSuccess = "function(){ OnSuccessEditCredits('" + Model.UserID + "'); }",
            UpdateTargetId = "CreditVal" + Model.UserID
        },
        new { @class = "btn ui-state-default" })%>
    
    <a href="javascript:void(0);" class="btn ui-state-default" onclick="CloseDialog('EditCredits<%= Model.UserID %>');"><span class="ui-icon ui-icon-circle-close"></span>Cancel</a>
</div>





