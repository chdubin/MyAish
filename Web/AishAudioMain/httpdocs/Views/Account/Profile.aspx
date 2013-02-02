<%@ Page Title="" Language="C#" MasterPageFile="../Shared/twoColumn.Master"
    Inherits="System.Web.Mvc.ViewPage<Main.Models.Account.EditProfileModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="<%= Url.Css("custom.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <div style="padding-bottom:10px;">
        <%= Html.ValidationSummary(false, "Update was unsuccessful. Please correct the errors and try again.", new { @class = "response-msg error ui-corner-all" })%>
        <% if (TempData["success"] != null && Convert.ToBoolean(TempData["success"]) == true)
           { %>
            <div class="response-msg success ui-corner-all">
                <span>Profile has been successfully updated.</span>
            </div>
        <%} %>
    </div>


    <div align="center" class="searchcategorydisplay">
        Update Account Information
    </div>
    <br />



        <table cellspacing="0" cellpadding="0" border="0" class="body">
            <tr>	
	            <td valign="top">
                    <% using (Html.BeginForm("Profile", "Account", new { user_id = Model.UserID }, FormMethod.Post, new { @class = "forms", enctype = "multipart/form-data" }))
                        { 
                    %>
                        <style type="text/css">
                            .editprofile
                            {
                            }
                            
                            .editprofile td
                            {
                                padding:5px;
                            }
                        </style>                        

                        <%= Html.HiddenFor(c => c.UserID)%>

	                    <table cellspacing="0" cellpadding="5" border="0" class="editprofile">	
                            <tr>
                                <td class="signuptextb" colspan="2">
                                    Below is the account information that we have for you.<br />
                                    Please make any necessary changes and click "Update".
                                </td>
                            </tr>
	                        <tr>
                                <td class="bodytextbigr" colspan="2">
                                </td>
                            </tr>
	                        <tr>
		                        <td class="signuptextb">
                                    First Name:
                                </td>
                                <td>
                                    <%= Html.TextBoxFor(c => c.FirstName, new { @class = "field" })%>
                                </td>
	                        </tr>
	                        <tr>
                                <td class="signuptextb">
                                    Last Name:
                                </td>
                                <td>
                                    <%= Html.TextBoxFor(c => c.LastName, new { @class = "field" })%>
                                </td>
	                        </tr>
	                        <tr>
                                <td class="signuptextb">
                                    Username:
                                </td>
                                <td>
                                    <%= Html.TextBoxFor(c => c.UserName, new { @class = "field" })%>
                                </td>
	                        </tr>	
	                        <tr>
                                <td class="signuptextb">
                                    Email Address:
                                </td>
                                <td>
                                    <%= Html.TextBoxFor(c => c.Email, new { @class = "field" })%>
                                </td>
	                        </tr>		
	                        <tr>
                                <td class="signuptextb">
                                    Postal Address:
                                </td>    
                                <td>
                                    <%= Html.TextAreaFor(c => c.PostalAddress1, new { @class = "field" })%>
                                </td>
	                        </tr>
	                        <tr>
                                <td class="signuptextb">
                                    City:
                                </td>
                                <td>
                                    <%= Html.TextBoxFor(c => c.City1, new { @class = "field" })%>
                                </td>
	                        </tr>
	                        <tr>
                                <td class="signuptextb">
                                    State or Province:
                                </td>
                                <td>
                                    <%=Html.DropDownListFor(c => c.State1, new SelectList(Main.GlobalConstant.STATES, "Key", "Value", Model.State1),
                                        "Select state", new { @class = "field select full" })%>     
	                            </td>
	                        </tr>
                            <tr>
                                <td class="signuptextb">
                                    Zip or Postal Code:
                                </td>
                                <td>
                                    <%= Html.TextBoxFor(c => c.PostalCode1, new { @class = "field" })%>
                                </td>
	                        </tr>
	                        <tr>
                                <td class="signuptextb">
                                    Country:
                                </td>
                                <td>
                                    <%=Html.DropDownListFor(c => c.Country1, new SelectList(Main.GlobalConstant.COUNTRIES, "Key", "Value", Model.Country1),
                                        "Select Country", new { @class = "field select full" })%>     
                                </td>
	                        </tr>
	                        <tr>
                                <td class="signuptextb">
                                    Phone Number:
                                </td>
                                <td>
                                    <%= Html.TextBoxFor(c => c.Phone1, new { @class = "field" })%>
                                </td>
	                        </tr>
	                        <tr>
                                <td class="signuptextb">
                                    Day Phone:
                                </td>                    
                                <td>
                                    <%= Html.TextBoxFor(c => c.DayPhone1, new { @class = "field" })%>                    
                                </td>
	                        </tr>	
	                        <tr>
                                <td valign="top" class="signuptextb">
                                    Credit Card Info:
		                        </td>
                                <td>
                                <%if (!string.IsNullOrEmpty(this.Model.CreditCardID))
                                  { %>
	                                Last 4 digits: <%=this.Model.CreditCardID %><br />
	                                Expiration: <%=this.Model.CreditCardExpiration.Value.ToString("MM/yyyy")%><br />
                                    <%} %>
                                    <b><%=Html.ActionLink("Enter Credit Card", "ChangeCreditCard")%></b>
	                            </td>
	                        </tr>
	                        <tr>
                                <td colspan="2">
                                    <b>
                                        Note: Please make any changes to your account information on this page before changing your credit-card information.
                                    </b>
                                </td>
                            </tr>	
	                        <tr>
     	                        <td colspan="2">
                                    <input type="submit" name="submit" class="btn" value="Update" />
                                </td>
                            </tr>	
	                    </table>
                    <%  } %>
	            </td>
	            <td width="5"></td>
	            <td bgcolor="gray" width="2">
                    <img height="1" width="2" src='<%= Url.Image("spacer.gif") %>' />
                </td>
	            <td width="5"></td>	
	            <td valign="top">	
                    <% using (Html.BeginForm("ChangePasswordProfile", "Account", FormMethod.Post, new { @class = "forms", enctype = "multipart/form-data" }))
                       { 
                    %>
                        <style type="text/css">
                            .changepassword
                            {
                                background-color: #FFFFD5;
                                border: 1px dotted orange
                            }
                            
                            .changepassword td
                            {
                                padding:5px;
                            }
                            .field
                            {
                                height: 26px;
                            }
                        </style>

	                    <table cellspacing="0" cellpadding="5" border="0" class="changepassword">
                            <tr>
                                <td width="280" class="signuptextb">
                                    To change your login password, type a new one and confirm it below.
                                </td>
                            </tr>
	                        <tr>
                                <td width="280" class="bodytextbigr">     
                                    <%= Html.Hidden("redirectUrl", HttpContext.Current.Request.RawUrl)%>
                                </td>
                            </tr>
                            <tr>
                                <td class="signuptextb">
                                    New Password
                                <br />
                                    <%= Html.Password("Password", "", new { @class = "field" })%>
                                </td>
                            </tr>
                            <tr>
                                <td class="signuptextb">
                                    Confirm New Password
                                <br />
                                    <%= Html.Password("ConfirmPassword", "", new { @class = "field" })%>                                    
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input type="submit" value="Change Password" class="btn" name="submit" />
                                </td>
                            </tr>
                        </table>
                    <% } %>	
                </td>	
            </tr>
        </table>


</asp:Content>
