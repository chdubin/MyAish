<%@ Page Language="C#" MasterPageFile="../Shared/oneColumnEmpty.Master" Inherits="System.Web.Mvc.ViewPage<Main.Models.Account.RegisterCustomModel>" %>

<asp:Content ID="registerTitle" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="registerContent" ContentPlaceHolderID="body" runat="server">
    <div align="left" style="position: relative; padding: 0px 60px 0px 60px;">
        <img height="15" width="1" alt="" src="<%= Url.Image("spacer.gif") %>" />
        <img height="619" width="595" src='<%= Url.Image("login_signup_new.jpg") %>' />
        <img height="15" width="4" alt="" src="<%= Url.Image("spacer.gif") %>" /><br />
        <div style="position: absolute; top: 5px; left: 320px;">
            <%= Html.ValidationSummary(true, "Action was unsuccessful. Please correct the errors and try again.", new { @class = "response-msg error ui-corner-all" })%>
        </div>
        <div class="pos_it_login">
            <% using (Html.BeginForm("LogOn", "Account", new { returnUrl = ViewData["returnUrl"] }, FormMethod.Post, new { @class = "forms", enctype = "multipart/form-data" }))
               { 
            %>
            <table cellspacing="0" cellpadding="0" border="0">
                <tr class="freetoploginformtext">
                    <td valign="bottom">
                        Username:&nbsp;&nbsp;
                    </td>
                    <td align="left">
                        <%= Html.TextBox("UserName", "") %>
                    </td>
                    <td width="20">
                        &nbsp;
                    </td>
                    <td>
                        Password:&nbsp;&nbsp;
                    </td>
                    <td align="left">
                        <%= Html.Password("Password", "") %>
                    </td>
                    <td width="15">
                    </td>
                    <td valign="bottom">
                        <input type="image" src='<%= Url.Image("login.jpg") %>' />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                    </td>
                    <td align="left" colspan="3">
                        <a class="freetoploginformforgotpwtext" href="<%= Url.Action("ForgotPassword", "Account") %>">
                            Forgot Username/Password?</a>
                    </td>
                </tr>
            </table>
            <% } %>
        </div>
        <div class="pos_it">
            <% using (Html.BeginForm("RegisterCustom", "Account", new { returnUrl = ViewData["returnUrl"] }, FormMethod.Post, new { @class = "forms", enctype = "multipart/form-data" }))
               { 
            %>
            <table cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td align="left">
                        <img src='<%= Url.Image("first_name.jpg") %>' alt="" />
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <%= Html.TextBox("FirstName", Model.FirstName, new { @class = "field text full", size = "17", style = "font-size:10pt" })%>
                    </td>
                </tr>
                <tr>
                    <td height="5">
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <img src='<%= Url.Image("last_name.jpg") %>' alt="" />
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <%= Html.TextBox("LastName", Model.LastName, new { @class = "field text full", size="17", style="font-size:10pt" })%>
                    </td>
                </tr>
                <tr>
                    <td height="5">
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <img src='<%= Url.Image("your_email.jpg") %>' alt="" />
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <%= Html.TextBox("Email", Model.Email, new { @class = "field text full", size = "30", style = "font-size:10pt" })%>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="email_explain">
                        (use full address, e.g. john@aol.com)<br>
                        This email address will be used to verify your account.
                    </td>
                </tr>
                <tr>
                    <td height="8">
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <input type="image" src='<%= Url.Image("continue.jpg") %>' />
                    </td>
                </tr>
            </table>
            <% } %>
        </div>
    </div>
</asp:Content>
