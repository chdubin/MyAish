<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Main.Models.Account.ChangeCreditCard>" %>
<%using (Html.BeginForm())
  { %>
<div style="padding-bottom: 10px;">
    <%= Html.ValidationSummary(false, "Update was unsuccessful. Please correct the errors and try again.", new { @class = "response-msg error ui-corner-all" })%>
    <% if (TempData["success"] != null && Convert.ToBoolean(TempData["success"]) == true)
       { %>
    <div class="response-msg success ui-corner-all">
        <span>Credit Card has been successfully updated.</span>
    </div>
    <%} %>
</div>
<div align="center" class="searchcategorydisplay">
    Change Credit Card
</div>
<br />
<table>
    <tr> <!-- test -->
        <td style="padding-right: 10px">
            <%if (!string.IsNullOrEmpty(Model.CurrentMembershipCartID))
              { %>
            <h2>
                Current Credit Card</h2>
            <br />
            <table cellspacing="0" cellpadding="5" border="0" class="input-info-table" style="width: auto">
                <tr>
                    <td class="checkoutinfoareatdheading">
                        Last 4 digits:&nbsp;
                    </td>
                    <td>
                        <%=Model.CurrentMembershipCartID%>
                    </td>
                </tr>
                <tr>
                    <td class="checkoutinfoareatdheading">
                        Expiration:&nbsp;
                    </td>
                    <td>
                        <%= Model.CurrentExpirationDate.Value.ToString("MM/yyyy")%>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <%} %>
            <h2>
                Billing information</h2>
            <br />
            <table cellspacing="0" cellpadding="0" border="0" class="input-info-table">
                <tbody>
                    <tr>
                        <td>
                            <div class="checkoutinfoareatdheading">
                                First name:
                            </div>
                            <%= Html.TextBoxFor(m=>m.FirstName, new { @class = "field text full" })%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="checkoutinfoareatdheading">
                                Last name:
                            </div>
                            <%= Html.TextBoxFor(m=>m.LastName, new { @class = "field text full" })%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="checkoutinfoareatdheading">
                                Email:
                            </div>
                            <%= Html.TextBoxFor(m=>m.Email, new { @class = "field text full" })%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="checkoutinfoareatdheading">
                                Postal Address:
                            </div>
                            <%= Html.TextAreaFor(m=>m.PostalAddress, new { @class = "field text full", rows = "6" })%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="checkoutinfoareatdheading">
                                City:
                            </div>
                            <%= Html.TextBoxFor(m=>m.City, new { @class = "field text full" })%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="checkoutinfoareatdheading">
                                State or Province:
                            </div>
                            <%=Html.DropDownListFor(c => c.State, new SelectList(Main.GlobalConstant.STATES, "Key", "Value", Model.State),
                                                    "Select state", new { @class = "field select full" })%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="checkoutinfoareatdheading">
                                Zip or Postal Code:
                            </div>
                            <%= Html.TextBoxFor(m=>m.PostalCode, new { @class = "field text full" })%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="checkoutinfoareatdheading">
                                Phone:
                                <br />
                                <div style="font-size: 8pt; font-weight: normal;">
                                    Please use only numeric characters and dash.
                                </div>
                            </div>
                            <%= Html.TextBoxFor(m=>m.Phone, new { @class = "field text full" })%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="checkoutinfoareatdheading">
                                Country:
                            </div>
                            <%=Html.DropDownListFor(c => c.Country, new SelectList(Main.GlobalConstant.COUNTRIES, "Key", "Value", Model.Country),
                                                    "Select Country", new { @class = "field select full" })%>
                        </td>
                    </tr>
                </tbody>
            </table>
        </td>
        <td valign="top">
            <h2>
                New Credit Card Info</h2>
            <br />
            <table cellspacing="0" cellpadding="0" border="0" class="input-info-table">
                <tbody>
                    <tr>
                        <td>
                            <div>
                                <%=Html.DropDownListFor(m => m.CreditCard, new SelectList(Main.GlobalConstant.CREDIT_CARDS, "Key", "Value", Model.CreditCard),
                                            null, new { @class = "field select full", style = "float:left;" })%>
                                <img alt="credit cards" src='<%= Url.Image("ccards.jpg") %>' style="float: left;"
                                    border="0" />
                                <div style="clear: both;">
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="checkoutinfoareatdheading">
                                Credit Card Number:
                            </div>
                            <%= Html.TextBoxFor(m => m.CreditCardNumber, new { @class = "field text full" })%>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-bottom: 15px;">
                            <div class="checkoutinfoareatdheading">
                                Expiration Date:
                            </div>
                            <div>
                                <div style="float: left;">
                                    <%=Html.DropDownListFor(m => m.ExpirationDateMonth, new SelectList(Main.GlobalConstant.MONTHS, "Key", "Value",
                                                 Model.ExpirationDateMonth), "", new { @class = "field select full", style = "width:60px;" })%>
                                </div>
                                <div style="float: left; padding: 6px;">
                                    /
                                </div>
                                <div style="float: left;">
                                    <%=Html.DropDownListFor(m => m.ExpirationDateYear, new SelectList(Main.GlobalConstant.YEARS, "Key", "Value", Model.ExpirationDateYear),
                                                "", new { @class = "field select full", style = "width:60px;" })%>
                                </div>
                                <div style="clear: both;">
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input type="submit" value="Change Credit Card" class="btn" name="submit" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </td>
    </tr>
</table>
<%} %>
