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
    public class CSE_GroupDA
    {
        public DataSet GetAll()
        {
            try
            {
                return SQLHelper.ExecuteDataset(CommonData.connectionString, CommandType.StoredProcedure, "sp_group_get_all");
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        public DataSet Search(string user_name, string key_search, int startRow, int endRow, string order_by, ref decimal totalRecord)
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
                    Direction = ParameterDirection.Output,
                    Value = -1
                };

                var dt = SQLHelper.ExecuteDataset(CommonData.connectionString, CommandType.StoredProcedure, "sp_group_search", lstParam);
                totalRecord = Convert.ToDecimal(lstParam[5].Value);
                return dt;
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        public DataSet GetById(decimal group_id)
        {
            try
            {
                var lstParam = new SqlParameter[1];
                lstParam[0] = new SqlParameter("@p_group_id", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = group_id,
                };

                return SQLHelper.ExecuteDataset(CommonData.connectionString, CommandType.StoredProcedure, "sp_group_get_by_id", lstParam);
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        public decimal Insert(CSE_GroupsInfo _info)
        {
            try
            {
                decimal _result = -1;
                var lstParam = new SqlParameter[7];
                lstParam[0] = new SqlParameter("@p_group_name", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Group_Name,
                };
                lstParam[1] = new SqlParameter("@p_group_type", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Group_Type,
                };
                lstParam[2] = new SqlParameter("@p_status", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Status,
                };
                lstParam[3] = new SqlParameter("@p_note", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Note,
                    IsNullable = true
                };
                lstParam[4] = new SqlParameter("@p_created_by", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Created_By,
                };
                lstParam[5] = new SqlParameter("@p_created_date", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Created_Date,
                };
                lstParam[6] = new SqlParameter("@p_result", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Output
                };

                SQLHelper.ExecuteNonQuery(CommonData.connectionString, CommandType.StoredProcedure, "sp_group_insert", lstParam);

                _result = Convert.ToDecimal(lstParam[6].Value.ToString());

                return _result;

            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return -1101;
            }
        }

        public decimal Update(CSE_GroupsInfo _info)
        {
            try
            {
                decimal _result = -1;
                var lstParam = new SqlParameter[8];
                lstParam[0] = new SqlParameter("@p_group_id", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Group_Id,
                };
                lstParam[1] = new SqlParameter("@p_group_name", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Group_Name,
                };
                lstParam[2] = new SqlParameter("@p_group_type", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Group_Type,
                };
                lstParam[3] = new SqlParameter("@p_status", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Status,
                };
                lstParam[4] = new SqlParameter("@p_note", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Note,
                    IsNullable = true
                };
                lstParam[5] = new SqlParameter("@p_modified_by", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Modified_By,
                };
                lstParam[6] = new SqlParameter("@p_modified_date", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Modified_Date,
                };
                lstParam[7] = new SqlParameter("@p_result", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Output,
                    Value = -1,
                };

                SQLHelper.ExecuteDataset(CommonData.connectionString, CommandType.StoredProcedure, "sp_group_update", lstParam);

                _result = Convert.ToDecimal(lstParam[7].Value.ToString());

                return _result;
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return -1101;
            }
        }

        public decimal Delete(CSE_GroupsInfo _info)
        {
            try
            {
                decimal _result = -1;
                var lstParam = new SqlParameter[4];
                lstParam[0] = new SqlParameter("@p_group_id", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Group_Id,
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

                SQLHelper.ExecuteNonQuery(CommonData.connectionString, CommandType.StoredProcedure, "sp_group_delete", lstParam);

                _result = Convert.ToDecimal(lstParam[3].Value.ToString());

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
