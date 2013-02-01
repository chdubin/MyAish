<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Main.Areas.Admin.Models.Common.DefaultPagingData>" %>
<% if (Model.TotalRowsCount > Model.PageSize)
   { %>
<style type="text/css">
    #pager a.btn
    {
        margin: 5px;
        display: block;
        float: left;
    }
</style>
<script type="text/javascript">
    function GoToPage(event, elem, url) {
        var keycode = 0;
        if (event)
            keycode = event.which;
        else if (window.event)
            keycode = window.event.keyCode;

        if (keycode == 13) {
            var val = parseInt(elem.value);

            if (!isNaN(val)) {
                var anchor = $("#pager a")[0];
                anchor.href = url + '&page_num=' + val;
                alert(anchor.href);
                Sys.Mvc.AsyncHyperlink.handleClick(anchor, new Sys.UI.DomEvent(event), { insertionMode: Sys.Mvc.InsertionMode.replace, httpMethod: 'GET', updateTargetId: 'addProductsToPackage', onBegin: Function.createDelegate(this, onBeginPagerRequest) });
            }
        }
    }


    function onBeginPagerRequest(ajaxContext) {
        var addParams = $("#addParams").attr("value");
        var url = ajaxContext.get_request().get_url();
        var toAddIds = $("#ProductsToAdd").attr("value") + $("#toAdd").attr("value");
        var toDeleteIds = $("#ProductsToDelete").attr("value") + $("#toDelete").attr("value");
        ajaxContext.get_request().set_url(url + addParams + "&products_to_add=" + toAddIds + "&products_to_delete=" + toDeleteIds);
    }
</script>
<div id="pager" style="padding:0px">
    <%=Ajax.ActionLink2(@"<span class=""ui-icon ui-icon-arrowthickstop-1-w""></span>",Model.ActionName,new { random = DateTime.Now.Ticks, page_num = 1 },
        new AjaxOptions(){HttpMethod = "GET",
            InsertionMode = InsertionMode.Replace,
            UpdateTargetId = Model.UpdateTargetId,
            OnBegin = "onBeginPagerRequest"
        },new{@class="btn_no_text btn ui-state-default ui-corner-all first", title="First Page"}) %>
    <%=Ajax.ActionLink2(@"<span class=""ui-icon ui-icon-circle-arrow-w""></span>", Model.ActionName, new { random = DateTime.Now.Ticks, page_num = Math.Max(1, Model.CurrentPageNum-1)},
        new AjaxOptions(){HttpMethod = "GET",
            InsertionMode = InsertionMode.Replace,
            UpdateTargetId = Model.UpdateTargetId,
            OnBegin = "onBeginPagerRequest"
        }, new { @class = "btn_no_text btn ui-state-default ui-corner-all prev", title = "Previous Page" })%>
    <input type="text" class="pagedisplay" value="<%= Model.CurrentPageNum + "/" + Model.MaxPageNum %>"
        onkeypress="GoToPage(event,this,'<%=Url.Action(Model.ActionName,new { random = DateTime.Now.Ticks }) %>')" />
    <%=Ajax.ActionLink2(@"<span class=""ui-icon ui-icon-circle-arrow-e""></span>", Model.ActionName, new { random = DateTime.Now.Ticks, page_num = Math.Min(Model.MaxPageNum,Model.CurrentPageNum+1) },
        new AjaxOptions(){HttpMethod = "GET",
            InsertionMode = InsertionMode.Replace,
            UpdateTargetId = Model.UpdateTargetId,
            OnBegin = "onBeginPagerRequest"
        }, new { @class = "btn_no_text btn ui-state-default ui-corner-all next", title = "Next Page" })%>
    <%=Ajax.ActionLink2(@"<span class=""ui-icon ui-icon-arrowthickstop-1-e""></span>", Model.ActionName, new { random = DateTime.Now.Ticks, page_num = Model.MaxPageNum },
        new AjaxOptions(){HttpMethod = "GET",
            InsertionMode = InsertionMode.Replace,
            UpdateTargetId = Model.UpdateTargetId,
            OnBegin = "onBeginPagerRequest"
        }, new { @class = "btn_no_text btn ui-state-default ui-corner-all last", title = "Last Page" })%>
</div>
<%--<ul class="pagination">
    <li class="<%= Model.CurrentPageNum == 1 ? "previous-off" : "previous" %>">
        <% if (Model.CurrentPageNum != 1)
           {%>
                <%= Ajax.ActionLink("First", Model.ActionName,
                    new { random = DateTime.Now.Ticks, page_num = 1 },
                    new AjaxOptions()
                    {
                        HttpMethod = "GET",
                        InsertionMode = InsertionMode.Replace,
                        UpdateTargetId = Model.UpdateTargetId,
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
                    new { random = DateTime.Now.Ticks, page_num = Model.CurrentPageNum - 1 },
                    new AjaxOptions()
                    {
                        HttpMethod = "GET",
                        InsertionMode = InsertionMode.Replace,
                        UpdateTargetId = Model.UpdateTargetId,
                        OnBegin = "onBeginPagerRequest"
                    })%>
        <% }
           else
           {%>
                Previous
        <% } %>
    </li>

    <% for (int i = 1; i < Model.MaxPageNum + 1; i++)
       { %>
       <li class="<%= Model.CurrentPageNum == i ? "active" : "" %>">
            <% if (Model.CurrentPageNum != i)
               {%>
                   <%= Ajax.ActionLink(i.ToString(), Model.ActionName,
                        new { random = DateTime.Now.Ticks, page_num = i.ToString() },
                        new AjaxOptions()
                        {
                            HttpMethod = "GET",
                            InsertionMode = InsertionMode.Replace,
                            UpdateTargetId = Model.UpdateTargetId,
                            OnBegin = "onBeginPagerRequest"
                        })%>
           <% }
               else
           {%>
               <%= i.ToString() %>     
        <% } %>
       </li>
    <% } %>

    <li class="<%= Model.CurrentPageNum == Model.MaxPageNum ? "next-off" : "next" %>">
        <% if (Model.CurrentPageNum != Model.MaxPageNum)
           {%>
                <%= Ajax.ActionLink("Next", Model.ActionName,
                    new { random = DateTime.Now.Ticks, page_num = Model.CurrentPageNum + 1 },
                    new AjaxOptions()
                    {
                        HttpMethod = "GET",
                        InsertionMode = InsertionMode.Replace,
                        UpdateTargetId = Model.UpdateTargetId,
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
                    new { random = DateTime.Now.Ticks, page_num = Model.MaxPageNum },
                    new AjaxOptions()
                    {
                        HttpMethod = "GET",
                        InsertionMode = InsertionMode.Replace,
                        UpdateTargetId = Model.UpdateTargetId,
                        OnBegin = "onBeginPagerRequest"
                    })%>
        <% }
           else
           {%>
                Last
        <% } %>
    </li>
</ul>--%>
<%= Html.HiddenFor(model => model.AddParams, new { @id = "addParams" }) %>
<% } %>