<%@ Page Title="" Language="C#" MasterPageFile="../Shared/twoColumn.Master"
    Inherits="System.Web.Mvc.ViewPage<Main.Models.Account.ChangeCreditCard>" %>

<asp:Content ID="Content4" ContentPlaceHolderID="head" runat="server">
    <link href="<%= Url.Css("custom.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="Body" runat="server">
<%= Html.Partial("ChangeCreditCardForm", this.Model) %>
</asp:Content>
