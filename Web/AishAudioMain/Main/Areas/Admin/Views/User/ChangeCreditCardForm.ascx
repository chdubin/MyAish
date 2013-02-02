<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Main.Models.Account.ChangeCreditCard>" %>
<%using (Html.BeginForm(null, null, FormMethod.Post, new { id = "editCCForm" }))
  { %>
<%=Html.HiddenFor(m=>m.UserID) %>
<div style="padding-bottom: 10px;">
    <%= Html.ValidationSummary(false, "Update was unsuccessful. Please correct the errors and try again.", new { @class = "response-msg error ui-corner-all" })%>
    <% if (TempData["success"] != null && Convert.ToBoolean(TempData["success"]) == true)
       { %>
    <div class="response-msg success ui-corner-all">
        <span>Credit Card has been successfully updated.</span><br />
        <%=Html.ActionLink("Return user catalog","Index") %>
    </div>

    <%} %>
</div>
<div class="portlet ui-widget ui-widget-content ui-helper-clearfix ui-corner-all form-container">
<div class="portlet-content">
    <ul>
        <%if (!string.IsNullOrEmpty(Model.CurrentMembershipCartID))
          {%><li>
      <div>
          Current Credit Card</div>
  </li>
        <li>
            <div class="editform-twocol-leftcol"><%=Html.LabelFor(m=>m.CurrentMembershipCartID) %></div>
            <div class="editform-twocol-rightcol">
                <%=Model.CurrentMembershipCartID%></div>
        </li>
        <li>
            <div class="editform-twocol-leftcol"><%=Html.LabelFor(m => m.CurrentExpirationDate)%></div>
            <div class="editform-twocol-rightcol">
                <%= Model.CurrentExpirationDate.Value.ToString("MM/yyyy")%></div>
        </li>
        <li>
            <div class="editform-twocol-leftcol"><%=Html.Label("Card not active?")%></div>
            <div class="editform-twocol-rightcol">
                <%= Html.CheckBox("InActive")%> Select this option to deactivate a card</div>
        </li>
        <%} %>
        <li>
            <h2>Billing information</h2>
        </li>
        <li>
            <div class="editform-twocol-leftcol"><%=Html.LabelFor(m => m.FirstName)%></div>
            <div class="editform-twocol-rightcol">
                <%= Html.TextBoxFor(m=>m.FirstName, new { @class = "field text full" })%></div>
        </li>
        <li>
            <div class="editform-twocol-leftcol"><%=Html.LabelFor(m => m.LastName)%></div>
            <div class="editform-twocol-rightcol">
                <%= Html.TextBoxFor(m=>m.LastName, new { @class = "field text full" })%></div>
        </li>
        <li>
            <div class="editform-twocol-leftcol"><%=Html.LabelFor(m => m.Email)%></div>
            <div class="editform-twocol-rightcol">
                <%= Html.TextBoxFor(m=>m.Email, new { @class = "field text full" })%></div>
        </li>
        <li>
            <div class="editform-twocol-leftcol"><%=Html.LabelFor(m => m.PostalAddress)%></div>
            <div class="editform-twocol-rightcol">
                <%= Html.TextAreaFor(m=>m.PostalAddress, new { @class = "field text full", rows = "6" })%></div>
        </li>
        <li>
            <div class="editform-twocol-leftcol"><%=Html.LabelFor(m => m.City)%></div>
            <div class="editform-twocol-rightcol">
                <%= Html.TextBoxFor(m=>m.City, new { @class = "field text full" })%></div>
        </li>
        <li>
            <div class="editform-twocol-leftcol"><%=Html.LabelFor(m => m.State)%></div>
            <div class="editform-twocol-rightcol">
                <%=Html.DropDownListFor(c => c.State, new SelectList(Main.GlobalConstant.STATES,
                "Key", "Value", Model.State), "Select state", new { @class = "field select full"
                })%></div>
        </li>
        <li>
            <div class="editform-twocol-leftcol"><%=Html.LabelFor(m => m.PostalCode)%></div>
            <div class="editform-twocol-rightcol">
                <%= Html.TextBoxFor(m=>m.PostalCode, new { @class = "field text full" })%></div>
        </li>
        <li>
            <div class="editform-twocol-leftcol"><%=Html.LabelFor(m => m.Phone)%>
                <div style="font-size: 8pt; font-weight: normal;">
                    Please use only numeric characters and dash.
                </div>
            </div>
            <div class="editform-twocol-rightcol">
                <%= Html.TextBoxFor(m=>m.Phone, new { @class = "field text full" })%></div>
        </li>
        <li>
            <div class="editform-twocol-leftcol"><%=Html.LabelFor(m => m.Country)%></div>
            <div class="editform-twocol-rightcol">
                <%=Html.DropDownListFor(c => c.Country, new SelectList(Main.GlobalConstant.COUNTRIES, "Key", "Value", Model.Country),
                                                    "Select Country", new { @class = "field select full" })%></div>
        </li>
        <li style="background-color:#ffffd5">
            <div class="editform-twocol-leftcol"><%=Html.Label("Add New Card?")%></div>
            <div class="editform-twocol-rightcol">
                <%= Html.CheckBox("AddNewCard")%>
            </div>
        </li>
        <li style="background-color:#ffffd5">
            <div class="editform-twocol-leftcol"><%=Html.Label("New Credit Card Info")%></div>
            <div class="editform-twocol-rightcol"><%=Html.DropDownListFor(m => m.CreditCard, new SelectList(Main.GlobalConstant.CREDIT_CARDS, "Key", "Value", Model.CreditCard),
                                            null, new { @class = "field select small" })%>
                <img alt="credit cards" src='<%= Url.Image("ccards.jpg") %>' border="0" /></div>
        </li>
        <li style="background-color:#ffffd5">
            <div class="editform-twocol-leftcol"><%=Html.LabelFor(m => m.CreditCardNumber)%></div>
            <div class="editform-twocol-rightcol">
                <%= Html.TextBoxFor(m => m.CreditCardNumber, new { @class = "field text full" })%></div>
        </li>
        <li style="background-color:#ffffd5">
            <div class="editform-twocol-leftcol"><%=Html.LabelFor(m => m.ExpirationDateYear)%></div>
            <div class="editform-twocol-rightcol">
                        <%=Html.DropDownListFor(m => m.ExpirationDateMonth, new SelectList(Main.GlobalConstant.MONTHS, "Key", "Value",
                                                 Model.ExpirationDateMonth), "", new { @class = "field select full", style = "width:60px;" })%> /
                        <%=Html.DropDownListFor(m => m.ExpirationDateYear, new SelectList(Main.GlobalConstant.YEARS, "Key", "Value", Model.ExpirationDateYear),
                                                "", new { @class = "field select full", style = "width:60px;" })%>
            </div>
        </li>
        <li>
            <div class="editform-twocol-leftcol"><%=Html.Label("Amount to Charge:")%></div>
            <div class="editform-twocol-rightcol">
                <%= Html.TextBox("AmountToCharge", null, new { @class = "field text full" })%>
            </div>
        </li>
    </ul>
    <a class="btn ui-state-default" href="javascript:void(0);" onclick="$('#editCCForm').submit();">
        <span class="ui-icon ui-icon-circle-check"></span>Change</a>&nbsp;&nbsp; <a class="btn ui-state-default"
            href="<%= Url.Action("Index") %>"><span class="ui-icon ui-icon-circle-close"></span>Cancel</a>
</div></div>
<%} %>
