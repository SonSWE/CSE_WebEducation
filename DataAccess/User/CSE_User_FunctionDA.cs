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
    public class CSE_User_FunctionDA
    {
        public List<CSE_FunctionsInfo> GetByUserId(decimal p_user_id)
        {
            try
            {
                var lstParam = new SqlParameter[1];
                lstParam[0] = new SqlParameter("@p_user_id", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = p_user_id
                };

                var dt = SQLHelper.ExecuteDataset(CommonData.connectionString, CommandType.StoredProcedure, "sp_user_function_by_user_id", lstParam);

                return CBO<CSE_FunctionsInfo>.FillCollection_FromDataSet(dt);
            }
            catch (Exception ex)
            {
                return new List<CSE_FunctionsInfo>();
            }
        }

        public decimal ResetFunction(decimal user_id)
        {
            try
            {
                decimal _result = -1;
                var lstParam = new SqlParameter[2];
                lstParam[0] = new SqlParameter("@p_user_id", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = user_id,
                };
                lstParam[1] = new SqlParameter("@p_result", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Output
                };

                SQLHelper.ExecuteNonQuery(CommonData.connectionString, CommandType.StoredProcedure, "sp_user_function_reset", lstParam);

                _result = Convert.ToDecimal(lstParam[1].Value.ToString());

                return _result;

            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return -1;
            }
        }

        public decimal UpdateFunction(List<CSE_User_Function_Info> _lst)
        {
            try
            {
                if (_lst != null)
                {
                    foreach (var _item in _lst)
                    {

                        var lstParam = new SqlParameter[3];
                        lstParam[0] = new SqlParameter("@p_user_id", SqlDbType.Decimal)
                        {
                            Direction = ParameterDirection.Input,
                            Value = _item.User_Id
                        };
                        lstParam[1] = new SqlParameter("@p_func_id", SqlDbType.Decimal)
                        {
                            Direction = ParameterDirection.Input,
                            Value = _item.Function_Id
                        };
                        lstParam[2] = new SqlParameter("@p_authcode", SqlDbType.NVarChar)
                        {
                            Direction = ParameterDirection.Input,
                            Value = _item.Authcode
                        };


                        SQLHelper.ExecuteNonQuery(CommonData.connectionString, CommandType.StoredProcedure, "sp_user_function_update", lstParam);
                    }
                }

                return 1;
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return -1;
            }
        }
    }
}
