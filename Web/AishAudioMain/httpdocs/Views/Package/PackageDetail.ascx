<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MainEntity.Models.Catalog.EntityItem>" %>

<%@ Import Namespace="MainCommon" %>
<%@ Import Namespace="Main.Areas.Admin.Models.Common" %>
<div class="big_item">
        <img alt="" src='<%= Url.Image("search_grey_line.jpg") %>' style="display: block; float: left;
            margin-bottom: 18px;" />
        <div>
            <div class="left_img">
                <div class="search_big_img">
                        <% if (Model.FileEntity != null)
                           { %><img border="0" alt="<%= Html.AttributeEncode(Model.title)  %>" src="<%= ResolveClientUrl(MainCommon.MyUtils.GetImageUrl(ConfigurationManager.AppSettings["UploadClassImgFolderName"], Model.FileEntity.filePath)) %>" /><%
                           } %>
                </div>
            </div>
            <div class="right_info">
                <div class="title_info">
                    <span class="search_title" onclick="return AddMediaToCart(<%= Model.entityID %>)" style="cursor: pointer"><%=Html.Encode(Model.title) %></span>
                </div>
                <div class="search_id full_id">
                    <%= Html.Encode(Model.CatalogItemExtend.code) %>
                </div>
                <div class="search_synopsis">
                    <%= Model.ProductEntity.description %>
                </div>
                <span class="search_synopsis" style="margin-top: 3px; margin-bottom: 0px;">
				<%= Model.ProductEntity.ShippingLocationTitle %> </span>
            </div>
        </div>
    </div>