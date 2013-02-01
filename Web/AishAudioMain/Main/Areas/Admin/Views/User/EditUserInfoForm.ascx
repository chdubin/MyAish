<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Main.Areas.Admin.Models.User.UserEditModel>" %>

<script type="text/javascript">

    $(document).ready(function () {

        $("#StartDate").datepick({ dateFormat: 'mm/dd/yy' })
        $("#PaidThrough").datepick({ dateFormat: 'mm/dd/yy' })

    });

</script>


    <%= Html.Hidden("UserID", Model.UserID) %>
<%--    <%= Html.Hidden("Email", Model.Email)%>
    <%= Html.Hidden("Password", Model.Password)%>
    <%= Html.Hidden("ConfirmPassword", Model.Password)%>
--%>    
<div id="tabs-1">
    <ul>
        <li>
            <div class="editform-twocol-leftcol">
                <label class="desc" for="UserName">Username:</label>
            </div>
            <div class="editform-twocol-rightcol">
                <div class="editor-field"><%= Html.TextBox("UserName", Model.UserName, new { @class = "field text medium", tabindex = 1 })%></div>
            </div>
        </li>

        <li>
            <div class="editform-twocol-leftcol">
                <label class="desc" for="Password">Password:</label>
            </div>
            <div class="editform-twocol-rightcol">
                <div class="editor-field"><%= Html.Password("Password", null, new { @class = "field text medium", tabindex = 2 })%></div>
            </div>
        </li>

        <li>
            <div class="editform-twocol-leftcol">
                <label class="desc" for="Password">Confirm password:</label>
            </div>
            <div class="editform-twocol-rightcol">
                <div class="editor-field"><%= Html.Password("ConfirmPassword", null, new { @class = "field text medium", tabindex = 2 })%></div>
            </div>
        </li>

        <li>
            <div class="editform-twocol-leftcol">
                <label class="desc" for="Email">Email Address:</label>
            </div>
            <div class="editform-twocol-rightcol">
                <div class="editor-field"><%= Html.TextBox("Email", Model.Email, new { @class = "field text medium", tabindex = 1 })%></div>
            </div>
        </li>

        <li>&nbsp;</li>

        <li>
            <div class="editform-twocol-leftcol">
                <label class="desc" for="FirstName">First Name:</label>
            </div>
            <div class="editform-twocol-rightcol">
                <div class="editor-field"><%= Html.TextBox("FirstName", Model.FirstName, new { @class = "field text medium" })%></div>
            </div>
        </li>
        <li>
            <div class="editform-twocol-leftcol">
                <label class="desc" for="LastName">Last Name:</label>
            </div>
            <div class="editform-twocol-rightcol">
                <div class="editor-field"><%= Html.TextBox("LastName", Model.LastName, new { @class = "field text medium" })%></div>
            </div>
        </li>

        <li>&nbsp;</li>

        <li>
           <div class="editform-twocol-leftcol">
                <label class="desc" for="Address">Address:</label>
            </div>
            <div class="editform-twocol-rightcol">
                <div class="editor-field"><%= Html.TextBox("Address", Model.Address, new { @class = "field text medium" })%></div>
           </div> 
        </li>

        <li>
            <div class="editform-twocol-leftcol">
                <label class="desc" for="City">City:</label>
            </div>
            <div class="editform-twocol-rightcol">
                <div class="editor-field"><%= Html.TextBox("City", Model.City, new { @class = "field text medium" })%></div>
            </div>
        </li>
        <li>
            <div class="editform-twocol-leftcol">
                <label class="desc" for="State">State or Province:</label>
            </div>
            <div class="editform-twocol-rightcol">
                <div class="editor-field">
                    <%=Html.DropDownListFor(c => c.State, new SelectList(Main.GlobalConstant.STATES, "Key", "Value", Model.State),
                                                                        "Select state", new { @class = "field select medium" })%>                               

                </div>
            </div>
        </li>
        <li>
            <div class="editform-twocol-leftcol">
                <label class="desc" for="PostalCode">Zip or Postal Code:</label>
            </div>
            <div class="editform-twocol-rightcol">
                <div class="editor-field"><%= Html.TextBox("PostalCode", Model.PostalCode, new { @class = "field text medium" })%></div>
            </div>
        </li>
        <li>
            <div class="editform-twocol-leftcol">
                <label class="desc" for="Country">Country:</label>
            </div>
            <div class="editform-twocol-rightcol">
                <div class="editor-field">                  
                    <%=Html.DropDownListFor(c => c.Country, new SelectList(Main.GlobalConstant.COUNTRIES, "Key", "Value", Model.Country),
                                                "Select Country", new { @class = "field select medium" })%>                               
                </div>
            </div>
        </li>

        <li>&nbsp;</li>

        <li>
            <div class="editform-twocol-leftcol">
                <label class="desc" for="Phone">Home Phone:</label>
            </div>
            <div class="editform-twocol-rightcol">
                <div class="editor-field"><%= Html.TextBox("Phone", Model.Phone, new { @class = "field text medium" })%></div>
            </div>
        </li>
        <li>
            <div class="editform-twocol-leftcol">
                <label class="desc" for="DayPhone">Work Phone:</label>
            </div>
            <div class="editform-twocol-rightcol">
                <div class="editor-field"><%= Html.TextBox("DayPhone", Model.DayPhone, new { @class = "field text medium" })%></div>
            </div>
        </li>
        
        <li>&nbsp;</li>

        <li>
            <div class="editform-twocol-leftcol">
                <label class="desc" for="Description">Other:</label>
            </div>
            <div class="editform-twocol-rightcol">
                <div class="editor-field"><%= Html.TextArea("Description", Model.Description, new { @class = "field textarea medium", rows = "6" })%></div>            
            </div>
        </li>

        <li>&nbsp;</li>

        <li>
            <div class="editform-twocol-leftcol">
                <label class="desc" for="Balance">Units</label>
            </div>
            <div class="editform-twocol-rightcol">
                <div class="editor-field"><%= Html.TextBox("Balance", Model.Balance.ToString("F2"), new { @class = "field text medium" })%></div>
            </div>
        </li>
        <li>
            <div class="editform-twocol-leftcol">
                <label class="desc" for="ReferralCode">Referral Code ("rc" code, if any):</label>
            </div>
            <div class="editform-twocol-rightcol">
                <div class="editor-field"><%= Html.TextBox("ReferrerCode", Model.ReferrerCode, new { @class = "field text medium" })%></div>
            </div>
        </li>
        <li>
            <div class="editform-twocol-leftcol">
                <label class="desc" for="FreeOfferCnt">Free Downloads Count:</label>
            </div>
            <div class="editform-twocol-rightcol">
                <div class="editor-field">
                    <%= Html.TextBox("FreeOfferCnt", Model.FreeOfferCnt, new { @class = "field text medium" })%>
                </div>    
            </div>
        </li>
        <li>
            <div class="editform-twocol-leftcol">
                <label class="desc" for="Suspended">Has unrestricted library access?</label>
            </div>
            <div class="editform-twocol-rightcol">
                <div class="editor-field">
                    <%= Html.CheckBox("FullLibraryAccess", Model.FullLibraryAccess, new { @class = "field checkbox" })%>
                </div>    
            </div>
        </li>                   
        <li>
            <div class="editform-twocol-leftcol">
                <label class="desc" for="Suspended">Suspended:</label>
            </div>
            <div class="editform-twocol-rightcol">
                <div class="editor-field">
                    <%= Html.CheckBox("Suspended", Model.Suspended, new { @class = "field checkbox" })%>
                </div>    
            </div>
        </li>                 
        <li>
            <div class="editform-twocol-leftcol">
                <label class="desc" for="plan">Plan Type:</label>
            </div>
            <div class="editform-twocol-rightcol">
                <ul>
                <li><input type="radio"<%=Model.PlanID == 0 ? " checked=\"checked\" " : "" %> value="0" name="PlanID" id="PlanID0" style='float:left' /><label for="PlanID0" style="float:left;clear:none;margin-left:10px;">None</label></li>
                <li><input type="radio"<%=Model.PlanID == 237 ? " checked=\"checked\" " : "" %> value="237" name="PlanID" id="PlanID237" style='float:left' /><label for="PlanID237" style="float:left;clear:none;margin-left:10px;">Monthly (first month free)</label></li>
                <li><input type="radio"<%=Model.PlanID == 238 ? " checked=\"checked\" " : "" %> value="238" name="PlanID" id="PlanID238" style='float:left' /><label for="PlanID238" style="float:left;clear:none;margin-left:10px;">Monthly</label></li>
                <li><span><input type="radio"<%=Model.PlanID > 238 ? " checked=\"checked\" " : "" %> value="<%=Model.PlanID>238?Model.PlanID:int.MaxValue %>" name="PlanID" id="PlanCustom" style='float:left' /><label for="PlanCustom" style="float:left;clear:none;margin-left:10px;">Prepaid by</label></span>
                <span><%=Html.TextBoxFor(m=>m.PrepaidPlanMonths) %><%=Html.LabelFor(m => m.PrepaidPlanMonths)%></span>
                <span><%=Html.TextBoxFor(m=>m.PrepaidPlanUnitsPerMonth) %><%=Html.LabelFor(m => m.PrepaidPlanUnitsPerMonth)%></span>
                </li>
<%--
                <% foreach (var plan in Model.PlansList)
                   {
                %>
                <%= String.Format("<li><input type='radio' " + (Model.PlanID == plan.Key ? "checked='checked' " : "") + "value='{0}' class='field radio' name='{1}' id='{2}' style='float:left'><label for='{2}' style='float:left;clear:none;margin-left:10px;'>{3}</label></li>",
                                        plan.Key, "PlanID", "PlanID", plan.Value)
                       
                %>
                <% } %>--%>
                </ul>
            </div>
        </li>
        <li>
            <div class="editform-twocol-leftcol">
                <label class="desc" for="StartDate">Start Date:</label>
            </div>
            <div class="editform-twocol-rightcol">
                <div class="editor-field">
                    <%= Html.TextBox("StartDate", Model.StartDate != null ? Model.StartDate : "", new { @class = "field text small" })%>
                </div>
            </div>
        </li>
<%--        <li>
            <div class="editform-twocol-leftcol">
                <label class="desc" for="PaidThrough">Paid Through:</label>
            </div>
            <div class="editform-twocol-rightcol">
                <div class="editor-field">
                    <%= Html.TextBox("PaidThrough", Model.PaidThrough != null ? Model.PaidThrough : "", new { @class = "field text small" })%>
                </div>
            </div>
        </li>
--%>    </ul>
    <div class="response-msg notice ui-corner-all">
	    <span>Note that the day number of the end date should be one day LESS than the start date.</span>
	    For example, if start date is September 3, the end date should be October 2.
    </div>
</div>

<%--<div id="tabs-2"> 
</div>--%>

<div id="tabs-2">
    <ul>
        <li>
            <div>
                <label class="desc">
                    Select role:
                </label>
            </div>
        </li>


        <% foreach (var role in Model.Roles)
            {
        %>
        <li>
            <div>            

                <%= String.Format("<input type='checkbox' " + (Model.UserRoles.Contains(role) ? "checked='checked' " : "") + "value='{0}' class='field checkbox' name='{1}' id='{2}' style='float:left;'><label for='{2}' style='float:left;clear:none;margin-left:10px;'>{3}</label>",
                                                                            role, "UserRoles", "UserRoles_" + role, role)
                       
                    %>
            </div>
        </li>
        <% } %>
    </ul>
</div>
