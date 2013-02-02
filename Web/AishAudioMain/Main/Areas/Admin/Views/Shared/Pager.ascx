<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Main.Areas.Admin.Models.Common.PagingData>" %>

<script type="text/javascript">
    function changePS(elem, url) {
        document.location.href =  url + 'p=1&ps=' + elem.value;
    }

    function toPage(event, elem, url) {
        var keycode = 0;
        if (event) 
            keycode = event.which;
        else if (window.event)
            keycode = window.event.keyCode;

        if (keycode == 13) {
            var val = parseInt(elem.value);

            if (!isNaN(val))
                document.location.href = url + 'p=' + val;
        }
    }
</script>

<div id="pager">
    <a class="btn_no_text btn ui-state-default ui-corner-all first" title="First Page"
        href="<%= Model.FirsPageUrl %>"><span class="ui-icon ui-icon-arrowthickstop-1-w"></span></a><a class="btn_no_text btn ui-state-default ui-corner-all prev"
            title="Previous Page" href="<%= Model.PrevPageUrl %>"><span class="ui-icon ui-icon-circle-arrow-w"></span>
        </a>
    <input type="text" class="pagedisplay" value="<%= Model.CurrentPageNum + "/" + Model.TotalPageCnt %>" onkeypress="toPage(event,this,'<%= Model.PagerPathWithSize %>')"  />
    <a class="btn_no_text btn ui-state-default ui-corner-all next" title="Next Page"
        href="<%= Model.NextPageUrl %>"><span class="ui-icon ui-icon-circle-arrow-e"></span></a><a class="btn_no_text btn ui-state-default ui-corner-all last"
            title="Last Page" href="<%= Model.LastPageUrl %>"><span class="ui-icon ui-icon-arrowthickstop-1-e"></span>
        </a>
    <select class="pagesize" onchange="changePS(this,'<%= Model.ClearPagerPath %>')">
        <option value="50" <%= Model.PageSize == 50 ? "selected=\"selected\"" : string.Empty  %>>50 results</option>
        <option value="100" <%= Model.PageSize == 100 ? "selected=\"selected\"" : string.Empty  %>>100 results</option>
        <option value="200" <%= Model.PageSize == 200 ? "selected=\"selected\"" : string.Empty  %>>200 results</option>
        <option value="500" <%= Model.PageSize == 500 ? "selected=\"selected\"" : string.Empty  %>>500 results</option>
        <option value="1000" <%= Model.PageSize == 1000 ? "selected=\"selected\"" : string.Empty  %>>1000 results</option>
    </select>
</div>
