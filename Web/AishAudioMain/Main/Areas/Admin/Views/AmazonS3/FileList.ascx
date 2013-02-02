<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Main.Common.AmazonS3.S3File[]>" %>
<%
	var start = (int?)ViewData["Start"] ?? 0;
	var count = (int?)ViewData["Count"] ?? 2;
	var pageNum = start / count;
	var visiblePageNum = Math.Max(pageNum - 3, 0);
	var endPage = Model.Length / count;
	var tatalPages = Math.Min(visiblePageNum + 7, endPage);
 %>
<script type="text/javascript">
    function AmazonS3_Find(target) {
        var element = $(target);
        element.attr("href", element.attr("href") + "&contains_with=" + encodeURI(element.parent().find("input").attr("value")) + "&start=0&count=" + <%=count %>);
        Sys.Mvc.AsyncHyperlink.handleClick(target, new Sys.UI.DomEvent(event), {
            insertionMode: Sys.Mvc.InsertionMode.replace,
            updateTargetId: 'amazons3_filelist',
            onSuccess: AmazonS3_OnSuccessChangePath
        });
          }
          function AmazonS3_ChangePage(target, start) {
          	var element = $(target);
			element.attr("href", element.attr("href") + "&start=" + start + "&count=" + <%=count %>);
          	Sys.Mvc.AsyncHyperlink.handleClick(target, new Sys.UI.DomEvent(event), {
          		insertionMode: Sys.Mvc.InsertionMode.replace,
          		updateTargetId: 'amazons3_filelist',
          		onSuccess: AmazonS3_OnSuccessChangePath
          	});
          }
</script>
<div id="amazons3_filelist">
    <h1>
        Files in <span id="amazons3_currentpath">
            <%= (string)ViewData["Path"]??"Root" %></span></h1>
    <div class="other">
    <div class="hastable">
        <table>
            <thead>
                <tr>
                    <td style="text-align:left">
                        <div class="float-left" style="padding-right:1ex">File name</div>
                        <div class="float-left"><input type="text" class="field text" style="height:12px;font-size:10px" value="<%=Url.Encode((string)ViewData["ContainsWith"]) %>" /></div>
                        <a class="btn ui-state-default ui-corner-all" style="margin:7px 0 0 10px" href="/Admin/AmazonS3/FileList?path=<%= Url.Encode((string)ViewData["Path"]??"") +"&func="+Url.Encode((string)ViewData["Function"]) %>" onclick="AmazonS3_Find(this)">
                            <span class="ui-icon ui-icon-search"></span>Search</a>
                    </td>
                    <td>
                        Size
                    </td>
                </tr>
                            </thead>
    <%if (Model != null & Model.Length > 0)
      {
		  %>
				
                <%foreach (var file in Model.Skip(start).Take(count))
                  { %>
                <tr>
                    <td style="width:100%">
                        <a href="#" onclick="<%=ViewData["Function"] + "('" + file.Path + file.Name +"')" %>;return false">
                            <%if (string.IsNullOrEmpty((string)ViewData["ContainsWith"]))
                              { %>
                                <%=Html.Encode(file.Name)%>
                            <%}
                              else
                              { %>
                              <%=Html.Encode(file.Name).Replace((string)ViewData["ContainsWith"], "<span style='color:red'>" + ViewData["ContainsWith"] + "</span>")%>
                            <%} %>
                            </a>
                    </td>
                    <td>
                        <%=file.Size %>
                    </td>
                </tr>
                    <%} %>
					<tr>
					<td colspan="2">
					<a style="margin-right:10px" href="/Admin/AmazonS3/FileList?path=<%= Url.Encode((string)ViewData["Path"]??"") +"&func="+Url.Encode((string)ViewData["Function"]) %>" onclick="AmazonS3_ChangePage(this,0)">&lt;&lt;</a>
					<%for (int i = visiblePageNum; i < tatalPages; i++)
	   {
		   if (i == pageNum)
		   {
			   %>
			   <b style="margin-right:10px"><%=i+1%></b>
			   <%
		   }
		   else
		   {
			   %>
			   <a style="margin-right:10px" href="/Admin/AmazonS3/FileList?path=<%= Url.Encode((string)ViewData["Path"]??"") +"&func="+Url.Encode((string)ViewData["Function"]) %>" onclick="AmazonS3_ChangePage(this,<%=i*count %>)">
			   <%=i+1 %>
			   </a>
			   <%
		   }
	   } %>
	   <a style="margin-right:10px" href="/Admin/AmazonS3/FileList?path=<%= Url.Encode((string)ViewData["Path"]??"") +"&func="+Url.Encode((string)ViewData["Function"]) %>" onclick="AmazonS3_ChangePage(this,<%=(endPage-1)*count %>)">&gt;&gt;</a>
						
					</td>
					</tr>
<%} %>
        </table>
    </div></div>
</div>
