<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Main.Common.AmazonS3.S3Hierarchy>" %>

<%if (Model != null)
  { %>
<table>
    <tr>
        <td class="page-title ui-widget-content ui-corner-all">
            <h1>
                Paths
            </h1>
            <div class="other" style="padding-left:1ex">
                <% Html.RenderPartial("../AmazonS3/PathList", Model.Paths, this.ViewData); %>
            </div>
        </td>
        <td class="page-title ui-widget-content ui-corner-all" style="width: 100%;border-left:0 solid white">
			<% Html.RenderAction("Files", "AmazonS3", new { path = string.Empty, func = ViewData["Function"] }); %>
        </td>
    </tr>
</table>
<%}
  else
  { %>
<h2>
    File list is empty, try later</h2>
<%} %>
<div id="amazons3_uploadform" style="margin-top:1ex"><iframe src="/Admin/AmazonS3/UploadFile?func=<%= ViewData["Function"] %>&path=/" style="height:30px;width:100%;" scrolling="no" frameborder="0" marginheight="0" marginwidth="0"></iframe></div>

<%--<table>
<tr>
<td>
<div style="white-space:nowrap">
    <a class="btn ui-state-default" href="#" onclick="$('#amazons3_uploadform').show().find('iframe').attr('src','/Admin/AmazonS3/UploadFile?func=<%= ViewData["Function"] %>&path='+encodeURI($('#amazons3_uploadto').html())+'&r='+Math.random())">
        <span class="ui-icon ui-icon-refresh"></span>Upload to <span id="amazons3_uploadto">Root</span></a>
</div>
</td>
<td>
</td>
</tr>
</table>
--%>