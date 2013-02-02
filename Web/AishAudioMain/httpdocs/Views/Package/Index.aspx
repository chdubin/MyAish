<%@ Page Title="" Language="C#" MasterPageFile="../Shared/twoColumn.Master"
    Inherits="System.Web.Mvc.ViewPage<MainEntity.Models.Catalog.EntityItem[]>" %>

<%@ Import Namespace="MainCommon" %>
<%@ Import Namespace="Main.Areas.Admin.Models.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <div class="title_match">
        Packages</div>
    <%foreach (var item in this.Model)
      { %><%=Html.Partial("PackageDetail", item) %>
    <%} %>

    <script type="text/javascript">
        function AddMediaToCart(id) {
            $.ajax({
                url: '/shopping/AddItemToShoppingCart?item_type_id=<%=(int)CartItemTypeEnum.Package %>&item_id=' + id,
                success: function (data) {
                    $("#HeaderCartSectionContent").html(data);
                    alert("Product was successfully added to your shopping cart");
                }
            });
        }
    </script>

</asp:Content>
