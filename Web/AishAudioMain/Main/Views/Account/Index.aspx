<%@ Page Title="" Language="C#" MasterPageFile="../Shared/twoColumn.Master"
    Inherits="System.Web.Mvc.ViewPage<Main.Models.ControllerView.Account.AccountInfo>" %>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <style type="text/css">
        /*	FOR my_account	*/
        
        #top_info
        {
            margin-bottom: 25px;
        }
        #top_info div
        {
            width: 560px;
            padding-top: 3px;
            padding-bottom: 3px;
            font-family: verdana;
            font-size: 12px;
            color: #4F77B4;
            font-weight: bold;
            text-indent: 220px;
        }
        #top_info span
        {
            display: block;
            width: 560px;
            height: 3px;
            font-size: 2px;
            background-image: url(<%= Url.Image("spacer.gif") %>);
        }
        #plans
        {
            margin-top: 15px;
            margin-left: 15px;
            margin-bottom: 10px;
        }
        #options
        {
            margin-top: 15px;
            margin-left: 15px;
            margin-bottom: 40px;
        }
        
        /* account page */
        .myLibraryArchivesblueText
        {
            color: #2453a1; /*blue*/
            font-family: tacoma, arial, helvetica;
            font-weight: bold;
            font-size: 12px;
            text-decoration: none;
        }
        
        a.myLibraryArchivesblueText:link
        {
            color: #2453a1;
            text-decoration: none;
        }
        a.myLibraryArchivesblueText:visited
        {
            color: #2453a1;
            text-decoration: none;
        }
        a.myLibraryArchivesblueText:hover
        {
            color: #C0203C;
            text-decoration: none;
        }
        
        .myLibraryArchivesblueTextU
        {
            color: #2453a1; /*blue*/
            font-family: tacoma, arial, helvetica;
            font-weight: bold;
            font-size: 12px;
            text-decoration: underline;
        }
        
        a.myLibraryArchivesblueTextU:link
        {
            color: #2453a1;
            text-decoration: underline;
        }
        a.myLibraryArchivesblueTextU:visited
        {
            color: #2453a1;
            text-decoration: underline;
        }
        a.myLibraryArchivesblueTextU:hover
        {
            color: #C0203C;
            text-decoration: underline;
        }
        
        .blueheaderbackground
        {
            background-color: #C9DDF8;
            color: #2752A1;
        }
        
        .myLibraryArchivesbluePrevNext
        {
            color: #669acc; /*blue*/
            font-family: tacoma, arial, helvetica; /*font-weight:bold;*/
            font-size: 12px;
            text-decoration: none;
        }
        
        .libraryblueboldsmall
        {
            /*color:#C9DDF8; very light blue*/
            color: #406CAD;
            font-family: arial, helvetica;
            font-weight: bold;
            font-size: 11px;
        }
        a.libraryblueboldsmall:link
        {
            color: #406CAD;
            text-decoration: underline;
        }
        a.libraryblueboldsmall:visited
        {
            color: #406CAD;
            text-decoration: underline;
        }
        a.libraryblueboldsmall:hover
        {
            color: #406CAD;
            text-decoration: underline;
        }
        
        .search_big_blue_rabbitext
        {
            font-family: verdana;
            font-style: italic;
            font-size: 24px;
        }
        
        .resultstableblack
        {
            font-family: verdana, tahoma, arial, helvetica;
            font-size: 11px;
        }
        
        .resultstablebluelink
        {
            /*color:#C9DDF8; very light blue*/
            color: #406CAD;
            font-family: verdana, tahoma, arial, helvetica;
            font-weight: bold;
            font-size: 11px;
        }
        a.resultstablebluelink:link
        {
            color: #406CAD;
            text-decoration: underline;
        }
        a.resultstablebluelink:visited
        {
            color: #406CAD;
            text-decoration: underline;
        }
        a.resultstablebluelink:hover
        {
            color: #406CAD;
            text-decoration: underline;
        }
        
        .account-info-table td
        {
            padding-bottom:4px;
        }
    </style>
    <table cellspacing="0" cellpadding="5" border="0" width="100%">
        <tbody>
            <tr>
                <td>
                    <div style="font-weight: bold;">
                        <div class="red">
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" width="580" class="account-info-table">
                            <tr valign="top">
                                <td>
                                    <a href="<%= Url.RouteUrl("IPodOffer") %>" style="text-decoration: none;">
                                        <img height="52" border="0" width="166" src='<%= Url.Image("ipod_166X52.jpg") %>' />
                                    </a>
                                    <img height="8" width="15" src='<%= Url.Image("spacer.gif") %>' alt="" />
                                    <a href="<%= Url.RouteUrl("FreeDownloads") %>" style="text-decoration: none;">
                                        <img height="52" border="0" width="166" src='<%= Url.Image("2_free_166X52.jpg") %>' />
                                    </a>
                                    <img height="9" width="15" src='<%= Url.Image("spacer.gif") %>' alt="" />
                                    <a href="<%= Url.RouteUrl("FreeMP3") %>" style="text-decoration: none;">
                                        <img height="52" width="166" src='<%= Url.Image("Free_mp3_166X52.jpg") %>' />
                                    </a>
                                    <br /><br />
                                        <img height="2" width="551" src='<%= Url.Image("line.jpg") %>' />
                                    <br /><br />
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td>
                                    <div id="top_info">
                                        <img src='<%= Url.Image("top.jpg") %>' alt="" /><br />
                                        <img src='<%= Url.Image("first_space.jpg") %>' alt="" /><br />
                                        <span>&nbsp;</span>
                                        <div style="background-image: url(<%= Url.Image("user_name.jpg") %>);">
                                            <%= Model.UserName %></div>
                                        <span>&nbsp;</span>
                                        <div style="background-image: url(<%= Url.Image("membership_type.jpg") %>)">
                                            <%= Model.SubscribePlanName %></div>
                                        <span>&nbsp;</span>
                                        <div style="background-image: url(<%= Url.Image("date_membership.jpg") %>)">
                                            <%= Model.DateMembershipBegan.HasValue ? Model.DateMembershipBegan.Value.ToString("MMMM dd, yyyy", System.Globalization.CultureInfo.InvariantCulture) : "&nbsp;" %>
                                            </div>
                                        <span>&nbsp;</span>
                                        <div style="background-image: url(<%= Url.Image("units_remaining.jpg") %>)">
                                            <%= Model.UnitsCount.ToString("N0") %></div>
                                        <span>&nbsp;</span>
                                        <img src='<%= Url.Image("last_space.jpg") %>' alt="" />
                                    </div>

                                    <img src='<%= Url.Image("line.jpg") %>' alt="" /><br />
                                    <div id="plans">
                                        <img src='<%= Url.Image("upgrade.jpg") %>' alt="" />
                                        <div style="font-size: 2px; height: 5px;">
                                            &nbsp;
                                        </div>
                                        <a href="<%= Model.SubscribePlanName.StartsWith("Monthly", StringComparison.CurrentCultureIgnoreCase) ? "#" : Url.RouteUrl("Offerings") %>">
                                            <img border="0" name="economical_download" id="economical_download" src='<%= Url.Image("economical_download.jpg") %>' alt="" />
                                        </a>
                                        <div style="font-size: 2px; height: 2px;">
                                            &nbsp;
                                        </div>
                                        <a href="<%= Url.RouteUrl("IPodOffer") %>">
                                            <img border="0" name="free_ipod" id="free_ipod" src='<%= Url.Image("free_ipod.jpg") %>' alt="" />
                                        </a>
                                        <div style="font-size: 2px; height: 2px;">
                                            &nbsp;</div>
                                        <a href="<%= Url.RouteUrl("FreeMP3") %>">
                                            <img border="0" name="free_mp3" id="free_mp3" src='<%= Url.Image("free_mp3.jpg") %>' alt="" />
                                        </a>
                                    </div>
                                    <img src='<%= Url.Image("line.jpg") %>' alt="" />
                                </td>
                                <td width="2" rowspan="2">
                                    <img height="18" width="8" src='<%= Url.Image("spacer.gif") %>' alt="" />
                                </td>
                            </tr>
                            <tr>
                                <td width="95%" valign="top">
                                </td>
                            </tr>
                            <% if (Model.IsCancelSubscribe)
                               { %>
                               <tr>
                                    <td>

                                        <% if (Model.CancelledOnDate != null)
                                           { %>
                                        <div class="response-msg inf ui-corner-all" style="margin-top: 30px;">
                                            <span>Your membership was cancelled on <%= Model.CancelledOnDate.HasValue ? Model.CancelledOnDate.Value.ToString("MMMM dd, yyyy", System.Globalization.CultureInfo.InvariantCulture) : "&nbsp;"%>.</span>
                                            <ul>
                                                <li>You can continue downloading until your current month is up or until you have used all your units.</li>
                                            </ul>
                                        </div>
                                        <%} %>
                                    </td>
                               </tr>
                               <tr>
                                 <td style="line-height:1.3;font-size:10px">
                            <%}
                               else
                               { %>

                            <tr>
                                <td style="line-height:1.3;font-size:10px">
                                <% if ((bool)(this.ViewData["IsUserHaveSubscribePlan"] ?? false))
                                   { %>
                                    <div>                                    
                                        <a class="myLibraryArchivesblueTextU" href="<%= Url.Action("CancelSubscribe", "Shopping")%>">Cancel
                                            Membership</a><br /><font size="1">(you will continue to be able to use your username and
                                                password and can purchase units as needed for $3.50 each)</font><br /><br />
                                    </div>
                                <% }
                               } %>


                                <% if (Model.FreeOffersCnt > 0)
                                   { %>
                                <div>
                                    <a class="myLibraryArchivesblueTextU" href="<%= Url.Action("FreeOffer", "Shopping")%>">
                                        Click here to choose your <%= Model.FreeOffersCnt %> free download<%= Model.FreeOffersCnt > 1 ? "s" : "" %></a><br /><br />
                                </div>
                                <% } %>

                                <% if ((bool)(this.ViewData["IsUserHaveSubscribePlan"] ?? false))
                                   { %>
                                <div class="myLibraryArchivesblueTextU" style="text-decoration:none;">
                                    Purchase units for $1.00 each (to add units to cart, choose the desired quantity):<br />
                                    <form style="margin: 0pt;" action="" method="post" name="unitsform">
                                        <%=Html.DropDownList("quantity", new SelectList((Dictionary<long, string>)ViewData["UnitsQuantity"], "Key", "Value", 5),
                                            "--Number of Units--", new { @class = "field select full", style = "font-size: 10px;", onchange = "ChangeUnitsCnt(this)" })%>                        
                                    </form>
                                    <br /><br />
                                </div>
                                        
                                <% } %>

                                    <div>                                        
                                        <a class="myLibraryArchivesblueTextU" href="<%= Url.Action("ShoppingTransactions", "Account") %>">
                                            View Current or Past Orders
                                        </a><br /><br />
                                    </div> 
                                    <div>                                
                                        <a class="myLibraryArchivesblueTextU" href="<%= Url.Action("Profile", "Account") %>">
                                            Change Account Information
                                        </a><br /><br />
                                    </div>

                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <br />
                                    <br />
                                    <br />
                                    <img height="55" width="363" src='<%= Url.Image("valued_membership.jpg") %>' />
                                </td>
                            </tr>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
    <script type="text/javascript">
        function ChangeUnitsCnt(sel) {
            //alert(sel.options[sel.selectedIndex].value);
            window.location = '/shopping/OrderUnits?unitsID=' + sel.options[sel.selectedIndex].value;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>
