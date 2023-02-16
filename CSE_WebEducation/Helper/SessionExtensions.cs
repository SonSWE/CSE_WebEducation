using CommonLib;
using Newtonsoft.Json;
using ObjectInfo;

namespace CSE_WebEducation
{
    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            try
            {
                session.SetString(key, JsonConvert.SerializeObject(value));
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
            }
        }

        public static void Remove_Session_ByKey(this ISession session, string key)
        {
            try
            {
                session.Remove(key);
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
            }
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            try
            {
                var value = session.GetString(key);
                return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
            }
            return default(T);
        }

        public static CSE_UsersInfo GetCurrentUser(this HttpContext context)
        {
            CSE_UsersInfo user = new CSE_UsersInfo();
            try
            {
                user = context.Session.GetObjectFromJson<CSE_UsersInfo>("user");

                if (user != null)
                {
                    if (MemoryData.c_dic_Function_ByUser.ContainsKey(user.User_Id))
                    {
                        user.List_User_Functions = MemoryData.c_dic_Function_ByUser[user.User_Id];
                    }
                    else
                    {
                        user.List_User_Functions = new List<CSE_FunctionsInfo>();
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                user = new CSE_UsersInfo();
            }
            return user;
        }
    }
}
