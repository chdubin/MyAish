<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Main.Areas.Admin.Models.Speaker.SpeakerEditModel>" %>
<tr>
    <td>
        <%=Html.Encode(Model.Name) %>
    </td>
<%--    <td>
        <=Html.Encode(Model.ContactInfo)%>
    </td>--%>
    <td>
        <%=Html.Encode(Model.CreateDate.ToString("d")) %>
    </td>
<%--    <td>
        <=Html.Encode(Model.Description) >
    </td>--%>
    <td>
        <%=Html.CheckBoxFor(a => a.Active, new { disabled = "disabled" })%>
    </td>
    <td>
        <a href="<%= Url.Action("Edit","Speaker",  new { speaker_id = Model.SpeakerID }) %>" class = "btn_no_text btn ui-state-default ui-corner-all tooltip" 
        title="Edit this speaker">
            <span class="ui-icon ui-icon-wrench"></span>
        </a>
        <a href="<%= Url.Action("Delete","Speaker", new { speaker_id = Model.SpeakerID }) %>" class="btn_no_text btn ui-state-default ui-corner-all tooltip" 
            title="Delete this speaker" onclick="return confirm('Are you sure to delete this speaker?');">
            <span class="ui-icon ui-icon-trash"></span>
        </a>
        <a href="<%= Url.Action("Classes","Speaker",  new { speaker_id = Model.SpeakerID }) %>" class = "btn_no_text btn ui-state-default ui-corner-all tooltip" 
            title="View classes">
            <span class="ui-icon ui-icon-folder-open"></span>
            </a>        
    </td>
</tr>



