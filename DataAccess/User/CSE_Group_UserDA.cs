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
    public class CSE_Group_UserDA
    {
        public DataSet GetByGroupId(decimal group_id)
        {
            try
            {
                var lstParam = new SqlParameter[1];
                lstParam[0] = new SqlParameter("@p_group_id", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = group_id,
                };

                return SQLHelper.ExecuteDataset(CommonData.connectionString, CommandType.StoredProcedure, "sp_group_user_get_by_group_id", lstParam);
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        public decimal AddUserToGroup(CSE_Group_User_Info _info)
        {
            try
            {
                decimal _result = -1;
                var lstParam = new SqlParameter[5];
                lstParam[0] = new SqlParameter("@p_group_id", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Group_Id,
                };
                lstParam[1] = new SqlParameter("@p_user_id", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.User_Id,
                };
                lstParam[2] = new SqlParameter("@p_created_by", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Created_By,
                };
                lstParam[3] = new SqlParameter("@p_created_date", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Created_Date
                };
                lstParam[4] = new SqlParameter("@p_result", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Output
                };

                SQLHelper.ExecuteNonQuery(CommonData.connectionString, CommandType.StoredProcedure, "sp_group_user_add", lstParam);

                _result = Convert.ToDecimal(lstParam[4].Value.ToString());

                return _result;

            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return -1101;
            }
        }

        public decimal RemoveUserFromGroup(CSE_Group_User_Info _info)
        {
            try
            {
                decimal _result = -1;
                var lstParam = new SqlParameter[5];
                lstParam[0] = new SqlParameter("@p_group_id", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Group_Id,
                };
                lstParam[1] = new SqlParameter("@p_user_id", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.User_Id,
                };
                lstParam[2] = new SqlParameter("@p_created_by", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Created_By,
                };
                lstParam[3] = new SqlParameter("@p_created_date", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Created_Date
                };
                lstParam[4] = new SqlParameter("@p_result", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Output
                };

                SQLHelper.ExecuteNonQuery(CommonData.connectionString, CommandType.StoredProcedure, "sp_group_user_remove", lstParam);

                _result = Convert.ToDecimal(lstParam[4].Value.ToString());

                return _result;

            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return -1101;
            }
        }
    }
}
