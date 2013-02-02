<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/twocolumns.Master"
    Inherits="System.Web.Mvc.ViewPage<Main.Areas.Admin.Models.ControllerView.HomeIndexModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<div class="page-title ui-widget-content ui-corner-all">
		<h1>Administration Options</h1>
		<div class="other">
			<ul id="dashboard-buttons">
				<li>
                    <%=Html.ActionLink("Users", "Index", "User", null, new { Class = "Book_phones tooltip", title = "Users" })%>
				</li>
				<li>
                    <%=Html.ActionLink("Catalog", "Index", "Catalog", null, new { Class="Books tooltip", title="Catalog" })%>
				</li>
				<li>
                    <%=Html.ActionLink("Branches", "Index", "Portal", null, new { Class = "Globe tooltip", title = "Branches" })%>
				</li>
				<!-- li>
					<a href="#" class="Box_recycle tooltip" title="Streaming and Downloads">
						Stream/Downloads
					</a>
				</li>
				<li>
					<a href="#" class="Glass tooltip" title="Search Catalog">
						Search Catalog
					</a>

				</li>
				<li>
					<a href="#" class="Books tooltip" title="Books">
						Books
					</a>

				</li>
				<li>
					<a href="#" class="Box_content tooltip" title="Box content">
						Box content
					</a>
				</li>
				<li>
					<a href="#" class="Briefcase_files tooltip" title="Briefcase files">
						Briefcase files
					</a>

				</li>
				<li>
					<a href="#" class="Chart_4 tooltip" title="Chart 4">
						Chart
					</a>
				</li>
				<li>
					<a href="#" class="Clipboard_3 tooltip" title="Clipboard 3">
						Clipboard
					</a>

				</li>
				<li>
					<a href="#" class="Chart_5 tooltip" title="Chart 5">
						Chart
					</a>
				</li>
				<li>
					<a href="#" class="Mail_compose tooltip" title="Mail compose">
						Mail compose
					</a>

				</li>
				<li>
					<a href="#" class="Mail_open tooltip" title="Mail open">
						Mail open
					</a>
				</li>
				<li>
					<a href="#" class="Monitor tooltip" title="Monitor">
						Monitor
					</a>

				</li>
				<li>
					<a href="#" class="Star tooltip" title="Star">
						Star
					</a>
					<div class="clearfix"></div>
				</li -->
			</ul>

             <br /><br />
            <% if (Main.MvcApplication.ActivationDaemon.IsStarted)
               {
                   using (Html.BeginForm("StopSubscribeDaemon", "Home"))
                   {  %>
               <h1>Subscribe daemon started</h1>
               <input type="submit" value="Stop subscribe daemon" />
            <%};
               }
               else
               {
                   using (Html.BeginForm("StartSubscribeDaemon", "Home"))
                   {%>
               <h1>Subscribe daemon stopped</h1>
               <input type="submit" value="Start subscribe daemon" />
            <%};
               } %>

    <div id="amazons3_selectfile" style="clear:both;display:none">
        <%var data = new ViewDataDictionary();  %>
        <%Html.RenderPartial("../AmazonS3/SelectFile", Main.MvcApplication.S3Amazon.Hierarchy, data); %>
    </div>
			<div class="clearfix"></div>
		</div>
    </div>
</asp:Content>
