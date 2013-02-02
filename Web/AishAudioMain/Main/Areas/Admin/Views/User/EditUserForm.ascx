<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Main.Areas.Admin.Models.User.UserEditModel>" %>
<script type="text/javascript">

    $(document).ready(function () {

        $("#AffiliateStartDate").datepick({ dateFormat: 'mm/dd/yy' })
        $("#StartDate").datepick({ dateFormat: 'mm/dd/yy' })
        $("#PaidThrough").datepick({ dateFormat: 'mm/dd/yy' })

    });

</script>
<%if (Model.PortalID.HasValue)
  { %>
<%=Html.HiddenFor(m=>m.PortalID) %>
<%} %>
<div id="tabs-1">
    <ul>
        <li>
            <%--     <div class="editform-twocol-leftcol">
                <label class="desc" for="Username">Username</label>
                <div class="editor-field"><%= Html.TextBox("Username", Model.UserName, new { @class = "field text full" }) %></div>
            </div>--%>
            <div class="editform-twocol-leftcol">
                <label class="desc" for="Email">
                    Email Address:</label>
                <div class="editor-field">
                    <%= Html.TextBox("Email", Model.Email, new { @class = "field text full", tabindex = 1 })%></div>
            </div>
            <div class="editform-twocol-rightcol">
                <label class="desc" for="FirstName">
                    First Name:</label>
                <div class="editor-field">
                    <%= Html.TextBox("FirstName", Model.FirstName, new { @class = "field text full", tabindex = 4 })%></div>
            </div>
        </li>
        <li>
            <div class="editform-twocol-leftcol">
                <label class="desc" for="Password">
                    Password:</label>
                <div class="editor-field">
                    <%= Html.Password("Password", Model.Password, new { @class = "field text full", tabindex = 2 })%></div>
            </div>
            <div class="editform-twocol-rightcol">
                <label class="desc" for="LastName">
                    Last Name:</label>
                <div class="editor-field">
                    <%= Html.TextBox("LastName", Model.LastName, new { @class = "field text full", tabindex = 5 })%></div>
            </div>
        </li>
        <li>
            <div class="editform-twocol-leftcol">
                <label class="desc" for="ConfirmPassword">
                    Confirm Password:</label>
                <div class="editor-field">
                    <%= Html.Password("ConfirmPassword", Model.ConfirmPassword, new { @class = "field text full", tabindex = 3 })%></div>
            </div>
            <div class="editform-twocol-rightcol">
                <label class="desc" for="City">
                    City:</label>
                <div class="editor-field">
                    <%= Html.TextBox("City", Model.City, new { @class = "field text full", tabindex = 6 })%></div>
            </div>
        </li>
        <li>
            <div class="editform-twocol-leftcol">
                <label class="desc" for="Balance">
                    Units</label>
                <div class="editor-field">
                    <%= Html.TextBox("Balance", Model.Balance.ToString("#0,#"), new { @class = "field text full", tabindex = 1 })%></div>
            </div>
            <div class="editform-twocol-rightcol">
                <label class="desc" for="PostalAddress">
                    Postal Address:</label>
                <div class="editor-field">
                    <%= Html.TextBox("Address", Model.Address, new { @class = "field text full", tabindex = 1 })%></div>
            </div>
        </li>
        <li>
            <div class="editform-twocol-leftcol">
                <label class="desc" for="Zip">
                    Zip or Postal Code:</label>
                <div class="editor-field">
                    <%= Html.TextBox("PostalCode", Model.PostalCode, new { @class = "field text full" })%>
                </div>
                <label class="desc" for="Username">
                    State or Province:</label>
                <div class="editor-field">
                    <%=Html.DropDownListFor(c => c.State, new SelectList(Main.GlobalConstant.STATES, "Key", "Value", Model.State), 
                        "Select state", new { @class = "field select full" })%>
                </div>
                <label class="desc" for="Username">
                    Country:</label>
                <div class="editor-field">
                    <%=Html.DropDownListFor(c => c.Country, new SelectList(Main.GlobalConstant.COUNTRIES, "Key", "Value", Model.Country), 
                        "Select Country", new { @class = "field select full" })%>
                </div>
            </div>
            <div class="editform-twocol-rightcol">
                <label class="desc" for="Description">
                    Other:</label>
                <div class="editor-field">
                    <%= Html.TextArea("Description", Model.Description, new { @class = "field text full", rows = "6" })%></div>
            </div>
        </li>
        <li>
            <div class="editform-twocol-leftcol">
                <label class="desc" for="Phone">
                    Phone Number:</label>
                <div class="editor-field">
                    <%= Html.TextBox("Phone", Model.Phone, new { @class = "field text full" })%></div>
            </div>
            <div class="editform-twocol-rightcol">
                <label class="desc" for="ReferralCode">
                    Referral Code ("rc" code, if any):</label>
                <div class="editor-field">
                    <%= Html.TextBox("ReferrerCode", Model.ReferrerCode, new { @class = "field text full" })%></div>
            </div>
        </li>
        <li>
            <%--            <div class="editform-twocol-leftcol">
                <label class="desc" for="ReferredBy">Referred by:</label>
                <div class="editor-field"><%= Html.TextBox("ReferredBy", Model.ReferredBy, new { @class = "field text full" })%></div>
 
            </div>
            <div class="editform-twocol-rightcol">
            --%>
            <div>
                <label class="desc" for="DayPhone">
                    Day Phone:</label>
                <div class="editor-field">
                    <%= Html.TextBox("DayPhone", Model.DayPhone, new { @class = "field text small" })%></div>
            </div>
        </li>
        <%--        <li>
            <div>
                <label class="desc" for="ReferralCode">Referral Code:<br/>("rc" code, if any)</label>
                <div class="editor-field"><%= Html.TextBox("ReferrerCode", Model.ReferrerCode, new { @class = "field text small" })%></div>
            </div>
        </li>
        --%><%--        <li>
            <div>
                <label class="desc" for="Username">Affiliate:</label>
                <div class="editor-field"><select NAME="affiliateID" class="field select small"> 
                    <option value="">NONE</option> 
                    <option value='4'>Aishaudio.com
                    <option value='3'>test 3
                    <option value='1'>Test Affiliate
                    <option value='2'>Test Affiliate 2
    	        </select></div>
            </div>
        </li>
        <li>
            <div>
                <label class="desc" for="AffiliateStartDate">Affiliate Start Date:</label>
                <div class="editor-field">
                    <%= Html.TextBox("AffiliateStartDate", null, new { @class = "field text small" })%>
                </div>    
            </div>
        </li>--%>
        <li>
            <div>
                <label class="desc" for="FreeOfferCnt">
                    Free offer count:</label>
                <div class="editor-field">
                    <%= Html.TextBox("FreeOfferCnt", Model.FreeOfferCnt, new { @class = "field text small" })%>
                </div>
            </div>
        </li>
        <li>
            <div>
                <label class="desc" for="plan">
                    Plan Type:</label>
            </div>
        </li>
        <li><input type="radio"<%=Model.PlanID == 0 ? " checked=\"checked\" " : "" %> value="0" name="PlanID" id="PlanID0" style='float:left' /><label for="PlanID0" style="float:left;clear:none;margin-left:10px;">None</label></li>
        <li><input type="radio"<%=Model.PlanID == 237 ? " checked=\"checked\" " : "" %> value="237" name="PlanID" id="PlanID237" style='float:left' /><label for="PlanID237" style="float:left;clear:none;margin-left:10px;">Monthly (first month free)</label></li>
        <li><input type="radio"<%=Model.PlanID == 238 ? " checked=\"checked\" " : "" %> value="238" name="PlanID" id="PlanID238" style='float:left' /><label for="PlanID238" style="float:left;clear:none;margin-left:10px;">Monthly</label></li>
        <li><span><input type="radio"<%=Model.PlanID > 238 ? " checked=\"checked\" " : "" %> value="<%=Model.PlanID>238?Model.PlanID:int.MaxValue %>" name="PlanID" id="PlanCustom" style='float:left' /><label for="PlanCustom" style="float:left;clear:none;margin-left:10px;">Prepaid by</label></span>
            <span><%=Html.TextBoxFor(m=>m.PrepaidPlanMonths) %><%=Html.LabelFor(m => m.PrepaidPlanMonths)%></span>
            <span><%=Html.TextBoxFor(m=>m.PrepaidPlanUnitsPerMonth) %><%=Html.LabelFor(m => m.PrepaidPlanUnitsPerMonth)%></span>
        </li>


<%--        <%if (Model != null && Model.PlansList != null && Model.PlansList.Count > 0)
          {
              foreach (var plan in Model.PlansList)
              {
        %>
        <li>
            <div>
                <%= String.Format("<input type=\"radio\" " + (Model.PlanID == plan.Key ? "checked=\"checked\" " : "") + "value=\"{0}\" class=\"field radio\" name=\"{1}\" id=\"{2}\"><label for=\"{2}\">{3}</label>",
                                    plan.Key, "PlanID", "PlanID", plan.Value)
                       
                %>
            </div>
        </li>
        <% }
               } %>--%>
        <li>
            <div>
                <label class="desc" for="fullLibraryAccess">
                    All files available:</label>
                <div class="editor-field">
                    <%= Html.CheckBox("fullLibraryAccess", (Model.FullLibraryAccess != null ? Model.FullLibraryAccess : false)) %>
                </div>
            </div>
        </li>
        <li>
            <div>
                <label class="desc" for="StartDate">
                    Start Date:</label>
                <div class="editor-field">
                    <%= Html.TextBox("StartDate", (Model.StartDate != null ? Model.StartDate : DateTime.Now.ToString("MM.dd.yyyy")).Replace(".", "/"), new { @class = "field text small" })%>
                </div>
            </div>
        </li>
<%--        <li>
            <div>
                <label class="desc" for="PaidThrough">
                    Paid Through:</label>
                <div class="editor-field">
                    <%= Html.TextBox("PaidThrough", Model.PaidThrough != null ? Model.PaidThrough : "", new { @class = "field text small" })%>
                </div>
            </div>
        </li>
--%>    </ul>
    <div class="response-msg notice ui-corner-all">
        <span>Note that the day number of the end date should be one day LESS than the start
            date.</span> For example, if start date is September 3, the end date should
        be October 2.
    </div>
</div>
<div id="tabs-2">
    <ul>
        <li>
            <div>
                <label class="desc">
                    Select role:
                </label>
            </div>
        </li>
        <% if (Model != null && Model.Roles != null && Model.Roles.Count > 0)
           {
               foreach (var role in Model.Roles)
               {
        %>
        <li>
            <div>
                <%= String.Format("<input type=\"checkbox\" " + (Model.UserRoles.Contains(role) ? "checked=\"checked\" " : "") + "value=\"{0}\" class=\"field checkbox\" name=\"{1}\" id=\"{2}\"><label for=\"{2}\">{3}</label>",
                                                                            role, "UserRoles", "UserRoles_" + role, role)
                       
                %>
            </div>
        </li>
        <% }
               }%>
    </ul>
</div>
