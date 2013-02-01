<%@ Page Title="" Language="C#" MasterPageFile="../Shared/twoColumn.Master"
    Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">

    <div align="center">
        <div class="searchcategorydisplay">
            Send Login Information
        </div>
    
    <br /><br />
    

    <% 
        string forgotPasswordStatus = ((string)ViewData["ForgotPasswordStatus"] ?? "EnterEmail");
        switch (forgotPasswordStatus)
        {
            case "NotFoundAccount":
                { %>

                <div style="width:300px;">
                    Sorry, we were unable to find your account.
                    For assistance, please send an email to <a href="mailto:cdubin@aish.com?subject=Customer Service: forgotten password">customer service</a>.
                </div>

        <%    break;
                }

            case "FoundAccount":
                { %>

                <div style="width:300px;">
                    We found your account, and emailed your password.            
                </div>
            
        <%   break;
                }

            default:
                {
                    using (Html.BeginForm("ForgotPassword", "Account", null, FormMethod.Post, new { @class = "forms", enctype = "multipart/form-data" }))
                    {
            %>                      
                    
                <table cellspacing="0" cellpadding="5" border="0" align="center" width="300" class="body">    
                    <tr>
                        <td class="bodytextb" colspan="2" style="padding:5px;">
                            Just type in your email address and submit this form. Your login information will
                            be emailed to you immediately.
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" class="bodytextb" style="padding:5px;">
                            Email Address:
                        </td>
                        <td style="padding:5px;">
                            <%= Html.TextBox("email", "", new { size="25" }) %>                            
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" style="padding:5px;">
                            <input type="submit" value="Send my login info" class="btn" name="Submit" />
                        </td>
                    </tr>                    
                </table>
        <%  }
                    break;
                }
        }        
        %>
    </div>



</asp:Content>
