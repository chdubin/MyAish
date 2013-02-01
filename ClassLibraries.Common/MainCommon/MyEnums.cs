using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainCommon
{

    public enum SortParametersEnum
    {
        None = 0,
        Title,
        Date,
        Active,
        Visible,
        Free,
        FreeOffer
    }

    public enum ViewMessageEnum
    {
        None = 0,
        CreateSuccess,
        UpdateSuccess,
        DeleteSuccess,
        DeleteError
    }

    public enum CartItemTypeEnum
    {
        Unit = 0,
        Class = 1,
        Package = 2,
        Subscribe = 3
    }

    public enum SortClassesEnum
    {
        New = 0,
        Top = 1,
        Code = 2
    }

    public enum MembershipTypeEnum : long
    {
        [Display("All subscriptions")]
        AllSubscriptions = -1,

        [Display("Free Listening")]
        FreeListening = -2,

        [Display("None")]
        None = 0,

        [Display("Standard")]
        Standard,

        [Display("Current Monthly")]
        CurrentMonthly,

        [Display("All Monthly")]
        AllMonthly,

        [Display("Monthly (First month free)")]
        Monthly,

        [Display("Pre-paid users")]
        PrePaidUsers,

        [Display("Non-valid users")]
        NonValidUsers,

        [Display("Today Subscribers Activation")]
        TodaySubscribersActivation,

    }

    public enum MailMessageTypeEnum
    {
        OnMonthlyMembershipCancellation,
        OnSomeoneUpdatingTheirCreditCard,
        OnSomeoneJoiningAsNewMonthlyMember,
        OnSomeoneJoiningAsUpgradeMonthlyMember,
        PrepaidSaleGoesThroughForExistAccount,
        PrepaidSaleGoesThroughForNewUser,

        RegularSalesIsMade,

    }

}
