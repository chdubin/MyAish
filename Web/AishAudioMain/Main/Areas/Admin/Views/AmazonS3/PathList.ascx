<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Main.Common.AmazonS3.S3Path[]>" %>

<script type="text/javascript">
    function AmazonS3_OnSuccessChangePath() {
        var func = '<%= ViewData["Function"] %>';
        var path = encodeURI($('#amazons3_currentpath').html());
        var frame = $('#amazons3_uploadform iframe');
        frame.attr('src','/Admin/AmazonS3/UploadFile?func='+func+'&path='+path);
    }
</script>

<ul class="side-menu">
<%foreach (var path in Model)
  { %>
  <li style="margin-left:<%=path.Level-1 %>em; border-bottom:1px dotted gray; padding-top:1ex;padding-bottom:2px;white-space:nowrap">
        <a href="/Admin/AmazonS3/FileList?path=<%=path.Path +"&func="+ViewData["Function"] %>" onclick="$('a',$(this).parent().parent()).css('font-weight','normal'); $(this).css('font-weight','bold');Sys.Mvc.AsyncHyperlink.handleClick(this, new Sys.UI.DomEvent(event), { insertionMode: Sys.Mvc.InsertionMode.replace, updateTargetId: 'amazons3_filelist',onSuccess:AmazonS3_OnSuccessChangePath});">
            <%= Html.Encode(path.Name) %></a>
  </li>
<%} %>
</ul>