<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>UploadFile</title>
    <link href="<%= Url.Css("style.css") %>" rel="stylesheet" media="all" />
    <script src="<%= Url.JavaScript("jquery-1.4.1.min.js") %>" type="text/javascript"></script>
</head>
<body>
<%if (ViewData["UploadComplete"] == null)
  { %>
<form action="/Admin/AmazonS3/UploadFile" enctype="multipart/form-data" method="post">
<table><tr>
<td style="white-space:nowrap;color:Gray">Upload to: </td>
<td style="white-space:nowrap;">
    <%=Html.TextBox("PathFile", ViewData["Path"], new { @class = "field text", style = "display:none;width:"+(((string)ViewData["Path"]).Length+10)*10+"px" })%>
    <span style="padding-right:1ex" onclick="$(this).hide().parent().find('input').show();" ><%=ViewData["Path"]%></span>
    </td>
<td style="width:100%">
    <input type="file" name="UploadFile" onchange="OnFileSelect(this)" class="field file" />
    <input type="hidden" name="FunctionName" value="<%=ViewData["FunctionName"] %>" />
</td>
</tr></table>
</form>
<div style="display:none">
File uploading
</div>
<script type="text/javascript">

    function OnFileSelect(file) {
        var uploadInputFile = $(file);
        var uploadForm = $("form");
        if (uploadInputFile.attr("value") != null && uploadInputFile.attr("value") != "") {
            uploadForm.hide();
            $("div").show();
            uploadForm.submit();
        }
    }

</script>
<%}else{ %>
<script type="text/javascript">
    window.parent.<%= ViewData["FunctionName"] %>('<%=ViewData["Path"] %>');
</script>
<%} %>
</body>
</html>
