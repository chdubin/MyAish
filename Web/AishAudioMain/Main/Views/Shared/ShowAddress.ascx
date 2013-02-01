<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MainEntity.Models.Shopping.MembershipAddress>" %>

<div>
    <%=Model.firstName %>
    <%=Model.lastName %></div>
<div>
    <%=Model.postalAdderss %></div>
<div>
    <%=Model.country %>
    <%=Model.city %>,
    <%=Model.postalCode %></div>
<div>
    <%=Model.state %></div>
<%if (!string.IsNullOrEmpty(Model.phone))
  { %><div>
      Phone: <%=Model.phone%></div><%} %>
<%if (!string.IsNullOrEmpty(Model.dayPhone))
  { %><div>
      Home phone: <%=Model.dayPhone %></div><%} %>
