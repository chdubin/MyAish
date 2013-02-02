<%@ Page Title="" Language="C#" MasterPageFile="../Shared/oneColumnEmpty.Master"
    Inherits="System.Web.Mvc.ViewPage<Main.Models.Account.RegisterCustomModel>" %>

<asp:Content ID="registerTitle" ContentPlaceHolderID="title" runat="server">
    <%=this.Context.GetCurrentPortal().Title %>
</asp:Content>
<asp:Content ID="registerContent" ContentPlaceHolderID="body" runat="server">
    <div style="padding-bottom:2em">
        <img height="15" width="4" alt="" src="<%= Url.Image("spacer.gif") %>" /><br />
        <div><%= Html.ValidationSummary(true, "Action was unsuccessful. Please correct the errors and try again.", new { @class = "response-msg error ui-corner-all" })%></div>
        <center>
            <% using (Html.BeginForm("LogOnPasswordProtection", "Account", new { returnUrl = ViewData["returnUrl"] }, FormMethod.Post, new { style = "display:inline", enctype = "multipart/form-data" }))
               { 
            %>
            <table cellspacing="0" cellpadding="0" border="0">
                <tr class="freetoploginformtext">
                    <td>
                        Password:&nbsp;&nbsp;
                    </td>
                    <td align="left">
                        <%= Html.Hidden("UserName", "PasswordProtection") %>
                        <%= Html.Password("Password", "") %>
                    </td>
                    <td width="15">
                    </td>
                    <td valign="bottom">
                        <input type="image" src='<%= Url.Image("login.jpg") %>' />
                    </td>
                </tr>
            </table>
            <% } %>
            </center>
    </div>
</asp:Content>
