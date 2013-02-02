<%@ Page Title="" Language="C#" MasterPageFile="../Shared/twoColumn.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">

    <div align="center">
        <p>
            <img src='<%= Url.Image("offers_2.jpg") %>' height="432" usemap="#select" alt="Offers" width="626" />
            <map id="select" name="select">              
                <area href='<%= Url.RouteUrl("FreeMP3") %>' coords="520,228,577,253" shape="rect" />
                <area href='<%= Url.Action("Checkout", "Account", new { email = HttpUtility.UrlDecode(HttpContext.Current.Request.QueryString["email"]), add_monthly_membership = true })%>' coords="521,137,579,160" shape="rect" />
                <area href='<%= Url.RouteUrl("FreeMP3") %>' coords="372,230,491,250" shape="rect" />
            </map>
        </p>
    </div>

</asp:Content>

