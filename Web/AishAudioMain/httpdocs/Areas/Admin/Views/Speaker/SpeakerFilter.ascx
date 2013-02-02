<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Main.Areas.Admin.Models.ControllerView.Speaker.SpeakerFilter>" %>
<script type="text/javascript">
	$(document).ready(function () {
		$(".datepicker").datepick({ dateFormat: 'mm/dd/yy' });
	});
</script>
<%using (Html.BeginForm(
	  null,
	  null, FormMethod.Get, new { @class = "forms" }))
  {  %>
  <%--<%=Html.HiddenFor(m=>m.user_id) %>--%>
<ul>
	<li>
		<div class="float-left">
			<span>
				<%= Html.LabelFor(m => m.ftitle)%>
				<%= Html.TextBoxFor(m => m.ftitle, new { @class = "datepicker" })%>
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