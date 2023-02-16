using CommonLib;
using ObjectInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CSE_Group_FunctionDA
    {
        public List<CSE_FunctionsInfo> GetByGroupId(decimal p_group_id)
        {
            try
            {
                var lstParam = new SqlParameter[1];
                lstParam[0] = new SqlParameter("@p_group_id", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = p_group_id
                };

                var dt = SQLHelper.ExecuteDataset(CommonData.connectionString, CommandType.StoredProcedure, "sp_function_get_by_group_id", lstParam);

                return CBO<CSE_FunctionsInfo>.FillCollection_FromDataSet(dt);
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new List<CSE_FunctionsInfo>();
            }
        }

        public decimal Function_Group_Delete(decimal p_group_id)
        {
            try
            {
                var lstParam = new SqlParameter[2];
                lstParam[0] = new SqlParameter("@p_group_id", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = p_group_id
                };
                lstParam[1] = new SqlParameter("@p_result", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Output
                };

                SQLHelper.ExecuteNonQuery(CommonData.connectionString, CommandType.StoredProcedure, "sp_group_function_delete", lstParam);

                return Convert.ToDecimal(lstParam[1].Value.ToString());
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return -1;
            }
        }

        public decimal Function_Group_Insert_Batch(List<CSE_Group_Function_Info> _lst)
        {
            try
            {
                foreach (var _item in _lst)
                {

                    var lstParam = new SqlParameter[2];
                    lstParam[0] = new SqlParameter("@p_group_id", SqlDbType.Decimal)
                    {
                        Direction = ParameterDirection.Input,
                        Value = _item.Group_Id
                    };
                    lstParam[1] = new SqlParameter("@p_func_id", SqlDbType.Decimal)
                    {
                        Direction = ParameterDirection.Input,
                        Value = _item.Function_Id
                    };


                    SQLHelper.ExecuteNonQuery(CommonData.connectionString, CommandType.StoredProcedure, "sp_group_function_insert", lstParam);
                }

                return 1;
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return -1;
            }
        }

        public decimal SetFunctionForUser(decimal p_group_id)
        {
            try
            {
                var lstParam = new SqlParameter[2];
                lstParam[0] = new SqlParameter("@p_group_id", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = p_group_id
                };
                lstParam[1] = new SqlParameter("@p_result", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Output
                };

                SQLHelper.ExecuteNonQuery(CommonData.connectionString, CommandType.StoredProcedure, "sp_set_func_for_user_in_gr", lstParam);

                return Convert.ToDecimal(lstParam[1].Value.ToString());
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return -1;
            }
        }


    }
}
