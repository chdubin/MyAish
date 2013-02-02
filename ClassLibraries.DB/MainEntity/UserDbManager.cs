using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Data.Linq;
using MainEntity.Models.User;
using MainCommon;

namespace MainEntity
{
    public partial class UserDbManager
    {

        #region Select


        public bool IsEmailUniqueForApplication(string application_name, string email)
        {
            var res = (from am in this.AspnetMembershipItems
                        where am.aspnet_Application.ApplicationName == application_name && am.Email == email
                        select am).FirstOrDefault();


            return (res == null);
        }

        public bool IsUserNameUniqueForApplication(string application_name, string user_name)
        {
            var res = (from au in this.AspnetUserItems
                       where au.aspnet_Application.ApplicationName == application_name && au.UserName == user_name
                       select au).FirstOrDefault();


            return (res == null);
        }

        public IQueryable<SubscribePlanEntity> GetSubscribePlanEntity(long plan_id)
        {
            var rval = (from s in this.SubscribePlanEntityItems
                        where s.subscribePlanID == plan_id
                        select new SubscribePlanEntity()
                        {
                            subscribePlanID = s.subscribePlanID,
                            durationInDays = s.durationInDays,
                            durationInMonths = s.durationInMonths,
                            freeUnitsOnSubscribe = s.freeUnitsOnSubscribe,
                            freeOfferCnt = s.freeOfferCnt,
                            description = s.description,                            
                            EntityItem = new EntityItem()
                            {
                                entityID = s.EntityItem.entityID,
                                deleted = s.EntityItem.deleted,
                                creatorID = s.EntityItem.creatorID,
                                createDate = s.EntityItem.createDate,
                                active = s.EntityItem.active,
                                hierarchiID = s.EntityItem.hierarchiID,
                                parentEntityID = s.EntityItem.parentEntityID,
                                rootEntityID = s.EntityItem.rootEntityID,
                                sortOrder = s.EntityItem.sortOrder,
                                title = s.EntityItem.title,
                                typeID = s.EntityItem.typeID,
                                ProductEntity = new ProductEntity()
                                {
                                    productID = s.EntityItem.ProductEntity.productID,
                                    description = s.EntityItem.ProductEntity.description,
                                    price1 = s.EntityItem.ProductEntity.price1,
                                    price2 = s.EntityItem.ProductEntity.price2,
                                    productTypeID = s.EntityItem.ProductEntity.productTypeID
                                }
                            }
                        });


            return rval;
        }


        public IQueryable<MembershipXrefSubscribePlan> GetMembershipXrefSubscribePlan(Guid user_id)
        {
            var rval = (from m in this.MembershipXSubscrPlanItems
                        where m.UserId == user_id
                        select new MembershipXrefSubscribePlan()
                        {
                            UserId = m.UserId,
                            subscribePlanID = m.subscribePlanID,
                            startSubscribeDate = m.startSubscribeDate,
                            endSubscribeDate = m.endSubscribeDate,
                            SubscribePlanEntity = new SubscribePlanEntity()
                            {
                                subscribePlanID = m.subscribePlanID,
                                durationInDays = m.SubscribePlanEntity.durationInDays,
                                durationInMonths = m.SubscribePlanEntity.durationInMonths,
                                description = m.SubscribePlanEntity.description
                            }
                        });


            return rval;
        }

        public IQueryable<Membership> GetUser(Guid user_id)
        {
            var rval = (from m in this.MembershipItems     
                        where m.UserId == user_id /*&&
                        !m.aspnet_User.aspnet_Membership.IsLockedOut*/
                        select new Membership()
                        {
                            CreatedDate = m.aspnet_User.aspnet_Membership.CreateDate,
                            Email = m.aspnet_User.aspnet_Membership.Email,
                            UserName = m.aspnet_User.UserName,
                            Password = m.aspnet_User.aspnet_Membership.Password,
                            ReferrerCode = m.MembershipXrefReferrer.referrerCode,
                            UserId = m.UserId,
                            StartSubscribeDate = m.MembershipXrefSubscribePlan == null ? null : m.MembershipXrefSubscribePlan.startSubscribeDate,
                            EndSubscribeDate = m.MembershipXrefSubscribePlan == null ? null : m.MembershipXrefSubscribePlan.endSubscribeDate,
                            IsCancelSubscribe = m.MembershipXrefSubscribePlan != null && m.MembershipXrefSubscribePlan.cancelSubscribe,
                            Description = m.aspnet_User.aspnet_Membership.Comment,
                            firstName = m.firstName,
                            lastName = m.lastName,
                            balance = m.balance,
                            activatedCart = m.activatedCart,
                            activationCartDate = m.activationCartDate,
                            subscribeActivation = m.subscribeActivation,
                            PlanID = m.MembershipXrefSubscribePlan.SubscribePlanEntity.subscribePlanID,
                            PlanName = m.MembershipXrefSubscribePlan.SubscribePlanEntity.EntityItem.title,
                            freeOfferCnt = m.freeOfferCnt,
                            chargeDay = m.chargeDay,
                            suspended = m.suspended,
                            fullLibraryAccess = m.fullLibraryAccess,

							city = m.city,
							country = m.country,
							dayPhone = m.dayPhone,
							phone = m.phone,
							postalAdderss = m.postalAdderss,
							postalCode = m.postalCode,
							refferedBy = m.refferedBy,
							state = m.state
                        });


            return rval;
        }

        //public IQueryable<SubscribePlanEntity> GetSubscribePlanEntities()
        //{
        //    var rval = (from s in this.SubscribePlanEntityItems
        //                    select s);

        //    return rval;
        //}

        public IQueryable<aspnet_User> SelectAspnetUsers(string cur_app_name)
        {
            var rval = from am in this.AspnetMembershipItems
                       join au in this.AspnetUserItems on am.UserId equals au.UserId
                       join aa in this.AspnetAppItems on au.ApplicationId equals aa.ApplicationId
                       where aa.ApplicationName == cur_app_name //&& !am.IsLockedOut
                       orderby am.CreateDate descending
                       select new aspnet_User()
                       {
                            ApplicationId = au.ApplicationId,
                            aspnet_Membership = am,
                            IsAnonymous = au.IsAnonymous,
                            LastActivityDate =au.LastActivityDate,
                            LoweredUserName = au.LoweredUserName,
                            MobileAlias = au.MobileAlias,
                            UserId = au.UserId,
                            UserName = au.UserName
                       };

            return rval;
        }

        public IQueryable<aspnet_User> SelectAspnetUsers(string cur_app_name, string role_name)
        {
            var rval = from am in this.AspnetMembershipItems
                       join au in this.AspnetUserItems on am.UserId equals au.UserId
                       join aa in this.AspnetAppItems on au.ApplicationId equals aa.ApplicationId
                       join ar in this.AspnetRoles on aa.ApplicationId equals ar.ApplicationId
                       join aur in this.AspnetUsersInRoles on ar.RoleId equals aur.RoleId
                       where aa.ApplicationName == cur_app_name /*&& !am.IsLockedOut*/ && ar.LoweredRoleName == role_name && aur.UserId == au.UserId
                       orderby am.CreateDate descending
                       select new aspnet_User()
                       {
                           ApplicationId = au.ApplicationId,
                           aspnet_Membership = am,
                           IsAnonymous = au.IsAnonymous,
                           LastActivityDate = au.LastActivityDate,
                           LoweredUserName = au.LoweredUserName,
                           MobileAlias = au.MobileAlias,
                           UserId = au.UserId,
                           UserName = au.UserName
                       };

            return rval;
        }

		public IQueryable<Membership> GetUsers(string cur_app_name)
        {
            var rval = (from m in this.MembershipItems
						join am in this.AspnetMembershipItems on m.UserId equals am.UserId
						join au in this.AspnetUserItems on m.UserId equals au.UserId
						join aa in this.AspnetAppItems on au.ApplicationId equals aa.ApplicationId
						join ms in this.MembershipXSubscrPlanItems on m.UserId equals ms.UserId into mss
						from msss in mss.DefaultIfEmpty()
						join mse in this.EntityItems on msss.subscribePlanID equals mse.entityID into msee
						from mseee in msee.DefaultIfEmpty()
						join mr in this.MembershipXReferrerItems on m.UserId equals mr.UserId into mrr
						from mrrr in mrr.DefaultIfEmpty()
                        where aa.ApplicationName == cur_app_name //&& !am.IsLockedOut
                        orderby m.subscribeActivation descending, am.CreateDate descending
                        select new Membership()
                        {
							CreatedDate = am.CreateDate,
							Email = am.Email,
                            UserName = au.UserName,
                            Password = am.Password,
                            IsApproved = am.IsApproved,
							ReferrerCode = mrrr.referrerCode,
							RefferedBy = mrrr.referredBy,
							LastRefer = mrrr.lastRefer,
                            UserId = m.UserId,
							StartSubscribeDate = msss.startSubscribeDate,
                            EndSubscribeDate = msss.endSubscribeDate,
                            IsCancelSubscribe = msss.cancelSubscribe,
                            IsDeclinedSubscribe = msss.declineSubscribe,
                            SubscribeActivation = msss.activationDate,
                            firstName = m.firstName,
                            lastName = m.lastName,
                            balance = m.balance,
                            activatedCart = m.activatedCart,
                            chargeDay = m.chargeDay,
                            PlanID = msss.subscribePlanID,
                            subscribeActivation = m.subscribeActivation,
                            activationCartDate = m.activationCartDate,
                            freeOfferCnt = m.freeOfferCnt,
                            MembershipType = mseee.title,
							suspended = m.suspended,

							city = m.city,
							country = m.country,
							dayPhone = m.dayPhone,
							phone = m.phone,
							postalAdderss = m.postalAdderss,
							postalCode = m.postalCode,
							refferedBy = m.refferedBy,
							state = m.state,
                            IsLockedOut = am.IsLockedOut,
                        });


            return rval;
        }

        public IQueryable<Membership> GetUsersCount(string cur_app_name)
        {
			var rval = (from m in this.MembershipItems
						join am in this.AspnetMembershipItems on m.UserId equals am.UserId
						join au in this.AspnetUserItems on m.UserId equals au.UserId
						join aa in this.AspnetAppItems on au.ApplicationId equals aa.ApplicationId
						join ms in this.MembershipXSubscrPlanItems on m.UserId equals ms.UserId into mss
						from msss in mss.DefaultIfEmpty()
						where aa.ApplicationName == cur_app_name //&& !am.IsLockedOut
                        orderby am.CreateDate descending
                        select new Membership()
                        {
							CreatedDate = am.CreateDate,
							Email = am.Email,
                            UserName = au.UserName,
                            Password = am.Password,
                            IsApproved = am.IsApproved,
                            UserId = m.UserId,
							StartSubscribeDate = msss.startSubscribeDate,
                            EndSubscribeDate = msss.endSubscribeDate,
                            IsCancelSubscribe = msss.cancelSubscribe,
                            IsDeclinedSubscribe = msss.declineSubscribe,
                            firstName = m.firstName,
                            lastName = m.lastName,
                            balance = m.balance,
                            activatedCart = m.activatedCart,
                            PlanID = msss.subscribePlanID,
                            subscribeActivation = m.subscribeActivation,
                            activationCartDate = m.activationCartDate,
                            freeOfferCnt = m.freeOfferCnt,
                            chargeDay = m.chargeDay,

							city = m.city,
							country = m.country,
							dayPhone = m.dayPhone,
							phone = m.phone,
							postalAdderss = m.postalAdderss,
							postalCode = m.postalCode,
							refferedBy = m.refferedBy,
							state = m.state
                        });

            return rval;
        }

        public IQueryable<Membership> GetSubscribers(DateTime start_activation_date)
        {
            var rval = from m in this.MembershipItems
                       join au in this.AspnetUserItems on m.UserId equals au.UserId
                       join am in this.AspnetMembershipItems on m.UserId equals am.UserId
                       join x in this.MembershipXSubscrPlanItems on m.UserId equals x.UserId
                       join xp in this.SubscribePlanXrefItems on x.subscribePlanID equals xp.subscribePlanID
                       join np in this.SubscribePlanEntityItems on xp.nextSubscribePlanID equals np.subscribePlanID
                       join pe in this.EntityItems on x.subscribePlanID equals pe.entityID
                       join mr in this.MembershipXReferrerItems on m.UserId equals mr.UserId into mrr
                       from mrrr in mrr.DefaultIfEmpty()
                       where x.activationDate < start_activation_date /*&& !am.IsLockedOut*/ && !x.cancelSubscribe && !x.declineSubscribe
                       orderby x.activationDate
                       select new Membership()
                       {
                           CreatedDate = am.CreateDate,
                           Email = am.Email,
                           UserName = au.UserName,
                           Password = am.Password,
                           IsApproved = am.IsApproved,
                           ReferrerCode = mrrr.referrerCode,
                           RefferedBy = mrrr.referredBy,
                           LastRefer = mrrr.lastRefer,
                           UserId = m.UserId,
                           StartSubscribeDate = x.startSubscribeDate,
                           EndSubscribeDate = x.endSubscribeDate,
                           IsCancelSubscribe = x.cancelSubscribe,
                           IsDeclinedSubscribe = x.declineSubscribe,
                           SubscribeActivation = x.activationDate,

                           firstName = m.firstName,
                           lastName = m.lastName,
                           balance = m.balance,
                           activatedCart = m.activatedCart,
                           chargeDay = m.chargeDay,
                           PlanID = x.subscribePlanID,
                           subscribeActivation = m.subscribeActivation,
                           activationCartDate = m.activationCartDate,
                           freeOfferCnt = m.freeOfferCnt,
                           MembershipType = pe.title,

                           city = m.city,
                           country = m.country,
                           dayPhone = m.dayPhone,
                           phone = m.phone,
                           postalAdderss = m.postalAdderss,
                           postalCode = m.postalCode,
                           refferedBy = m.refferedBy,
                           state = m.state,

                           NextSubscribePlanID = np.subscribePlanID,
                           IsLockedOut = am.IsLockedOut,
                       };
            return rval;
        }


        public IQueryable<SubscribePlanXref> SelectSubscribePlanXref(long subscribe_plan_id)
        {
            return from s in this.SubscribePlanXrefItems
                   where s.subscribePlanID == subscribe_plan_id
                   select s;
        }


		public IQueryable<MembershipCart> GetMembershipCart(Guid user_id)
		{
			var rval = (from c in this.MembershipCartsItems
						where c.UserId == user_id
						select c);

			return rval;
		}



        #endregion


        #region Update

        public int UpdateUserName(Guid user_id, string user_name)
        {
            var userName = user_name.ToLower();
            return this.AspnetUserItems.Where(m => m.UserId == user_id).
                Update(m => new aspnet_User()
                {
                    UserName = user_name,
                    LoweredUserName = userName
                });
        }

        public int UpdatePlanDates(Guid user_id, DateTime? start_date, DateTime? end_date, DateTime? activation_date)
        {
            return this.MembershipXSubscrPlanItems.Where(m => m.UserId == user_id).
                Update(m => new MembershipXrefSubscribePlan()
                {
                    startSubscribeDate = start_date,
                    endSubscribeDate = end_date,
                    activationDate = activation_date,
                });

        }

        public int UpdateBalance(Guid user_id, decimal balance)
        {
            return this.MembershipItems.Where(m => m.UserId == user_id).
                Update(m => new Membership()
                {
                    balance = balance
                });
        }

		public int UpdateMembership(Guid user_id, string first_name, string last_name,
			string country, string state, string city, string postal_address, string postal_code, string phone, string day_phone)
		{
			return this.MembershipItems.Where(m => m.UserId == user_id).
				Update(m => new Membership()
				{
					firstName = first_name,
					lastName = last_name,
					country = country,
					state = state,
					city = city,
					postalAdderss=postal_address,
					postalCode = postal_code,
					phone = phone,
					dayPhone = day_phone
				});
		}

        public int UpdateMembership(Guid user_id, decimal balance, string first_name, string last_name, int free_offer_cnt,
            string country, string state, string city, string postal_address, string postal_code, string phone, string day_phone, bool suspended, bool full_library_access)
        {
            return this.MembershipItems.Where(m => m.UserId == user_id).
                Update(m => new Membership()
                {
                    balance = balance,
                    firstName = first_name,
                    lastName = last_name,
                    freeOfferCnt = free_offer_cnt,
					country = country,
					state = state,
					city = city,
					postalAdderss = postal_address,
					postalCode = postal_code,
					phone = phone,
					dayPhone = day_phone,
                    suspended = suspended,
                    fullLibraryAccess = full_library_access
                });
        }

        public int UpdateMembership(Guid user_id, bool activated_cart, DateTime activated_cart_date)
        {
            return this.MembershipItems.Where(m => m.UserId == user_id).
                Update(m => new Membership()
                {
                    activatedCart = activated_cart,
                    activationCartDate = activated_cart_date
                });
        }

        public int UpdateMembership(Guid user_id, DateTime? subscribe_activation)
        {
            return this.MembershipItems.Where(m => m.UserId == user_id).
                Update(m => new Membership()
                {
                    subscribeActivation = subscribe_activation
                });
        }

        public int UpdateMembership(Guid user_id, byte? charge_day)
        {
            return this.MembershipItems.Where(m => m.UserId == user_id).
                Update(m => new Membership()
                {
                    chargeDay = charge_day
                });
        }

        public int UpdateMembershipSuspendFlag(Guid user_id, bool suspended)
        {
            return this.MembershipItems.Where(m => m.UserId == user_id).
                Update(m => new Membership()
                {
                    suspended = suspended
                });
        }

		/*
        public int UpdateMembershipAddress(string city, string country, string day_phone, string phone, string postal_address, 
            string postal_code, string state, long address_id)
        {
            return this.MembershipAddressItems.Where(m => m.addressID == address_id).
                Update(m => new MembershipAddress()
                {
                    city = city,
                    country = country,
                    dayPhone = day_phone,
                    phone = phone,
                    postalAddress = postal_address,
                    postalCode = postal_code,
                    state = state
                });
        }
		 * */

        public int UpdateMembershipXrefSubscribePlan(Guid user_id, long plan_id, DateTime? start_date, DateTime? end_date)
        {
            return this.MembershipXSubscrPlanItems.Where(m => m.UserId == user_id).
                Update(m => new MembershipXrefSubscribePlan()
                {
                    subscribePlanID = plan_id,
                    startSubscribeDate = start_date,
                    endSubscribeDate = end_date
                });
        }


		public int UpdateMembershipCardState(Guid user_id, string card_number, long cart_state_id)
		{
			return this.MembershipCartsItems.Where(m => m.UserId == user_id && m.membershipCartID == card_number).
				Update(m => new MembershipCart()
				{
					cartStateID = cart_state_id
				});
		}

        public int UpdateMembershipCardTranID(Guid user_id, string card_number, string tran_id, long shopping_transaction_id)
		{
			return this.MembershipCartsItems.Where(m => m.UserId == user_id && m.membershipCartID == card_number).
				Update(m => new MembershipCart()
				{
					tranID = tran_id,
                    shoppingTransactionID = shopping_transaction_id
				});
		}

		public int CancelSubscribe(Guid user_id)
		{
			return this.MembershipXSubscrPlanItems.Where(x => x.UserId == user_id)
				.Update(x => new MembershipXrefSubscribePlan()
				{
					cancelSubscribe = true
				});
		}

        public int UpdateMembershipCard(Guid user_id, DateTime? expiration_date, string card_type, string membership_card_id, long card_state_id)
        {
            return this.MembershipCartsItems.Where(m => m.UserId == user_id && m.membershipCartID == membership_card_id).
                Update(m => new MembershipCart()
                {
                    expirationDate = expiration_date,
                    cartTypeID = card_type,
                    membershipCartID = membership_card_id,
                    cartStateID = card_state_id
                });
        }

        public int UpdateSubscribeActivation(Guid user_id, DateTime subscribe_activation, byte activation_fail_cnt)
        {
            return this.MembershipXSubscrPlanItems.Where(x => x.UserId == user_id)
                .Update(x => new MembershipXrefSubscribePlan()
                {
                    activationDate = subscribe_activation,
                    activationFailCnt = activation_fail_cnt
                });
        }

        public int UpdateSubscribeDecline(Guid user_id, bool decline_subscribe)
        {
            return this.MembershipXSubscrPlanItems.Where(x => x.UserId == user_id)
                .Update(x => new MembershipXrefSubscribePlan()
                {
                    declineSubscribe = decline_subscribe
                });
        }

        public int UpdateEmail(Guid user_id, string email)
        {
            var loweredEmail = email.ToLower();
            return this.AspnetMembershipItems.Where(m => m.UserId == user_id).
                Update(m => new aspnet_Membership()
                {
                    LoweredEmail = loweredEmail,
                    Email = email
                });
        }

        public int LockUser(Guid user_id, string email)
        {
            var loweredEmail = email.ToLower();
            return this.AspnetMembershipItems.Where(m => m.UserId == user_id).
                Update(m => new aspnet_Membership()
                {
                    LoweredEmail = loweredEmail,
                    Email = email,
                    IsLockedOut = true
                });
        }


        #endregion



        #region Insert

        public int InsertMembershipCard(Guid user_id, DateTime? expiration_date, string card_type,
            string membership_card_id, long card_state_id)
        {
            return this.MembershipCartsItems.Insert(() => new MembershipCart()
                {
                    UserId = user_id,
                    expirationDate = expiration_date,
                    cartTypeID = card_type,
                    membershipCartID = membership_card_id,
                    cartStateID = card_state_id
                });
        }

        public int InsertMembershipCart(string membership_cart_id, Guid user_id, long cart_state_id, DateTime expiration_date,
            string cart_type_id, string tran_id)
        {
            return this.MembershipCartsItems.Insert(() => new MembershipCart()
            {
                membershipCartID = membership_cart_id.Substring(membership_cart_id.Length - 4),
                UserId = user_id,
                cartStateID = (long)cart_state_id,
                expirationDate = expiration_date,
                cartTypeID = cart_type_id,
                tranID = tran_id
            });
        }

        public int InsertMembershipXrefSubscribePlan(Guid user_id, long plan_id, DateTime? start_date, DateTime? end_date, DateTime? activation_date,
            bool cancel_subscribe = false, bool decline_subscribe=false)
        {
            return this.MembershipXSubscrPlanItems.Insert(() => new MembershipXrefSubscribePlan()
            {
                UserId = user_id,
                subscribePlanID = plan_id,
                startSubscribeDate = start_date,
                endSubscribeDate = end_date,
                cancelSubscribe = cancel_subscribe,
                activationDate = activation_date,
                declineSubscribe = decline_subscribe
            });
        }

		public int InsertMembership(Guid user_id, decimal balance, string first_name, string last_name, int free_offer_cnt,
			string country, string state, string city, string postal_address, string postal_code, string phone, string day_phone, bool all_Files_Available,
			DateTime? subscribe_activation = null, bool activated_card = false, DateTime? activation_cart_date = null)
		{
			return this.MembershipItems.Insert(() => new Membership()
			{
				UserId = user_id,
				balance = balance,
				firstName = first_name,
				lastName = last_name,

				country = country,
				state = state,
				city = city,
				postalAdderss = postal_address,
				postalCode = postal_code,
				phone = phone,
				dayPhone = day_phone,
                fullLibraryAccess = all_Files_Available,

				activatedCart = activated_card,
                subscribeActivation = subscribe_activation,
				activationCartDate = activation_cart_date,
				freeOfferCnt = free_offer_cnt
			});
		}

		public long InsertMembershipAddress(Guid user_id, bool is_billing_address, string country, string postal_code,
			string state, string city, string postal_address, string phone, string dayPhone = null, string first_name = null,
			string last_name = null, string description = null)
		{
			return long.Parse(this.MembershipAddressItems.InsertWithIdentity(() => new MembershipAddress()
			{
				UserId = user_id,
				isBillingAddress = is_billing_address,
				country = country,
				postalCode = postal_code,
				state = state,
				city = city,
				postalAddress = postal_address,
				phone = phone,
				dayPhone = dayPhone,
				firstName = first_name,
				lastName = last_name,
				description = description
			}).ToString());
		}

        public int InsertMembershipXReferrer(Guid user_id, string referrer_code)
        {
            return this.MembershipXReferrerItems.Insert(() => new MembershipXrefReferrer()
            {
                UserId = user_id,
                referrerCode = referrer_code                
            });
        }


        #endregion

        #region Delete

        public int DeleteMembershipXrefSubscribePlan(Guid user_id)
        {
            return this.MembershipXSubscrPlanItems.Where(s => s.UserId == user_id).Delete();
        }

        #endregion

    }
}
