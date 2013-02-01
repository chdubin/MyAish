<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Main.Areas.Admin.Models.ControllerView.Catalog.CreateClass>" %>
<script type="text/javascript">

    $(document).ready(function () {
        InitializeEditCategories("edit-class-form-categories");
    });

    function InitializeEditCategories(id) {
        var autocompleeteUrl = '<%= Url.Action("GetCategories") %>';
        var data = $("#"+id).val().split('|');
        var prePopulateData = [];
        for (var i in data) {
            if (data[i] != "")
                prePopulateData[i] = { 'name': data[i] };
        }

        $("#"+id).tokenInput(autocompleeteUrl, {

            noResultsText: "No results",
            searchingText: "Searching...",

            classes: {
                tokenList: "token-input-list-facebook",
                token: "token-input-token-facebook",
                tokenDelete: "token-input-delete-token-facebook",
                selectedToken: "token-input-selected-token-facebook",
                highlightedToken: "token-input-highlighted-token-facebook",
                dropdown: "token-input-dropdown-facebook",
                dropdownItem: "token-input-dropdown-item-facebook",
                dropdownItem2: "token-input-dropdown-item2-facebook",
                selectedDropdownItem: "token-input-selected-dropdown-item-facebook",
                inputToken: "token-input-input-token-facebook"
            },

            prePopulate: prePopulateData

        });

    }

</script>
<% bool isSuperUser = (bool)(ViewData["isSuperUser"] ?? false);
   if (Model.ClassID != 0)
   { %>
<%= Html.HiddenFor(m => m.ClassID) %>
<% } %>
<ul>
    <% if (isSuperUser)
       { %>
    <li><span>
        <label class="desc">
            Active
        </label>
        <%= Html.CheckBoxFor(model => model.Active)%>
    </span></li>
    <% }
       else
       { %>
    <li><span style="width: 50px">
        <label class="desc">
            Visible
        </label>
        <%= Html.CheckBoxFor(m => m.IsVisible) %>
    </span><span style="width: 50px">
        <label class="desc">
            Free
        </label>
        <%= Html.CheckBoxFor(m => m.IsFree) %>
    </span><span>
        <label class="desc">
            Is Free offer
        </label>
        <%= Html.CheckBoxFor(m => m.IsFreeOffer) %>
    </span><span>
        <label class="desc">
            Is Full Free
        </label>
        <%= Html.CheckBoxFor(m => m.IsFullFree) %>
    </span></li>
    <% } %>
    <li>
        <label class="desc" for="Title">
            Title</label>
        <div>
            <%= Html.TextBoxFor(model => model.Title, new { @class = "field text large" }) %></div>
    </li>
    <li>
        <label class="desc" for="SpeakerID">
            Speaker</label>
        <div>
            <%=Html.DropDownListFor(m => m.SpeakerID, Model.Speakers, new { @class = "field select large" })%>
        </div>
        <div>
            <a class="btn ui-state-default" href="javascript:void(0);" onclick="$('#createSpeakerD').dialog('open');return false;">
                <span class="ui-icon ui-icon-circle-plus"></span>Create speaker</a>
            <%--
            <button id="createSpeaker" type="button" class="ui-state-default ui-corner-all">Create speaker</button>
            --%>
        </div>
    </li>
    <li>
        <label class="desc" for="Description">
            Description</label>
        <div>
            <%= Html.TextAreaFor(model => model.Description, new { @class = "field textarea large", id = "description" })%>
        </div>
    </li>
    <li>
        <label class="desc" for="Categories">
            Categories</label>
        <div>
            <%= Html.TextBoxFor(model => model.Categories, new { @class = "field text large", id = "edit-class-form-categories"} )%>
        </div>
    </li>
    <li class="date">
        <label class="desc" id="title2" for="Field2">
            Duration
        </label>
        <span>
            <%= Html.TextBoxFor(model => model.Hour, new { @class = "field text", size = 2, maxlength = 2 }) %>
            <label>
                hour</label>
        </span><span class="symbol">:</span> <span>
            <%= Html.TextBoxFor(model => model.Min, new { @class = "field text", size = 2, maxlength = 2 }) %>
            <label>
                min</label>
        </span></li>
    <li>
        <div class="two-column">
            <div class="column">
                <label class="desc" for="Code">
                    Code</label>
                <div>
                    <%= Html.TextBoxFor(model => model.Code, new { @class = "field text large"} )%>
                </div>
            </div>
            <div class="column">
                <label class="desc" for="NewOrder">
                    New (empty or 1 to 10)</label>
                <div>
                    <%= Html.TextBoxFor(model => model.NewOrder, new { @class = "field text large"} )%>
                </div>
            </div>
        </div>
    </li>
    <li>
        <label class="desc" for="Notes">
            Notes</label>
        <div>
            <%= Html.TextAreaFor(model => model.Notes, new { @class = "field textarea normal" })%>
        </div>
    </li>
    <% if (Model.ClassID > 0 && !string.IsNullOrEmpty(Model.ImageUrl))
       { %>
    <li>
        <img src="<%= ResolveClientUrl(Model.ImageUrl) + "?_=" + (new Random().Next()) %>" />
    </li>
        <% } %>
    <li>
        <div class="two-column">
            <div class="column">
                <label class="desc" for="Image">
                    Upload new image</label>
                <div>
                    <%=Html.RadioButtonFor(m => m.ImageType, 0, new { style = "margin-top:5px;" })%><input type="file" class="field text medium" name="Image" style="margin-top:5px;" id="Image" />
                </div>
            </div>
            <div class="column">
                <label class="desc" for="ImageUrl">
                    Set image url</label>
                <%=Html.RadioButtonFor(m => m.ImageType, 1, new { style = "float:left;margin-top:5px;" })%><%=Html.TextBoxFor(m => m.ImageUrl, new { @class = "field text medium", style = "margin-top:5px; float:left" })%>
                <%-- <%=Ajax.ActionLink2(@"<span class=""ui-icon ui-icon-circle-plus""></span>Select","GetImages",null,
                            new AjaxOptions() 
            { 
                HttpMethod="POST", 
                UpdateTargetId = "SelectImage",
                OnBegin = "onBeginSelectImage",
                OnSuccess = "function() { $('#SelectImage').dialog('open'); }"
            },new{@class="btn ui-state-default"})  %>--%>
            </div>
        </div>
    </li>
    <li>
        <div class="two-column">
            <div class="column">
                <label class="desc" for="SpeakerID">
                    Level</label>
                <div>
                    <%=Html.DropDownListFor(m => m.LevelTagID, Model.Level, new { @class = "field select large" })%>
                </div>
            </div>
            <div class="column">
                <label class="desc" for="SpeakerID">
                    Shipping Location</label>
                <div>
                    <%=Html.DropDownListFor(m => m.ShippingLocationID, Model.ShippingLocation, new { @class = "field select large" })%>
                </div>
            </div>
        </div>
    </li>
    <li>
        <div class="two-column">
            <div class="column">
            </div>
            <div class="column">
            </div>
        </div>
    </li>
    <li>
        <div class="portlet ui-widget ui-widget-content ui-helper-clearfix ui-corner-all form-container">
            <div class="portlet-header ui-widget-header">
                Product</div>
            <div class="portlet-content" style="display: block;">
                <div style="float: left; margin-right: 1em">
                    <label class="desc" for="DiskAvailable">
                        CD</label>
                    <div>
                        <%= Html.CheckBoxFor(model => model.DiskAvailable, new { @class = "field checkbox" })%></div>
                </div>
                <div style="float: left; margin-right: 1em">
                    <label class="desc" for="DiskPriceUSD">
                        Price in USD</label>
                    <div>
                        <%= Html.TextBoxFor(model => model.DiskPriceUSD, new { @class = "field text", style = "width:100px" })%></div>
                </div>
                <div style="float: left; margin-right: 1em">
                    <label class="desc" for="TapeAvailable">
                        Tape</label>
                    <div>
                        <%= Html.CheckBoxFor(model => model.TapeAvailable, new { @class = "field checkbox" })%></div>
                </div>
                <div style="float: left; margin-right: 3em">
                    <label class="desc" for="TapePriceUSD">
                        Price in USD</label>
                    <div>
                        <%= Html.TextBoxFor(model => model.TapePriceUSD, new { @class = "field text", style="width:100px" })%></div>
                </div>
            </div>
        </div>
    </li>
    <li>
        <div class="portlet ui-widget ui-widget-content ui-helper-clearfix ui-corner-all form-container">
            <div class="portlet-header ui-widget-header">
                File</div>
            <div class="portlet-content" style="display: block;">
                <div style="float: left; margin-right: 1em">
                    <label class="desc" for="DownloadAvailable">
                        File</label>
                    <div>
                        <%= Html.CheckBoxFor(model => model.DownloadAvailable, new { @class = "field checkbox" })%></div>
                </div>
                <div style="float: left; margin-right: 1em">
                    <label class="desc" for="DownloadPriceUSD">
                        Price in USD</label>
                    <div>
                        <%= Html.TextBoxFor(model => model.DownloadPriceUSD, new { @class = "field text", style = "width:100px" })%></div>
                </div>
                <div style="float: left; margin-right: 1em">
                    <label class="desc" for="DownloadPriceUnit">
                        Units</label>
                    <div>
                        <%= Html.TextBoxFor(model => model.DownloadPriceUnit, new  {@class = "field text", style = "width:100px" })%></div>
                </div>
                <div class="clearfix">
                </div>
                <div style="margin-right: 1em">
                    <label class="desc">
                        Downloadable file path</label>
                    <div>
                        <%= Html.TextBoxFor(model => model.AmazonFilePath, new { @readonly = "readonly", @class = "field text large df_HQ", style="float:left;margin-top:5px" })%>
                        <a class="btn ui-state-default" href="javascript:void(0);" onclick="amazonSelect('AmazonFilePath');">
                            <span class="ui-icon ui-icon-circle-plus"></span>Upload File</a>
                    </div>
                    <% if (!string.IsNullOrEmpty(Model.AmazonFilePath))
                       { %>
                    <div>
                        <a href="#" onclick="return playFile('<%= Url.Action("GetStream", "Audio") + "?name=" %>','df_HQ')"
                            class="btn_no_text btn ui-state-default ui-corner-all tooltip" title="Listen in High Quality">
                            <span class="ui-icon ui-icon-play"></span></a><a href="#" onclick="downloadFile('<%= Url.Action("GetAudio", "Audio") + "?name=" %>','df_HQ',this)"
                                title="Download in High Quality" target="_blank" class="btn_no_text btn ui-state-default ui-corner-all tooltip">
                                <span class="ui-icon ui-icon-disk"></span></a>
                    </div>
                    <% } %>
                </div>
                <div style="margin-right: 1em">
                    <label class="desc">
                        Alternate file path</label>
                    <div>
                        <%= Html.TextBoxFor(model => model.AmazonFilePath2, new { @readonly = "readonly", @class = "field text large df_LQ", style = "float:left;margin-top:5px" })%>
                        <a class="btn ui-state-default" href="javascript:void(0);" onclick="amazonSelect('AmazonFilePath2');">
                            <span class="ui-icon ui-icon-circle-plus"></span>Upload File</a>
                    </div>
                    <% if (!string.IsNullOrEmpty(Model.AmazonFilePath2))
                       { %>
                    <div>
                        <a href="#" onclick="return playFile('<%= Url.Action("GetStream", "Audio") + "?name=" %>','df_LQ')"
                            class="btn_no_text btn ui-state-default ui-corner-all tooltip" title="Listen in Low Quality">
                            <span class="ui-icon ui-icon-play"></span></a><a href="#" onclick="downloadFile('<%= Url.Action("GetAudio", "Audio") + "?name=" %>','df_LQ',this)"
                                title="Download in Low Quality" target="_blank" class="btn_no_text btn ui-state-default ui-corner-all tooltip">
                                <span class="ui-icon ui-icon-disk"></span></a>
                    </div>
                    <% } %>
                </div>
            </div>
        </div>
    </li>
</ul>
<div style="display: none">
    <div id="createSpeakerD" title="Create new speaker">
        <label class="desc" for="speakerName">
            Speaker name
        </label>
        <div>
            <input type="text" id="speakerName" class="field text large" maxlength="255" />
        </div>
    </div>
</div>
<div style="dysplay: none">
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
<div style="display:none">
    <div id="SelectImage" title="Select image for class">
    </div>
</div>


<script type="text/javascript">
    var ChangeAmazonFilePathObj = null;
    function amazonSelect(target_id) {
        ChangeAmazonFilePathObj = $('#' + target_id);
        $('#amazons3_selectfile').dialog('open')
    }
    function amazonSuccess(path) {
        $('#DownloadAvailable').attr('checked', 'true');
        ChangeAmazonFilePathObj.val(path);
        $('#amazons3_selectfile').dialog('close');
    }
</script>

<script type="text/javascript">


    $(function () {
        $('#createSpeakerD').dialog({
            autoOpen: false,
            modal: true,
            buttons: {
                'Ok': function () {
                    var name = $('#createSpeakerD #speakerName').val();
                    $.post('/Catalog/CreateSpeaker', { name: name }, speakerCreated, 'json');
                },
                'Cancel': function () {
                    $(this).dialog('close');
                }
            }
        });

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

    //    $('#aamazon').click(function () {
    //        $('#amazons3_selectfile').dialog('open');
    //        return false;
    //    });

    //    $('button#createSpeaker').click(function () {
    //        $('#createSpeakerD').dialog('open');
    //        return false;
    //    });

    function speakerCreated(data) {
        if ($('select#SpeakerID option[value=' + data.id + ']').length == 0) {
            $('select#SpeakerID').append(
                $('<option></option>').val(data.id).html(data.name)
            );
        }

        $('select#SpeakerID option[value=' + data.id + ']').attr('selected', 'selected');
        $('#createSpeakerD').dialog('close');
    }


</script>
<script type="text/javascript">
    var oFCKeditor = new FCKeditor("description");
    oFCKeditor.BasePath = "<%= Url.Content("~/Areas/Admin/Content/fckeditor/2_6_6/") %>";
    //oFCKeditor.Width = 507;
    oFCKeditor.ReplaceTextarea();
</script>
<script type="text/javascript">

    function playFile(url, textbox_class) {

        return streamPopupflex(url + encodeURIComponent($('.' + textbox_class).val()), 300, 50);
    }

    function downloadFile(url, textbox_class, elem) {
        $(elem).attr('href', url + encodeURIComponent($('.' + textbox_class).val()));
    }

    function streamPopupflex(page, width, height) {
        self.name = "TMmain";
        options = "toolbar=0,status=0,menubar=0,scrollbars=0,resizable=0,width=" + width + ",height=" + height;
        window.open(page, "TMPlayer", options);

        return false;
    }
</script>
