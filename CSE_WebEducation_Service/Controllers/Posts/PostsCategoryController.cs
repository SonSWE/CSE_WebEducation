using CommonLib;
using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ObjectInfo;
using System.Data;

namespace CSE_WebEducation_Service.Controllers.Posts
{
    public class PostsCategoryController : Controller
    {
        [HttpGet]
        //[Authorize]
        [Route("api/posts-category/get-all")]
        public List<CSE_Posts_CategoriesInfo> GetAll()
        {
            try
            {
                CSE_Posts_CategoryDA DA = new CSE_Posts_CategoryDA();
                DataSet _ds = DA.GetAll();

                return CBO<CSE_Posts_CategoriesInfo>.FillCollection_FromDataSet(_ds);
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new List<CSE_Posts_CategoriesInfo>();
            }
        }

        [HttpGet]
        //[Authorize]
        [Route("api/posts-category/get-by-id")]
        public CSE_Posts_CategoriesInfo GetById([FromQuery] string id)
        {
            try
            {
                CSE_Posts_CategoryDA DA = new CSE_Posts_CategoryDA();
                DataSet ds = DA.GetById(id);

                return CBO<CSE_Posts_CategoriesInfo>.FillObjectFromDataSet(ds);
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new CSE_Posts_CategoriesInfo();
            }
        }
    }
}
