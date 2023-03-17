using CommonLib;
using ObjectInfo;

namespace CSE_WebEducation_Guest
{
    public class AllCodeMemory
    {

        private static object _locklstAllcode = new object();
        private static List<CSE_AllcodeInfo> lstAllcode = new List<CSE_AllcodeInfo>();

        //private static object _locklstEmailTemplate = new object();
        //private static List<Msg_Email_Template> lstEmailTemplate = new List<Msg_Email_Template>();

        public static void Allcode_ReloadMemory()
        {
            try
            {
                List<CSE_AllcodeInfo> _lst = ApiClient_System.Allcode_GetAll();
                lock (_locklstAllcode)
                {
                    lstAllcode = _lst;
                }
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
            }
        }

        public static List<CSE_AllcodeInfo> Allcode_GetAll()
        {
            try
            {
                return lstAllcode;
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new List<CSE_AllcodeInfo>();
            }
        }

        public static List<CSE_AllcodeInfo> Allcode_GetByCdnameCdtype(string cdname, string cdtype)
        {
            try
            {
                return lstAllcode.Where(x => x.CDName.Equals(cdname) && x.CDType.Equals(cdtype)).OrderBy(x => x.LstOdr).ToList();
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new List<CSE_AllcodeInfo>();
            }
        }

        public static List<CSE_AllcodeInfo> Allcode_GetByCdname(string cdname)
        {
            try
            {
                return lstAllcode.Where(x => x.CDName.Equals(cdname)).OrderBy(x => x.LstOdr).ToList();
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new List<CSE_AllcodeInfo>();
            }
        }

        public static List<CSE_AllcodeInfo> Allcode_GetByCdtype(string cdtype)
        {
            try
            {
                return lstAllcode.Where(x => x.CDType.Equals(cdtype)).OrderBy(x => x.LstOdr).ToList();
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new List<CSE_AllcodeInfo>();
            }
        }

        public static string Allcode_GetContent(string cdname, string cdtype, string cdval)
        {
            try
            {
                CSE_AllcodeInfo _allcode = lstAllcode.Where(x => x.CDName.Equals(cdname) && x.CDType.Equals(cdtype) && x.CDVal.Equals(cdval)).FirstOrDefault();
                if (_allcode != null)
                {
                    return _allcode.Content;
                }

                return "";
                //return lstAllcode.Where(x => x.CdName.Equals(cdname) && x.CdType.Equals(cdtype) && x.CdValue.Equals(cdval)).FirstOrDefault().Content;
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return "";
            }
        }

        public static List<CSE_AllcodeInfo> Allcode_GetByCdtypeCdVal(string cdtype, string vdval)
        {
            try
            {
                return lstAllcode.Where(x => x.CDType.Equals(cdtype) && x.CDVal.Equals(vdval)).OrderBy(x => x.LstOdr).ToList();
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new List<CSE_AllcodeInfo>();
            }
        }

        public static string Allcode_GetContent_GetByCdtypeCdVal(string cdtype, string cdval)
        {
            try
            {
                return lstAllcode.FirstOrDefault(x => x.CDType == cdtype && x.CDVal == cdval).Content;
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return null;
            }
        }

        public static List<CSE_AllcodeInfo> Allcode_Get_Like_CDVal(string cdname, string cdval)
        {
            try
            {
                return lstAllcode.Where(x => x.CDName.Equals(cdname) && x.CDVal.StartsWith(cdval)).OrderBy(x => x.LstOdr).ToList();
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new List<CSE_AllcodeInfo>();
            }
        }
    }
}
