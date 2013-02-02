<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MainEntity.Models.User.aspnet_User[]>" %>
<div class="hastable">
        <table>
            <thead>
                <tr>
                    <th>id</th>
                    <th>Name</th>
                    <th>Email</th>
                    <th>Last activity date</th>
                    <th>Create date</th>
                    <th>Active</th>
                </tr>
            </thead>
            <tbody>
            <% foreach (var item in Model)
               { %>
               <tr>
                <td><%=item.UserId %></td>
                <td><%=item.UserName %></td>
                <td><%=item.aspnet_Membership.Email %></td>
                <td><%=item.LastActivityDate.ToLocalTime().ToString() %></td>
                <td><%=item.aspnet_Membership.CreateDate.ToLocalTime().ToString()%></td>
                <td><%=!item.aspnet_Membership.IsLockedOut %></td>
                </tr>
            <%} %>
            </tbody>
        </table>
    </div>
