<%@ Page Title="" Language="C#" MasterPageFile="../Shared/twoColumn.Master"
    Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content4" ContentPlaceHolderID="head" runat="server">
    <link href="<%= Url.Css("messages.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <br />
    <div align="center" class="searchcategorydisplay">
        Register
    </div>
    <br />
    <%= Html.ValidationSummary(true, "Action was unsuccessful. Please correct the errors and try again.", new { @class = "response-msg error ui-corner-all" })%>

    <% using (Html.BeginForm("RegisterCustomAuthorize", "Account", null, FormMethod.Post, new { @class = "forms", enctype = "multipart/form-data" }))
       { 
    %>                

        <%= Html.Hidden("email", ViewData["email"]) %>
        <%= Html.Hidden("old_password", ViewData["old_password"])%>

    <table border="0" width="584">        
            <tr style="color: red; font-weight: bold;">
            </tr>
            <tr>
                <td>
                    <br />
                    Before choosing your two free downloads, please take a moment to select a permanent
                    password, and let us know how you found us so we can learn how to reach others also
                    looking for authentic lively Torah.
                    <br /><br /><br />
                </td>
            </tr>
            <tr>
                <td>
                    <table cellspacing="2" cellpadding="0" border="0">
                            <tr>
                                <td align="right" style="font-size: 8pt;">
                                    Password:
                                </td>
                                <td style="padding-bottom:5px;">
                                    <%= Html.Password("password", "") %>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="font-size: 8pt;">
                                    Confirm Password:
                                </td>
                                <td>
                                    <%= Html.Password("confirm_password", "")%>                                    
                                </td>
                            </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>                    
                    <br />
                    <table cellspacing="0" cellpadding="5" border="0">
                            <tr>
                                <td width="280" class="booktitle" colspan="2">
                                    <br />
                                    I came to aishaudio.com from:
                                    <br />
                                </td>
                            </tr>                            
                            <tr>
                                <td>
                                    <br />
                                    <%=Html.DropDownList("came_from", new SelectList(Main.GlobalConstant.CAME_FROM, "Key", "Value", ""),
                                        "-- Choose One --", new { @class = "field select full" })%>
                                    <br /><br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Friend/News&nbsp;Link/Other:&nbsp;&nbsp;<%= Html.TextBox("fill_in", "") %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                    <input type="submit" class="btn" value="Submit" /><br />
                                    &nbsp;
                                </td>
                            </tr>
                    </table>
                </td>
            </tr>
    </table>

    <% } %>

    <br />
    <br />
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
</asp:Content>
