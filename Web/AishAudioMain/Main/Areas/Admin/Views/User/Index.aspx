<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/twocolumns.Master"
    Inherits="System.Web.Mvc.ViewPage<Main.Areas.Admin.Models.ControllerView.User.Index>" %>

<asp:Content ID="Header1" ContentPlaceHolderID="MainHeader" runat="server">

    <link href="<%= Url.Css("css/jquery/plugins/datepicker/3_7_5/smoothness.datepick.css") %>" rel="stylesheet" media="all" />
    <script type="text/javascript" src="<%= Url.JavaScript("jquery/plugins/datepicker/3_7_5/jquery.datepick.js") %>"></script>
    

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript">

    function OnSuccessEditCredits(user_id) {        
        CloseDialog("EditCredits" + user_id);
    }

    function OnBeginEditCredits(ajaxContext, user_id) {
        var url = ajaxContext.get_request().get_url();
        var creditsNew = $('#Credits' + user_id).val();
        ajaxContext.get_request().set_url(url + "&credits=" + creditsNew + "&user_id=" + user_id);
    }

    function CloseDialog(id) {        
        $('#' + id).dialog('close');
        return false;
    }

    function OnSuccessEditPlanDates(user_id) {
        CloseDialog("EditPlanDates" + user_id);
    }

    function OnBeginEditPlanDates(ajaxContext, user_id) {
        var url = ajaxContext.get_request().get_url();
        var newStartDate = $('#StartDate' + user_id).val();
        var newEndDate = $('#EndDate' + user_id).val();
        var chargeDay = $('#ChargeDay' + user_id).val();
        ajaxContext.get_request().set_body("&user_id=" + user_id + "&start_date=" + newStartDate + "&end_date=" + newEndDate + "&charge_day=" + chargeDay);
    }

    function OnBeginLockUser(ajaxContext, user_id) {
        var url = ajaxContext.get_request().get_url();
        ajaxContext.get_request().set_url(url + "&user_id=" + user_id);
    }

    function filter(obj) {
        window.location = '<%= Url.Action("Index", "User") %>' + '?membership_type=' + $(obj).val();
    }
</script>

	<div class="title title-spacing">
        <h2>Users</h2>
    </div>

    <div style="margin-bottom:20px;">
        <% Html.RenderAction("UserFilter"); %>    
    </div>

<%if (Model.UserItems.Length > 0)
  { %>
    <div class="hastable">
        <table>
            <thead> 
                <tr> 
	                <th class="header">User</th> 
	                <th class="header">Address</th> 
	                <th class="header">Date Registered<br />for Plan</th> 
	                <th class="header">Date Created</th> 
	                <th class="header">Units</th> 
	                <th class="header">Membership<br />Type</th>
                    <%--<th class="header">Former Plan<br>Type</th>--%>
                    <th>
                        <a class="btn ui-state-default" href="<%= Url.Action("CreateUser") %>">
                            <span class="ui-icon ui-icon-suitcase"></span><span style="white-space:nowrap;">Create New User</span>
                        </a>                    
                    </th> 
                </tr> 
            </thead>
            <tbody>


            <% foreach (var userItem in Model.UserItems)
               { %>
                <tr>
                    <td>
                        Name: <b><%= userItem.Name%></b><br />
                        Email: <a href='mailto:<%= userItem.Email %>'><%= userItem.Email%></a><br />
                        Username: <%= userItem.UserName %><br />
                        Referrer code: <%= userItem.ReferrerCode%><br />
                        Validated: <%= userItem.Validated ? "Yes" : "No" %><br />
                        
                        <br /><%= Ajax.ActionLink2((userItem.IsLockedOut ? "Unlock User" : "Lock User"), "LockUser", null,
                            new AjaxOptions()
                            {
                                HttpMethod = "Get",
                                InsertionMode = InsertionMode.Replace,
                                OnBegin = "function(e){ OnBeginLockUser(e, '" + userItem.UserID + "'); }",
                                OnSuccess = "function(){ OnSuccessLockUser('" + userItem.UserID + "'); }",
                                UpdateTargetId = "LockUser" + userItem.UserID
                            },
                             new { @id = "LockUser" + userItem.UserID })%>

                    </td>
                    <td>
                        <%= !string.IsNullOrEmpty(userItem.Address) ? "<div>" + userItem.Address + "</div>" : string.Empty%>
                        <div><%= !string.IsNullOrEmpty(userItem.City) ? userItem.City : string.Empty%><%= !string.IsNullOrEmpty(userItem.State) ? ", " + userItem.State : string.Empty%><%= !string.IsNullOrEmpty(userItem.PostalCode) ? " " + userItem.PostalCode : string.Empty%></div>
                        <%= !string.IsNullOrEmpty(userItem.Country) ? "<div>" + userItem.Country + "</div>" : string.Empty%>
                        <%= !string.IsNullOrEmpty(userItem.Phone) ? "<div style='white-space:nowrap;'>Home phone: " + userItem.Phone + "</div>" : string.Empty%>
                        <%= !string.IsNullOrEmpty(userItem.DayPhone) ? "<div style='white-space:nowrap;'>Work phone: " + userItem.DayPhone + "</div>" : string.Empty%>
                        <%= !string.IsNullOrEmpty(userItem.RefferedBy) ? "<div>Reffered by: " + userItem.RefferedBy + "</div>" : string.Empty%>
                    </td>
                    <td>
                        <div><%= userItem.SubscribeActivation == null ? "" : userItem.SubscribeActivation.Value.ToString("MM/dd/yyyy hh:mm:ss")%></div>
                        <br />
                        <% if (userItem.PlanID != 0)
                           { %>
                           <div id="UpdatePlanDates<%=userItem.UserID %>">
                           <%if (userItem.StartSubscribeDate != null){ %><div>Last Charge Date: <span id="StartSubscribeDate_<%=userItem.UserID %>"><%= userItem.StartSubscribeDate.Value.ToString("MM/dd/yyyy")%></span></div><%} %>
                           <%if (userItem.ChargeDay != null){ %><div>Charge Day: <span id="ChargeDay_<%=userItem.UserID %>"><%= userItem.ChargeDay%></span></div><%} %>
                           <%if (userItem.EndSubscribeDate != null){ %><div>Next Charge Date: <span id="ChargeDate_<%=userItem.UserID %>"><%= userItem.EndSubscribeDate.Value.ToString("MM/dd/yyyy")%></span></div><%} %>
                        <div>
                            <%=Ajax.ActionLink("Edit Charge Info", "EditPlanDates", new { user_id = userItem.UserID, start_subscribe_date = userItem.StartSubscribeDate, end_subscribe_date = userItem.EndSubscribeDate, charge_day=userItem.ChargeDay },
                                                                                              new AjaxOptions() { HttpMethod="Get", UpdateTargetId = "EditPlanDates" + userItem.UserID, OnSuccess = "function(){CreateAndOpenDialog('EditPlanDates" + userItem.UserID + "');}" })%>
                        </div>
                           </div>
                            <div id="<%= "EditPlanDates" + userItem.UserID %>" style="display:none;" title="Edit Charge Info"></div>
                        <%} %>
                    </td>
                    <td> <%= userItem.CreatedDate.ToString("MM/dd/yyyy hh:mm:ss")%> </td>
                    <td style="white-space:nowrap;">
                            <span id='<%= "CreditVal" + userItem.UserID %>'><%= userItem.Credits.ToString("N0")%> units</span><br />
                            <%=Ajax.ActionLink("Edit Units", "EditCredits", new { user_id = userItem.UserID, credits = userItem.Credits }, new AjaxOptions() { UpdateTargetId = "EditCredits" + userItem.UserID, OnSuccess = "function(){CreateAndOpenDialog('EditCredits"+userItem.UserID+"');}" })%>
                            <div id='<%= "EditCredits" + userItem.UserID %>' style="display:none;" title="Change units"></div>
                    </td>
                    <td>                         
                        <%= userItem.MembershipType +
						(userItem.IsDeclinedSubscribe ? ", declined" : string.Empty)+
						(userItem.IsSuspended ? ", suspended" : string.Empty) +
						(userItem.IsCancelSubscribe && userItem.EndSubscribeDate != null ? ", cancelation on " + userItem.EndSubscribeDate.Value.ToString("MM.dd.yyyy").Replace(".", "/") : "")%>
                    </td>
               
                     <td style="vertical-align:top;"> 

                        <div>
                            <a href="<%= Url.Action("EditUserInfo","User",  new { user_id = userItem.UserID }) %>">
                                Edit User Info
                            </a>
                        </div>
                        <div>
							<%=Html.ActionLink("View Files", "ClassActivityLog", new { user_id = userItem.UserID })%>
                        </div>
                        <div>                        
                            <a href="<%= Url.Action("Delete","User", new { user_id = userItem.UserID }) %>" onclick="return confirm('Are you sure to delete this user?');">
                                Delete User
                            </a>                        
                        </div>
                        <div>
                            <a href="<%= Url.Action("ViewShoppingTransactions", new { user_id = userItem.UserID }) %>">
                                View Transactions
                            </a>  
                        </div>
                        <div>
                            <a href="<%= Url.Action("ViewFiles","User",  new { user_id = userItem.UserID })%>">
                                Add Downloads
                            </a>
                        </div>
                        <div><%=Html.ActionLink("Change CC", "ChangeCreditCard", new { user_id = userItem.UserID })%></div>
                        <div> 
                            <%--<a href="<= Url.Action("EnterReturn","User",  new { user_id = userItem.UserID }) >">--%>
                                Enter Return
                            <%--</a> --%>
                        </div>
                        <div> 
                            <% if (userItem.PlanID != 0 && !userItem.IsCancelSubscribe)
                               { %>

                            <a href="<%= Url.Action("CancelMembership","User",  new { user_id = userItem.UserID }) %>" onclick="return confirm('Are you sure to cancel membership for this user?');">
                                Cancel Membership
                            </a> 
                            <% }
                               else
                               { %>
                               Cancel Membership
                            <%} %>
                        </div>
                        <div>
                            <%--<a href="<= Url.Action("PlaceOrder","User",  new { user_id = userItem.UserID }) >">--%>
                                Place Order
                            <%--</a> --%>
                        </div>
                        

                        <%--<ul>
                            <li><a href='#'>Edit User Info</a></li>
                            <li><a href='#type=all&showid=12345&detail=full'>View Files</a></li>
                            <li><a href='#showid=12345'>Delete User</a></li>
                            <li><a href='#showid=12345'>View Transactions</a></li>
                            <li><a href='#showid=12345&usertype=monthly&searchsubmitted=&searchtxt=&sortby='>Add Downloads</a></li>
                            <li><a href='chargeuser_return.php?sid=1sf91xeztaic30x&ucid=12345&usertype=monthly&searchsubmitted=&searchtxt=&sortby='>Enter Return</a></li>
                            <li><a href='#ucid=12345&usertype=monthly&searchsubmitted=&searchtxt=&sortby='>Charge User</a></li>                            
                            <li><a href='javascript:cancelmembership(12345)'>Cancel Membership</a></li>
                            <li><a href='orders/placeOrder.php?sid=1sf91xeztaic30x&uid=12345'>Place Order</a></li> 
                        </ul>--%>
                    </td>
                </tr>
            <%}%>
            </tbody>
        </table>

        <div style="margin-top:-70px">
            <% Html.RenderPartial("Pager", ViewData["Pager"]); %>
        </div>
    </div>
    <%}
  else
  { %>
  <h2>No records found</h2>
    <%} %>
<div id="dialog" style="display:none;">

</div>

<script type="text/javascript">

    function OpenDialog(id) {
        $('#'+id).dialog('open');
        return false;
    }

    function CreateAndOpenDialog(id) {
        var container = $('#' + id);
        $(".datepicker", container).datepick({ dateFormat: 'mm/dd/yy' });
        container.dialog({
            width: 600,
            autoOpen: false,
            modal: true,
            close: function (event, ui) {
                $(".datepicker", $(this)).datepick('destroy');
            }
        }).dialog('open');
    }


</script>

</asp:Content>
