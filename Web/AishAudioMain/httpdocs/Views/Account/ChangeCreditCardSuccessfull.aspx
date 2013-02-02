<%@ Page Title="" Language="C#" MasterPageFile="../Shared/twoColumn.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Body" runat="server">

        <div class="response-msg success ui-corner-all">
            <h1>Credit Card has been successfully updated.</h1>
        </div>
        <p>Go to <%=Html.ActionLink("My Account", "")%></p>
</asp:Content>

