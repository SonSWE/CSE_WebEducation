using CommonLib;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using ObjectInfo;
using System.Data;

namespace CSE_WebEducation_Service.Controllers.System
{
    [ApiController]
    public class SystemController : Controller
    {
        [HttpGet]
        //[Authorize]
        [Route("api/system/allcode-get-all")]
        public List<CSE_AllcodeInfo> GetAll()
        {
            try
            {
                CSE_AllcodeDA DA = new CSE_AllcodeDA();
                DataSet _ds = DA.GetAll();

                return CBO<CSE_AllcodeInfo>.FillCollection_FromDataSet(_ds);
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new List<CSE_AllcodeInfo>();
            }
        }
    }
}
