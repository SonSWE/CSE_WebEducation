using CommonLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CSE_Posts_CategoryDA
    {
        public DataSet GetAll()
        {
            try
            {
                return SQLHelper.ExecuteDataset(CommonData.connectionString, CommandType.StoredProcedure, "sp_postsCategories_get_all");
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new DataSet();
            }
        }
    }
}
