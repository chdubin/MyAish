<%@ Page Title="" Language="C#" MasterPageFile="../Shared/twoColumn.Master" Inherits="System.Web.Mvc.ViewPage<Main.Models.LogOnModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">

<table cellspacing="0" cellpadding="0" border="0">
    <tbody>
        <tr>
	        <td><img height="36" width="549" alt="Login" src="<%= Url.Image("login_banner.jpg") %>" /></td>
        </tr>
        <tr>
	        <td><img height="10" width="1" src="<%= Url.Image("spacer.gif") %>" /></td>
        </tr>
        <tr>
	        <td><img height="31" width="549" src="<%= Url.Image("login_to_access.jpg") %>" /></td>
        </tr>
        <tr>
	        <td valign="top" id="login_table">
                <table cellspacing="0" cellpadding="0" border="0">
             	    <tbody>
                        <tr>
                            <td>
                                <img height="1" width="15" src="<%= Url.Image("spacer.gif") %>" /></td>
     	                    <td>
                                <% 
                                    using (Html.BeginForm("LogOn", "Account", new { returnUrl = ViewData["returnUrl"] }, FormMethod.Post, new { @class = "forms", enctype = "multipart/form-data" }))
                                    {
                                %>
                                <table cellspacing="5" cellpadding="0" border="0">
                                    <tbody>
                                        <tr>
     		                                <td colspan="3">
                                                <span class="red">Invalid username and password.<br />Please verify and enter them again.<br />
                                                </span>
                                            </td>
                                        </tr>
     		     		                <tr>
                                            <td colspan="3"><img height="1" width="1" src="<%= Url.Image("spacer.gif") %>" alt="" /></td>
     	            	                </tr>
     		                            <tr>
                                            <td align="right"><img src="<%= Url.Image("username.jpg") %>" /></td>
                                            <td>                                                                        
                                                <%= Html.TextBox("UserName", Model.UserName, new { maxlength="75", size="15" }) %>                                              
                                            </td>
     			                            <td></td>
                                        </tr>
                                        <tr>
                                            <td colspan="3"><img height="1" width="1" src="<%= Url.Image("spacer.gif") %>" alt="" /></td>
     		                            </tr>
                                        <tr>
                                            <td align="right"><img src="<%= Url.Image("password.jpg") %>" /></td>
                                            <td>
                                                <%= Html.Password("Password", Model.Password, new { maxlength = "75", size = "15", onkeypress = "onPressKey(event, this)" })%>
                                            </td>
                                            <td><input type="image" src="<%= Url.Image("login.jpg") %>" /></td>
                                        </tr>
                                        <tr>
                                            <td colspan="3"><img height="1" width="1" src="<%= Url.Image("spacer.gif") %>" alt="" /></td>
                                        </tr>
                                        <tr>
                                            <td></td>
     			                            <td><a href="<%= Url.Action("ForgotPassword", "Account") %>"><img height="10" width="147" src="<%= Url.Image("forgot_password.jpg") %>" /></a></td>
          		                            <td></td>
                                        </tr>
                                        <tr>
                                            <td colspan="3"><img height="1" width="1" src="<%= Url.Image("spacer.gif") %>" alt="" /></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <%
                                 } 
                                %>
                            </td>
                        </tr>
                    </tbody>
                </table>
        	</td>
        </tr>
        <tr>
	        <td><img height="10" width="1" src="<%= Url.Image("spacer.gif") %>" alt="" /></td>
        </tr>
        <tr>
	        <td>                
                <a href="<%= Url.Action("RegisterCustom", "Account")%>">
                    <img usemap="#Map" src="<%= Url.Image("login_bottom.jpg") %>" />
                </a>
            </td>
	    </tr>
    </tbody>
</table>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>
