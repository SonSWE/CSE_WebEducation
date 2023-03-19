using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLib
{
    public class CommonData
    {
        public static string connectionString = "";
        public static string JwtKey = "";
        public static string JwtIssuer = "";

        public static string ContentRootPath = "";
        public static string FileAttach = "";

        public static int pageDefaut = 1;
        public static int RecordsPerPage = 10;

        public static int TimeOutLogin = 0;
    }

    public class ActionResultConfig
    {
        public const int VERIFY = -94;
        public const int NOT_FOUND = -95;
        public const int TIME_OUT = -96;
        public const int UNAUTHORIZED_REDIRECT = -97;
        public const int UNAUTHORIZED_AJAX = -98;
        public const int CONCURRENT_USER_LOGIN = -99;
        public const int VERIFY_CA = -93;
        public const int VERIFY_GA = -92;
        public const int VERIFY_SMS = -91;
    }
    public class RouteConfig
    {
        public const string PAGE_VERIFY = "/verifyga";
        public const string PAGE_NOT_FOUND = "/page-not-found";
        public const string ACCESS_DENIED = "/access-denied";
        public const string RE_LOGIN = "/";
        public const string LOGIN_CONFLICT = "/login-conflict";
        public const string TIME_OUT = "/time-out";
        public const string LOGOUT = "/logout";
        public const string Verify_CA = "/verify-ca";
        public const string Verify_GA = "/verify-gg-authentication";
        public const string Verify_SMS = "/verify-sms";

    }

    public class CSE_Group_Status
    {
        public const string HetHieuLuc = "C";
        public const string HieuLuc = "A";
    }

    public class CSE_User_Status
    {
        public const string UnActive = "P";
        public const string Active = "A";
    }

    public class CSE_User_Type
    {
        public const string amdin = "C";
        public const string siteuser = "A";
    }

    public class CSE_Post_Type
    {
        public const decimal posts = 1;
        public const decimal events = 2;
    }

    public class CSE_Post_Status
    {
        public const string Active = "A";
        public const string Pending = "P";
        public const string Reject = "R";
        public const string Hide = "H";
    }
}
