<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Main.Areas.Admin.Models.ControllerView.Catalog.ClassFilter>" %>
<%using (Html.BeginForm(
      ViewContext.ParentActionViewContext.RouteData.Values["Action"].ToString(),
      ViewContext.ParentActionViewContext.RouteData.Values["Controller"].ToString(), FormMethod.Get, new { @class = "forms" }))
  {  %>
<ul>
    <li>
        <div class="float-left">
            <span>
                <%= Html.LabelFor(m => m.sspeaker)%>
                <%= Html.TextBoxFor(m => m.sspeaker)%>
            </span>
        </div>
        <div class="float-left">
            <span>
                <%= Html.LabelFor(m => m.stitle)%>
                <%= Html.TextBoxFor(m => m.stitle)%>
            </span>
        </div>
        <div class="float-left">
            <span>
                <%= Html.LabelFor(m => m.scode)%>
                <%= Html.TextBoxFor(m => m.scode)%>
            </span>
        </div>
        <div class="float-left">
            <span>
                <%= Html.LabelFor(m => m.scategory)%>
                <%= Html.ListBoxFor(m => m.scategory, Model.scategory)%>
            </span>
        </div>
        <%if ((bool?)this.ViewData["IsSuperUser"] == true)
          {%>
        <div class="float-left">
            <span>
                <%= Html.LabelFor(m => m.sportal)%>
                <%= Html.ListBoxFor(m => m.sportal, Model.sportal)%>
            </span>
        </div>
        <%}%>
        <%=Html.HiddenFor(m=>m.portal_id) %>
        <%=Html.HiddenFor(m=>m.user_id)  %>
        <div class="float-left">
            <span>
                <input type="submit" value="Filter" />
            </span>
        </div>
    </li>
</ul>
<%} %>