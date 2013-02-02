<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/twocolumns.Master" 
Inherits="System.Web.Mvc.ViewPage<List<MainEntity.Models.Activity.RoyaltyLog>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<div class="title title-spacing">
        <h2>Total File Purchases and Downloads: for <%= ViewData["SpeakerName"].ToString() %></h2>
        <% //if (TempData["success"] != null && Convert.ToBoolean(TempData["success"]) == true)
            var cnt = 0;

           //{ %>
           <!-- div class="response-msg success ui-corner-all" span Speaker has been successfully saved. /span /div -->
        <%//} %>
    </div>
    
    <div style="margin-bottom:10px;background-color:#D0E9ED;">
        <% Html.RenderPartial("RoyaltiesFilter", ViewData["Filter"]); %>
    </div>

    <table cellpadding="2" cellspacing="0" style="width:100%;">
    <tr>
    <td style="vertical-align:baseline;">
        Total Records Found: <%= ViewData["TotalCount"] %>
    </td>
    <td style="vertical-align:baseline;">
        <% Html.RenderPartial("Pager", ViewData["Pager"]); %>
    </td>
    <td style="text-align:right;" align="right">
        <a class="btn ui-state-default" href="<%= Url.Action("Royalties", "Speaker", new RouteValueDictionary { { "speaker_id", ViewData["SpeakerId"] }, { "downloadType","text" }, { "ps", ((Main.Areas.Admin.Models.Common.PagingData)ViewData["Pager"]).PageSize } } ) %>"><span class="ui-icon ui-icon-circle-plus"></span>Download as Text</a>
        <a class="btn ui-state-default" href="<%= Url.Action("Royalties", "Speaker", new RouteValueDictionary { { "speaker_id", ViewData["SpeakerId"] }, { "downloadType","excel" }, { "ps", ((Main.Areas.Admin.Models.Common.PagingData)ViewData["Pager"]).PageSize } } ) %>"><span class="ui-icon ui-icon-circle-plus"></span>Download All as Excel 2007</a>
    </td>
    </tr>
    </table>

    <div class="hastable">
        <table>
            <thead>
                <tr>
                    <th class="header">
                        Class ID
                    </th>
                    <th>Title</th>
                    <th>User</th>
                    <th>Price</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
            <% 
                var cid = new long();
                var uid = new Guid();
                foreach (var item in Model.OrderBy(x => x.ClassID).ThenBy(x => x.UserId))
               {
                    if (!item.ClassID.Equals(cid) || !item.UserId.Equals(uid))
                    {       
            %>
                <tr>
                    <td><%= item.ClassID %></td>
                    <td><%= item.Title %></td>
                    <td><%= String.Format("<a href='/Admin/User/EditUserInfo?user_id={0}'>{1}</a>", item.UserId, (string.IsNullOrEmpty(item.Name) ? "N/A" : item.Name))%></td>
                    <td><%= item.Price == -1 ? "1 unit" : string.Format("{0:C}", item.Price)%></td>
                    <td><%= String.Format("<a href='/Admin/User/ViewFiles?user_id={0}'>View Files</a>", item.UserId) %></td>
                </tr>
            <%
                        cnt++;
                    } 
                                 cid = item.ClassID;
                                 uid = item.UserId;
                }
              
                %>

            </tbody>
        </table>
Total Count: <%= cnt %>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainHeader" runat="server">
        <link href="<%= Url.Css("css/jquery/plugins/datepicker/3_7_5/smoothness.datepick.css") %>" rel="stylesheet" media="all" />
		<script type="text/javascript" src="<%= Url.JavaScript("jquery/plugins/datepicker/3_7_5/jquery.datepick.js") %>"></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="rightTop" runat="server">
</asp:Content>