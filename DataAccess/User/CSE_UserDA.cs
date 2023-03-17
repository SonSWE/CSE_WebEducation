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
    public class CSE_UserDA
    {
        public CSE_UsersInfo Login(string p_user_name, string p_password)
        {
            try
            {
                var lstParam = new SqlParameter[2];
                lstParam[0] = new SqlParameter("@p_user_name", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = p_user_name
                };
                lstParam[1] = new SqlParameter("@p_password", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = p_password
                };


                var dt = SQLHelper.ExecuteDataset(CommonData.connectionString, CommandType.StoredProcedure, "sp_cse_user_login", lstParam);

                return CBO<CSE_UsersInfo>.FillObjectFromDataSet(dt);
            }
            catch (Exception ex)
            {
                return new CSE_UsersInfo();
            }
        }

        public DataSet GetAll()
        {
            try
            {
                return SQLHelper.ExecuteDataset(CommonData.connectionString, CommandType.StoredProcedure, "sp_user_get_all");
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        public DataSet Search(string user_name,string key_search, int startRow, int endRow, string order_by, ref decimal totalRecord)
        {
            try
            {
                var lstParam = new SqlParameter[6];
                lstParam[0] = new SqlParameter("@p_user_name", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = user_name
                };
                lstParam[1] = new SqlParameter("@p_key_search", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = key_search
                };
                lstParam[2] = new SqlParameter("@p_startrow", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = startRow
                };
                lstParam[3] = new SqlParameter("@p_endrow", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = endRow
                };
                lstParam[4] = new SqlParameter("@p_orderby", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = order_by == null ? "" : order_by
                };
                lstParam[5] = new SqlParameter("@p_total_record", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Output
                };
                

                var dt = SQLHelper.ExecuteDataset(CommonData.connectionString, CommandType.StoredProcedure, "sp_user_search", lstParam);
                totalRecord = Convert.ToDecimal(lstParam[5].Value.ToString());
                return dt;
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        public DataSet GetById(decimal user_id)
        {
            try
            {
                var lstParam = new SqlParameter[1];
                lstParam[0] = new SqlParameter("@p_user_id", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = user_id,
                };

                return SQLHelper.ExecuteDataset(CommonData.connectionString, CommandType.StoredProcedure, "sp_user_get_by_id", lstParam);
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        public decimal Insert(CSE_UsersInfo _info)
        {
            try
            {
                decimal _result = -1;
                var lstParam = new SqlParameter[11];
                lstParam[0] = new SqlParameter("@p_user_name", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.User_Name,
                };
                lstParam[1] = new SqlParameter("@p_password", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Password,
                };
                lstParam[3] = new SqlParameter("@p_status", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Status,
                };
                lstParam[4] = new SqlParameter("@p_phone", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Phone,
                    IsNullable = true
                };
                lstParam[5] = new SqlParameter("@p_email", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Email,
                };
                lstParam[6] = new SqlParameter("@p_identity_card", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Identity_Card,
                    IsNullable = true
                };
                lstParam[7] = new SqlParameter("@p_created_by", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Created_By,
                };
                lstParam[8] = new SqlParameter("@p_created_date", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Created_Date,
                };
                lstParam[9] = new SqlParameter("@p_result", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Output
                };
                lstParam[10] = new SqlParameter("@p_full_name", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Full_Name,
                };

                SQLHelper.ExecuteNonQuery(CommonData.connectionString, CommandType.StoredProcedure, "sp_user_insert", lstParam);

                _result = Convert.ToDecimal(lstParam[9].Value.ToString());

                return _result;

            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return -1;
            }
        }
        public decimal Update(CSE_UsersInfo _info)
        {
            try
            {
                decimal _result = -1;
                var lstParam = new SqlParameter[11];
                lstParam[0] = new SqlParameter("@p_user_id", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.User_Id,
                };
                lstParam[2] = new SqlParameter("@p_full_name", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Full_Name,
                };
                lstParam[4] = new SqlParameter("@p_status", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Status,
                };
                lstParam[5] = new SqlParameter("@p_phone", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Phone,
                    IsNullable = true
                };
                lstParam[6] = new SqlParameter("@p_email", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Email,
                };
                lstParam[7] = new SqlParameter("@p_identity_card", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Identity_Card,
                    IsNullable= true
                };
                lstParam[8] = new SqlParameter("@p_modified_by", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Modified_By,
                };
                lstParam[9] = new SqlParameter("@p_modified_date", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Modified_Date,
                };
                lstParam[10] = new SqlParameter("@p_result", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Output,
                };

                SQLHelper.ExecuteDataset(CommonData.connectionString, CommandType.StoredProcedure, "sp_user_update", lstParam);

                _result = Convert.ToDecimal(lstParam[10].Value.ToString());

                return _result;
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return -1;
            }
        }

        public decimal Delete(CSE_UsersInfo _info)
        {
            try
            {
                decimal _result = -1;
                var lstParam = new SqlParameter[4];
                lstParam[0] = new SqlParameter("@p_user_id", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.User_Id,
                };
                lstParam[1] = new SqlParameter("@p_modified_by", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Modified_By,
                };
                lstParam[2] = new SqlParameter("@p_modified_date", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Modified_Date,
                };
                lstParam[3] = new SqlParameter("@p_result", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Output
                };

                SQLHelper.ExecuteNonQuery(CommonData.connectionString, CommandType.StoredProcedure, "sp_user_delete", lstParam);

                _result = Convert.ToDecimal(lstParam[3].Value.ToString());

                return _result;
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return -1;
            }
        }
    }
}
