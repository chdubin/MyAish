<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Main.Areas.Admin.Models.Speaker.SpeakerEditModel>" %>
<ul>
    <li>
        <% if (Model.SpeakerID != 0)
           { %>
            <%=Html.HiddenFor(m => m.SpeakerID)%>
        <% } %>
        <div style="float: left; margin-right: 1em">
            <label class="desc" for="Active">
                Active</label>
            <div class="editor-field">
                <%= Html.CheckBoxFor(model => model.Active, new { @class = "field checkbox" })%>
            </div>
        </div>
    </li>
    <li>
        <label for="Name" class="desc">Name</label>
        <div>
            <%= Html.TextBoxFor(m => m.Name, new { @class = "field text medium" })%>
        </div>
    </li>
    <% if (Model.SpeakerID == 0)
       { %>
    <li>
        <label class="desc" for="Photo">
            Upload photo</label>
        <div>
            <input type="file" name="Photo" id="Photo" />
        </div>
    </li>
    <% }
       else
       { %>
    <li style="clear:both;">
        <% if (!string.IsNullOrEmpty(Model.PhotoPath))
           { %>
        <img style="float:left;margin-right:25px;" src="<%= ResolveClientUrl(ConfigurationManager.AppSettings["UploadSpeakerImgFolderName"] + "/" + MainCommon.MyUtils.GetFileName(Model.PhotoPath)) + "?_=" + (new Random().Next()) %>" />
        <% } %>

        <div style="float:left;">
        <label class="desc" for="Photo">
            Upload new photo</label>
        <div>
            <input type="file" name="Photo" id="Photo" />
        </div>
         </div>
    </li>
    <% } %>
    <li>
        <label for=" ContactInfo" class="desc">
            Contact info
        </label>
        <div>
            <%=Html.TextAreaFor(m => m.ContactInfo, new { @class = "field textarea small", cols = 50 })%>
        </div>
    </li>
    <li>
        <label class="desc" for="Description">
            Description
        </label>
        <div>
            <%=Html.TextAreaFor(m => m.Description, new { @class = "field textarea small", cols = 50 })%>
        </div>
    </li>
</ul>
