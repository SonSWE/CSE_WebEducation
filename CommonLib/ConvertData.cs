using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public class ConvertData
    {

        #region Định dạng datetime

        public const string strDate = "dd/MM/yyyy";
        public const string strDate_Time = "dd/MM/yyyy HH:mm";
        public const string strDate_Times = "dd/MM/yyyy HH:mm:ss";

        public static string ConvertDate2String(DateTime p_date)
        {
            try
            {
                return p_date.ToString(strDate);
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return "";
            }
        }

        public static DateTime ConvertString2Date(string str)
        {
            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.CurrentCulture;
            try
            {
                if (str != null)
                {
                    if (str.Length > strDate.Length)
                    {
                        string _tem = str.Substring(0, 10);
                        if (_tem == "01/01/0001")
                        {
                            return DateTime.MinValue;
                        }
                        return DateTime.ParseExact(_tem, strDate, provider); // strDate 
                    }
                }
                else
                {
                    return DateTime.MinValue;
                }

                if (str == "01/01/0001" || str == "1/01/0001" || str == "1/1/0001" || str == "")
                {
                    return DateTime.MinValue;
                }

                return DateTime.ParseExact(str, strDate, provider); // strDate 
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return DateTime.MinValue;
            }
        }

        public static DateTime Convert_String_2DateTime(string p_str_datetime)
        {
            try
            {
                string[] _arr_au_date = p_str_datetime.Trim().Split('-');
                string[] _arr_time = _arr_au_date[0].Split(':');
                DateTime _dt_au = ConvertData.ConvertString2Date(_arr_au_date[1].Trim());
                DateTime _dt = new DateTime(_dt_au.Year, _dt_au.Month, _dt_au.Day, Convert.ToInt16(_arr_time[0]), Convert.ToInt16(_arr_time[1]), 0);
                return _dt;
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return DateTime.MinValue;
            }
        }

        public static DateTime ConvertString2Date_dd_MM_yyyy(string str)
        {
            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.CurrentCulture;
            try
            {
                return DateTime.ParseExact(str, "dd-MM-yyyy", provider); // strDate 
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// Convert string to datetime format dd/MM/yyyy HH:mm
        /// </summary>
        /// <param name="str">ví dụ 17/01/2015 09:10</param>
        /// <returns></returns>
        public static DateTime ConvertString2DateWithTime(string str)
        {
            str = str.Replace("_", "0");
            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.CurrentCulture;
            try
            {
                return DateTime.ParseExact(str, strDate_Time, provider);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// dd/MM/yyyy HH:mm:ss
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime ConvertString2DateWithTimes(string str)
        {
            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.CurrentCulture;
            try
            {
                return DateTime.ParseExact(str, strDate_Times, provider);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }


        public const string strDate_1 = "yyyy/MM/dd";
        public static string ConvertDate2String_yyyyMMdd(DateTime p_date)
        {
            try
            {
                return p_date.ToString(strDate_1);
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return "";
            }
        }
        #endregion

        #region Dinh dang kieu Money
        public static String ConvertString2Money(string str)
        {

            try
            {
                decimal _st = Convert.ToDecimal(str);
                //"{0:0.##}"
                //"{0:0,0 VND}"
                return String.Format("{0:0,0.##}", _st);

            }
            catch
            {
                return "";
            }
        }

        public static Decimal ConvertStringMoney2Decimal(string soTien)
        {
            try
            {
                Decimal tid = Convert.ToDecimal(soTien);
                return tid;
            }
            catch
            {
                return -1;
            }

        }
        #endregion

        #region kieu byte

        public static Byte[] string2byte(string t)
        {
            try
            {
                return Encoding.ASCII.GetBytes(t);
            }
            catch
            {
                return new Byte[0];
            }
        }

        public static string byte2string(Byte[] t)
        {
            try
            {
                return Encoding.ASCII.GetString(t);
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region Convert DataTable

        public static void ConvertArrayListToDataTable(ArrayList arrayList, ref DataTable p_dt)
        {
            //DataTable dt = new DataTable();

            if (arrayList.Count != 0)
            {
                ConvertObjectToDataTableSchema(arrayList[0], ref p_dt);
                FillData(arrayList, p_dt);
            }

            //return p_dt;
        }

        public static void ConvertObjectToDataTableSchema(Object o, ref DataTable dt)
        {
            //DataTable dt = new DataTable();
            PropertyInfo[] properties = o.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                DataColumn dc = new DataColumn(property.Name);
                dc.DataType = property.PropertyType; dt.Columns.Add(dc);
            }
            //return dt;
        }

        private static void FillData(ArrayList arrayList, DataTable dt)
        {
            foreach (Object o in arrayList)
            {
                DataRow dr = dt.NewRow();
                PropertyInfo[] properties = o.GetType().GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    dr[property.Name] = property.GetValue(o, null);
                }
                dt.Rows.Add(dr);
            }
        }

        public static DataTable ConvertToDatatable<T>(IList<T> data, bool p_isAdd_STT = true)
        {
            try
            {
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
                DataTable table = new DataTable();
                for (int i = 0; i < props.Count; i++)
                {
                    PropertyDescriptor prop = props[i];
                    table.Columns.Add(prop.Name, prop.PropertyType);
                }

                int SoThuTu = 0;
                object[] values;
                if (p_isAdd_STT == true)
                {
                    table.Columns.Add("STT");
                    values = new object[props.Count + 1];
                }
                else
                    values = new object[props.Count];

                foreach (T item in data)
                {
                    if (p_isAdd_STT == true)
                    {
                        for (int i = 0; i < values.Length - 1; i++)
                        {
                            values[i] = props[i].GetValue(item);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < values.Length; i++)
                        {
                            values[i] = props[i].GetValue(item);
                        }
                    }

                    SoThuTu++;
                    if (p_isAdd_STT == true)
                    {
                        values[values.Length - 1] = SoThuTu.ToString();
                    }

                    table.Rows.Add(values);
                }
                return table;
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new DataTable();
            }

        }

        public static DataSet ConvertToDataSet<T>(IList<T> data, bool p_isAdd_STT = true)
        {
            try
            {
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
                DataTable table = new DataTable();
                for (int i = 0; i < props.Count; i++)
                {
                    PropertyDescriptor prop = props[i];
                    table.Columns.Add(prop.Name, prop.PropertyType);
                }

                int SoThuTu = 0;
                object[] values;
                if (p_isAdd_STT == true)
                {
                    table.Columns.Add("STT");
                    values = new object[props.Count + 1];
                }
                else
                {
                    values = new object[props.Count];
                }

                foreach (T item in data)
                {
                    if (p_isAdd_STT == true)
                    {
                        for (int i = 0; i < values.Length - 1; i++)
                        {
                            values[i] = props[i].GetValue(item);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < values.Length; i++)
                        {
                            values[i] = props[i].GetValue(item);
                        }
                    }

                    SoThuTu++;
                    if (p_isAdd_STT == true)
                    {
                        values[values.Length - 1] = SoThuTu.ToString();
                    }

                    table.Rows.Add(values);
                }

                DataSet _ds = new DataSet();
                _ds.Tables.Add(table);
                return _ds;
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new DataSet();
            }

        }

        // Convert datatable to dataset
        public static DataSet ConvertDataTableToDataSet<T>(DataTable dataTable, T objects)
        {
            try
            {
                List<T> lstObject = new List<T>();
                lstObject = CBO<T>.FillCollection_FromDataTable(dataTable);
                return ConvertToDataSet(lstObject, false); ;
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new DataSet();
            }
        }
        #endregion
    }
}
