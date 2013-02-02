<%@ Page Title="" Language="C#" MasterPageFile="../Shared/twoColumn.Master" Inherits="System.Web.Mvc.ViewPage<Main.Models.ControllerView.Search.ClassListItem[]>" %>

<%@ Import Namespace="MainCommon" %>
<%@ Import Namespace="Main.Areas.Admin.Models.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <% foreach (var item in Model)
       {%>
	       <div class="mp3_item">
        <div class="music_icons">
        <% if (item.IsFree)
           { %>
                <a href="#" onclick="return streamPopupflex('<%= Url.Action("Class", "Search") + "?id=" + item.ClassID + "&sp=true" %>',670,400)"> 
                <!--<a target="_blank" href="javascript:void(0)" onclick="return streamPopupflex('<%= Url.Action("GetFreeStream", "Audio") + "?id=" + item.FileID %>',250,220)">-->
                        <img height="25" border="0" width="26" title="FREE Listening" src="<%= Url.Image("listening_icon-over.jpg") %>" /></a>
                <% }
           else
           { %>
                <img height="25" border="0" width="26" title="FREE Listening Not Available" src="<%= Url.Image("listening_icon.jpg") %>" />
                <% } %>
            <% if (item.FileAvailable)
               { %>
               <a href="javascript:void(0)" onclick="return AddToCart(<%= item.FileID %>, this)">
                    <img height="25" width="25" border="0" src='<%= Url.Image("arrow_icon-over.jpg") %>' title="Buy MP3 Download" alt="Buy MP3 Download" /></a>
            <% }
               else
               { %>
            <img height="25" width="25" border="0" src='<%= Url.Image("arrow_icon.jpg") %>' title="MP3 Download Not Available" />
            <% } %>
            <% if (item.TypeAvailable)
               { %>
            <a href="javascript:void(0)" onclick="return AddMediaToCart(<%= item.TypeID %>, this)">
                <img height="25" width="26" border="0" src='<%= Url.Image("tape_icon_over.jpg") %>' title="Order Tape" /></a>
            <% }
               else
               { %>
            <img height="25" width="26" border="0" src='<%= Url.Image("tape_icon.jpg") %>' title="Tape Not Available" />
            <% } %>
            <% if (item.DiscAvailable)
               { %>
            <a href="javascript:void(0)" onclick="return AddMediaToCart(<%= item.DiscID %>, this)">
                <img height="25" width="26" border="0" src='<%= Url.Image("compactDisc_icon-over.jpg") %>' title="Order CD" /></a>
            <% }
               else
               { %>
            <img height="25" width="26" border="0" src='<%= Url.Image("compactDisc_icon.jpg") %>' title="CD Not Available" />
            <% } %>
        </div>
        <div class="rightpart_mp3">
            <div class="rightpart_mp3">
                <div class="title">
                <%if(item.NewOrder!=null){ %>
                    <span style="color:Red"><b>NEW&nbsp;&nbsp;</b></span>
                <%} %>
                    <a href="#" onclick="return streamPopupflex('<%= Url.Action("Class", "Search") + "?id=" + item.ClassID + "&sp=true" %>',670,400)" class="search_title">
                        <%= Html.Encode(item.Title) %>
                    </a><span class="search_author">&nbsp;&nbsp; <%= Html.ActionLink(item.SpeakerName,"resultsdetail", "search", new {speaker = item.SpeakerName},null) %></span>
                </div>
                <div class="search_id">
                    <%= item.Code %>
                </div>
            </div>
            <div class="rightpart_mp3">
                <div class="price_and_details">
                    <% if(item.FileAvailable){ %>
                    <a href="javascript:void(0)" onclick="return AddToCart(<%= item.FileID %>, this)" class="nolinkformat">                    
                    <%} %>
                        <span class="price_prompt">mp3: </span>
                        <span class="price_value">
                            <%= MyUtils.FormatPrice(item.FilePrice1,item.FileAvailable,"$","N/A") %>
                        </span>
                    <% if (item.FileAvailable)
                       { %>
                    </a>                    
                    <%} %>
                    <% if (item.FileAvailable && (bool)ViewData["IsAuthorized"])
                       { %>
                    <a href="javascript:void(0)" onclick="return AddToCart(<%= item.FileID %>, this)" class="nolinkformat">                    
                    <%}
                       else if(item.FileAvailable)
                       { %>
                    <a href='<%= Url.Action("Register", "Account") %>' class="nolinkformat">
                    <%} %>
                        &nbsp; <span class="price_prompt">
                            Members
                        </span>
                        <span class="price_value">
                            <%= MyUtils.FormatPrice(item.FilePrice2, item.FileAvailable, "$", "N/A")%>
                        </span>
                    <% if(item.FileAvailable){%>
                    </a>
                    <%} %>
                    &nbsp;&nbsp; 
                    <% if (item.TypeAvailable)
                       { %>
                    <a href="javascript:void(0)" onclick="return AddMediaToCart(<%= item.TypeID %>, this)" class="nolinkformat">
                    <%} %>
                        <span class="price_prompt">
                            Tape:&nbsp; 
                        </span>
                        <span class="price_value">
                            <%= MyUtils.FormatPrice(item.TypePrice, item.TypeAvailable, "$", "N/A")%>
                        </span>
                    <% if (item.TypeAvailable)
                    { %>
                    </a>
                    <%} %>
                    &nbsp;&nbsp; 
                    <% if (item.DiscAvailable)
                    { %>
                    <a href="javascript:void(0)" onclick="return AddMediaToCart(<%= item.DiscID %>, this)" class="nolinkformat">
                    <%} %>
                    <span class="price_prompt">CD:&nbsp; </span><span class="price_value">
                        <%= MyUtils.FormatPrice(item.DiscPrice, item.DiscAvailable, "$", "N/A")%>
                    </span>
                    <% if (item.TypeAvailable)
                    { %>
                    </a>
                    <%} %>
                </div>
                <div class="search_details">
                    <a href="#" onclick="return streamPopupflex('<%= Url.Action("Class", "Search") + "?id=" + item.ClassID + "&sp=true" %>',670,400)">DETAILS </a>
                </div>
            </div>
        </div>
    </div>

	   <%--
		   if (ViewData.IsAuthorized())
			   Html.RenderPartial("ClassDetailAuthorized", item);
		   else
			   Html.RenderPartial("ClassDetailUnauthorized", item);
	   --%>
		   
	  <% }%>
    
    <script type="text/javascript">
    	function AddToCart(id, elem) {
    		$.ajax({
    			url: '/shopping/AddItemToShoppingCart?item_type_id=1&item_id=' + id,
    			success: function (data) {
    			    if (data != "") {
    			        $(elem).html("<img width='60' height='25' border='0' src='/Content/Default/Images/in_cart.jpg' alt='MP3 Download Already In Cart' />");
    				    $("#HeaderCartSectionContent").html(data);
    				    alert("Product was successfully added to your shopping cart");
    				} else {
    				    alert("This lecture already exists in your library.");
    				}
    }
    		});

    		return false;
    	}

    	function AddMediaToCart(id, elem) {
    		$.ajax({
    			url: '/shopping/AddItemToShoppingCart?item_type_id=1&item_id=' + id,
    			success: function (data) {
    				//$(elem).children().eq(0).unwrap();
    				//$(elem).after($(elem).text()).remove();                    

    				// Добавление обновленной инфы в шапку
    				$("#HeaderCartSectionContent").html(data);
    				alert("Product was successfully added to your shopping cart");
    			}
    		});
    		return false;
    	}

    	function streamPopupflex(page, width, height) {
    		self.name = "TMmain";
    		options = "toolbar=0,status=0,menubar=0,scrollbars=0,resizable=0,width=" + width + ",height=" + height;
    		window.open(page, "TMPlayer", options);

    		return false;
    	}

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
</asp:Content>
