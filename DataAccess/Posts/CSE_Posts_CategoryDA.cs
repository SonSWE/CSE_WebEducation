using CommonLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        public DataSet GetById(string id)
        {
            try
            {
                var lstParam = new SqlParameter[1];
                lstParam[0] = new SqlParameter("@p_id", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = id,
                };

                return SQLHelper.ExecuteDataset(CommonData.connectionString, CommandType.StoredProcedure, "sp_postCategories_get_by_id", lstParam);
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new DataSet();
            }
        }
    }
}
