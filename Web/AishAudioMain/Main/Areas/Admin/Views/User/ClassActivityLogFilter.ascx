<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Main.Areas.Admin.Models.ControllerView.User.ClassActivityLogFilter>" %>
<script type="text/javascript">
	$(document).ready(function () {
		$(".datepicker").datepick({ dateFormat: 'mm/dd/yy' });
	});
</script>
<%using (Html.BeginForm(
	  null,
	  null, FormMethod.Get, new { @class = "forms" }))
  {  %>
  <%=Html.HiddenFor(m=>m.user_id) %>
<ul>
	<li>
		<div class="float-left">
			<span>
				<%= Html.LabelFor(m => m.fsincedata)%>
				<%= Html.TextBoxFor(m => m.fsincedata, new { @class = "datepicker" })%>
			</span>
		</div>
		<div class="float-left">
			<span>
				<%= Html.LabelFor(m => m.fbeforedata)%>
				<%= Html.TextBoxFor(m => m.fbeforedata, new { @class = "datepicker" })%>
			</span>
		</div>
   		<div class="float-left">
			<span>
				<%= Html.LabelFor(m => m.fgrouping)%>
				<%= Html.CheckBoxFor(m => m.fgrouping)%>
			</span>
		</div>
   		<div class="float-left">
			<span>
                <label for="ftype0">Show All:</label>
                <%= Html.RadioButtonFor(m => m.ftype, Main.Areas.Admin.Models.ControllerView.User.ClassActivityLogFilter.TypeFilter.ShowAll, new { id = "ftype0" })%>
			</span>
		</div>
   		<div class="float-left">
			<span>
                <label for="ftype1">Show Download Only:</label>
                <%= Html.RadioButtonFor(m => m.ftype, Main.Areas.Admin.Models.ControllerView.User.ClassActivityLogFilter.TypeFilter.ShowDownloadOnly, new { id = "ftype1" })%>
			</span>
		</div>
   		<div class="float-left">
			<span>
                <label for="ftype2">Show Streaming Only:</label>
                <%= Html.RadioButtonFor(m => m.ftype, Main.Areas.Admin.Models.ControllerView.User.ClassActivityLogFilter.TypeFilter.ShowStreamingOnly, new { id = "ftype2" })%>
			</span>
		</div>
		<div class="float-left">
			<span>
				<label>
					&nbsp;</label>
				<input type="submit" value="Filter" />
			</span>
		</div>
	</li>
</ul>
<%} %>
