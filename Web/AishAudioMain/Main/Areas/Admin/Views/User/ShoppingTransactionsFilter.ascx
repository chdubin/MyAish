﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Main.Areas.Admin.Models.ControllerView.User.ShoppingTransactionsFilter>" %>
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
                <%= Html.LabelFor(m => m.ftranstate)%>
                <%= Html.DropDownListFor(m => m.ftranstate, Model.TransactionState)%>
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