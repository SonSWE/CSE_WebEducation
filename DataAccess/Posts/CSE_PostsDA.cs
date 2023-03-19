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
    public class CSE_PostsDA
    {
        public DataSet Search(string key_search, int startRow, int endRow, string order_by, ref decimal totalRecord)
        {
            try
            {
                var lstParam = new SqlParameter[5];
                lstParam[0] = new SqlParameter("@p_key_search", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = key_search
                };
                lstParam[1] = new SqlParameter("@p_startrow", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = startRow
                };
                lstParam[2] = new SqlParameter("@p_endrow", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = endRow
                };
                lstParam[3] = new SqlParameter("@p_orderby", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = order_by == null ? "" : order_by
                };
                lstParam[4] = new SqlParameter("@p_total_record", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Output,
                    Value = -1
                };

                var dt = SQLHelper.ExecuteDataset(CommonData.connectionString, CommandType.StoredProcedure, "sp_posts_search", lstParam);
                totalRecord = Convert.ToDecimal(lstParam[4].Value);
                return dt;
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        public DataSet GetById(decimal id)
        {
            try
            {
                var lstParam = new SqlParameter[1];
                lstParam[0] = new SqlParameter("@p_id", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = id,
                };

                return SQLHelper.ExecuteDataset(CommonData.connectionString, CommandType.StoredProcedure, "sp_posts_get_by_id", lstParam);
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        public decimal Insert(CSE_PostsInfo _info)
        {
            try
            {
                decimal _result = -1;
                var lstParam = new SqlParameter[12];
                lstParam[0] = new SqlParameter("@p_category_id", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Category_Id,
                };
                lstParam[1] = new SqlParameter("@p_title", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Title,
                };
                lstParam[2] = new SqlParameter("@p_thumbnail", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Thumbnail,
                };
                lstParam[3] = new SqlParameter("@p_description", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Description
                };
                lstParam[4] = new SqlParameter("@p_content", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Content
                };
                lstParam[5] = new SqlParameter("@p_start_date", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Start_Date == DateTime.MinValue ? null : _info.Start_Date,
                    IsNullable = true
                };
                lstParam[6] = new SqlParameter("@p_location", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Location,
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
                lstParam[10] = new SqlParameter("@p_post_type", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Post_Type,
                };

                SQLHelper.ExecuteNonQuery(CommonData.connectionString, CommandType.StoredProcedure, "sp_posts_insert", lstParam);

                _result = Convert.ToDecimal(lstParam[9].Value.ToString());

                return _result;

            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return -1;
            }
        }

        public decimal Update(CSE_PostsInfo _info)
        {
            try
            {
                decimal _result = -1;
                var lstParam = new SqlParameter[12];
                lstParam[0] = new SqlParameter("@p_category_id", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Category_Id,
                };
                lstParam[1] = new SqlParameter("@p_title", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Title,
                };
                lstParam[2] = new SqlParameter("@p_thumbnail", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Thumbnail,
                };
                lstParam[3] = new SqlParameter("@p_description", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Description
                };
                lstParam[4] = new SqlParameter("@p_content", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Content
                };
                lstParam[5] = new SqlParameter("@p_start_date", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Start_Date == DateTime.MinValue ? null : _info.Start_Date,
                    IsNullable = true
                };
                lstParam[6] = new SqlParameter("@p_location", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Location,
                    IsNullable = true
                };
                lstParam[7] = new SqlParameter("@p_modified_by", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Modified_By,
                };
                lstParam[8] = new SqlParameter("@p_modified_date", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Modified_Date,
                };
                lstParam[9] = new SqlParameter("@p_result", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Output
                };
                lstParam[10] = new SqlParameter("@p_id", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Id,
                };


                SQLHelper.ExecuteDataset(CommonData.connectionString, CommandType.StoredProcedure, "sp_posts_update", lstParam);

                _result = Convert.ToDecimal(lstParam[9].Value.ToString());

                return _result;
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return -1;
            }
        }

        public decimal Delete(CSE_PostsInfo _info)
        {
            try
            {
                decimal _result = -1;
                var lstParam = new SqlParameter[4];
                lstParam[0] = new SqlParameter("@p_id", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Id,
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

                SQLHelper.ExecuteNonQuery(CommonData.connectionString, CommandType.StoredProcedure, "sp_posts_delete", lstParam);

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
