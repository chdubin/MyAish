<%@ Page Title="" Language="C#" MasterPageFile="../Shared/oneColumnEmpty.Master" Inherits="System.Web.Mvc.ViewPage<Main.Models.Account.RegisterModel>" %>

<asp:Content ID="registerTitle" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="registerContent" ContentPlaceHolderID="body" runat="server">

    <%= Html.ValidationSummary(true, "Incorrect email.", new { @class = "response-msg error ui-corner-all" })%>


    <div align="left" style="position: relative;padding:0px 50px 0px 70px;">
        <img src="<%= Url.Image("spacer.gif") %>" style="height:15px;width:1px" />
        <img src="<%= Url.Image("download_new.jpg") %>" style="height:530px;width:677px" alt="Downloads" usemap="#Map" />
        <map id="Map" name="Map">        
            <area alt="See details" href='<%= Url.Action("Offerings", "Account") %>' coords="300,140,365,160" shape="rect" />            
        </map>

        <img src="<%= Url.Image("spacer.gif") %>" style="height:25px;width:4px" />
        <div class="pos_it2" style="padding-left:10px;">
           <% using (Html.BeginForm("Register", "Account", null, FormMethod.Post, new { @class = "forms", enctype = "multipart/form-data" }))
           { 
            %>
                <div style="text-align:left; padding-bottom:8px;">
                        <%= Html.TextBox("Email", Model.Email, new { @class = "field text full" })%>                                
                            
                </div>                        
                <div align="left">
                    <input type="image" src='<%= Url.Image("continue.jpg") %>' />
                </div>
            <% } %>
        </div>
    </div>


</asp:Content>
