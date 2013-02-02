<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/twocolumns.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Fixed files</h1>
    <%var fixedFiles = (List<KeyValuePair<string,string>>)this.ViewData["FixedFiles"];
        foreach (var fixedfile in fixedFiles) {%>
        <div>file path: <b><%= fixedfile.Key%></b>, fixed: <%=fixedfile.Value%></div>
        <%} %>
        <h1>Problem files</h1>
    <%
        var problemFiles = (List<Main.Areas.Admin.Models.ControllerView.FixAmazonFile>)this.ViewData["ProblemFiles"];
        foreach (var problem in problemFiles)
        {
            var id = (problem.Alternate ? "alternateFilePath_" : "filePath_") + problem.FileID;
            %>
    <div style="margin-top:1em">class id: <b><%=problem.ClassID%></b>, 
    <span style="white-space:nowrap">title: <b><%=problem.Title %></b>,</span>
    <span style="white-space:nowrap"><b><%= problem.Alternate?"HI":"LO" %></b>,</span>
    <span style="white-space:nowrap">file: <b><%=problem.FilePath %></b></span><br />
    <span style="white-space:nowrap"><%= Html.TextBox("amazonFilePath", problem.FilePath, new { id = id, @readonly = "readonly", @class = "field text small df_HQ" })%>
        <a class="btn ui-state-default" href="javascript:void(0);" onclick="amazonSelect('<%= id%>');" style="display:inline; float:none">
            <span class="ui-icon ui-icon-circle-plus"></span>Upload</a>,
    finded: <%=problem.Files.Length %>
    <%=Html.ActionLink("edit","EditClass", "Catalog", new {catalog_item_id=problem.ClassID},null) %></span></div>
    <%} %>


<div style="dysplay: none;">
    <div id="amazons3_selectfile">
        <div id="page-wrapper">
            <div id="main-wrapper">
                <div id="main-content" style="width: 95%">
                    <% var data = new ViewDataDictionary(); data["Function"] = "amazonSuccess";  %>
                    <% Html.RenderPartial("../AmazonS3/SelectFile", Main.MvcApplication.S3Amazon.Hierarchy, data); %>
                </div>
            </div>
        </div>
    </div>
</div>

    <script type="text/javascript">
        $(function () {
            $('#amazons3_selectfile').dialog({
                autoOpen: false,
                modal: true,
                width: 600,
                buttons: {
                    /* 'Ok': function () {
                    var name = $('#createSpeakerD #speakerName').val();
                    $.post('/Admin/Speaker/Create', { name: name }, speakerCreated, 'json');
                    },*/
                    'Cancel': function () {
                        $(this).dialog('close');
                    }
                }
            });
        });


        var ChangeAmazonFilePathObj = null;
        function amazonSelect(target_id) {
            ChangeAmazonFilePathObj = $('#' + target_id);
            $('#amazons3_selectfile').dialog('open')
        }
        function amazonSuccess(path) {
            ChangeAmazonFilePathObj.val(path);
            $('#amazons3_selectfile').dialog('close');

            var parts = ChangeAmazonFilePathObj.attr('id').split('_');
            var url = '<%=Url.Action("ChangeFilePath")%>?file_id=' + parts[1] + '&path=' + encodeURIComponent(path) + '&alternate=' + (parts[0] == 'alternateFilePath' ? 'true' : 'false');
            $.ajax({
                url:url,
                success: function(){
                    ChangeAmazonFilePathObj.parent().parent().css('text-decoration', 'line-through');
                }
            });
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainHeader" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="rightTop" runat="server">
</asp:Content>
