<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Main.Areas.Admin.Models.ControllerView.User.UserFilter>" %>
<script type="text/javascript">
    $(document).ready(function () {
        $(".datepicker").datepick({ dateFormat: 'mm/dd/yy' });
    });
</script>
<%using (Html.BeginForm(
      ViewContext.ParentActionViewContext.RouteData.Values["Action"].ToString(),
      ViewContext.ParentActionViewContext.RouteData.Values["Controller"].ToString(), FormMethod.Get, new { @class = "forms" }))
  {  %>
  <ul>
  <li>
  <div class="float-left">
  <span>
    <%= Html.LabelFor(m => m.ssubscribe)%>
    <%= Html.DropDownListFor(m => m.ssubscribe, Model.SubscribePlan)%>
  </span>
  </div>
  <div class="float-left">
  <span>
    <%= Html.LabelFor(m => m.semail)%>
    <%= Html.TextBoxFor(m => m.semail)%>
  </span>
  </div>
  <div class="float-left">
  <span>
    <%= Html.LabelFor(m => m.susername)%>
    <%= Html.TextBoxFor(m => m.susername)%>
  </span>
  </div>
  <div class="float-left">
  <span>
    <%= Html.LabelFor(m => m.sfirstname)%>
    <%= Html.TextBoxFor(m => m.sfirstname)%>
  </span>
  </div>
  <div class="float-left">
  <span>
    <%= Html.LabelFor(m => m.slastname)%>
    <%= Html.TextBoxFor(m => m.slastname)%>
  </span>
  </div>
  <div class="float-left">
  <span>
    <%= Html.LabelFor(m => m.msdate)%>
    <%= Html.TextBoxFor(m => m.msdate, new { @class = "datepicker" })%>
  </span>
  </div>
  <div class="float-left">
  <span>
    <%= Html.LabelFor(m => m.medate)%>
    <%= Html.TextBoxFor(m => m.medate, new { @class = "datepicker" })%>
  </span>
  </div>
  <div class="float-left">
  <span>
    <%= Html.LabelFor(m => m.scanceled)%>
    <input type="checkbox" id="scanceled" name="scanceled" <%= Model.scanceled?"checked=\"checked\" ":"" %>value="true" />
  </span>
  </div>
  <div class="float-left">
  <span>
    <%= Html.LabelFor(m => m.sdeclined)%>
    <input type="checkbox" id="sdeclined" name="sdeclined" <%= Model.sdeclined?"checked=\"checked\" ":"" %>value="true" />
  </span>
  </div>
  <div class="float-left" style="margin-left:30px">
  <span>
    <%= Html.LabelFor(m => m.schargeindays) %>
    <%= Html.TextBoxFor(m => m.schargeindays)%>
  </span>
  </div>
  <div class="float-left">
  <span>
    <%= Html.LabelFor(m => m.schargeindaysexactly)%>
    <input type="checkbox" id="schargeindaysexactly" name="schargeindaysexactly" <%= Model.schargeindaysexactly?"checked=\"checked\" ":"" %>value="true" />
  </span>
  </div>
  <div class="float-left">
  <span>
  <label>&nbsp;</label>
  <input type="submit" value="Filter" />
  </span>
  </div>
  </li>
  </ul>
          <%--Html.DropDownListFor(c => c.SelectedSubscribe, new SelectList(Model.PlansList, "Key", "Value", Model.SelectedSubscribe), 
            null, new { @class = "field select", style="width:200px;", onchange="filter(this)" })--%> 

<%} %>