using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyPartnerKinoBL.Extension;
using MainEntity.Interfaces;
using MainEntity.Models.User;
using MainEntity;
using MainCommon;


namespace MainBL
{
    public class UserService : BaseBO, IUserService
    {
        public UserService(string connection_name)
            : base(connection_name)
        {
        }


        #region Select

        public bool IsEmailUniqueForApplication(string application_name, string email)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
            (UserDbManager context) =>
            {
                return context.IsEmailUniqueForApplication(application_name, email);

            });
        }

        public bool IsUserNameUniqueForApplication(string application_name, string user_name)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
            (UserDbManager context) =>
            {
                return context.IsUserNameUniqueForApplication(application_name, user_name);

            });
        }

        public SubscribePlanEntity GetSubscribePlan(long plan_id)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
            (UserDbManager context) =>
            {
                return context.GetSubscribePlanEntity(plan_id).FirstOrDefault();

            });
        }


        //public List<KeyValuePair<long, string>> GetSubscribePlanEntities()
        //{
        //    return this.Exec(System.Data.IsolationLevel.Snapshot,
        //    (UserDbManager context) =>
        //    {
        //        return context.GetSubscribePlanEntities().Select(s => new KeyValuePair<long, string>(s.subscribePlanID, s.EntityItem.title)).ToList();

        //    });
        //}

        public Membership GetUser(Guid user_id)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (UserDbManager context) =>
                {
                    var rval = context.GetUser(user_id).SingleOrDefault();
                    if (rval != null)
                    {
                        rval.MembershipXrefSubscribePlan = context.GetMembershipXrefSubscribePlan(user_id).FirstOrDefault();
                        //rval.PlansList = GetSubscribePlanEntities();
                    }

                    return rval;
                });
        }

        public long GetNexSubscribePlan(long current_subscribe_plan)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (UserDbManager context) =>
                {
                    var rval = context.SelectSubscribePlanXref(current_subscribe_plan).Select(s => s.nextSubscribePlanID).Single();
                    return rval;
                });
        }

        public aspnet_User[] GetAspnetUsers(string cur_app_name, string role_name, int start_index, int page_size)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (UserDbManager context) =>
                {
                    IQueryable<aspnet_User> rval = null;
                    if (string.IsNullOrEmpty(role_name)) rval = context.SelectAspnetUsers(cur_app_name);
                    else rval = context.SelectAspnetUsers(cur_app_name, role_name);

                    return rval.Skip(start_index).Take(page_size).ToArray();
                });

        }


        public Membership[] GetUsers(string cur_app_name, int start_index, int page_size, MembershipTypeEnum? membership_type,
            string filter_email, string filter_user_name, string filter_first_name, string filter_last_name, DateTime? filter_membership_start, DateTime? filter_membership_end,
            bool? filter_canceled, bool? filter_declined, int? filter_charge_in_days, bool? filter_charge_in_days_exact)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (UserDbManager context) =>
                {
                    IQueryable<Membership> rval = null;
                    if (membership_type == MembershipTypeEnum.TodaySubscribersActivation)
                        rval = context.GetSubscribers(DateTime.UtcNow.AddHours(24));
                    else rval = context.GetUsers(cur_app_name);

                    rval = ApplyUserFilter(membership_type,
                        filter_email, filter_user_name, filter_first_name, filter_last_name, filter_membership_start, filter_membership_end,
                        filter_canceled, filter_declined, filter_charge_in_days, filter_charge_in_days_exact, rval);

                    return rval.Skip(start_index).Take(page_size).ToArray();
                });
        }

        public int GetUsersCount(string cur_app_name, MembershipTypeEnum? membership_type,
            string filter_email, string filter_user_name, string filter_first_name, string filter_last_name, DateTime? filter_membership_start, DateTime? filter_membership_end,
            bool? filter_canceled, bool? filter_declined, int? filter_charge_in_days, bool? filter_charge_in_days_exact)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (UserDbManager context) =>
                {
                    IQueryable<Membership> rval = null;
                    if (membership_type == MembershipTypeEnum.TodaySubscribersActivation)
                        rval = context.GetSubscribers(DateTime.UtcNow.AddHours(24));
                    else rval = context.GetUsersCount(cur_app_name);

                    rval = ApplyUserFilter(membership_type,
                        filter_email, filter_user_name, filter_first_name, filter_last_name, filter_membership_start, filter_membership_end,
                        filter_canceled, filter_declined, filter_charge_in_days, filter_charge_in_days_exact, rval);

                    return rval.Count();
                });
        }

        public Membership[] GetActivationSubscribers(int start_row_index, int max_rows)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (UserDbManager context) =>
                {
                    var startDate = DateTime.UtcNow;
                    var rval = context.GetSubscribers(startDate);
                    if (start_row_index > 0) rval = rval.Skip(start_row_index);
                    if (max_rows < int.MaxValue) rval = rval.Take(max_rows);

                    return rval.ToArray();
                });
        }

        private static IQueryable<Membership> ApplyUserFilter(MembershipTypeEnum? membership_type,
            string filter_email, string filter_user_name, string filter_first_name, string filter_last_name, DateTime? filter_membership_start, DateTime? filter_membership_end,
            bool? filter_canceled, bool? filter_declined, int? filter_charge_in_days, bool? filter_charge_in_days_exact, IQueryable<Membership> user_query)
        {
            IQueryable<Membership> rval = user_query;
            var now = DateTime.Now;

            if (!string.IsNullOrEmpty(filter_email))
                rval = rval.Where(u => u.Email.Contains(filter_email));
            if (!string.IsNullOrEmpty(filter_user_name))
                rval = rval.Where(u => u.UserName.Contains(filter_user_name));
            if (!string.IsNullOrEmpty(filter_first_name))
                rval = rval.Where(u => u.firstName.Contains(filter_first_name));
            if (!string.IsNullOrEmpty(filter_last_name))
                rval = rval.Where(u => u.lastName.Contains(filter_last_name));
            if (filter_membership_start != null)
                rval = rval.Where(u => u.StartSubscribeDate >= filter_membership_start);
            if (filter_membership_end != null)
                rval = rval.Where(u => u.EndSubscribeDate <= filter_membership_end).OrderByDescending(u => u.EndSubscribeDate);
            if (filter_canceled != null)
                rval = rval.Where(u => u.IsCancelSubscribe == filter_canceled);
            if (filter_declined != null)
            {
                rval = rval.Where(m => m.suspended == filter_declined);
            }
            if (filter_charge_in_days.HasValue)
            {
                membership_type = MembershipTypeEnum.CurrentMonthly;
                DateTime chargeDate = DateTime.Today.AddDays(filter_charge_in_days.Value);
                int chargeDay = chargeDate.Day;

                if (filter_charge_in_days_exact.HasValue && filter_charge_in_days_exact.Value == true)
                    rval = rval.Where(u => u.chargeDay == chargeDay);
                else
                    rval = rval.Where(u => u.chargeDay <= chargeDay);
                rval = rval.Where(u => u.EndSubscribeDate < chargeDate.AddDays(1)).OrderByDescending(u => u.chargeDay).ThenByDescending(u => u.EndSubscribeDate);
            }

            if (membership_type != null)
            {
                if (membership_type.Equals(MembershipTypeEnum.CurrentMonthly) || membership_type.Equals(MembershipTypeEnum.TodaySubscribersActivation))
                {
                    rval = rval.Where(u => !u.suspended && !u.IsCancelSubscribe );
                }


                if (membership_type == MembershipTypeEnum.AllSubscriptions)
                    rval = rval.Where(u => u.PlanID > 0);
                else if (membership_type == MembershipTypeEnum.FreeListening || membership_type == MembershipTypeEnum.Standard)
                    rval = rval.Where(u => u.PlanID == null);
                else if (membership_type == MembershipTypeEnum.CurrentMonthly)
                {
                    var monthlySubscribe = new long[] { 237, 238, 27495 };
                    rval = rval.Where(u => !u.IsDeclinedSubscribe && !u.IsCancelSubscribe && monthlySubscribe.Contains(u.PlanID));
                }
                else if (membership_type == MembershipTypeEnum.AllMonthly)
                {
                    var monthlySubscribe = new long[] { 237, 238, 27495 };
                    rval = rval.Where(u => monthlySubscribe.Contains(u.PlanID));
                }
                else if (membership_type == MembershipTypeEnum.PrePaidUsers)
                {
                    //var monthlySubscribe = new long[] { 27490, 27491, 27492, 27493, 27494 };
                    rval = rval.Where(u => u.PlanID > 238);
                }
                else if (membership_type == MembershipTypeEnum.Monthly)
                    rval = rval.Where(u => u.PlanID == 238);
                else if (membership_type == MembershipTypeEnum.NonValidUsers)
                    rval = rval.Where(u => !u.IsApproved);
            }

            return rval;
        }


        public MembershipCart GetLastMembershipCard(Guid user_id)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (UserDbManager context) =>
                {
                    DateTime now = DateTime.UtcNow;
                    return context.GetMembershipCart(user_id)
                        .Where(c => c.cartStateID == 0)
                        .FirstOrDefault();
                });
        }

        #endregion



        #region Insert

        public int InsertOrUpdateMembershipCard(Guid user_id, DateTime? expiration_date, string card_type,
            string membership_card_id, CartStateEnum card_state)
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted,
           (UserDbManager context) =>
           {
               var rval = context.UpdateMembershipCard(user_id, expiration_date, card_type, membership_card_id, (long)card_state);
               if (rval > 0)
               {
                   if (card_state == CartStateEnum.Deleted)
                   {
                       var activeCard = context.GetMembershipCart(user_id).Where(c => c.cartStateID == (long)CartStateEnum.Active).FirstOrDefault();
                       if (activeCard == null) context.UpdateMembership(user_id, false, DateTime.UtcNow);
                   }
               }
               else
                   rval = context.InsertMembershipCard(user_id, expiration_date, card_type, membership_card_id, (long)card_state);

               return rval;
           });
        }


        public int InsertMembership(Guid user_id, string first_name, string last_name, decimal balance, int free_offer_cnt,
            string country, string state, string city, string postal_address, string postal_code, string phone, string day_phone, bool fullLibraryAccess,
            string referrer_code = null, long subscribe_plan_id = 0, DateTime? start_subscribe_date = null)
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted, (UserDbManager context) =>
            {
                var rval = context.InsertMembership(user_id, balance, first_name, last_name, free_offer_cnt,
                    country, state, city, postal_address, postal_code, phone, day_phone, fullLibraryAccess, subscribe_plan_id != null ? (DateTime?)DateTime.Now : null);
                if (referrer_code != null)
                    rval += context.InsertMembershipXReferrer(user_id, referrer_code);
                if (subscribe_plan_id != 0)
                    AssignSubscribePlan(user_id, start_subscribe_date, subscribe_plan_id, context, null);

                return rval;
            });
        }

        public KeyValuePair<long, long?> InsertAddesses(Guid user_id,
            string first_name, string last_name, string country, string state, string city, string postal_address, string postal_code, string phone, string day_phone = null, string description = null,
            string ship_first_name = null, string ship_last_name = null, string ship_country = null, string ship_state = null, string ship_city = null, string ship_postal_address = null, string ship_postal_code = null, string ship_phone = null, string ship_day_phone = null, string ship_description = null)
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted, (UserDbManager context) =>
            {
                long? shippingAddressID = null;
                long billingAddressID = context.InsertMembershipAddress(user_id, true, country, postal_code, state, city, postal_address, phone, day_phone, first_name, last_name, description);
                if (!string.IsNullOrEmpty(ship_postal_address))
                    shippingAddressID = context.InsertMembershipAddress(user_id, false, ship_country, ship_postal_code, ship_state, ship_city, ship_postal_address, ship_phone, ship_day_phone, ship_first_name, ship_last_name, ship_description);
                return new KeyValuePair<long, long?>(billingAddressID, shippingAddressID);
            });

        }

        public System.Web.Security.MembershipUser CreateUser(System.Web.Security.MembershipProvider curProvider, string email, string password, bool is_approved,
            string first_name, string last_name, int free_offer_cnt, string country, string state, string city, string postal_address, string postal_code, string phone, string day_phone, bool fullLibraryAccess)
        {
            Guid userKey = Guid.NewGuid();
            System.Web.Security.MembershipCreateStatus status;
            var user = curProvider.CreateUser(email, password, email, null, null, is_approved, userKey, out status);
            if (status != System.Web.Security.MembershipCreateStatus.Success)
            {
                var message = AccountValidation.ErrorCodeToString(status);
                if (status == System.Web.Security.MembershipCreateStatus.DuplicateEmail || status == System.Web.Security.MembershipCreateStatus.DuplicateUserName) throw new ArgumentException(message);
                throw new Exception(message);
            }

            if (InsertMembership((Guid)user.ProviderUserKey, first_name, last_name, 0, free_offer_cnt,
                country, state, city, postal_address, postal_code, phone, day_phone, fullLibraryAccess) == 0)
            {
                curProvider.DeleteUser(user.UserName, true);
                throw new Exception("Failed to create user");
            }

            return user;

        }

        #endregion


        #region Update

        public int UpdateUserAfterCardAuthorization(Guid user_id, string cart_number, string sj_tran_id, long shopping_transaction_id)
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted, (UserDbManager context) =>
            {
                int rval = 0;
                rval = context.UpdateMembership(user_id, true, DateTime.UtcNow);
                context.UpdateMembershipCardState(user_id, cart_number, (long)CartStateEnum.Active);
                context.UpdateMembershipCardTranID(user_id, cart_number, sj_tran_id, shopping_transaction_id);

                return rval;
            });
        }


        public int UpdateUserName(Guid user_id, string user_name)
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted,
            (UserDbManager context) =>
            {
                int rval = context.UpdateUserName(user_id, user_name);
                return rval;
            });
        }

        public int UpdatePlanDates(Guid user_id, DateTime? start_date, DateTime? end_date, byte? charge_day)
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted,
            (UserDbManager context) =>
            {
                int rval = context.UpdatePlanDates(user_id, start_date, end_date, end_date);
                context.UpdateMembership(user_id, charge_day);
                return rval;
            });
        }

        public int UpdateBalance(Guid user_id, decimal balance)
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted,
            (UserDbManager context) =>
            {
                int rval = context.UpdateBalance(user_id, balance);
                return rval;
            });
        }

        public int UnSuspend(Guid user_id)
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted,
            (UserDbManager context) =>
            {
                int rval = context.UpdateMembershipSuspendFlag(user_id, false);
                return rval;
            });
        }


        public int UpdateProfile(Guid user_id, string first_name, string last_name, string city, string country, string day_phone,
            string phone, string postal_address, string postal_code, string state)
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted,
                (UserDbManager context) =>
                {
                    context.UpdateMembership(user_id, first_name, last_name, country, state, city, postal_address, postal_code, phone, day_phone);
                    return 1;
                }
            );
        }

        public int UpdateUser(Guid user_id, decimal balance, string description, string first_name,
            string last_name, string referrer_code, string city, string country, string day_phone,
            string phone, string postal_address, string postal_code, string state,
            DateTime? start_subscribe_date, long plan_id,
            int free_offer_cnt, bool suspended, bool full_library_access)
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted,
               (UserDbManager context) =>
               {
                   int rval = context.UpdateMembership(user_id, balance, first_name, last_name, free_offer_cnt, country, state, city, postal_address, postal_code, phone, day_phone, suspended, full_library_access);

                   var plan = context.GetMembershipXrefSubscribePlan(user_id).FirstOrDefault();
                   var hasNewPlan = (plan == null || plan.subscribePlanID != plan_id || (start_subscribe_date.HasValue && plan.startSubscribeDate != start_subscribe_date));
                   if (plan != null && hasNewPlan) context.DeleteMembershipXrefSubscribePlan(user_id);

                   if (plan_id != 0 && hasNewPlan)
                       AssignSubscribePlan(user_id, start_subscribe_date, plan_id, context, plan);

                   return rval;
               });
        }

        private static void AssignSubscribePlan(Guid user_id, DateTime? start_subscribe_date, long plan_id, UserDbManager context, MembershipXrefSubscribePlan prev_plan)
        {
            var assignPlan = context.GetSubscribePlanEntity(plan_id).Single();
            start_subscribe_date = start_subscribe_date ?? DateTime.UtcNow;
            var endSubscribeDate = start_subscribe_date.Value.AddMonths(assignPlan.durationInMonths).AddDays(assignPlan.durationInDays);

            context.InsertMembershipXrefSubscribePlan(user_id, plan_id, start_subscribe_date, endSubscribeDate, endSubscribeDate);

            if (prev_plan == null)
            {
                context.UpdateMembership(user_id, start_subscribe_date);
                context.UpdateMembership(user_id, (byte)start_subscribe_date.Value.Day);
            }
        }

        public int UpdateEmail(Guid user_id, string email)
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted,
                (UserDbManager context) => context.UpdateEmail(user_id, email));
        }

        public int UpdateMembershipCardState(Guid user_id, string card_number, CartStateEnum cart_state)
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted,
               (UserDbManager context) =>
               {
                   var rval = context.UpdateMembershipCardState(user_id, card_number, (long)cart_state);
                   if (cart_state == CartStateEnum.Deleted)
                   {
                       var activeCard = context.GetMembershipCart(user_id).Where(c => c.cartStateID == (long)CartStateEnum.Active).FirstOrDefault();
                       if (activeCard == null) context.UpdateMembership(user_id, false, DateTime.UtcNow);
                   }
                   return rval;
               });

        }


        public int CancelSubscribe(Guid user_id)
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted,
                (UserDbManager context) =>
                {
                    return context.CancelSubscribe(user_id);
                });
        }

        public int DelaySubscribeActivation(Guid user_id, int delay_hours, byte max_tray_activation_cnt)
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted,
                (UserDbManager context) =>
                {
                    int rval = 0;
                    var subscribe = context.GetMembershipXrefSubscribePlan(user_id).Single();
                    var activationFailCnt = subscribe.activationFailCnt + 1;

                    context.UpdateMembershipSuspendFlag(user_id, true);

                    if (activationFailCnt < max_tray_activation_cnt)
                    {
                        var subscribeActivation = DateTime.Now.AddHours(delay_hours);
                        rval = context.UpdateSubscribeActivation(user_id, subscribeActivation, (byte)activationFailCnt);
                    }
                    else
                        rval = context.UpdateSubscribeDecline(user_id, true);

                    return rval;
                });
        }


        private static bool IsEqual(string city, string country, string day_phone, string phone, string postal_address, string postal_code, string state, MembershipAddress address)
        {
            return (string.Compare(address.city, city, true) == 0 && string.Compare(address.country, country, true) == 0 &&
                                   string.Compare(address.dayPhone, day_phone, true) == 0 && string.Compare(address.phone, phone, true) == 0 &&
                                   string.Compare(address.postalAddress, postal_address, true) == 0 && string.Compare(address.postalCode, postal_code, true) == 0 &&
                                   string.Compare(address.state, state, true) == 0);
        }

        private static bool IsEmpty(string city, string country, string day_phone, string phone, string postal_address, string postal_code, string state)
        {
            return (string.IsNullOrEmpty(city) && string.IsNullOrEmpty(country) && string.IsNullOrEmpty(day_phone) &&
                                   string.IsNullOrEmpty(phone) && string.IsNullOrEmpty(postal_code) && string.IsNullOrEmpty(postal_address) &&
                                   string.IsNullOrEmpty(state));
        }

        #endregion


        #region Delete

        public int LockUser(Guid user_id)
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted,
               (UserDbManager context) =>
               {
                   int rval = 0;
                   var user = context.GetUser(user_id).Single();
                   var email = user.Email;
                   var userName = user.UserName;

                   do
                   {
                       try
                       {
                           var hash = Guid.NewGuid().ToString("N");
                           email = "!" + hash + "_" + email;
                           userName = "!" + hash + "_" + userName;
                           if (email.Length > 256) email = hash;
                           if (userName.Length > 256) userName = hash;
                           context.UpdateUserName(user_id, userName);
                           rval = context.LockUser(user_id, email);
                       }
                       catch { ;}

                   } while (rval == 0);

                   return rval;
               }
            );
        }

        public int LockUser(Guid user_id, string email)
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted,
               (UserDbManager context) =>
               {
                   int rval = 0;
                   var user = context.GetUser(user_id).Single();
                   var userName = user.UserName;

                   do
                   {
                       try
                       {
                           rval = context.LockUser(user_id, email);
                       }
                       catch { ;}

                   } while (rval == 0);

                   return rval;
               }
            );
        }

        public int DeletePlan(Guid user_id)
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted,
                (UserDbManager context) =>
                {
                    return context.DeleteMembershipXrefSubscribePlan(user_id);
                }
            );
        }

        #endregion
    }
}
