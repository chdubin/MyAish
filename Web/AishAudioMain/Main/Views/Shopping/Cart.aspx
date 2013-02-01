<%@ Page Title="" Language="C#" MasterPageFile="../Shared/twoColumn.Master" Inherits="System.Web.Mvc.ViewPage<Main.Models.Shopping.CartModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <link href="<%= Url.Css("cartstyles.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">

<script type="text/javascript">

    function toggleDetails(toggleval) {
        if (document.getElementById) { // DOM3 = IE5, NS6 
            document.getElementById('membershipinfotoggle').style.visibility = toggleval;
        } else {
            if (document.layers) { // Netscape 4 
                document.membershipinfotoggle.visibility = toggleval;
            }
            else { // IE 4 
                document.all.membershipinfotoggle.style.visibility = toggleval;
            }
        }
    }

    function AddToCartMonthlyMembership() {
        var url = '<%= Url.Action("AddItemToShoppingCart", "Shopping", new {item_id = ConfigurationManager.AppSettings["MonthlyMembershipSubscribeID"], item_type_id = MainCommon.CartItemTypeEnum.Subscribe }) %>';

        $.ajax({
            url: url,
            success: function (data) {
                window.location.reload();
            }
        });
    }

</script>  
 



<div class="blueheaderbackground" style="margin-bottom:4px;">
    <img height="36" width="549" alt="Shopping Cart" src='<%= Url.Image("shopping_cart.jpg") %>' />
</div>
	

<% if (Model.ProductList1.Length>0||Model.ProductList2.Length>0||Model.ProductSubscribePlan!=null || Model.ProductUnits!=null)
   {       
       using (Html.BeginForm("UpdateQuantity", "Shopping", FormMethod.Post,
                    new { id = "class_products_form", enctype = "multipart/form-data" }))
            { %>

<table cellspacing="0" cellpadding="0" border="0" width="100%">
    <tr>
	    <td>
    
        <% if (Model.ProductList1.Length > 0 || Model.ProductUnits!=null || Model.ProductSubscribePlan!=null){ %>
            <table width="100%" class="in-cart-table">
                <tr>
     	            <td width="0" class="cartheadinglefttd">
                        <img height="1" width="1" src='<%= Url.Image("spacer.gif") %>' alt="" />
                    </td>
			        <td class="cartheadingtd">Title</td>
			        <td class="cartheadingspacertd">&nbsp;</td>
			        <td class="cartheadingtd">Type</td>
			        <td class="cartheadingspacertd">&nbsp;</td>
			        <td class="cartheadingtd">Qty.&nbsp;</td>
			        <td class="cartheadingspacertd">&nbsp;</td> 
			        <td align="right" class="cartheadingspacertd">
			            Price			
                    </td>
			        <td class="cartheadingtd">&nbsp;</td>
			        <td class="cartheadingspacertd">&nbsp;</td>
			        <td class="cartheadingtd">Total&nbsp;</td>
			        <td class="cartheadingspacertd">&nbsp;</td>
			        <td class="cartheadingspacertd">&nbsp;</td>
			        <td class="cartheadingspacertd">&nbsp;</td>
     	        </tr>
			    <tr>
                    <td colspan="14">    
                    </td>
                </tr>
                			
                <% foreach (var product in Model.ProductList1)
                   { %>
                
          	    <tr class="cartitem">
               	    <td class="cartitemtd"></td>
     			    <td width="230" class="cartitemitemnolink">
     			        <a href="#" class="cartitemboldtd">
                            <%= product.Title.Replace("[Tape] ", "").Replace("[Disk] ", "").Replace("[File/Small Poster] ", "").Replace("[File] ", "") %>
                        </a>
                    </td>
     			    <td class="cartitemtd">&nbsp;</td>
     			    <td class="cartitemtd">                        
                        <%= ((MainCommon.ProductTypeEnum)product.ProductTypeID).ToString()%>
                    </td>
     			    <td class="cartitemtd">&nbsp;</td>
     			    <td class="cartitemtd">
                        <% if (product.ProductTypeID == (short)MainCommon.ProductTypeEnum.File)
                           { %>
                           N/A
                        <% }
                           else
                           { %>
                            <%= Html.TextBox("quantity", product.cnt, new { @class = "quantity-val", size = "3" })%>
     			            <%= Html.Hidden("id", product.entityID)%>
                        <%} %>
                    </td>
     			    <td class="cartitemtd">&nbsp;</td>
     			    <td align="right" class="cartitemtd">
                        <%if (product.Price1WithoutDiscount != product.price1)
                          { %>
                           <strike style="color:#BF203A;">
                                $<%= (product.Price1WithoutDiscount / product.cnt).ToString("N2")%>                                
                            </strike>
                            <div style="padding-top:4px;color:#BF203A;">
                                $<%= (product.price1 / product.cnt).ToString("N2") %>
                            </div>
                        <%} else{%>
                            $<%= (product.price1 / product.cnt).ToString("N2")%>
                        <%} %>
                    </td>
				    <td class="cartitemtd">&nbsp;</td>
     			    <td class="cartitemtd">&nbsp;</td>
     			    <td align="right" class="cartitemtd">
                        $<%= product.price1.ToString("N2")%>
                    </td>
     			    <td class="cartitemtd">&nbsp;</td>
     			    <td width="50" class="cartitemtd">
                        <a class="cartitemtd" onclick="return confirm('Are you sure you want to remove <%= product.Title.Replace("[Tape] ", "").Replace("[Disk] ", "").Replace("[File/Small Poster] ", "").Replace("[File] ", "") %> from your cart?')" href="<%= Url.Action("DeleteItemFromCart","Shopping", new { item_id = product.entityID, item_type_id = product.ProductTypeID == (short)MainCommon.ProductTypeEnum.Package ? MainCommon.CartItemTypeEnum.Package : MainCommon.CartItemTypeEnum.Class  }) %>">Remove</a>
                    </td>
     			    <td class="cartitemtd">&nbsp;</td>
                </tr>
							
          	    <tr>
                    <td height="2" colspan="14">
                        <div class="lightgrayline" style="height:1px;">
                            <img height="1" width="1" src='<%= Url.Image("spacer.gif") %>' alt="" />
                        </div>
                    </td>
                </tr>     		     		            
     		    <% } %>

                <% if(Model.ProductSubscribePlan != null)
                   { %>
                
          	    <tr class="cartitem">
               	    <td class="cartitemtd"></td>
     			    <td width="230" class="cartitemitemnolink">
     			        <a href="#" class="cartitemboldtd">
                            <%= Model.ProductSubscribePlan.Title%>
                        </a>
                    </td>
     			    <td class="cartitemtd">&nbsp;</td>
     			    <td class="cartitemtd">                        
                        Item
                    </td>
     			    <td class="cartitemtd">&nbsp;</td>
     			    <td class="cartitemtd">
                        1
                    </td>
     			    <td class="cartitemtd">&nbsp;</td>
     			    <td align="right" class="cartitemtd">
     			        $<%= Model.ProductSubscribePlan.price1.ToString("N2")%>
                    </td>
				    <td class="cartitemtd">&nbsp;</td>
     			    <td class="cartitemtd">&nbsp;</td>
     			    <td align="right" class="cartitemtd">
                        $<%= Model.ProductSubscribePlan.price1.ToString("N2")%>
                    </td>
     			    <td class="cartitemtd">&nbsp;</td>
     			    <td width="50" class="cartitemtd">
                        <a class="cartitemtd" onclick="return confirm('Are you sure you want to remove <%=Model.ProductSubscribePlan.Title %> from your cart?')" href="<%= Url.Action("DeleteItemFromCart","Shopping", new { item_id = Model.ProductSubscribePlan.entityID, item_type_id = MainCommon.CartItemTypeEnum.Subscribe }) %>">Remove</a>
                    </td>
     			    <td class="cartitemtd">&nbsp;</td>
                </tr>
                    
                    <% if (Model.ProductUnits != null){ %>
          	            <tr>
                            <td height="2" colspan="14">
                                <div class="lightgrayline" style="height:1px;">
                                    <img height="1" width="1" src='<%= Url.Image("spacer.gif") %>' alt="" />
                                </div>
                            </td>
                        </tr>     		     		            
                    <% } %>
		     		            
     		    <% } %>

                <% if (Model.DoSubscribe || Model.IsSubscriber)
                   { %>
                
          	    <tr class="cartitem">
               	    <td class="cartitemtd"></td>
     			    <td width="230" class="cartitemitemnolink">
     			        UNITS
                    </td>
     			    <td class="cartitemtd">&nbsp;</td>
     			    <td class="cartitemtd">                        
                        Units
                    </td>
     			    <td class="cartitemtd">&nbsp;</td>
     			    <td class="cartitemtd">
<%--                        <%=Html.DropDownList("units_quantity", new SelectList((IEnumerable<KeyValuePair<long, string>>)ViewData["UnitsQuantity"], "Key", "Value", Model.ProductUnits!=null?Model.ProductUnits.entityID:0),
                            null, new { @class = "field select full", style = "float:left;" })%>                        
--%>                <%= Model.ProductUnits != null ? Model.ProductUnits.Title : "0" %>
                    </td>
     			    <td class="cartitemtd">&nbsp;</td>
     			    <td align="right" class="cartitemtd">
     			        $<%= Model.ProductUnits!=null?(Model.ProductUnits.price1 / Model.ProductUnits.price2).ToString("N2"):"0"%>
                    </td>
				    <td class="cartitemtd">&nbsp;</td>
     			    <td class="cartitemtd">&nbsp;</td>
     			    <td align="right" class="cartitemtd">
                        $<%= Model.ProductUnits!=null?Model.ProductUnits.price1.ToString("N2"):"0"%>
                    </td>
     			    <td class="cartitemtd">&nbsp;</td>
     			    <td width="50" class="cartitemtd">
                        <%if (Model.ProductUnits != null)
                          { %>
                        <a class="cartitemtd" onclick='<%= "return confirm('Are you sure you want to remove &quot;UNITS&quot; from your cart?')" %>' href="<%= Url.Action("DeleteUnitsFromCart","Shopping", new { units_are_needed_for_purchase = Model.ProductUnits.entityID }) %>">Remove</a>
                        <%} %>
                    </td>
     			    <td class="cartitemtd">&nbsp;</td>
                </tr>
		     		            
     		    <% } %>


			    <tr>
                    <td colspan="14">    
                    </td>
                </tr>
                <tr class="cartheading">
                    <td colspan="6" class="cartheadinglefttd">
                        <img height="1" width="1" src='<%= Url.Image("spacer.gif") %>' alt="" />
                    </td>
		            <td align="right" colspan="5" class="cartheadingtd">
		                Subtotal:&nbsp;
                    </td>
		            <td align="right" colspan="2" class="cartheadingspacertd">
			            $<%= Model.Amount.ToString("N2")%>
                    </td>
		            <td class="cartheadingrighttd">&nbsp;</td>
                </tr>

            </table>

            <%} %>

         </td>
    </tr>

    <% if(Model.ProductList2.Length>0){ %>
    <tr>
        <td style="padding:25px 0px 5px 5px;font-weight:bold;">
            <div style="float:left;">
                UNIT Purchases
            </div>
            <div style="float:right;">
                Units before purchase(s): <%= Model.Balance.ToString("0.##") %>
            </div>
            <div style="clear:both;"></div>
        </td>
    </tr>
    <tr>
        <td>
        
          <table width="100%" class="in-cart-table">
                <tr>
     	            <td width="0" class="cartheadinglefttd">
                        <img height="1" width="1" src='<%= Url.Image("spacer.gif") %>' alt="" />
                    </td>
			        <td class="cartheadingtd">Title</td>
			        <td class="cartheadingspacertd">&nbsp;</td>
			        <td class="cartheadingtd">Type</td>
			        <td class="cartheadingspacertd">&nbsp;</td>			        
			        <td align="right" class="cartheadingspacertd">
			            Units
                    </td>
			        <td class="cartheadingtd">Total&nbsp;</td>
			        <td class="cartheadingspacertd">&nbsp;</td>
			        <td class="cartheadingspacertd">&nbsp;</td>
			        <td class="cartheadingspacertd">&nbsp;</td>
     	        </tr>
			    <tr>
                    <td colspan="10">    
                    </td>
                </tr>
                <% 
                    var i = 0;
                    foreach (var product in Model.ProductList2)
                    {
                        i++;%>
                
          	    <tr class="cartitem">
               	    <td class="cartitemtd"></td>
     			    <td width="230" class="cartitemitemnolink">
     			        <a href="#" class="cartitemboldtd">
                            <%= product.Title.Replace("[Tape] ", "").Replace("[Disk] ", "").Replace("[File/Small Poster] ", "").Replace("[File] ", "") %>
                        </a>
                    </td>
     			    <td class="cartitemtd">&nbsp;</td>
     			    <td class="cartitemtd">                        
                        <%= ((MainCommon.ProductTypeEnum)product.ProductTypeID).ToString()%>
                    </td>
     			    <td class="cartitemtd">&nbsp;</td>
     			    <td align="right" class="cartitemtd">
     			        <%= product.price2.ToString("0.##")%>
                    </td>
				    <td class="cartitemtd">&nbsp;</td>
     			    <td class="cartitemtd">&nbsp;</td>
     			    <td width="50" class="cartitemtd">
                        <a class="cartitemtd" onclick="return confirm('Are you sure you want to remove <%= product.Title.Replace("[Tape] ", "").Replace("[Disk] ", "").Replace("[File/Small Poster] ", "").Replace("[File] ", "") %> from your cart?')" href="<%= Url.Action("DeleteItemFromCart","Shopping", new { item_id = product.entityID, item_type_id = MainCommon.CartItemTypeEnum.Class  }) %>">Remove</a>
                    </td>
     			    <td class="cartitemtd">&nbsp;</td>
                </tr>
							
                        <% if (i != Model.ProductList2.Length)
                                       { %>
          	            <tr>
                            <td height="2" colspan="10">
                                <div class="lightgrayline" style="height:1px;">
                                    <img height="1" width="1" src='<%= Url.Image("spacer.gif") %>' alt="" />
                                </div>
                            </td>
                        </tr>

                        <%}
                    } %>

			    <tr>
                    <td colspan="10">    
                    </td>
                </tr>
                <tr class="cartheading">
                    <td colspan="5" class="cartheadinglefttd">
                        <img height="1" width="1" src='<%= Url.Image("spacer.gif") %>' alt="" />
                    </td>
		            <td align="right" colspan="2" class="cartheadingtd">
		                Total Units:&nbsp;
                    </td>
		            <td align="right" colspan="2" class="cartheadingspacertd">
			            <%= Model.ProductList2.Select(s => s.price2).Sum().ToString("0.##")%>
                    </td>
		            <td class="cartheadingrighttd">&nbsp;</td>
                </tr>


            </table>        
        </td>    
    </tr>
    <%} %>

    <tr>
	    <td style="padding:35px 0px 20px 0px;text-align:right;">
            <span style="white-space:nowrap;padding-left:10px;">
                <a class="cartbottomlinksred" href="javascript:history.go(-1);">Continue Shopping</a>
            </span>
            <span style="white-space:nowrap;padding-left:10px;">    
                <a class="cartbottomlinks" onclick="return confirm('Are you sure you want to empty your cart?')" href="<%= Url.Action("EmptyCart", "Shopping") %>">Empty Cart</a>
            </span>
            <span style="white-space:nowrap;padding-left:10px;">
                <a class="cartbottomlinks" onclick="$('#class_products_form').submit(); return false;" href="javascript:void(0)">Update Quantity</a>
            </span>
            <span style="white-space:nowrap;padding-left:10px;font-size:14px;">
                <a class="cartbottomlinksred" href="<%= Url.Action("Checkout", "Account") %>">Checkout</a>
            </span>
	    </td>
    </tr>

    <% if (!Model.DoSubscribe && !Model.IsSubscriber)
       { %>

    <tr>
        <td align="right">
            <img height="66" border="0" width="442" usemap="#Map" src='<%= Url.Image("save_big.jpg") %>' />
            <map id="Map" name="Map">
                <area onmouseout="toggleDetails('hidden')" onmouseover="toggleDetails('visible')" alt="Details" href="javascript:void(0)" coords="1,37,83,60" shape="rect" />
                <area alt="Add to Cart" coords="382,29,430,66" onclick="AddToCartMonthlyMembership()" href="javascript:void(0)" shape="rect" />                
            </map>
        </td>
    </tr>

    <% } %>

</table>

  

<div id="membershipinfotoggle" class="membershipinfotoggle" style="visibility: hidden;"></div>

<%} }else{ %>

<div style="font-weight:bold; padding:10px;">
    Your cart is empty! 
</div>

<% } %>


</asp:Content>

