<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Domain Not Resolve</title>
    <link href="<%= Url.Css("common.css") %>" rel="stylesheet" type="text/css" />
    <script src="<%= Url.JavaScript("jquery-1.4.1.min.js") %>" type="text/javascript"></script>
</head>

<body>
    <div class="page">

        <div id="header">
            <div id="title">
                <h1>Domain Not Resolve</h1>
            </div>
              
            <div id="logindisplay">
                <% Html.RenderPartial("LogOnUserControl"); %>
            </div> 
            
            <div id="menucontainer">
            
                <ul id="menu">              
                    <li><%= Html.ActionLink("Home", "Index", "Home")%></li>
                    <li><%= Html.ActionLink("Admin", "Index", "Home", new { area = "Admin" }, null)%></li>
                </ul>
            
            </div>
        </div>

        <div id="main">
                <h2>Domain Not Resolved</h2>
    <p>Sorry, but this domain is not resolved as portal alias for our site</p>

            <div id="footer">
            </div>
        </div>
    </div>
</body>
</html>