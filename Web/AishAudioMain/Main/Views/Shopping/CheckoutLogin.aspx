<%@ Page Title="" Language="C#" MasterPageFile="../Shared/twoColumn.Master"
    Inherits="System.Web.Mvc.ViewPage<Main.Models.Shopping.CheckoutLoginModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <table cellspacing="0" cellpadding="0" border="0" id="login_table">
        <tbody>
            <tr>
                <td colspan="2">
                    <img height="36" width="549" alt="Shopping Cart" src='<%= Url.Image("shopping_cart.jpg") %>' />
                </td>
            </tr>
            <tr bgcolor="white">
                <td width="60">
                </td>
                <td>
                    <img height="15" width="1" src='<%= Url.Image("spacer.gif") %>' />
                </td>
            </tr>
            <tr>
                <td height="10" colspan="2">
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <img height="19" width="449" src='<%= Url.Image("incorporate.jpg") %>' />
                </td>
            </tr>
            <tr>
                <td height="10" colspan="2">
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td valign="top">
                    
                    <%
                        using (Html.BeginForm("LogOn", "Account", new { returnUrl = "/Shopping/Cart" }, FormMethod.Post, new { @class = "forms" }))
                        {
                    %>                    
                        <table cellspacing="3" cellpadding="0" border="0">                        
                            <tr>
                                <td colspan="5">
                                    <span class="red"></span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img src='<%= Url.Image("username.jpg") %>' />
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                    <img src='<%= Url.Image("password.jpg") %>' />
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%= Html.TextBox("UserName", "", new { maxlength="75", size="15" }) %>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <%= Html.Password("Password", "", new { maxlength = "75", size = "15" })%>
                                </td>
                                <td width="5">
                                </td>
                                <td>
                                    <input type="image" src='<%= Url.Image("login.jpg") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                </td>
                                <td>                                    
                                    <a href="<%= Url.Action("ForgotPassword", "Account") %>">
                                        <img height="10" width="147" src='<%= Url.Image("forgot_password.jpg") %>' /></a>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">
                                    <img height="1" width="1" src='<%= Url.Image("spacer.gif") %>' alt="" />
                                </td>
                            </tr>                        
                        </table>

                    <% } %>
                </td>
            </tr>
            <tr bgcolor="white">
                <td colspan="2">
                    <br /><p style="padding: 10px;">It appears that you already have an account with us. Please login to continue.</p>
                </td>
            </tr>
            <tr bgcolor="white">
                <td colspan="2">
                    <br />                        
                    <a href="<%= Url.Action("Cart", "Shopping") %>">
                        <img height="30" width="552" src='<%= Url.Image("checkout_login_continue.jpg") %>' /></a>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
