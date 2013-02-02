<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Main.Areas.Admin.Models.Portal.EditModel>" %>
<tr>
    <td>
        <%=Html.Encode(Model.Name) %>
    </td>
    <td>
        <%=Html.Encode(Model.ApplicationName) %>
    </td>
    <td>
        <%=Html.Encode(Model.ThemeName) %>
    </td>
    <td>
        <%=Html.Encode(Model.CreateDate.ToString("d")) %>
    </td>
    <td>
        <%=Html.Encode(Model.Aliases) %>
    </td>
    <td>
        <%=Html.CheckBoxFor(a => a.Active, new { disabled = "disabled" })%>
    </td>
    <td>
        <a href="<%= Url.Action("Edit","Portal",  new { portal_id = Model.PortalID }) %>" class = "btn_no_text btn ui-state-default ui-corner-all tooltip" 
        title="Edit this branch">
            <span class="ui-icon ui-icon-wrench"></span>
        </a>
        <% if (Model.PortalID != Main.GlobalConstant.MAIN_PORTAL_ID)
          { %>
            <a href="<%= Url.Action("Delete","Portal", new { portal_id = Model.PortalID }) %>" class="btn_no_text btn ui-state-default ui-corner-all tooltip" 
                title="Delete this portal" onclick="return confirm('Are you sure to delete this branch?');">
                <span class="ui-icon ui-icon-trash"></span>
            </a>
        <%}%>
        <a href="<%= Url.Action("Products","Portal",  new { portal_id = Model.PortalID }) %>" class = "btn_no_text btn ui-state-default ui-corner-all tooltip" 
        title="View Branch Files">
            <span class="ui-icon ui-icon-folder-open"></span>
        </a>        
    </td>
</tr>
