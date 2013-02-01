<%@ Page Title="" Language="C#" MasterPageFile="../Shared/oneColumnEmpty.Master"
    Inherits="System.Web.Mvc.ViewPage<Main.Models.Account.EnterCCInfoModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <link href="<%= Url.Css("custom.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= Url.Css("jquery/plugins/datepicker/3_7_5/smoothness.datepick.css") %>"
        rel="stylesheet" media="all" />
    <script type="text/javascript" src="<%= Url.JavaScript("jquery/plugins/datepicker/3_7_5/jquery.datepick.js") %>"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {

            $("#ExpirationDate").datepick({ dateFormat: 'yy-mm-dd' });
            if ("<%= Model.UseStandaloneShippingAddress %>" == "True") {
                $("#shipping_address_cb").click();
            }
            //$("#billing_address_cb").click();

        });

        function EnterNewCreditCard() {
            $("#CreditCardValidated").val("False");
            $("#CreditCard").val("");
            $("#CreditCardNumber").val("");
            $("#ExpirationDateMonth").val("");
            $("#ExpirationDateYear").val("");

            $("#CreditCardForm").show();

            return false;
        }

    </script>
    <table cellspacing="0" cellpadding="0" border="0" width="100%">
        <tbody>
            <tr>
                <td>
                    <img height="459" width="214" usemap="#securitymap" src='<%= Url.Image("security_box.jpg") %>' />
                    <map name="securitymap">
                        <area target="_blank" href="<%= Url.RouteUrl("PrivacyPolicyPage") %>" coords="28,400,169,418" shape="RECT" />
                    </map>
                </td>

                <% if (1==1) /*((DateTime.Now.TimeOfDay.CompareTo(new TimeSpan(20, 0, 0)) <= 0 && DateTime.Now.Date.CompareTo(new DateTime(2012, 4, 12)) == 0)
                       ||
                       (DateTime.Now.TimeOfDay.CompareTo(new TimeSpan(22, 0, 0)) >= 0 && DateTime.Now.Date.CompareTo(new DateTime(2012, 4, 14)) == 0))*/
                   { %>

                <td style="vertical-align: top; text-align: left; width: 100%;">
                    <%= Html.ValidationSummary(false, "Please correct the errors and try again.", new { @class = "response-msg error ui-corner-all" })%>
                    <%
                        using (Html.BeginForm("EnterCCInfo", "Account", null, FormMethod.Post, new { @class = "forms", enctype = "multipart/form-data" }))
                        {
                    %>
                    <%= Html.Hidden("CreditCardValidated", Model.CreditCardValidated)%>
                    <%= Html.Hidden("Email", Model.Email) %>
                    <%= Html.Hidden("Password", Model.Password)%>
                    <%= Html.Hidden("Authorized", Model.Authorized)%>
                    <%= Html.Hidden("UseStandaloneShippingAddress", Model.UseStandaloneShippingAddress)%>
                    <table cellspacing="0" cellpadding="0" border="0" class="input-info-table">
                        <tbody>
                            <tr>
                                <td class="maintitle">
                                    Checkout
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table cellspacing="0" cellpadding="0" border="0" class="input-info-table">
                        <tr>
                            <td colspan="2" align="center" class="checkoutheadingtd2">
                                <b>Enter Your Billing Information</b>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <label>
                                    <input id="billing_address_cb" type="radio" onclick="$('.shipping').val('');$('#shipping_information').hide();$('#UseStandaloneShippingAddress').attr('value', false)"
                                        checked="checked" value="billing" name="shipto" />Ship to my billing address.</label>
                                <br />
                                <label>
                                    <input id="shipping_address_cb" type="radio" onclick="$('#shipping_information').show();$('#UseStandaloneShippingAddress').attr('value', true);"
                                        value="shipping" name="shipto" />Ship to a different address that I will provide below.</label>
                                <br />
                                <i>&nbsp;&nbsp;&nbsp;&nbsp;Shipping information is required only if different from billing information.</i>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%; padding-bottom: 30px;">
                                <table cellspacing="0" cellpadding="0" border="0" style="width: 100%;">
                                    <tr>
                                        <td style="font-weight: bold; padding: 5px 0px 10px 0px;">
                                            Billing Information:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="checkoutinfoareatdheading">
                                                First Name:
                                            </div>
<%--                                                <%= Html.TextBox("FirstName1", Model.FirstName1, new { @class = "field text full" })%>
--%>                                                <%= Html.TextBox("FirstName1", string.Empty, new { @class = "field text full" })%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div class="checkoutinfoareatdheading">
                                                Last Name:
                                            </div>
<%--                                                <%= Html.TextBox("LastName1", Model.LastName1, new { @class = "field text full" })%>
--%>                                                <%= Html.TextBox("LastName1", string.Empty, new { @class = "field text full" })%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="checkoutinfoareatdheading">
                                                Postal Address:
                                            </div>
<%--                                            <%= Html.TextArea("PostalAddress1", Model.PostalAddress1, new { @class = "field text full", rows = "6" })%>
--%>                                            <%= Html.TextArea("PostalAddress1", string.Empty, new { @class = "field text full", rows = "6" })%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="checkoutinfoareatdheading">
                                                City:
                                            </div>
<%--                                            <%= Html.TextBox("City1", Model.City1, new { @class = "field text full" })%>
--%>                                            <%= Html.TextBox("City1", string.Empty, new { @class = "field text full" })%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="checkoutinfoareatdheading">
                                                State:
                                            </div>
<%--                                            <%=Html.DropDownListFor(c => c.State1, new SelectList(Main.GlobalConstant.STATES, "Key", "Value", Model.State1),
                                                    "Select state", new { @class = "field select full" })%>
--%>                                            <%=Html.DropDownListFor(c => c.State1, new SelectList(Main.GlobalConstant.STATES, "Key", "Value", string.Empty),
                                                    "Select State", new { @class = "field select full" })%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="checkoutinfoareatdheading">
                                                Other:
                                            </div>
                                            <%--<%= Html.TextBox("Description1", Model.Description1, new { @class = "field text full" })%>
                                            --%><%= Html.TextBox("Description1", string.Empty, new { @class = "field text full" })%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="checkoutinfoareatdheading">
                                                Zip or Postal Code:
                                            </div>
<%--                                            <%= Html.TextBox("PostalCode1", Model.PostalCode1, new { @class = "field text full" })%>
--%>                                            <%= Html.TextBox("PostalCode1", string.Empty, new { @class = "field text full" })%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="checkoutinfoareatdheading">
                                                Phone:
                                                <br />
                                                <div style="font-size: 8pt; font-weight: normal;">
                                                    Please use only numbers and dashes.
                                                </div>
                                            </div>
<%--                                            <%= Html.TextBox("Phone1", Model.Phone1, new { @class = "field text full" })%>
--%>                                            <%= Html.TextBox("Phone1", string.Empty, new { @class = "field text full" })%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="checkoutinfoareatdheading">
                                                Country:
                                            </div>
<%--                                            <%=Html.DropDownListFor(c => c.Country1, new SelectList(Main.GlobalConstant.COUNTRIES, "Key", "Value", Model.Country1),
                                                    "Select Country", new { @class = "field select full" })%>
--%>                                            <%=Html.DropDownListFor(c => c.Country1, new SelectList(Main.GlobalConstant.COUNTRIES, "Key", "Value", string.Empty),
                                                    "Select Country", new { @class = "field select full" })%>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%;">
                                <table id="shipping_information" cellspacing="0" cellpadding="0" border="0" style="width: 100%;
                                    display: none;">
                                    <tr>
                                        <td style="font-weight: bold; padding: 5px 0px 10px 0px;">
                                            Shipping Information:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="checkoutinfoareatdheading">
                                                First Name:
                                            </div>
<%--                                            <%= Html.TextBox("FirstName2", Model.FirstName2, new { @class = "field text full shipping" })%>
--%>                                            <%= Html.TextBox("FirstName2", string.Empty, new { @class = "field text full shipping" })%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="checkoutinfoareatdheading">
                                                Last Name:
                                            </div>
<%--                                            <%= Html.TextBox("LastName2", Model.LastName2, new { @class = "field text full shipping" })%>
--%>                                            <%= Html.TextBox("LastName2", string.Empty, new { @class = "field text full shipping" })%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="checkoutinfoareatdheading">
                                                Postal Address:
                                            </div>
<%--                                            <%= Html.TextArea("PostalAddress2", Model.PostalAddress2, new { @class = "field text full shipping", rows = "6" })%>
--%>                                            <%= Html.TextArea("PostalAddress2", string.Empty, new { @class = "field text full shipping", rows = "6" })%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="checkoutinfoareatdheading">
                                                City:
                                            </div>
<%--                                            <%= Html.TextBox("City2", Model.City2, new { @class = "field text full shipping" })%>
--%>                                            <%= Html.TextBox("City2", string.Empty, new { @class = "field text full shipping" })%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="checkoutinfoareatdheading">
                                                State:
                                            </div>
<%--                                            <%=Html.DropDownListFor(c => c.State2, new SelectList(Main.GlobalConstant.STATES, "Key", "Value", Model.State2),
                                                    "Select state", new { @class = "field select full" })%>
--%>                                            <%=Html.DropDownListFor(c => c.State2, new SelectList(Main.GlobalConstant.STATES, "Key", "Value", string.Empty),
                                                    "Select State", new { @class = "field select full" })%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="checkoutinfoareatdheading">
                                                Other:
                                            </div>
<%--                                            <%= Html.TextBox("Description2", Model.Description2, new { @class = "field text full shipping" })%>
--%>                                            <%= Html.TextBox("Description2", string.Empty, new { @class = "field text full" })%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="checkoutinfoareatdheading">
                                                Zip or Postal Code:
                                            </div>
<%--                                            <%= Html.TextBox("PostalCode2", Model.PostalCode2, new { @class = "field text full shipping" })%>
--%>                                            <%= Html.TextBox("PostalCode2", string.Empty, new { @class = "field text full shipping" })%>
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
<%--                                            <%= Html.TextBox("Phone2", Model.Phone2, new { @class = "field text full shipping" })%>
--%>                                            <%= Html.TextBox("Phone2", string.Empty, new { @class = "field text full shipping" })%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="checkoutinfoareatdheading">
                                                Country:
                                            </div>
<%--                                            <%=Html.DropDownListFor(c => c.Country2, new SelectList(Main.GlobalConstant.COUNTRIES, "Key", "Value", Model.Country2),
                                                    "Select Country", new { @class = "field select full" })%>
--%>                                            <%=Html.DropDownListFor(c => c.Country2, new SelectList(Main.GlobalConstant.COUNTRIES, "Key", "Value", string.Empty),
                                                    "Select Country", new { @class = "field select full" })%>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <% if (Model.CreditCardValidated)
                       { %>
                    <table cellspacing="0" cellpadding="0" border="0" class="input-info-table" id="CreditCardInfo">
                        <tbody>
                            <tr>
                                <td align="center" class="checkoutheadingtd2">
                                    <b>Credit Card Information</b>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding:10px 0;">
                                    <%=Model.CreditCard %>
                                    ending
                                    <%=Model.CreditCardNumber.Replace("xxxx-xxxx-xxxx-","") %>
                                    expires
                                    <%=Model.ExpirationDateMonth %>/<%= Model.ExpirationDateYear %>
                                    <a href="javascript:void(0);" onclick="EnterNewCreditCard()">change</a>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <%} %>
                    <table cellspacing="0" cellpadding="0" border="0" class="input-info-table" id="CreditCardForm"
                        <%=Model.CreditCardValidated?"style=\"display:none\" ":string.Empty %>>
                        <tbody>
                            <tr>
                                <td align="center" class="checkoutheadingtd2">
                                    <b>Enter Credit Card Information</b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="checkoutinfoareatdheading">
                                        Credit Card Type:
                                    </div>
                                    <div>
                                        <%=Html.DropDownListFor(c => c.CreditCard, new SelectList(Main.GlobalConstant.CREDIT_CARDS, "Key", "Value", Model.CreditCard),
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
                                    <%= Html.TextBox("CreditCardNumber", Model.CreditCardNumber, new { @class = "field text full" })%>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-bottom: 15px;">
                                    <div class="checkoutinfoareatdheading">
                                        Expiration Date:
                                    </div>
                                    <div>
                                        <div style="float: left;">
                                            <%=Html.DropDownListFor(c => c.ExpirationDateMonth, new SelectList(Main.GlobalConstant.MONTHS, "Key", "Value", 
                                                Model.ExpirationDateMonth), "", new { @class = "field select full", style = "width:60px;" })%>
                                        </div>
                                        <div style="float: left; padding: 6px;">
                                            /
                                        </div>
                                        <div style="float: left;">
                                            <%=Html.DropDownListFor(c => c.ExpirationDateYear, new SelectList(Main.GlobalConstant.YEARS, "Key", "Value", Model.ExpirationDateYear),
                                                "", new { @class = "field select full", style = "width:60px;" })%>
                                        </div>
                                        <div style="clear: both;">
                                        </div>
                                    </div>
<%--                                    <div class="checkoutinfoareatdheading" style="font-size: 17px;">
                                        (To change the expiration date, you must reenter the card number above.)
                                    </div>--%>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table cellspacing="0" cellpadding="0" border="0" class="input-info-table">
                        <tbody>
                            <tr>
                                <td align="center" class="checkoutheadingtd2">
                                    <b>&nbsp;</b>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; padding: 20px 0px 80px 0px;">
                                    <input type="image" src='<%= Url.Image("continue_large.jpg") %>' />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <% } %>
                </td>
<% } else { %>
<td  style="vertical-align: top; text-align: left; width: 100%;">
<div style="padding: 20px 0;">
    <p style="padding: 20px;color:#B94A48;padding: 8px 35px 8px 14px;
margin-bottom: 18px;
text-shadow: 0 1px 0 rgba(255, 255, 255, 0.5);
background-color: #FCF8E3;
border: 1px solid #FBEED5;
-webkit-border-radius: 4px;
-moz-border-radius: 4px;
border-radius: 4px;
border-image: initial;margin-bottom:10px;">
        <b>Please note:</b><br /><br />
        Due to observance of Passover Holiday, the shopping cart will not be active from 4/12/2012 8 PM EST until 4/14/2012 10PM EST<br /><br /> 
        Thank you!
     </p>
</div>

</td>
<% } %>
            </tr>
        </tbody>
    </table>
</asp:Content>
