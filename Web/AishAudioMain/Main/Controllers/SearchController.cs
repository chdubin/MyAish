using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MainEntity.Interfaces;
using MainEntity.Models.Class;
using Main.Models.ControllerView.Search;
using Main.Areas.Admin.Models.Common;
using System.Web.Routing;
using System.Collections.Specialized;
using Main.Common;
using MainCommon;
using Main.Common.Attributes;

using Main.Utilities;

namespace Main.Controllers
{
    [MyRequireHttpsAttribute]
    [AuthorizedOnlyPortal]
    public partial class SearchController : BaseController
    {
        private ITagService _tagService;
        private IUserService _userService;
        //private ISpeakerService _speakerService;

        public SearchController(ITagService tag_service, IUserService user_service)//, ISpeakerService speaker_service)
        {
            _tagService = tag_service;
            _userService = user_service;
            //_speakerService = speaker_service;
        }

        //
        // GET: /Search/

        public ActionResult Index()
        {
            return View("Index", "twoColumn");
        }

        private static string ParseInputString(string input_string)
        {
            return input_string.Replace("-", " ").Replace("slash", "/");
        }

        private static string[] ParseInputStringArray(string input_string)
        {
            return input_string.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private static long[] ParseInputLongArray(string input_string)
        {
            return input_string.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries).Select(s => long.Parse(s)).ToArray();
        }

        private static string[] ParseInputWordsArray(string input_string)
        {
            /* TEST */
            string temp;
            string codeExpression = @"[a-zA-Z]{2}\-\d{3}(\-[a-zA-Z]{1,2}){0,3}$";
            string stopwordExpression = @"[\-]{0,1}\b(a|am|an|and|any|are|as|at|be|but|by|did|do|does|doesn't|doing|don't|each|few|for|from|had|hadn't|has|hasn't|have|haven't|having|he|he'd|he'll|he's|her|here|here's|hers|herself|him|himself|his|how|how's|i|i'd|i'll|i'm|i've|if|in|into|is|isn't|it|it's|its|itself|let's|me|more|most|my|myself|no|nor|not|of|off|on|once|only|or|other|ought|our|ours|ourselves|out|over|own|same|shan't|she|she'd|she'll|she's|should|shouldn't|so|some|such|than|that|that's|the|their|theirs|them|themselves|then|there|there's|these|they|they'd|they'll|they're|they've|this|those|through|to|too|under|until|up|very|was|wasn't|we|we'd|we'll|we're|we've|were|weren't|what|what's|when|when's|where|where's|which|while|who|who's|whom|why|why's|with|won't|would|wouldn't|you|you'd|you'll|you're|you've|your|yours|yourself|yourselves)\b[\-]{0,1}";

            Regex codeRegex = new Regex(codeExpression);

            Match match = codeRegex.Match(input_string);

            temp = Regex.Replace(input_string, stopwordExpression, "-");
            temp = Regex.Replace(temp, @"^\-+$", "");

            if (match.Success)
            {
                string[] arr = { input_string.Replace("-", " ") };
                return arr;
            }
            else if (temp != "")
            {
                return temp.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            }
            else
                return input_string.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public ActionResult SearchForm()
        {
            // get levels
            this.ViewData["levels"] = _tagService.GetLevels();

            // get categories
            this.ViewData["categories"] = _tagService.GetCategories();

            return View("SearchForm", "twoColumn");
        }

        /// <summary>
        /// Страница с классами. Отфильтрованными и с учетом поиска.
        /// </summary>
        /// <param name="p">Номер страницы.</param>
        /// <param name="ps">Размер страницы.</param>
        /// <param name="cat">Название категории.</param>
        /// <param name="words">Ключевые слова (поиск а заголовке и в описании).</param>
        /// <param name="code">Код.</param>
        /// <param name="title">Заголовок (поиск в заголовке).</param>
        /// <param name="id">Идентификаторы классов, которые надо включить в результат поиска.</param>
        /// <param name="speaker">Имя спикера (поиск в спикере).</param>
        /// <param name="class">Идентификатор класса (только он будет отображен в результатах поиска).</param>
        /// <param name="cd">Фильтр по классам, имеющим диски.</param>
        /// <param name="mp3">Фильтр по классам, имеющим файлы.</param>
        /// <param name="tape">Фильтр по классам, имеющим кассеты.</param>
        /// <param name="filter">Флаг. true - в выдаче отфильтрованные классы, false - в выдаче поиск по классам.</param>
        /// <param name="cat_equals">Название категории. Предполагает точное совпадение с именем категории.</param>
        /// <param name="level">Уровень классов для фильтра.</param>
        /// <returns>Страница с результатами посика или фильтрования.</returns>
        public ActionResult Results(int p = 1, int ps = 0, int rn = 0, string cat = "", string words = "",
            string code = "", string title = "", string id = "", string speaker = "", int @class = 0, string category = null,
            bool cd = false, bool mp3 = false, bool tape = false, short? sort = null, bool filter = false, string cat_equals = "", int level = 0, int additional = 0)
        {
            bool excludeWithoutFile = GetExcludeWithoutFile(additional);

            long currentPortalID = this.HttpContext.GetCurrentPortal().portalID;
            long[] classInCartIDs = ShoppingService.GetClassProductInCartIDs(System.Web.HttpContext.Current);
            int recordsCount;
            int recordsCountWithoutFile = 0;
            string[] searchWords = ParseInputWordsArray(words);
            long[] searchIDs = ParseInputLongArray(id);
            cat = ParseInputString(cat);
            cat_equals = ParseInputString(cat_equals);
            speaker = ParseInputString(speaker);

            List<ClassListItem> model;

            if (ps > 0) this.HttpContext.SetValue(SessionEnum.CatalogPageSize, ps.ToString());
            else ps = int.Parse(this.HttpContext.GetValue(SessionEnum.CatalogPageSize, "10"));

            if (filter)
                model = GetFilteredClassModel(ClassService, p, ps, rn, currentPortalID, cat, title, speaker, cat_equals, code, level, cd, mp3, tape, out recordsCount, classInCartIDs,
                    excludeWithoutFile, out recordsCountWithoutFile);
            else if (sort != null)
                model = GetSortedClassModel(ClassService, p, ps, rn, currentPortalID, out recordsCount, classInCartIDs, (SortClassesEnum)sort.Value,
                    excludeWithoutFile, out recordsCountWithoutFile);
            else
                model = GetClassesModel(ClassService, p, ps, rn, currentPortalID, cat, searchWords, code, title, searchIDs, speaker, @class, classInCartIDs, out recordsCount,
                    excludeWithoutFile, out recordsCountWithoutFile, SortClassesEnum.Code);

            this.ViewData["SearchWord"] = !string.IsNullOrEmpty(code) ? code : words;
            this.ViewData["Pager"] = new PagingData(Request.Path, Request.QueryString, recordsCount, ps, p, rn - 1, 10);
            this.ViewData["RecordsCountWithoutFile"] = recordsCountWithoutFile;
            this.ViewData["ActiveSubscribe"] = this.Subscriber != null;
            this.ViewData["UnitsRate"] = Properties.Settings.Default.UnitsRate;
            this.ViewData["SubscriberDiscount"] = (decimal)Properties.Settings.Default.SubscriberDiscount / 100;
            this.ViewData["CurrentCategory"] = cat;

            if (!string.IsNullOrEmpty(speaker))
            {
                Main.Areas.Admin.Models.Speaker.SpeakerEditModel speakerModel;
                var speakerInfo = SpeakerService.GetSpeaker(speaker);
                if (speakerInfo != null)
                {
                    speakerModel = new Main.Areas.Admin.Models.Speaker.SpeakerEditModel(speakerInfo);
                    speakerModel.PhotoPath = SpeakerService.GetSpeakerPhoto(speakerInfo.SpeakerEntity.speakerID);
                }
                else speakerModel = new Main.Areas.Admin.Models.Speaker.SpeakerEditModel() { Name = speaker };

                this.ViewData["Speaker"] = speakerModel;
            }

            //if ((cat + code.Replace(words.Length == 0 ? " " : words, "") + title + speaker).Length == 0 && string.Join(" ", searchWords).Length > 0)
            //    this.ViewData["SearchTitleWords"] = string.Join(" ", searchWords);
            //else if (sort != null)
            //    this.ViewData["SearchTitleWords"] = ((SortClassesEnum)sort.Value == SortClassesEnum.New ? "New" : "Top");
            this.ViewData["SearchTitleWords"] = !string.IsNullOrEmpty(category) ? category.Replace('+', ' ') : !string.IsNullOrEmpty(title) ? title : cat;

            MainBL.CookieBL.SetDetailedList(Request.Cookies, Response.Cookies, false);

            return View("Results", "twoColumn", (sort == null ? model.OrderBy(x => x.Code).ToArray() : model.ToArray()));
        }

        public ActionResult ResultsDetail(int p = 1, int ps = 0, int rn = 0, string cat = "", string words = "", string code = "",
            string title = "", string id = "", string speaker = "", long @class = 0, string category = null,
            bool cd = false, bool mp3 = false, bool tape = false, short? sort = null, bool filter = false, string cat_equals = "", int level = 0, int additional = 0)
        {
            bool excludeWithoutFile = GetExcludeWithoutFile(additional);
            int recordsCountWithoutFile = 0;
            long currentPortalID = this.HttpContext.GetCurrentPortal().portalID;
            // long[] classInCartIDs = ShoppingService.GetClassProductInCartIDs(System.Web.HttpContext.Current);
            long[] classInCartIDs = ShoppingService.GetAllProductsInCart(System.Web.HttpContext.Current);
            int recordsCount;

            string[] searchWords = ParseInputWordsArray(words);
            long[] searchIDs = ParseInputLongArray(id);
            cat = ParseInputString(cat);
            cat_equals = ParseInputString(cat_equals);
            speaker = ParseInputString(speaker);

            List<ClassListItem> model;

            if (ps > 0) this.HttpContext.SetValue(SessionEnum.CatalogPageSize, ps.ToString());
            else ps = int.Parse(this.HttpContext.GetValue(SessionEnum.CatalogPageSize, "10"));

            if (filter)
                model = GetFilteredClassModel(ClassService, p, ps, rn, currentPortalID, cat, title, speaker,
                    cat_equals, code, level, cd, mp3, tape, out recordsCount, classInCartIDs, excludeWithoutFile, out recordsCountWithoutFile);
            else if (sort != null)
                model = GetSortedClassModel(ClassService, p, ps, rn, currentPortalID, out recordsCount, classInCartIDs, (SortClassesEnum)sort.Value, excludeWithoutFile, out recordsCountWithoutFile);
            else
                model = GetClassesModel(ClassService, p, ps, rn, currentPortalID, cat, searchWords, code,
                    title, searchIDs, speaker, @class, classInCartIDs, out recordsCount, excludeWithoutFile, out recordsCountWithoutFile, SortClassesEnum.Code);

            this.ViewData["Pager"] = new PagingData(Request.Path, Request.QueryString, recordsCount, ps, p, rn - 1, 10);
            this.ViewData["RecordsCountWithoutFile"] = recordsCountWithoutFile;
            this.ViewData["SearchWord"] = !string.IsNullOrEmpty(code) ? code : words;
            this.ViewData["ActiveSubscribe"] = this.Subscriber != null;
            this.ViewData["UnitsRate"] = Properties.Settings.Default.UnitsRate;
            this.ViewData["SubscriberDiscount"] = (decimal)Properties.Settings.Default.SubscriberDiscount / 100;
            this.ViewData["CurrentCategory"] = cat;

            if (!string.IsNullOrEmpty(speaker))
            {
                Main.Areas.Admin.Models.Speaker.SpeakerEditModel speakerModel;
                var speakerInfo = SpeakerService.GetSpeaker(speaker);
                if (speakerInfo != null)
                {
                    speakerModel = new Main.Areas.Admin.Models.Speaker.SpeakerEditModel(speakerInfo);
                    speakerModel.PhotoPath = SpeakerService.GetSpeakerPhoto(speakerInfo.SpeakerEntity.speakerID);
                }
                else speakerModel = new Main.Areas.Admin.Models.Speaker.SpeakerEditModel() { Name = speaker };

                this.ViewData["Speaker"] = speakerModel;
            }

            //if ((cat + code.Replace(words.Length == 0 ? " " : words, "") + title + speaker).Length == 0 && string.Join(" ", searchWords).Length > 0)
            //    this.ViewData["SearchTitleWords"] = string.Join(" ", searchWords);
            //else if (sort != null)
            //    this.ViewData["SearchTitleWords"] = ((SortClassesEnum)sort.Value == SortClassesEnum.New ? "New" : "Top");
            this.ViewData["SearchTitleWords"] = !string.IsNullOrEmpty(category) ? category.Replace('+', ' ') : !string.IsNullOrEmpty(title) ? title : cat;

            MainBL.CookieBL.SetDetailedList(Request.Cookies, Response.Cookies, true);

            return View("ResultsDetail", "twoColumn", (sort == null ? model.OrderBy(x => x.Code).ToArray() : model.ToArray()));
        }

        private bool GetExcludeWithoutFile(int additional)
        {
            if (additional != 0)
            {
                if (additional > 0)
                    this.HttpContext.Profile.GetProfileGroup("UI")["Search_ShowAdditionalTapeOrCD"] = true;
                else
                    this.HttpContext.Profile.GetProfileGroup("UI")["Search_ShowAdditionalTapeOrCD"] = false;
                this.HttpContext.Profile.Save();
            }
            bool excludeWithoutFile = !(bool)this.HttpContext.Profile.GetProfileGroup("UI")["Search_ShowAdditionalTapeOrCD"];
            return excludeWithoutFile;
        }

        public ActionResult FullList(string sort, int additional = 0)
        {
            bool excludeWithoutFile = GetExcludeWithoutFile(additional);
            long currentPortalID = this.HttpContext.GetCurrentPortal().portalID;
            var classes = ClassService.GetClasses(Main.GlobalConstant.ROOT_ENTITY_ID, currentPortalID, 0, int.MaxValue, excludeWithoutFile ? (int?)ProductTypeEnum.File : null);
            int recordsCountWithoutFile = ClassService.GetClassesCnt(Main.GlobalConstant.ROOT_ENTITY_ID, currentPortalID, excludeWithoutFile ? null : (int?)ProductTypeEnum.File) - classes.Length;

            this.ViewData["RecordsCountWithoutFile"] = recordsCountWithoutFile;

            var model = ClassListItem.GetForList(classes, new long[0]);
            model = model.OrderBy(i => i.SpeakerName).ToList();
            if (!string.IsNullOrEmpty(sort) && (sort == "title" || sort == "code"))
            {
                this.ViewData["sort"] = sort;
                if (sort == "title")
                    model = model.OrderBy(i => i.Title).ToList();
                else
                    model = model.OrderBy(i => i.Code).ToList();
                return View("FullListSorted", "twoColumn", model.ToArray());
            }


            return View("FullList", "twoColumn", model.ToArray());
        }

        public ActionResult Class(long id, bool sp = false)
        {
            long currentPortalID = this.HttpContext.GetCurrentPortal().portalID;

            var data = ClassService.GetClass(Main.GlobalConstant.ROOT_ENTITY_ID, currentPortalID, id);
            var model = ClassListItem.GetForList(new ClassEntity[] { data }, new long[0]);

            ViewData["showPlayer"] = sp;

            return View(model[0]);
        }

        private static void ParseInpuParams(string p1, string v1, string p2, string v2, string p3, string v3, string p4, string v4, out string categoryName, out string[] searchWords, out string filterCode, out string wordInTitle, out long[] class_ids, out string speakerName, out long classID)
        {
            NameValueCollection filterParams = GetParamsObject(p1, v1, p2, v2, p3, v3, p4, v4);
            categoryName = (filterParams["cat"] ?? string.Empty).Replace("-", " ").Replace("slash", "/");
            searchWords = (filterParams["words"] ?? string.Empty).Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            filterCode = (filterParams["code"] ?? string.Empty);
            wordInTitle = (filterParams["title"] ?? string.Empty);
            class_ids = (filterParams["id"] ?? string.Empty).Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries).Select(s => long.Parse(s)).ToArray();
            speakerName = (filterParams["speaker"] ?? string.Empty).Replace("-", " ");
            classID = long.Parse((string)(filterParams["class"] ?? "0"));

        }

        /// <summary>
        /// Возвращает классы после применения фильтра. Запрос фильтра содержит "AND".
        /// </summary>
        /// <returns>Коллекция классов</returns>
        private static List<ClassListItem> GetFilteredClassModel(IClassService service, int p, int ps, int rn, long currentPortalID,
            string cat, string title, string speaker, string cat_equals, string code, int level,
            bool cd, bool mp3, bool tape, out int total_record_cnt, long[] classInCartIDs, bool exclude_without_file, out int record_cnt_without_file)
        {
            List<ClassListItem> rval;
            ClassEntity[] classes;

            List<int> productTypes = new List<int>();
            if (cd)
                productTypes.Add((int)ProductTypeEnum.Disk);
            if (tape)
                productTypes.Add((int)ProductTypeEnum.Tape);
            if (mp3)
                productTypes.Add((int)ProductTypeEnum.File);

            classes = service.GetFilteredClasses(Main.GlobalConstant.ROOT_ENTITY_ID, currentPortalID, rn > 1 ? rn - 1 : (p - 1) * ps, ps, title,
                cat, cat_equals, code, speaker, level, productTypes.ToArray(), exclude_without_file ? (int?)ProductTypeEnum.File : null);

            total_record_cnt = service.GetFilteredClassesCnt(Main.GlobalConstant.ROOT_ENTITY_ID, currentPortalID, title,
                cat, cat_equals, code, speaker, level, productTypes.ToArray(), exclude_without_file ? (int?)ProductTypeEnum.File : null);

            record_cnt_without_file = service.GetFilteredClassesCnt(Main.GlobalConstant.ROOT_ENTITY_ID, currentPortalID, title,
                cat, cat_equals, code, speaker, level, productTypes.ToArray(), exclude_without_file ? null : (int?)ProductTypeEnum.File) - total_record_cnt;

            rval = ClassListItem.GetForList(classes, classInCartIDs);

            return rval;
        }

        private static List<ClassListItem> GetSortedClassModel(IClassService service, int p, int ps, int rn, long currentPortalID,
            out int total_record_cnt, long[] classInCartIDs, SortClassesEnum sort, bool exclude_without_file, out int record_cnt_without_file)
        {
            ClassEntity[] classes = null;

            switch (sort)
            {
                case SortClassesEnum.Top:
                    {
                        classes = service.GetSortedClasses(Main.GlobalConstant.ROOT_ENTITY_ID, currentPortalID, 0, 10, sort);
                        total_record_cnt = classes.Count();
                        record_cnt_without_file = 0;
                        break;
                    }
                case SortClassesEnum.New:
                    {
                        classes = service.GetSortedClasses(Main.GlobalConstant.ROOT_ENTITY_ID, currentPortalID, rn > 1 ? rn - 1 : (p - 1) * ps, ps, sort,
                            exclude_without_file ? (int?)ProductTypeEnum.File : null);
                        total_record_cnt = service.GetSortedClassesCnt(Main.GlobalConstant.ROOT_ENTITY_ID, currentPortalID, sort,
                            exclude_without_file ? (int?)ProductTypeEnum.File : null);
                        record_cnt_without_file = service.GetSortedClassesCnt(Main.GlobalConstant.ROOT_ENTITY_ID, currentPortalID, sort,
                            exclude_without_file ? null : (int?)ProductTypeEnum.File) - total_record_cnt;
                        break;
                    }
                case SortClassesEnum.Code:
                    {
                        classes = service.GetSortedClasses(Main.GlobalConstant.ROOT_ENTITY_ID, currentPortalID, rn > 1 ? rn - 1 : (p - 1) * ps, ps, sort,
    exclude_without_file ? (int?)ProductTypeEnum.File : null);
                        total_record_cnt = service.GetSortedClassesCnt(Main.GlobalConstant.ROOT_ENTITY_ID, currentPortalID, sort,
                            exclude_without_file ? (int?)ProductTypeEnum.File : null);
                        record_cnt_without_file = service.GetSortedClassesCnt(Main.GlobalConstant.ROOT_ENTITY_ID, currentPortalID, sort,
                            exclude_without_file ? null : (int?)ProductTypeEnum.File) - total_record_cnt;
                        break;
                    }
                default:
                    {
                        throw new NotSupportedException("This sort type is not supported");
                    }
            }



            List<ClassListItem> rval = ClassListItem.GetForList(classes, classInCartIDs);
            return rval;
        }

        private static List<ClassListItem> GetClassesModel(IClassService service, int p, int ps, int rn, long currentPortalID, string categoryName,
            string[] searchWords, string filterCode, string wordInTitle, long[] class_ids, string speakerName, long classID,
            long[] classInCartIDs, out int total_record_cnt, bool exclude_without_file, out int record_cnt_without_file, SortClassesEnum sort = (SortClassesEnum)(-1))
        {
            List<ClassListItem> rval;
            ClassEntity[] classes;

            if (classID != 0)
            {
                classes = new ClassEntity[] { service.GetClass(Main.GlobalConstant.ROOT_ENTITY_ID, currentPortalID, classID) };
                total_record_cnt = 1;
                record_cnt_without_file = 0;
            }
            else if (!string.IsNullOrEmpty(speakerName))
            {
                total_record_cnt = service.SearchClassesCnt(Main.GlobalConstant.ROOT_ENTITY_ID, currentPortalID, speakerName,
                    exclude_without_file ? (int?)ProductTypeEnum.File : null);
                record_cnt_without_file = service.SearchClassesCnt(Main.GlobalConstant.ROOT_ENTITY_ID, currentPortalID, speakerName,
                    exclude_without_file ? null : (int?)ProductTypeEnum.File) - total_record_cnt;
                classes = service.SearchClasses(Main.GlobalConstant.ROOT_ENTITY_ID, currentPortalID, speakerName, rn > 1 ? rn - 1 : (p - 1) * ps, ps,
                    exclude_without_file ? (int?)ProductTypeEnum.File : null, sort);
            }
            else
            {
                total_record_cnt = service.SearchClassesCnt(Main.GlobalConstant.ROOT_ENTITY_ID, currentPortalID, categoryName, searchWords, filterCode, wordInTitle, class_ids,
                    exclude_without_file ? (int?)ProductTypeEnum.File : null);
                record_cnt_without_file = service.SearchClassesCnt(Main.GlobalConstant.ROOT_ENTITY_ID, currentPortalID, categoryName, searchWords, filterCode, wordInTitle, class_ids,
                    exclude_without_file ? null : (int?)ProductTypeEnum.File) - total_record_cnt;
                classes = service.SearchClasses(Main.GlobalConstant.ROOT_ENTITY_ID, currentPortalID, categoryName, rn > 1 ? rn - 1 : (p - 1) * ps, ps, searchWords, filterCode, wordInTitle, class_ids,
                    exclude_without_file ? (int?)ProductTypeEnum.File : null);
            }

            rval = ClassListItem.GetForList(classes, classInCartIDs);

            return rval;
        }

        private static NameValueCollection GetParamsObject(params string[] p)
        {
            NameValueCollection nvc = new NameValueCollection();

            for (int i = 0; i < p.Length; i += 2)
                if (!string.IsNullOrEmpty(p[i]) && nvc[p[i]] == null)
                    nvc.Add(p[i], p[i + 1]);

            return nvc;
        }
    }
}
