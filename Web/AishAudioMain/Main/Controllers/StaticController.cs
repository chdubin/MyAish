using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MainEntity.Interfaces;
using Main.Common.Attributes;
using Main.Utilities;

namespace Main.Controllers
{
    [MyRequireHttpsAttribute]
    public class StaticController : BaseController
    {
        /*private ISpeakerService _speakerService;

        public StaticController(ISpeakerService speaker_service)
        {
            _speakerService = speaker_service;
        }*/

        public ActionResult TorahPortions()
        {
            //this.ViewData["Speakers"] = _speakerService.GetSpeakers(Main.GlobalConstant.ROOT_ENTITY_ID, true, true, 0, 10000, MainCommon.SortParametersEnum.Title, true);

            return View();
        }
        #region Top Navigation
        public ActionResult HDRWhatIsAishAudio()
        {
            return View();
        }

        public ActionResult HDRGettingStarted()
        {
            return View();
        }

        public ActionResult HDRBenefitsOfMembership()
        {
            return View();
        }

        public ActionResult HDRAvailablePlans()
        {
            return View();
        }
        #endregion

        #region Bottom Navigation
        public ActionResult ContactUs()
        {
            return View();
        }

        public ActionResult HelpFAQ()
        {
            return View();
        }

        public ActionResult LegalPolicy()
        {
            return View();
        }

        public ActionResult PrivacyPolicy()
        {
            return View();
        }

        #region Press Room pages
        public ActionResult PressRoom()
        {
            return View();
        }

        public ActionResult PressRoomAishFacts()
        {
            return View();
        }

        public ActionResult PressRoomSpeakers()
        {
            return View();
        }

        public ActionResult PressRoomTestimonials()
        {
            return View();
        }
        #endregion

        #endregion

        #region Getting Started block
        public ActionResult GSHowAishAudioWorks()
        {
            return View();
        }

        public ActionResult GSWhatIsAishAudio()
        {
            return View();
        }

        public ActionResult GSBasicRequirements()
        {
            return View();
        }

        public ActionResult GSFindingClasses()
        {
            return View();
        }

        public ActionResult GSDownloadAndStream()
        {
            return View();
        }

        public ActionResult GSBurningExporting()
        {
            return View();
        }

        public ActionResult GSAccountAndPayments()
        {
            return View();
        }

        public ActionResult GSCommonQuestions()
        {
            return View();
        }

        public ActionResult GSCustomerSupport()
        {
            return View();
        }
        #endregion

    }
}