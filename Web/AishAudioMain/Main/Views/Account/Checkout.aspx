<%@ Page Title="" Language="C#" MasterPageFile="../Shared/oneColumnEmpty.Master"
    Inherits="System.Web.Mvc.ViewPage<Main.Models.Account.CheckoutModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="<%= Url.Css("custom.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <% if (TempData["already_member"] != null && (bool)TempData["already_member"])
       { %>
    <p>
        You are already subscribed to this plan</p>
    <% }
       else
       { %>
    <table cellspacing="0" cellpadding="0" border="0" width="100%">
        <tr>
            <td valign="top" colspan="2">
                <img height="60" width="775" src='<%= Url.Image("purchasing.jpg") %>' />
            </td>
        </tr>
        <tr>
            <td>
                <img height="459" width="214" usemap="#securitymap" src='<%= Url.Image("security_box.jpg") %>' />
                <map name="securitymap">
                    <area target="_blank" href="<%= Url.RouteUrl("PrivacyPolicyPage") %>" coords="28,400,169,418" shape="RECT" />
                </map>
            </td>
            <td style="vertical-align: top; text-align: left; width: 100%;">
                <% using (Html.BeginForm("Checkout", "Account", null, FormMethod.Post, new { @class = "forms", enctype = "multipart/form-data" }))
                   { 
                %>
                <%= Html.HiddenFor(m=>m.Authorized) %>
                <table cellspacing="0" cellpadding="0" border="0" class="input-info-table">
                    <% if (Model.TransactionInfo.SubscribePlanFree)
                       { %>
                    <tr>
                        <td colspan="2">
                            <img height="184" width="560" alt="fmf" src='<%= Url.Image("fmf.jpg") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <hr align="left" width="100%" />
                        </td>
                    </tr>
                    <% } %>
                    <% if (Model.TransactionInfo.ShoppingPrice.Length > 0)
                       { %>
                    <tr>
                        <td colspan="2">
                            <table cellspacing="0" cellpadding="1" border="0" width="85%" class="checkoutinfoareatdheading">
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td align="right">
                                        Qty
                                    </td>
                                    <td>
                                    </td>
                                    <td align="right">
                                        Total
                                    </td>
                                </tr>
                                <% foreach (var p in Model.TransactionInfo.ShoppingPrice.Where(s => (s.price1 > 0 && s.ProductTypeID != (short)MainCommon.ProductTypeEnum.Units) || (s.ProductTypeID == (short)MainCommon.ProductTypeEnum.Subscribe)))
                                   { %>
                                <tr>
                                    <td>
                                        <%= p.Title.Replace("[Tape] ", "").Replace("[Disk] ", "").Replace("[File/Small Poster] ", "").Replace("[File] ", "")%>
                                    </td>
                                    <td width="10">
                                    </td>
                                    <td align="right">
                                        <%= p.cnt %>
                                    </td>
                                    <td width="10">
                                    </td>
                                    <td align="right">
                                        $<%= p.price1.ToString("N2")%>
                                    </td>
                                </tr>
                                <% } %>
                                <%-- TODO packadge
                                            <tr>
                                                <td>3-Month Super Deal with Jewish History Crash Course and Free 1 GB Sansa Clip + FM Beamer</td>
                                                <td width="10"></td>
                                                <td align="right">1</td>
                                                <td width="10"></td>
                                                <td align="right">$88.95</td>
                                            </tr>--%>
                                <% if (Model.TransactionInfo.UnitsBuy != null)
                                   { %>
                                <tr>
                                    <td>
                                        UNITS
                                    </td>
                                    <td width="10">
                                    </td>
                                    <td align="right">
                                        <%= Model.TransactionInfo.UnitsBuy.ProductEntity.price2.Value.ToString("N0")%>
                                    </td>
                                    <td width="30">
                                    </td>
                                    <td align="right">
                                        $<%= Model.TransactionInfo.UnitsBuy.ProductEntity.price1.Value.ToString("N2")%>
                                    </td>
                                </tr>
                                <% } %>
                                <%
                                   var price2 = Model.TransactionInfo.ShoppingPrice.Where(s => s.price2 > 0 && s.ProductTypeID != (short)MainCommon.ProductTypeEnum.Units).Select(p => p.price2).Sum();
                                   if (price2 > 0)
                                   { %>
                                <tr>
                                    <td>
                                        MP3 Download(s) (Total of Units Used)
                                    </td>
                                    <td width="10">
                                    </td>
                                    <td align="right">
                                    </td>
                                    <td width="30">
                                    </td>
                                    <td align="right">
                                        <%= price2.ToString("N2") %>
                                        units
                                    </td>
                                </tr>
                                <% } %>
                            </table>
                        </td>
                    </tr>
                    <% } %>
                    <tr>
                        <td colspan="2">
                            <%= Html.ValidationSummary(false, "Please correct the errors and try again.", new { @class = "response-msg error ui-corner-all" })%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="maintitle" style="padding-top: 5px;">
                            <% if (!(bool)ViewData["IsAuthorized"])
                               { %>
                            Create Account
                            <% }
                               else
                               { %>
                            Checkout
                            <% } %>
                        </td>
                    </tr>
                    <% if (!(bool)ViewData["IsAuthorized"])
                       { %>
                    <tr>
                        <td colspan="2" align="center" class="checkoutheadingtd2">
                            <b>Create Your Login</b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table cellspacing="0" cellpadding="0" border="0" style="width: 100%;">
                                <tr>
                                    <td style="width: 50%;">
                                        <div class="checkoutinfoareatdheading">
                                            E-mail Address:
                                        </div>
                                        <%= Html.TextBox("Email", Model.Email, new { @class = "field text full" })%>
                                        <div style="font-size: 10px;">
                                            This will be your username.
                                        </div>
                                    </td>
                                    <td style="width: 50%;">
                                        <div class="checkoutinfoareatdheading">
                                            Password:
                                        </div>
                                        <%= Html.Password("Password", Model.Password, new { @class = "field text full" })%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <div class="checkoutinfoareatdheading">
                                            Confirm Password:
                                        </div>
                                        <%= Html.Password("ConfirmPassword", Model.ConfirmPassword, new { @class = "field text full" })%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <% } %>
                    <tr>
                        <td colspan="2" align="center" class="checkoutheadingtd2">
                            <b>&nbsp;</b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: right; padding: 15px 10px 30px 0px;">
                            By clicking the "Continue" button below<br />
                            I agree to Aishaudio's <a target="_blank" href="#">Terms and Conditions</a>.
                            <br />
                            <br />
                            <input type="image" src='<%= Url.Image("continue_large.jpg") %>' />
                        </td>
                    </tr>
                </table>
                <%} %>
            </td>
        </tr>
    </table>
    <%} %>
</asp:Content>
