<%@ Page Title="" Language="C#" MasterPageFile="../Shared/twoColumn.Master"
    Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <%--<% string menuActionName = (string)(ViewData["MenuActionName"] ?? "results"); %>--%>
    <% string menuActionName = MainBL.CookieBL.ShowDetailed(Request.Cookies,null) ? "resultsdetail" : "results"; %>
    <style type="text/css">
        #cust
        {
            width: 600px;
        }
        #cust .bordered
        {
            border-left: 1px solid #666666;
            border-right: 1px solid #666666;
        }
        #cust table
        {
            width: 100%;
        }
        #cust #top
        {
            height: 23px;
        }
        #cust .h
        {
            color: #3065cd;
            font-weight: bold;
            text-align: center;
            font-size: 150%;
            padding: 20px 0;
        }
        #cust .splitter
        {
            border-left: 1px solid #666666;
        }
        #cust td
        {
            padding-left: 5px;
        }
        #cust .th td
        {
            color: #990000;
            font-family: arial,verdana;
            font-size: 14px;
            padding-bottom: 15px;
            font-weight: bold;
        }
        #cust .link_1 a
        {
            display: block;
            color: #000000;
            font-family: arial,verdana;
            font-size: 14px;
            line-height: 2;
            text-decoration: underline;
        }
        #cust .link_1 a:hover
        {
            text-decoration: none;
        }
        #cust .link_2 a
        {
            color: #990000;
            font-family: arial,verdana;
            font-size: 14px;
            display: block;
            margin: 15px 0px 10px 0px;
            font-weight: bold;
            text-decoration: underline;
        }
        #cust .link_2 a:hover
        {
            text-decoration: none;
        }
    </style>
    <div id="cust">
        <div id="top">
            <map name="Mapparshatop">
                <area shape="RECT" coords="461,0,589,1" href="http://www.aish.com/torahportion/default.asp">
                <area shape="rect" coords="467,3,595,24" href="https://aishaudio.com">
            </map>
            <img src="/static/img/parshatop.gif" usemap="#mapParshaTop" width="600" border="0"
                height="23">
        </div>
        <div class="bordered">
            <div class="h">
                Torah portions
            </div>
            <table>
                <col width="20%" />
                <col width="20%" />
                <col width="20%" />
                <col width="20%" />
                <col width="20%" />
                <tr class="th">
                    <% SiteMapNodeCollection myNodes = SiteMap.Providers["Static"].RootNode.ChildNodes[0].ChildNodes; %>
                    <% for (int i = 0; i < myNodes.Count; i++)
                       { %>
                    <td <%= i > 0 ? "class=\"splitter\"" : string.Empty %>>
                        <%= myNodes[i].Description %>
                    </td>
                    <% } %>
                </tr>
                <tr class="link_1">
                    <% for (int i = 0; i < myNodes.Count; i++)
                       { %>
                    <td <%= i > 0 ? "class=\"splitter\"" : string.Empty %>>
                        <% foreach (SiteMapNode linkNode in myNodes[i].ChildNodes)
                           { %>
                        <a href="<%= string.Format(linkNode.Url,menuActionName).Replace(" ", "-") %>">
                            <%= linkNode.Title %></a>
                        <% } %>
                    </td>
                    <% } %>
                </tr>
                <tr class="link_2">
                    <% for (int i = 0; i < myNodes.Count; i++)
                       { %>
                    <td <%= i > 0 ? "class=\"splitter\"" : string.Empty %>>
                        <a href="<%= string.Format(myNodes[i].Url,menuActionName).Replace(" ", "-") %>">
                            <%= myNodes[i].Title %></a>
                    </td>
                    <% } %>
                </tr>
            </table>
        </div>
        <div>
            <img src="/static/img/parshabottom.gif" width="600" border="0" height="13">
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>
