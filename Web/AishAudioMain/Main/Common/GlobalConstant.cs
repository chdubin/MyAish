﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Main
{
    public static class GlobalConstant
    {
        public static string[] BlOCKED_ALIAS_NAMES = new[] {"/", "account/", "audio/", "error/", "home/", "package/", "page/", "search/", "shopping/", "static/",
            "admin/", "admin/amazons3/", "admin/audio", "admin/catalog", "admin/home", "admin/portal","admin/speaker", "admin/transaction", "admin/user"};

        public const string CONNECTION_NAME = "ApplicationServices";
        public const string DEFAULT_THEME_NAME = "Default";

        public const int PORTAL_ALIASES_IN_CACHE_SEC = 10;
        public const int USER_IN_CACHE_SEC = 1;
        public const int S3AMAZON_IN_CACHE_SEC = 60;
        public const int CATEGORY_IN_CACHE_SEC = 600;
		public const int SUBSCRIBEPLAN_IN_CACHE_SEC = 600;

        public const long MAIN_PORTAL_ID = 2;
        public const long ROOT_ENTITY_ID = 1;

        #region Lists

        public static List<KeyValuePair<string, string>> CAME_FROM = new List<KeyValuePair<string, string>>()
        {                    
            new KeyValuePair<string, string>("classicsinai", "ClassicSinai "),
            new KeyValuePair<string, string>("newsletter", "Aish.com newsletter "),
            new KeyValuePair<string, string>("website", "Aish.com site "),
            new KeyValuePair<string, string>("newsarticle", "Western Wall camera page "),
            new KeyValuePair<string, string>("surfing", "Surfing the Web "),
            new KeyValuePair<string, string>("shabbatshalomweekly", "Shabbatshalom Fax/Weekly "),
            new KeyValuePair<string, string>("3free", "3-Free Card "),
            new KeyValuePair<string, string>("friend", "From a friend (enter name below) "),
            new KeyValuePair<string, string>("other", "Other (please specify below)")
        };

        public static List<KeyValuePair<string, string>> STATES = new List<KeyValuePair<string, string>>()
        {                    
            new KeyValuePair<string, string>("AA", "AA"),
            new KeyValuePair<string, string>("AE", "AE"),
            new KeyValuePair<string, string>("AK", "AK"),
            new KeyValuePair<string, string>("AL", "AL"),
            new KeyValuePair<string, string>("AP", "AP"),
            new KeyValuePair<string, string>("AR", "AR"),
            new KeyValuePair<string, string>("AS", "AS"),
            new KeyValuePair<string, string>("AZ", "AZ"),
            new KeyValuePair<string, string>("CA", "CA"),
            new KeyValuePair<string, string>("CO", "CO"),
            new KeyValuePair<string, string>("CT", "CT"),
            new KeyValuePair<string, string>("DC", "DC"),
            new KeyValuePair<string, string>("DE", "DE"),
            new KeyValuePair<string, string>("FL", "FL"),
            new KeyValuePair<string, string>("FM", "FM"),
            new KeyValuePair<string, string>("GA", "GA"),
            new KeyValuePair<string, string>("GU", "GU"),
            new KeyValuePair<string, string>("HI", "HI"),
            new KeyValuePair<string, string>("IA", "IA"),
            new KeyValuePair<string, string>("ID", "ID"),
            new KeyValuePair<string, string>("IL", "IL"),
            new KeyValuePair<string, string>("IN", "IN"),
            new KeyValuePair<string, string>("KS", "KS"),
            new KeyValuePair<string, string>("KY", "KY"),
            new KeyValuePair<string, string>("LA", "LA"),
            new KeyValuePair<string, string>("MA", "MA"),
            new KeyValuePair<string, string>("MD", "MD"),
            new KeyValuePair<string, string>("ME", "ME"),
            new KeyValuePair<string, string>("MH", "MH"),
            new KeyValuePair<string, string>("MI", "MI"),
            new KeyValuePair<string, string>("MN", "MN"),
            new KeyValuePair<string, string>("MO", "MO"),
            new KeyValuePair<string, string>("MP", "MP"),
            new KeyValuePair<string, string>("MS", "MS"),
            new KeyValuePair<string, string>("MT", "MT"),
            new KeyValuePair<string, string>("NC", "NC"),
            new KeyValuePair<string, string>("ND", "ND"),
            new KeyValuePair<string, string>("NE", "NE"),
            new KeyValuePair<string, string>("NH", "NH"),
            new KeyValuePair<string, string>("NJ", "NJ"),
            new KeyValuePair<string, string>("NM", "NM"),
            new KeyValuePair<string, string>("NV", "NV"),
            new KeyValuePair<string, string>("NY", "NY"),
            new KeyValuePair<string, string>("OH", "OH"),
            new KeyValuePair<string, string>("OK", "OK"),
            new KeyValuePair<string, string>("OR", "OR"),
            new KeyValuePair<string, string>("PA", "PA"),
            new KeyValuePair<string, string>("PR", "PR"),
            new KeyValuePair<string, string>("PW", "PW"),
            new KeyValuePair<string, string>("RI", "RI"),
            new KeyValuePair<string, string>("SC", "SC"),
            new KeyValuePair<string, string>("SD", "SD"),
            new KeyValuePair<string, string>("TN", "TN"),
            new KeyValuePair<string, string>("TX", "TX"),
            new KeyValuePair<string, string>("UT", "UT"),
            new KeyValuePair<string, string>("VA", "VA"),
            new KeyValuePair<string, string>("VI", "VI"),
            new KeyValuePair<string, string>("VT", "VT"),
            new KeyValuePair<string, string>("WA", "WA"),
            new KeyValuePair<string, string>("WI", "WI"),
            new KeyValuePair<string, string>("WV", "WV"),
            new KeyValuePair<string, string>("WY", "WY")
            //new KeyValuePair<string, string>("AK", "AK"),
            //new KeyValuePair<string, string>("AL", "AL"),
            //new KeyValuePair<string, string>("AR", "AR"),
            //new KeyValuePair<string, string>("AS", "AS"),
            //new KeyValuePair<string, string>("AZ", "AZ"),
            //new KeyValuePair<string, string>("CA", "CA"),
            //new KeyValuePair<string, string>("CO", "CO"),
            //new KeyValuePair<string, string>("CT", "CT"),
            //new KeyValuePair<string, string>("DC", "DC"),
            //new KeyValuePair<string, string>("DE", "DE"),
            //new KeyValuePair<string, string>("FL", "FL"),
            //new KeyValuePair<string, string>("FM", "FM"),
            //new KeyValuePair<string, string>("GA", "GA"),
            //new KeyValuePair<string, string>("GU", "GU"),
            //new KeyValuePair<string, string>("HI", "HI"),
            //new KeyValuePair<string, string>("IA", "IA"),
            //new KeyValuePair<string, string>("ID", "ID"),
            //new KeyValuePair<string, string>("IL", "IL"),
            //new KeyValuePair<string, string>("IN", "IN"),
            //new KeyValuePair<string, string>("KS", "KS"),
            //new KeyValuePair<string, string>("KY", "KY"),
            //new KeyValuePair<string, string>("LA", "LA"),
            //new KeyValuePair<string, string>("MA", "MA"),
            //new KeyValuePair<string, string>("MD", "MD"),
            //new KeyValuePair<string, string>("ME", "ME"),
            //new KeyValuePair<string, string>("MH", "MH"),
            //new KeyValuePair<string, string>("MI", "MI"),
            //new KeyValuePair<string, string>("MN", "MN"),
            //new KeyValuePair<string, string>("MO", "MO"),
            //new KeyValuePair<string, string>("MS", "MS"),
            //new KeyValuePair<string, string>("MT", "MT"),
            //new KeyValuePair<string, string>("NC", "NC"),
            //new KeyValuePair<string, string>("ND", "ND"),
            //new KeyValuePair<string, string>("NE", "NE"),
            //new KeyValuePair<string, string>("NH", "NH"),
            //new KeyValuePair<string, string>("NJ", "NJ"),
            //new KeyValuePair<string, string>("NM", "NM"),
            //new KeyValuePair<string, string>("NV", "NV"),
            //new KeyValuePair<string, string>("NY", "NY"),
            //new KeyValuePair<string, string>("OH", "OH"),
            //new KeyValuePair<string, string>("OK", "OK"),
            //new KeyValuePair<string, string>("OR", "OR"),
            //new KeyValuePair<string, string>("PA", "PA"),
            //new KeyValuePair<string, string>("PR", "PR"),
            //new KeyValuePair<string, string>("PW", "PW"),
            //new KeyValuePair<string, string>("RI", "RI"),
            //new KeyValuePair<string, string>("SC", "SC"),
            //new KeyValuePair<string, string>("SD", "SD"),
            //new KeyValuePair<string, string>("TN", "TN"),
            //new KeyValuePair<string, string>("TX", "TX"),
            //new KeyValuePair<string, string>("UT", "UT"),
            //new KeyValuePair<string, string>("VA", "VA"),
            //new KeyValuePair<string, string>("VI", "VI"),
            //new KeyValuePair<string, string>("VT", "VT"),
            //new KeyValuePair<string, string>("WA", "WA"),
            //new KeyValuePair<string, string>("WI", "WI"),
            //new KeyValuePair<string, string>("WV", "WV"),
            //new KeyValuePair<string, string>("WY", "WY"),
            //new KeyValuePair<string, string>("AB", "AB"),
            //new KeyValuePair<string, string>("BC", "BC"),
            //new KeyValuePair<string, string>("MB", "MB"),
            //new KeyValuePair<string, string>("NB", "NB"),
            //new KeyValuePair<string, string>("NF", "NF"),
            //new KeyValuePair<string, string>("NT", "NT"),
            //new KeyValuePair<string, string>("NS", "NS"),
            //new KeyValuePair<string, string>("ON", "ON"),
            //new KeyValuePair<string, string>("PE", "PE"),
            //new KeyValuePair<string, string>("QC", "QC"),
            //new KeyValuePair<string, string>("SK", "SK"),
            //new KeyValuePair<string, string>("YT", "YT")
        };

        public static List<KeyValuePair<int, string>> MONTHS = new List<KeyValuePair<int, string>>()
        {                    
            new KeyValuePair<int, string>(1, "Jan"),
            new KeyValuePair<int, string>(2, "Feb"),
            new KeyValuePair<int, string>(3, "Mar"),
            new KeyValuePair<int, string>(4, "Apr"),
            new KeyValuePair<int, string>(5, "May"),
            new KeyValuePair<int, string>(6, "Jun"),
            new KeyValuePair<int, string>(7, "Jul"),
            new KeyValuePair<int, string>(8, "Aug"),
            new KeyValuePair<int, string>(9, "Sep"),
            new KeyValuePair<int, string>(10, "Oct"),
            new KeyValuePair<int, string>(11, "Nov"),
            new KeyValuePair<int, string>(12, "Dec")          
        };

        //public static List<KeyValuePair<int, string>> UNITS_QUANTITY = new List<KeyValuePair<int, string>>()
        //{                    
        //    new KeyValuePair<int, string>(5, "5"),
        //    new KeyValuePair<int, string>(6, "6"),
        //    new KeyValuePair<int, string>(7, "7"),
        //    new KeyValuePair<int, string>(8, "8"),
        //    new KeyValuePair<int, string>(9, "9"),
        //    new KeyValuePair<int, string>(10, "10"),
        //    new KeyValuePair<int, string>(15, "15"),
        //    new KeyValuePair<int, string>(20, "20"),
        //    new KeyValuePair<int, string>(25, "25"),
        //    new KeyValuePair<int, string>(50, "50"),
        //    new KeyValuePair<int, string>(75, "75"),
        //    new KeyValuePair<int, string>(100, "100"),      
        //    new KeyValuePair<int, string>(125, "125"),
        //    new KeyValuePair<int, string>(150, "150"),
        //    new KeyValuePair<int, string>(175, "175"),
        //    new KeyValuePair<int, string>(200, "200"),
        //    new KeyValuePair<int, string>(250, "250"),
        //    new KeyValuePair<int, string>(300, "300"),
        //    new KeyValuePair<int, string>(350, "350"),
        //    new KeyValuePair<int, string>(400, "400"),
        //    new KeyValuePair<int, string>(450, "450"),
        //    new KeyValuePair<int, string>(500, "500")
        //};


        public static List<KeyValuePair<int, string>> YEARS = new List<KeyValuePair<int, string>>()
        {                    
            new KeyValuePair<int, string>(2012, "2012"),
            new KeyValuePair<int, string>(2013, "2013"),
            new KeyValuePair<int, string>(2014, "2014"),
            new KeyValuePair<int, string>(2015, "2015"),
            new KeyValuePair<int, string>(2016, "2016"),
            new KeyValuePair<int, string>(2017, "2017"),
            new KeyValuePair<int, string>(2018, "2018"),
            new KeyValuePair<int, string>(2019, "2019"),
            new KeyValuePair<int, string>(2020, "2020")
        };

        public static List<KeyValuePair<string, string>> COUNTRIES = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("US", "United States"),
            new KeyValuePair<string, string>("IL", "Israel"),
            new KeyValuePair<string, string>("CA", "Canada"),
            new KeyValuePair<string, string>("GB", "United Kingdom"),
            new KeyValuePair<string, string>("AF", "Afghanistan"),
            new KeyValuePair<string, string>("AL", "Albania"),
            new KeyValuePair<string, string>("DZ", "Algeria"),
            new KeyValuePair<string, string>("AS", "American Samoa"),
            new KeyValuePair<string, string>("AD", "Andorra"),
            new KeyValuePair<string, string>("AO", "Angola"),
            new KeyValuePair<string, string>("AI", "Anguilla"),
            new KeyValuePair<string, string>("AQ", "Antarctica"),
            new KeyValuePair<string, string>("AG", "Antigua and Barbuda"),
            new KeyValuePair<string, string>("AR", "Argentina"),
            new KeyValuePair<string, string>("AM", "Armenia"),
            new KeyValuePair<string, string>("AW", "Aruba"),
            new KeyValuePair<string, string>("AU", "Australia"),
            new KeyValuePair<string, string>("AT", "Austria"),
            new KeyValuePair<string, string>("AZ", "Azerbaijan"),
            new KeyValuePair<string, string>("BS", "Bahamas"),
            new KeyValuePair<string, string>("BH", "Bahrain"),
            new KeyValuePair<string, string>("BD", "Bangladesh"),
            new KeyValuePair<string, string>("BB", "Barbados"),
            new KeyValuePair<string, string>("BY", "Belarus"),
            new KeyValuePair<string, string>("BE", "Belgium"),
            new KeyValuePair<string, string>("BZ", "Belize"),
            new KeyValuePair<string, string>("BJ", "Benin"),
            new KeyValuePair<string, string>("BM", "Bermuda"),
            new KeyValuePair<string, string>("BT", "Bhutan"),
            new KeyValuePair<string, string>("BO", "Bolivia"),
            new KeyValuePair<string, string>("BA", "Bosnia Hercegovina"),
            new KeyValuePair<string, string>("BW", "Botswana"),
            new KeyValuePair<string, string>("BV", "Bouvet Island"),
            new KeyValuePair<string, string>("BR", "Brazil"),
            new KeyValuePair<string, string>("BN", "Brunei Darussalam"),
            new KeyValuePair<string, string>("BG", "Bulgaria"),
            new KeyValuePair<string, string>("BF", "Burkina Faso"),
            new KeyValuePair<string, string>("BI", "Burundi"),
            new KeyValuePair<string, string>("KH", "Cambodia"),
            new KeyValuePair<string, string>("CM", "Cameroon"),
            new KeyValuePair<string, string>("CV", "Cape Verde"),
            new KeyValuePair<string, string>("KY", "Cayman Islands"),
            new KeyValuePair<string, string>("CF", "C. African Republic"),
            new KeyValuePair<string, string>("TD", "Chad"),
            new KeyValuePair<string, string>("CL", "Chile"),
            new KeyValuePair<string, string>("CN", "China"),
            new KeyValuePair<string, string>("CX", "Christmas Island"),
            new KeyValuePair<string, string>("CC", "Cocos Islands"),
            new KeyValuePair<string, string>("CO", "Colombia"),
            new KeyValuePair<string, string>("KM", "Comoros"),
            new KeyValuePair<string, string>("CG", "Congo"),
            new KeyValuePair<string, string>("CK", "Cook Islands"),
            new KeyValuePair<string, string>("CR", "Costa Rica"),
            new KeyValuePair<string, string>("CI", "Cote d'Ivoire"),
            new KeyValuePair<string, string>("HR", "Croatia"),
            new KeyValuePair<string, string>("CU", "Cuba"),
            new KeyValuePair<string, string>("CY", "Cyprus"),
            new KeyValuePair<string, string>("CS", "Czech Republic"),
            new KeyValuePair<string, string>("DK", "Denmark"),
            new KeyValuePair<string, string>("DJ", "Djibouti"),
            new KeyValuePair<string, string>("DM", "Dominica"),
            new KeyValuePair<string, string>("DO", "Dominican Republic"),
            new KeyValuePair<string, string>("TP", "East Timor"),
            new KeyValuePair<string, string>("EC", "Ecuador"),
            new KeyValuePair<string, string>("EG", "Egypt"),
            new KeyValuePair<string, string>("SV", "El Salvador"),
            new KeyValuePair<string, string>("GQ", "Equatorial Guinea"),
            new KeyValuePair<string, string>("EE", "Estonia"),
            new KeyValuePair<string, string>("ET", "Ethiopia"),
            new KeyValuePair<string, string>("FK", "Falkland Islands"),
            new KeyValuePair<string, string>("FO", "Faroe Islands"),
            new KeyValuePair<string, string>("FJ", "Fiji"),
            new KeyValuePair<string, string>("FI", "Finland"),
            new KeyValuePair<string, string>("FR", "France"),
            new KeyValuePair<string, string>("GF", "French Guiana"),
            new KeyValuePair<string, string>("GA", "Gabon"),
            new KeyValuePair<string, string>("GM", "Gambia"),
            new KeyValuePair<string, string>("GE", "Georgia"),
            new KeyValuePair<string, string>("DE", "Germany"),
            new KeyValuePair<string, string>("GH", "Ghana"),
            new KeyValuePair<string, string>("GI", "Gibraltar"),
            new KeyValuePair<string, string>("GR", "Greece"),
            new KeyValuePair<string, string>("GL", "Greenland"),
            new KeyValuePair<string, string>("GD", "Grenada"),
            new KeyValuePair<string, string>("GP", "Guadeloupe"),
            new KeyValuePair<string, string>("GU", "Guam"),
            new KeyValuePair<string, string>("GT", "Guatemala"),
            new KeyValuePair<string, string>("GS", "Guernsey"),
            new KeyValuePair<string, string>("GN", "Guinea"),
            new KeyValuePair<string, string>("GW", "Guinea Bissau"),
            new KeyValuePair<string, string>("GY", "Guyana"),
            new KeyValuePair<string, string>("HT", "Haiti"),
            new KeyValuePair<string, string>("HN", "Honduras"),
            new KeyValuePair<string, string>("HK", "Hong Kong"),
            new KeyValuePair<string, string>("HU", "Hungary"),
            new KeyValuePair<string, string>("IS", "Iceland"),
            new KeyValuePair<string, string>("IN", "India"),
            new KeyValuePair<string, string>("ID", "Indonesia"),
            new KeyValuePair<string, string>("IR", "Iran"),
            new KeyValuePair<string, string>("IQ", "Iraq"),
            new KeyValuePair<string, string>("IE", "Ireland"),
            new KeyValuePair<string, string>("IM", "Isle of Man"),
            new KeyValuePair<string, string>("IT", "Italy"),
            new KeyValuePair<string, string>("JM", "Jamaica"),
            new KeyValuePair<string, string>("JP", "Japan"),
            new KeyValuePair<string, string>("JE", "Jersey"),
            new KeyValuePair<string, string>("JO", "Jordan"),
            new KeyValuePair<string, string>("KZ", "Kazakhstan"),
            new KeyValuePair<string, string>("KE", "Kenya"),
            new KeyValuePair<string, string>("KI", "Kiribati"),
            new KeyValuePair<string, string>("KP", "Korea North"),
            new KeyValuePair<string, string>("KR", "Korea South"),
            new KeyValuePair<string, string>("KW", "Kuwait"),
            new KeyValuePair<string, string>("KG", "Kyrgyzstan"),
            new KeyValuePair<string, string>("LA", "Laos"),
            new KeyValuePair<string, string>("LV", "Latvia"),
            new KeyValuePair<string, string>("LB", "Lebanon"),
            new KeyValuePair<string, string>("LS", "Lesotho"),
            new KeyValuePair<string, string>("LR", "Liberia"),
            new KeyValuePair<string, string>("LI", "Liechtenstein"),
            new KeyValuePair<string, string>("LT", "Lithuania"),
            new KeyValuePair<string, string>("LU", "Luxembourg"),
            new KeyValuePair<string, string>("MO", "Macau"),
            new KeyValuePair<string, string>("MG", "Madagascar"),
            new KeyValuePair<string, string>("MW", "Malawi"),
            new KeyValuePair<string, string>("MY", "Malaysia"),
            new KeyValuePair<string, string>("MV", "Maldives"),
            new KeyValuePair<string, string>("ML", "Mali"),
            new KeyValuePair<string, string>("MT", "Malta"),
            new KeyValuePair<string, string>("MH", "Marshall Islands"),
            new KeyValuePair<string, string>("MQ", "Martinique"),
            new KeyValuePair<string, string>("MR", "Mauritania"),
            new KeyValuePair<string, string>("MU", "Mauritius"),
            new KeyValuePair<string, string>("MX", "Mexico"),
            new KeyValuePair<string, string>("FM", "Micronesia"),
            new KeyValuePair<string, string>("MD", "Moldova"),
            new KeyValuePair<string, string>("MC", "Monaco"),
            new KeyValuePair<string, string>("MN", "Mongolia"),
            new KeyValuePair<string, string>("MS", "Montserrat"),
            new KeyValuePair<string, string>("MA", "Morocco"),
            new KeyValuePair<string, string>("MZ", "Mozambique"),
            new KeyValuePair<string, string>("MM", "Myanmar"),
            new KeyValuePair<string, string>("NA", "Namibia"),
            new KeyValuePair<string, string>("NR", "Nauru"),
            new KeyValuePair<string, string>("NP", "Nepal"),
            new KeyValuePair<string, string>("NL", "Netherlands"),
            new KeyValuePair<string, string>("AN", "Netherlands Antilles"),
            new KeyValuePair<string, string>("NC", "New Caledonia"),
            new KeyValuePair<string, string>("NZ", "New Zealand"),
            new KeyValuePair<string, string>("NI", "Nicaragua"),
            new KeyValuePair<string, string>("NE", "Niger"),
            new KeyValuePair<string, string>("NG", "Nigeria"),
            new KeyValuePair<string, string>("NU", "Niue"),
            new KeyValuePair<string, string>("NF", "Norfolk Island"),
            new KeyValuePair<string, string>("NO", "Norway"),
            new KeyValuePair<string, string>("OM", "Oman"),
            new KeyValuePair<string, string>("PK", "Pakistan"),
            new KeyValuePair<string, string>("PW", "Palau"),
            new KeyValuePair<string, string>("PA", "Panama"),
            new KeyValuePair<string, string>("PG", "Papua New Guinea"),
            new KeyValuePair<string, string>("PY", "Paraguay"),
            new KeyValuePair<string, string>("PE", "Peru"),
            new KeyValuePair<string, string>("PH", "Philippines"),
            new KeyValuePair<string, string>("PN", "Pitcairn"),
            new KeyValuePair<string, string>("PL", "Poland"),
            new KeyValuePair<string, string>("PT", "Portugal"),
            new KeyValuePair<string, string>("PR", "Puerto Rico"),
            new KeyValuePair<string, string>("QA", "Qatar"),
            new KeyValuePair<string, string>("RO", "Romania"),
            new KeyValuePair<string, string>("RU", "Russian Federation"),
            new KeyValuePair<string, string>("RW", "Rwanda"),
            new KeyValuePair<string, string>("SH", "Saint Helena"),
            new KeyValuePair<string, string>("KN", "Saint Kitts and Nevis"),
            new KeyValuePair<string, string>("LC", "Saint Lucia"),
            new KeyValuePair<string, string>("WS", "Samoa"),
            new KeyValuePair<string, string>("SM", "San Marino"),
            new KeyValuePair<string, string>("ST", "Sao Tome and Principe"),
            new KeyValuePair<string, string>("SA", "Saudi Arabia"),
            new KeyValuePair<string, string>("SN", "Senegal"),
            new KeyValuePair<string, string>("SC", "Seychelles"),
            new KeyValuePair<string, string>("SL", "Sierra Leone"),
            new KeyValuePair<string, string>("SG", "Singapore"),
            new KeyValuePair<string, string>("SI", "Slovenia"),
            new KeyValuePair<string, string>("SK", "Slowak Republic"),
            new KeyValuePair<string, string>("SB", "Solomon Islands"),
            new KeyValuePair<string, string>("SO", "Somalia"),
            new KeyValuePair<string, string>("ZA", "South Africa"),
            new KeyValuePair<string, string>("SU", "Soviet Union"),
            new KeyValuePair<string, string>("ES", "Spain"),
            new KeyValuePair<string, string>("LK", "Sri Lanka"),
            new KeyValuePair<string, string>("SD", "Sudan"),
            new KeyValuePair<string, string>("SR", "Suriname"),
            new KeyValuePair<string, string>("SZ", "Swaziland"),
            new KeyValuePair<string, string>("SE", "Sweden"),
            new KeyValuePair<string, string>("CH", "Switzerland"),
            new KeyValuePair<string, string>("SY", "Syria"),
            new KeyValuePair<string, string>("TW", "Taiwan"),
            new KeyValuePair<string, string>("TJ", "Tajikistan"),
            new KeyValuePair<string, string>("TZ", "Tanzania"),
            new KeyValuePair<string, string>("TH", "Thailand"),
            new KeyValuePair<string, string>("TG", "Togo"),
            new KeyValuePair<string, string>("TK", "Tokelau"),
            new KeyValuePair<string, string>("TO", "Tonga"),
            new KeyValuePair<string, string>("TT", "Trinidad and Tobago"),
            new KeyValuePair<string, string>("TN", "Tunisia"),
            new KeyValuePair<string, string>("TR", "Turkey"),
            new KeyValuePair<string, string>("TM", "Turkmenistan"),
            new KeyValuePair<string, string>("TV", "Tuvalu"),
            new KeyValuePair<string, string>("UG", "Uganda"),
            new KeyValuePair<string, string>("UA", "Ukraine"),
            new KeyValuePair<string, string>("AE", "United Arab Emirates"),
            new KeyValuePair<string, string>("UY", "Uruguay"),
            new KeyValuePair<string, string>("UZ", "Uzbekistan"),
            new KeyValuePair<string, string>("VU", "Vanuatu"),
            new KeyValuePair<string, string>("VA", "Vatican City State"),
            new KeyValuePair<string, string>("VE", "Venezuela"),
            new KeyValuePair<string, string>("VN", "Vietnam"),
            new KeyValuePair<string, string>("VG", "Virgin Islands (UK)"),
            new KeyValuePair<string, string>("VI", "Virgin Islands (US)"),
            new KeyValuePair<string, string>("EH", "Western Sahara"),
            new KeyValuePair<string, string>("YE", "Yemen"),
            new KeyValuePair<string, string>("YU", "Yugoslavia"),
            new KeyValuePair<string, string>("ZR", "Zaire"),
            new KeyValuePair<string, string>("ZM", "Zambia"),
            new KeyValuePair<string, string>("ZW", "Zimbabwe")
        };

        public static List<KeyValuePair<string, string>> CREDIT_CARDS = new List<KeyValuePair<string, string>>()
        {                    
            new KeyValuePair<string, string>("visa", "Visa"),
            new KeyValuePair<string, string>("mc", "MasterCard"),
            new KeyValuePair<string, string>("amx", "American Express")
        };

        
        #endregion
    }
}