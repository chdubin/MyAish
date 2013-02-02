<%@ Page Title="" Language="C#" MasterPageFile="../Shared/oneColumnEmpty.Master" Inherits="System.Web.Mvc.ViewPage<Main.Models.Account.RegisterCustomModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">

 
	    <img height="50" width="408" src='<%= Url.Image("registerstreaming_408x50.gif") %>' />
	    <br /><br />

        <p style="font-size:20px;padding-bottom:15px;">
            Membership Validation
        </p>	
	    <p>
            Fill in the following to validate your membership.<br /><br /><br />
	    </p>
        <br />

        <table cellspacing="0" cellpadding="0" border="0">
            <tr>
                <td style="background-color:#0066cc;">
            	    <img height="25" align="top" width="299" src='<%= Url.Image("registeraccount_408x50.gif") %>' />
		        </td>
            </tr>
		    <tr>
                <td style="background-color:#dddddd;height:178px;">

                    <% using (Html.BeginForm("RegisterCustomValidateMembership", "Account", null, FormMethod.Post, 
                    new { @class = "forms", enctype = "multipart/form-data" }))
                       { 
                    %>
                        <%= Html.Hidden("Email", Model.Email) %>
                        <%= Html.Hidden("FirstName", Model.FirstName)%>
                        <%= Html.Hidden("LastName", Model.LastName)%>


        	            <table cellspacing="0" cellpadding="5" style="border-collapse: collapse;text-align:left;">
     		                <tr>
                                <td height="7" colspan="2">
                                    <% if (!string.IsNullOrEmpty((string)ViewData["ValidateMembershipError"]))
                                       {%>
                                    <div style="color:Red;padding:10px 10px 10px 5px;">
                                        <%= (string)ViewData["ValidateMembershipError"]%>
                                    </div>
                                    <% } %>
                                </td>
                            </tr>                    
		                    <tr>
                                <td style="padding:5px;width:100%;">
                                    Password:
                                </td>
                                <td style="padding:5px;text-align:right;">
                                    <%= Html.TextBox("Password", "", new { size = "40" })%>                                    
                                </td>
                            </tr>
                            <tr>  
                                <td height="34" align="center" colspan="2">
                                    <input type="submit" value="Validate Membership" />
                                </td>
                            </tr>
                        </table>

                    <% } %>
		                
                    <div height="24" align="center">
                        <hr noshade="" />
                    </div>
                        

                    <% using (Html.BeginForm("RegisterCustomResendRegistrationCode", "Account", null, FormMethod.Post, new { @class = "forms", enctype = "multipart/form-data" }))
                       { 
                    %>

                        <%= Html.Hidden("FirstName", Model.FirstName)%>
                        <%= Html.Hidden("LastName", Model.LastName)%>


                        <table cellspacing="0" cellpadding="5" style="border-collapse: collapse;text-align:left;">
     		                <tr>
                                <td height="7" colspan="2">
                                    <% if (!string.IsNullOrEmpty((string)ViewData["ResendRegistrationCodeError"]))
                                       {%>
                                    <div style="color:Red;padding:10px 10px 10px 5px;">
                                        <%= (string)ViewData["ResendRegistrationCodeError"]%>
                                    </div>
                                    <% } %>
                                </td>
                            </tr>
		                    <tr>
                                <td height="24" align="center" colspan="2">
                                    Resend Registration Code:
                                </td>
                            </tr>
		                    <tr>
                                <td style="padding:5px;">
                                    Enter Email Address:
                                </td>
                                <td width="162" style="padding:5px;text-align:right;">
                                    <%= Html.TextBox("Email", "", new { size = "40", value = "" })%>                                    
                                </td>
                            </tr>
		                    <tr>
                                <td height="34" align="center" colspan="2">
                                    <input type="submit" value="Resend Registration Code" />
                                </td>
                            </tr>		
                        </table>

                    <% } %>

     		    </td>
            </tr>
        </table>     		
    <br /><br /><br />



</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>
