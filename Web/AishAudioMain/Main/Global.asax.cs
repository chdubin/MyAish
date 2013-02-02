using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Main.Common;
using MvcContrib.ControllerFactories;
using Main.Common.Routing;
using MainEntity.Interfaces;
using System.Web.Security;
using System.IO;
using Main.Common.AmazonS3;
using MainCommon.Daemon;
using System.Web.Profile;
using Main.Common.Skipjack;
using MainCommon;
using System.Collections;
using System.Text.RegularExpressions;

namespace Main
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static UnityContainer Container { get; private set; }
        public static MainHelper Helper{get; private set;}
        public static S3Daemon S3Amazon { get; private set; }
        public static SkipjackDaemon Skipjack { get; private set; }
        public static SubscribeActivationDaemon ActivationDaemon { get; private set; }

        public static void RegisterAllRouters()
        {
            using (RouteTable.Routes.GetWriteLock())
            {
                RouteTable.Routes.Clear();
                AreaRegistration.RegisterAllAreas();
                RegisterRoutes(RouteTable.Routes);
            }
        }

        protected void Application_Start()
        {
            Container = new UnityContainer();
            Helper = InitializeHelper();
            S3Amazon = new S3Daemon(Properties.Settings.Default.S3AmazonAccessKey, Properties.Settings.Default.S3AmazonSecretKey,
                Properties.Settings.Default.S3AmazonBucketName, Properties.Settings.Default.S3AmazonKeyPrefix, Properties.Settings.Default.UploadTemporaryPath);
            SubscribeDaemon.StartSubscribeThread();
            ControllerBuilder.Current.SetControllerFactory(GetControllerFactory(Container));
            Skipjack = Container.Resolve<SkipjackDaemon>();
            ActivationDaemon = new SubscribeActivationDaemon(Container.Resolve<IUserService>(), Container.Resolve<IShoppingService>());

            RegisterAllRouters();

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new ThemeViewEngine());
        }

        protected void Application_End()
        {
            S3Amazon.Dispose();
        }

        private MainHelper InitializeHelper()
        {
            var applicationNames = Membership.Providers.Cast<MembershipProvider>().Select(m => m.ApplicationName).Where(n => string.Compare(n, Properties.Settings.Default.PasswordProtectionAppName, true) != 0).ToArray();
            var path = HttpContext.Current.Server.MapPath("~/Content");
            var themes = Directory.GetDirectories(path).
                Select(d => d.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries).Last()).
                Except(new string[]{"images","Views"}).ToArray();
            return new MainHelper(applicationNames, themes);
        }

        public void Profile_OnMigrateAnonymous(object sender, ProfileMigrateEventArgs args)
        {
            var anonimouseProfile = ProfileBase.Create(args.AnonymousID);


            long subscribePlanID = long.Parse(HttpContext.Current.GetValue(CartItemTypeEnum.Subscribe, "0"));
            long unitsID = long.Parse(HttpContext.Current.GetValue(CartItemTypeEnum.Unit, "0"));
            var productsIds = (HttpContext.Current.GetValue(CartItemTypeEnum.Subscribe, string.Empty)).Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(id => long.Parse(id)).ToArray();
            var packadgesIds = (HttpContext.Current.GetValue(CartItemTypeEnum.Package, string.Empty)).Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(id => long.Parse(id)).ToArray();

            if (subscribePlanID != 0 || unitsID != 0 || productsIds.Length > 0 || packadgesIds.Length > 0)
            {
                var shoppingService = Container.Resolve<IShoppingService>();

                var userID = (Guid)this.Context.GetCurrentMembershipProvider().GetUser(args.Context.User.Identity.Name, false).ProviderUserKey;
                var subscriber = shoppingService.GetMembershipAndActiveSubscribe(userID, DateTime.Now);
                var user = shoppingService.GetMembership(userID);

                foreach (var id in productsIds)
                    shoppingService.AddItemToShoppingCart(userID, id, CartItemTypeEnum.Class, System.Web.HttpContext.Current);
                foreach (var id in packadgesIds)
                    shoppingService.AddItemToShoppingCart(userID, id, CartItemTypeEnum.Package, System.Web.HttpContext.Current);
                if (subscribePlanID > 0 && subscriber == null)
                {
                    if (user.subscribeActivation != null)
                    {
                        var userService = Container.Resolve<IUserService>();
                        var freeSubscribeID = long.Parse(System.Configuration.ConfigurationManager.AppSettings["MonthlyMembershipSubscribeID"]);
                        subscribePlanID = userService.GetNexSubscribePlan(freeSubscribeID);

                        shoppingService.AddItemToShoppingCart(userID, subscribePlanID, CartItemTypeEnum.Subscribe, System.Web.HttpContext.Current);
                    }
                    else
                        shoppingService.AddItemToShoppingCart(userID, subscribePlanID, CartItemTypeEnum.Subscribe, System.Web.HttpContext.Current);
                }
            }


            ProfileManager.DeleteProfile(args.AnonymousID);
            AnonymousIdentificationModule.ClearAnonymousIdentifier();
        }

        private static void RegisterRoutes(RouteCollection routes)
        {
            var portalService = Container.Resolve<IPortalService>();

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /******** STATIC PAGES START ************/
            #region Top Navigation
            routes.MapPortalRoute("HDRWhatIsAishAudioPage", portalService, "what-is-aishaudio", new { controller = "Static", action = "HDRWhatIsAishAudio" },
                null,
                new string[] { "Main.Controllers" }
                );

            routes.MapPortalRoute("HDRGettingStartedPage", portalService, "getting-started-top", new { controller = "Static", action = "HDRGettingStarted" },
                null,
                new string[] { "Main.Controllers" }
                );

            routes.MapPortalRoute("HDRBenefitsOfMembershipPage", portalService, "benefits-of-membership", new { controller = "Static", action = "HDRBenefitsOfMembership" },
                null,
                new string[] { "Main.Controllers" }
                );

            routes.MapPortalRoute("HDRAvailablePlansPage", portalService, "available-plans", new { controller = "Static", action = "HDRAvailablePlans" },
                null,
                new string[] { "Main.Controllers" }
                );
            #endregion

            #region Bottom Navigation
            routes.MapPortalRoute("ContactPage", portalService, "contact-us", new { controller = "Static", action = "ContactUs" },
                null,
                new string[] { "Main.Controllers" }
                );

            routes.MapPortalRoute("HelpFAQPage", portalService, "help-and-faq", new { controller = "Static", action = "HelpFAQ" },
                null,
                new string[] { "Main.Controllers" }
                );

            routes.MapPortalRoute("LegalPolicyPage", portalService, "legal-policy", new { controller = "Static", action = "LegalPolicy" },
                null,
                new string[] { "Main.Controllers" }
                );

            routes.MapPortalRoute("PrivacyPolicyPage", portalService, "privacy-policy", new { controller = "Static", action = "PrivacyPolicy" },
                null,
                new string[] { "Main.Controllers" }
                );

            #region Press Room pages
            routes.MapPortalRoute("PressRoomPage", portalService, "press-room-and-contacts", new { controller = "Static", action = "PressRoom" },
                null,
                new string[] { "Main.Controllers" }
                );

            routes.MapPortalRoute("PressRoomAishFactsPage", portalService, "press-room-aishaudio-facts", new { controller = "Static", action = "PressRoomAishFacts" },
                null,
                new string[] { "Main.Controllers" }
                );

            routes.MapPortalRoute("PressRoomSpeakersPage", portalService, "press-room-speaker-bios", new { controller = "Static", action = "PressRoomSpeakers" },
                null,
                new string[] { "Main.Controllers" }
                );

            routes.MapPortalRoute("PressRoomTestimonialsPage", portalService, "press-room-testimonials", new { controller = "Static", action = "PressRoomTestimonials" },
                null,
                new string[] { "Main.Controllers" }
                );
            #endregion

            #endregion

            #region Getting Started block
            routes.MapPortalRoute("GSHowAishAudioWorksPage", portalService, "getting-started", new { controller = "Static", action = "GSHowAishAudioWorks" },
                null,
                new string[] { "Main.Controllers" }
                );

            routes.MapPortalRoute("GSWhatIsAishAudioPage", portalService, "getting-started/what-is-aishaudio", new { controller = "Static", action = "GSWhatIsAishAudio" },
                null,
                new string[] { "Main.Controllers" }
                );

            routes.MapPortalRoute("GSBasicRequirementsPage", portalService, "getting-started/basic-requirements", new { controller = "Static", action = "GSBasicRequirements" },
                null,
                new string[] { "Main.Controllers" }
                );

            routes.MapPortalRoute("GSFindingClassesPage", portalService, "getting-started/finding-classes", new { controller = "Static", action = "GSFindingClasses" },
                null,
                new string[] { "Main.Controllers" }
                );

            routes.MapPortalRoute("GSDownloadAndStreamPage", portalService, "getting-started/downloading-and-streaming", new { controller = "Static", action = "GSDownloadAndStream" },
                null,
                new string[] { "Main.Controllers" }
                );

            routes.MapPortalRoute("GSBurningExportingPage", portalService, "getting-started/burning-and-exporting", new { controller = "Static", action = "GSBurningExporting" },
                null,
                new string[] { "Main.Controllers" }
                );

            routes.MapPortalRoute("GSAccountAndPaymentsPage", portalService, "getting-started/account-and-payments", new { controller = "Static", action = "GSAccountAndPayments" },
                null,
                new string[] { "Main.Controllers" }
                );

            routes.MapPortalRoute("GSCommonQuestionsPage", portalService, "getting-started/common-questions", new { controller = "Static", action = "GSCommonQuestions" },
                null,
                new string[] { "Main.Controllers" }
                );

            routes.MapPortalRoute("GSCustomerSupportPage", portalService, "getting-started/customer-support", new { controller = "Static", action = "GSCustomerSupport" },
                null,
                new string[] { "Main.Controllers" }
                );
            #endregion

            routes.MapPortalRoute("TorahPortions", portalService, "torah-portions", new { controller = "Static", action = "TorahPortions" },
                null,
                new string[] { "Main.Controllers" }
            );
            
            /******** STATIC PAGES END **************/

            /******** ACCOUNT PAGES START **************/
            #region Account Routes

            routes.MapPortalRoute("Register", portalService, "register", new { controller = "Account", action = "Register" },
                null,
                new string[] { "Main.Controllers" }
            );

            routes.MapPortalRoute("Offerings", portalService, "offerings", new { controller = "Account", action = "Offerings" },
                null,
                new string[] { "Main.Controllers" }
            );

            #endregion

            #region Shopping Pages

            routes.MapPortalRoute("FreeMP3", portalService, "free-mp3-players", new { controller = "Shopping", action = "FreeMP3" },
                null,
                new string[] { "Main.Controllers" }
            );

            routes.MapPortalRoute("FreeDownloads", portalService, "free-downloads", new { controller = "Shopping", action = "FreeOffer" },
                null,
                new string[] { "Main.Controllers" }
            );

            routes.MapPortalRoute("IPodOffer", portalService, "ipod-offers", new { controller = "Shopping", action = "IPodOffer" },
                null,
                new string[] { "Main.Controllers" }
            );

            #endregion


            var aliases = portalService.GetAliases();
            var constraints = new { portal_part = new PortalConstraint(aliases) };

            routes.MapPortalRoute("Portal/SearchTemplate", portalService,
                "{portal_part}/search/results/{*key_values}",
                new { controller = "Search", action = "Results" },
                constraints,
                new string[] { "Main.Controllers" });

            routes.MapPortalRoute("Portal/SearchTemplateDetail", portalService,
                "{portal_part}/search/resultsdetail/{*key_values}",
                new { controller = "Search", action = "ResultsDetail" },
                constraints,
                new string[] { "Main.Controllers" });

            routes.MapPortalRoute("Portal/Default", portalService,
                "{portal_part}/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                constraints,
                new string[] { "Main.Controllers" });



            routes.MapPortalRoute("SearchTemplate", portalService,
                "search/results/{*key_values}",
                new { controller = "Search", action = "Results" },
                null,
                new string[] { "Main.Controllers" });

            routes.MapPortalRoute("SearchTemplateDetail", portalService,
                "search/resultsdetail/{*key_values}",
                new { controller = "Search", action = "ResultsDetail" },
                null,
                new string[] { "Main.Controllers" });

            //routes.MapPortalRoute("PageOneColumn", portalService,
            //    "Page/1/{param}",
            //    new { controller = "Page", action = "OneColumn" },
            //    null,
            //    new string[] { "Main.Controllers" });

            //routes.MapPortalRoute("PageTwoColumn", portalService,
            //    "Page/{param}",
            //    new { controller = "Page", action = "Default" },
            //    null,
            //    new string[] { "Main.Controllers" });

            routes.MapPortalRoute("Default", portalService,
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                null,
                new string[] { "Main.Controllers" });

            //foreach (var alias in aliases.Where(a => !a.alias.StartsWith("/"))
            //    .Select(a => a.alias.Split(new[] { '/' }, 2))
            //    .Select(a => a.Length > 1 ? a[1].ToLowerInvariant() + "/" : string.Empty)
            //    .OrderByDescending(a => a.Length))
            //{
            //    string routeName;

            //    routeName = alias + "Default";
            //    if (routes[routeName] == null)
            //    {
            //        routes.MapPortalRoute(routeName, portalService,
            //            alias + "{controller}/{action}/{id}",
            //            new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //            null,
            //            new string[] { "Main.Controllers" });
            //    }
            //}

        }

        private static IControllerFactory GetControllerFactory(UnityContainer container)
        {
            var resolver = new UnityDependencyResolver(container);
            var factory = new IoCControllerFactory(resolver);

            RegisterModelsForFactory(container);
            RegisterControllersForFactory(container);

            return factory;
        }

        private static void RegisterModelsForFactory(UnityContainer container)
        {
            container.RegisterType(typeof(IUserService), typeof(MainBL.UserService), new InjectionConstructor(GlobalConstant.CONNECTION_NAME));
            container.RegisterType(typeof(ITagService), typeof(MainBL.TagService), new InjectionConstructor(GlobalConstant.CONNECTION_NAME));
            container.RegisterType(typeof(IPortalService), typeof(MainBL.PortalService), new InjectionConstructor(GlobalConstant.CONNECTION_NAME));
            container.RegisterType(typeof(ICatalogService), typeof(MainBL.CatalogService), new InjectionConstructor(GlobalConstant.CONNECTION_NAME));
            container.RegisterType(typeof(ISpeakerService), typeof(MainBL.SpeakerService), new InjectionConstructor(GlobalConstant.CONNECTION_NAME));
            container.RegisterType(typeof(IFormsAuthenticationService), typeof(MainBL.FormsAuthenticationService));
            container.RegisterType(typeof(IMembershipService), typeof(MainBL.AccountMembershipService), new InjectionConstructor(GlobalConstant.CONNECTION_NAME));
            container.RegisterType(typeof(IClassService), typeof(MainBL.ClassService), new InjectionConstructor(GlobalConstant.CONNECTION_NAME));
            container.RegisterType(typeof(IShoppingService), typeof(MainBL.ShoppingService), new InjectionConstructor(GlobalConstant.CONNECTION_NAME));
            container.RegisterType(typeof(IFileService), typeof(MainBL.FileService), new InjectionConstructor(GlobalConstant.CONNECTION_NAME));
			container.RegisterType(typeof(IActivityLogService), typeof(MainBL.ActivityLogService), new InjectionConstructor(GlobalConstant.CONNECTION_NAME));
        }

        private static void RegisterControllersForFactory(UnityContainer container)
        {

            Type[] assemblyTypes = typeof(Main.Controllers.HomeController).Assembly.GetTypes();
            foreach (Type type in assemblyTypes)
            {
                if (typeof(IController).IsAssignableFrom(type))
                {
                    container.RegisterType(type, type);
                }
            }

        }


        private static readonly Regex wwwRegex =
        new Regex(@"www\.(?<mainDomain>.*)",
                  RegexOptions.Compiled
                      | RegexOptions.IgnoreCase
                      | RegexOptions.Singleline);

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            if (!Request.Url.Host.Equals("localhost"))
            {
                string hostName = Request.Headers["x-forwarded-host"];
                hostName = string.IsNullOrEmpty(hostName) ? Request.Url.Host : hostName;
                Match match = wwwRegex.Match(hostName);
                if (match.Success)
                {
                    string mainDomain = match.Groups["mainDomain"].Value;
                    var builder = new UriBuilder(Request.Url)
                    {
                        Host = mainDomain
                    };
                    string redirectUrl = builder.Uri.ToString();
                    Response.Clear();
                    Response.StatusCode = 301;
                    Response.StatusDescription = "Moved Permanently";
                    Response.AddHeader("Location", redirectUrl);
                    Response.End();
                }
            }
        }
    }
}
