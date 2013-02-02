<%@ Page Title="" Language="C#" MasterPageFile="../Shared/main.Master"
    Inherits="System.Web.Mvc.ViewPage<string>" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="title" runat="server">
    OneColumn
</asp:Content>
<asp:Content ContentPlaceHolderID="body" runat="server">
    <div id="aside">
        <h1>
            <a href="/">Aish Audio</a></h1>
    </div>
    <div id="main" style="height: 1100px;">
        <div id="header">
            <% 
                Html.RenderPartial("~/Views/Account/HeaderLoginSection.ascx", new Main.Models.LogOnModel());
            %>
        </div>
        <div id="content">
            <ul id="topContent">
                <li class="first">Jewish mp3 downloads</li>
                <li>Torah audio free listening</li>
                <li>Judaism mp3s, tapes, CDs</li>
            </ul>
            <%= Model %>
        </div>
    </div>
</asp:Content>