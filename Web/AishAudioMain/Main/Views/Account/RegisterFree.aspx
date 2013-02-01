<%@ Page Title="" Language="C#" MasterPageFile="../Shared/oneColumnEmpty.Master" Inherits="System.Web.Mvc.ViewPage<Main.Models.Account.RegisterCustomModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
 
 <div align="left" style="position: relative; padding: 0px 60px 0px 60px;">
            <img height="419" width="595" src='<%= Url.Image("360_free.jpg") %>' /><br/>


        <div class="pos_it" style="top:180px">
            <% using (Html.BeginForm("RegisterCustom", "Account", new {returnUrl=ViewData["returnUrl"]}, FormMethod.Post, new { @class = "forms", enctype = "multipart/form-data" }))
               { 
            %>
            <%= Html.ValidationSummary(true, null, new { style = "margin-bottom:1em" })%>
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

<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
Jewish mp3 audio tape downloads. Torah mp3s of Judaism - Free streaming.
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>
