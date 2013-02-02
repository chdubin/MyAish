<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div id="HeaderCartSectionContent">
<% if (!(ViewData["NotShowCart"] != null && (bool)ViewData["NotShowCart"]) && !(bool)(ViewData["IsCartEmpty"] ?? false)){

    var cartContentTitles = ((string)ViewData["CartContentTitles"] ?? ""); 
    if(!string.IsNullOrEmpty(cartContentTitles))
    {
%>
<div class="cartsection">     
    <div style="height:56px;width:217px;">
        <div style="float: left; width: 148px; height: 34px; padding: 1px; margin: 10px 0px 5px 8px; overflow: auto; font-size: 9px; line-height: 110%;">
            <div style="padding-bottom:3px;">
                <a href='<%= Url.Action("Cart", "Shopping") %>'>
                    SHOPPING CART:
                </a>
            </div>
            <%= cartContentTitles %>
        </div>
        <div style="float:right;">        
            <a href='<%= Url.Action("Cart", "Shopping") %>'>
                <img height="53" border="0" align="top" width="58" src='<%= Url.Image("cart_right.jpg") %>' alt="cart" />
            </a>    
        </div>
        <div style="clear:both;"></div>
    </div>
</div>

<% }
} %>

</div>
