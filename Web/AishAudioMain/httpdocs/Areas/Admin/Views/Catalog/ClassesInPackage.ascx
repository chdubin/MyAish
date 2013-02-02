<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Main.Areas.Admin.Models.ControllerView.Catalog.ClassesInPackage>" %>

<label class="desc" for="AddedProducts">Classes</label>
<div class="other hastable">
    <table>
        <tr>
            <th style="width:70%">Title</th>
            <th style="width:10%"></th>
        </tr>
        <% foreach (var c in Model.Classes)
            { %>
                <tr id="<%= c.entityID + "_tr" %>" class="odd">
                    <td>
                        <%= c.title %>
                    </td>
                    <td>
                        <a href="#" class="btn_no_text btn ui-state-default ui-corner-all tooltip" title="Delete this class"
                            onclick="$('#ClassesToDelete').attr('value', $('#ClassesToDelete').attr('value') + '<%= c.entityID %>' + ','); $('#<%= c.entityID + "_tr" %>').hide(); return false;">
                            <span class="ui-icon ui-icon-circle-close"></span>
                        </a>
                    </td>
                </tr>
        <% } %>       
    </table>
</div>