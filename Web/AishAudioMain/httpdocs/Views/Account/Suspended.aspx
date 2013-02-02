<%@ Page Title="" Language="C#" MasterPageFile="../Shared/twoColumn.Master" Inherits="System.Web.Mvc.ViewPage<Main.Models.Account.ChangeCreditCard>" %>

<asp:Content ID="Content4" ContentPlaceHolderID="head" runat="server">
    <link href="<%= Url.Css("custom.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Body" runat="server">
    <div class="response-msg notice ui-corner-all">
        <span>Your account is temporarily suspended</span>
        <ul>
            <li>Unfortunately, we were unable to charge your credit card during monthly membership renewal.</li>
            <li style="margin-bottom:15px;">Your account is temporarily suspended, pending a monthly membership payment.</li>
            <li style="margin-bottom:15px;">Please update your credit card and billing information to instantly gain access to thousands of inspiring lectures.</li>
            <li>You may also choose to downgrade your membership to a Free Listening type just below the update form.</li>
        </ul>
    </div>

<%= Html.Partial("ChangeCreditCardForm", this.Model) %>


    <div class="response-msg inf ui-corner-all" style="margin-top: 30px;">
        <span>Click the button below to downgrade your membership to Free Listening</span>
        <ul>
            <li>WARNING: Upon downgrade, you will only have access to files that you've purchased in the past 2 months.</li>
        </ul>
    </div>

<%using(Html.BeginForm()){ %>
    <input type="hidden" name="cancel_subscribe" value="true" />
    <input type="submit" class="btn" id="btnDowngrade" value="Downgrade to Free Listening Membership" />
<%} %>

<script type="text/javascript">

    $(document).ready(function(){
        $('#btnDowngrade').click(function(e){
            //e.preventDefault();
            return (confirm('Are you sure you want to downgrade your account to Free Listening?'));
        });
    });

</script>
</asp:Content>
