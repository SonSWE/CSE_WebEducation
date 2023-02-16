using Newtonsoft.Json;

namespace CSE_WebEducation
{
    public class ConstData
    {
        public const string authClaimType = "authLoginUser";
        public const string authType = "Cookies";
        public const string authCookieName = "authToken";

        /// <summary>
        /// Host http api client
        /// </summary>
        public static string httpApiClientHost = "http://localhost:26742/";

        public static readonly JsonSerializerSettings defaultSettings = new JsonSerializerSettings()
        {
            DateTimeZoneHandling = DateTimeZoneHandling.Local
        };

    }
}
