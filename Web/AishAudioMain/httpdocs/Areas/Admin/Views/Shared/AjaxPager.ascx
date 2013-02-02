<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Main.Areas.Admin.Models.Common.AjaxPagingData>" %>

<% if (Model.TotalRowsCount > Model.PageSize)
{ %>
    <ul class="pagination">
        <li class="<%= Model.CurrentPageNum == 1 ? "previous-off" : "previous" %>">
            <% if (Model.CurrentPageNum != 1)
               {%>
                    <%= Ajax.ActionLink("First", Model.ActionName,
                        Model.GetRouteValues(1),
                        new AjaxOptions()
                        {
                            HttpMethod = "GET",
                            InsertionMode = InsertionMode.Replace,
                            UpdateTargetId = Model.UpdateTargetID,
                            OnBegin = "onBeginPagerRequest"
                        })%>
            <% }
               else
               {%>
                    First
            <% } %>
        </li>
        <li class="<%= Model.CurrentPageNum == 1 ? "previous-off" : "previous" %>">
            <% if (Model.CurrentPageNum != 1)
               {%>
                    <%= Ajax.ActionLink("Previous", Model.ActionName,
                        Model.GetRouteValues(Model.CurrentPageNum - 1),
                        new AjaxOptions()
                        {
                            HttpMethod = "GET",
                            InsertionMode = InsertionMode.Replace,
                            UpdateTargetId = Model.UpdateTargetID,
                            OnBegin = "onBeginPagerRequest"
                        })%>
            <% }
               else
               {%>
                    Previous
            <% } %>
        </li>

        <% for (int i = Math.Max(1, Model.CurrentPageNum - 3); i < Model.CurrentPageNum; i++)
            {  %>
                <li class="next">
                    <%= Ajax.ActionLink(i.ToString(), Model.ActionName,
                        Model.GetRouteValues(i),
                        new AjaxOptions()
                        {
                            HttpMethod = "GET",
                            InsertionMode = InsertionMode.Replace,
                            UpdateTargetId = Model.UpdateTargetID,
                            OnBegin = "onBeginPagerRequest"
                        })%>
                </li>
        <% } %>

        <li class="next-off">
            <%= Model.CurrentPageNum.ToString() %>
        </li>

        <% for (int i = Model.CurrentPageNum + 1; (i < Math.Min(Model.CurrentPageNum + Math.Max(4, 8 - Model.CurrentPageNum), Model.MaxPageNum + 1)); i++)
           {  %>               
                <li class="next">
                    <%= Ajax.ActionLink(i.ToString(), Model.ActionName,
                        Model.GetRouteValues(i),
                        new AjaxOptions()
                        {
                            HttpMethod = "GET",
                            InsertionMode = InsertionMode.Replace,
                            UpdateTargetId = Model.UpdateTargetID,
                            OnBegin = "onBeginPagerRequest"
                        })%>
                </li>
        <% } %>

        <li class="<%= Model.CurrentPageNum == Model.MaxPageNum ? "next-off" : "next" %>">
            <% if (Model.CurrentPageNum != Model.MaxPageNum)
               {%>
                    <%= Ajax.ActionLink("Next", Model.ActionName,
                        Model.GetRouteValues(Model.CurrentPageNum + 1),
                        new AjaxOptions()
                        {
                            HttpMethod = "GET",
                            InsertionMode = InsertionMode.Replace,
                            UpdateTargetId = Model.UpdateTargetID,
                            OnBegin = "onBeginPagerRequest"
                        })%>
            <% }
               else
               {%>
                    Next
            <% } %>
        </li>
        <li class="<%= Model.CurrentPageNum == Model.MaxPageNum ? "next-off" : "next" %>">
            <% if (Model.CurrentPageNum != Model.MaxPageNum)
               {%>
                    <%= Ajax.ActionLink("Last", Model.ActionName,
                        Model.GetRouteValues(Model.MaxPageNum),
                        new AjaxOptions()
                        {
                            HttpMethod = "GET",
                            InsertionMode = InsertionMode.Replace,
                            UpdateTargetId = Model.UpdateTargetID,
                            OnBegin = "onBeginPagerRequest"
                        })%>
            <% }
               else
               {%>
                    Last
            <% } %>
        </li>
    </ul>
    <div class="clearfix"></div>
    
    
    


    <%--<%= Html.HiddenFor(model => model.AdditionalParams, new { @id = "addParams" })%>--%>
<% 
} %>
<script type="text/javascript">
    function onBeginPagerRequest(ajaxContext) {
        /*var addParams = $("#addParams").attr("value");
        alert(addParams);
        var url = ajaxContext.get_request().get_url();
        ajaxContext.get_request().set_url(url + "&" + addParams);
        */
    }
</script>
