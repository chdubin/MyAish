<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Main.Areas.Admin.Models.Portal.EditModel>" %>
<ul>
    <li>
        <% if (Model.PortalID != 0)
           { %>
            <%=Html.HiddenFor(m => m.PortalID)%>
        <% } %>
        <div style="float: left; margin-right: 1em">
            <label class="desc" for="Active">
                Active</label>
            <div class="editor-field">
                <%= Html.CheckBoxFor(model => model.Active, new { @class = "field checkbox" })%>
            </div>
        </div>
    </li>
    <li id="accessSettings">
        <span style="margin-right:2em">
        <label for="ApplicationName" class="desc">Application Name</label>
        <% if (Model.PortalID != Main.GlobalConstant.MAIN_PORTAL_ID)
           { %>
            <%=Html.DropDownListFor(m => m.ApplicationName, new SelectList(Main.MvcApplication.Helper.AllowedApplicationNames, Model.ApplicationName), 
                string.IsNullOrEmpty(Model.PasswordProtection) ?(object) new { @class = "field select medium" } : new { @class = "field checkbox", disabled="disabled" })%>
        <% }
           else
           { %>
        <%=Html.Encode(Model.ApplicationName) %>
        <%=Html.HiddenFor(m => m.ApplicationName)%>
        <% } %>
        </span>
                <span style="margin-right:2em">
        <label class="desc" for="AuthorizedOnly">
            Only authorized access
        </label>
            <%=Html.CheckBoxFor(m => m.AuthorizedOnly, string.IsNullOrEmpty(Model.PasswordProtection) ?(object) new { @class = "field checkbox" } : new { @class = "field checkbox", disabled="disabled" })%>
        </span>
        <span style="margin-right:2em">
        <label class="desc" for="AllowAuthorize">
            Allow access to user account
        </label>
            <%=Html.CheckBoxFor(m => m.AllowAuthorize, string.IsNullOrEmpty(Model.PasswordProtection) ? (object)new { @class = "field checkbox" } : new { @class = "field checkbox", disabled = "disabled" })%>
        </span>
        <span style="margin-right:2em">
        <label class="desc" for="AllowRegister">
            Allow register of Users
        </label>
            <%=Html.CheckBoxFor(m => m.AllowRegister, string.IsNullOrEmpty(Model.PasswordProtection) ? (object)new { @class = "field checkbox" } : new { @class = "field checkbox", disabled = "disabled" })%>
        </span>
    </li>
    <li>
        <label for="PasswordProtection" class="desc">Password Protection</label>
        <div>
            <%= Html.TextBoxFor(m => m.PasswordProtection, new { @class = "field text medium", onchange = "if($(this).val().length>0){$('#accessSettings input, #accessSettings select').attr('disabled','disabled');}else{$('#accessSettings input, #accessSettings select').removeAttr('disabled');}" })%>
        </div>
    </li>
    <li>
        <label for="Name" class="desc">Name</label>
        <div>
            <%= Html.TextBoxFor(m => m.Name, new { @class = "field text medium" })%>
        </div>
    </li>
    <li>
        <label for="ThemeName" class="desc">
            Design
        </label>
        <div>
            <%=Html.DropDownListFor(m => m.ThemeName, new SelectList(Main.MvcApplication.Helper.AllowedThemes, Model.ThemeName), new { @class = "field select medium" })%>
        </div>
    </li>
    <li>
        <label class="desc" for="Aliases">
            Domain Names
        </label>
        <div>
            <%=Html.TextAreaFor(m => m.Aliases, new { @class = "field textarea small", cols = 50 })%>
        </div>
    </li>
    <li>
<%--        <span style="margin-right:2em">
        <label class="desc" for="AllowBuyFiles">
            Allow buy files
        </label>
            <%=Html.CheckBoxFor(m => m.AllowBuyFiles, new { @class = "field checkbox" })%>
        </span>
        <span style="margin-right:2em">
        <label class="desc" for="AllowBuyProducts">
            Allow buy products
        </label>
            <%=Html.CheckBoxFor(m => m.AllowBuyProducts, new { @class = "field checkbox" })%>
        </span>
--%>    </li>
</ul>