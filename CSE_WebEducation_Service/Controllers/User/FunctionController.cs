using CommonLib;
using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ObjectInfo;
using System.Data;

namespace CSE_WebEducation_Service.Controllers.User
{
    public class FunctionController : Controller
    {
        [HttpGet]
        //[Authorize]
        [Route("api/quan-tri/function/get-all")]
        public List<CSE_FunctionsInfo> GetAll()
        {
            try
            {
                CSE_FunctionDA DA = new CSE_FunctionDA();
                DataSet _ds = DA.GetAll();

                return CBO<CSE_FunctionsInfo>.FillCollection_FromDataSet(_ds);
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new List<CSE_FunctionsInfo>();
            }
        }
    }
}
