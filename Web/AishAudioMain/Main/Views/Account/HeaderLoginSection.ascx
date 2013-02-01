<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Main.Models.LogOnModel>" %>

<% var user = (MainEntity.Models.User.Membership)ViewData["MembershipUser"]; %>

<% if (user == null)
   {
       using (Html.BeginForm("subscribenews", "Home", FormMethod.Post, new { @id = "subscribe" }))
       { %>
        <fieldset>
            <legend></legend>
            <label for="join">
                Join our weekly e-mail - <a href="#">See example</a></label>
			<%=Html.TextBox("Email", "your email address", new { onclick = "if($(this).val()=='your email address')$(this).val('')" })%>
            <button type="button" onclick="JoinWeeklyEmail(this)">
                OK</button>
        </fieldset>
    <%} %>


    <script type="text/javascript">

        function JoinWeeklyEmail(btn) {
            var email = $(btn).prev().val();

            if (!(email.length > 0 && /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}/.test(email))) {
                alert("Incorrect email");
            }
            else {
                $(btn).parents("form").first().submit();
            }
        }

        function ToggleHeaderLogin() {
            $('#login_new').toggleClass('hidden');
            $('#topMenu').toggleClass('hidden');
            $('#subscribe').toggleClass('hidden');
        }

        function onPressKey(evt, obj) {
            evt = (evt) ? evt : event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode == 13) {                
                $(obj).parents("form").submit();
            }
        }

    </script>


    <ul id="topMenu">                    
        <li><a href="<%= Url.RouteUrl("HDRWhatIsAishAudioPage") %>">What is AishAudio?</a> </li>
        <li class="login"><a href="javascript:void(0)" onclick="ToggleHeaderLogin();">Members Login</a> </li>
    </ul>

    <div id="login_new" class="hidden">
        <div class="loginsection">
            <%
                using (Html.BeginForm("LogOn", "Account", new { returnUrl = HttpContext.Current.Request.Url.PathAndQuery }, FormMethod.Post, new { @class = "forms", enctype = "multipart/form-data" }))
                {
            %> 
                <table border="0" width="293">
                    <tr>
                        <td width="103">Username</td>
                        <td colspan="2">Password</td>
                    </tr>
                    <tr>
                        <td>
                            <%= Html.TextBox("UserName", Model.UserName, new { maxlength="75", size="15" }) %>
                        </td>
                        <td width="98">
                            <%= Html.Password("Password", Model.Password, new { maxlength = "75", size = "15", onkeypress = "onPressKey(event, this)" })%>
                        </td>
                        <td width="78" valign="bottom" style="padding:3px;">
                            <input type="image" style="width:17px;height:18px;" src='<%= Url.Image("arrow_top.jpg") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td height="21" align="left" colspan="3">                                        
                            <a href="<%= Url.Action("ForgotPassword", "Account")%>">Forgot Password</a>
                        </td>
                    </tr>
                </table>

                <div style="display:none">
                    <input type="text" name="hiddenText"/>
                </div>     	                
            <% } %>
                        
        </div>
    </div>

    <div style="position:absolute;left: 100px; top: 52px;"> 
        <%
            Html.RenderPartial("~/Views/Shopping/HeaderCartSection.ascx"); 
        %>
    </div>

<% }
   else
   { %>
    
    <div id="toplogintxt2">
        <table border="0" width="262" id="toplogintxt3">
            <tr>
                <td height="21" align="left" width="256">
                    Logged in as <%= user.firstName %>.<br />You have <%= Convert.ToInt32(user.balance) %> units in your account.                         
                </td>
            </tr>
        </table>
    </div>

    <div id="login_new2">
        <table border="0" width="262">
            <tr>
                <td height="21" align="left" width="256">
                    <% if (user.UserName.Equals("aish_admin", StringComparison.CurrentCultureIgnoreCase))
                       { %>
                       <a href="/admin">Admin<span class="headerverticallinered"> |</span></a>                    
                    <% } else { %>
                    <a href="<%= Url.RouteUrl("HelpFAQPage") %>">Help<span class="headerverticallinered"> |</span></a>                    
                    <% } %>
                    <a href="<%= Url.Action("Index", "Account") %>"> My Account </a>
                    <span class="headerverticallinered">|</span><a href="<%= Url.Action("MyLibrary", "Account") %>"> My Library</a>                     
                    <span class="headerverticallinered">|</span> <a href="<%= Url.Action("LogOff", "Account", new { returnUrl = System.Web.HttpContext.Current.Request.RawUrl }) %>">Logout</a>
                </td>
            </tr>
        </table>     	 
    </div>        

    <div style="position:absolute; left: 412px; top: 52px;"> 
        <%
            Html.RenderPartial("~/Views/Shopping/HeaderCartSection.ascx"); 
        %>
    </div>

<% } %>




