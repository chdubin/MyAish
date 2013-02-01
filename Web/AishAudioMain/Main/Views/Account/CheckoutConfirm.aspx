<%@ Page Title="" Language="C#" MasterPageFile="../Shared/oneColumnEmpty.Master" Inherits="System.Web.Mvc.ViewPage<Main.Models.ControllerView.Account.DetailShoppingTransactions>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
<div class="maintitle" style="padding-bottom:15px;">
    <img height="60" width="775" src='<%= Url.Image("purchasing.jpg") %>' />
</div>

<%=Html.Partial("DetailShopping",Model) %>

<div style="padding: 20px 0;">
    <p style="padding: 20px;color:#B94A48;padding: 8px 35px 8px 14px;
margin-bottom: 18px;
text-shadow: 0 1px 0 rgba(255, 255, 255, 0.5);
background-color: #FCF8E3;
border: 1px solid #FBEED5;
-webkit-border-radius: 4px;
-moz-border-radius: 4px;
border-radius: 4px;
border-image: initial;margin-bottom:10px;">
        <b>Please review your order and click Finalize button below.</b><br /><br />
        <b>Please note:</b> After you click Finalize button, it may take up to 2 minutes to process your credit card information. 
     </p>

    <a href="<%= Url.Action("CheckoutComplete") %>"><img src='<%= Url.Image("finalize_large.jpg") %>' /></a>
    <%--<%= Html.ActionLink("Cancel", "Cart", "Shopping")%>--%>
</div>

<style type="text/css">
 .orders table td
 {
    background-color:#99ccff;
    padding:5px;
    border: 2px solid #ffffff;
 }
 .orders table table td
 {
    border: 0px;
 }
 .orders table th
 {
    padding:5px;
    font-weight:bold;
 }
 .orders table td b
{
    font-size:12px;
}
.orders table td
{
    font-size:11px;
}
 </style>
</asp:Content>
