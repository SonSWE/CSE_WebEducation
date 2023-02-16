using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public class NumberFormat
    {
        public const string NUMBER_FORMAT_2_DECIMAL = "_( #,##0.00_);_( (#,##0.00);_(* \"0\"??_);_(@_)";
        public const string NUMBER_FORMAT_PERCENTAGE = "0.00%";
        public const string NUMBER_FORMAT_INT = "_( #,##0_);_( (#,##0);_(* \"0\"_);_(@_)";
        public const string NUMBER_FORMAT_N0_0 = "#,###";
        public const string NUMBER_FORMAT_N0_1 = "#,##0";
        public const string NUMBER_FORMAT_N1_0 = "#,###.#";
        public const string NUMBER_FORMAT_N1_1 = "#,##0.#";
        public const string NUMBER_FORMAT_N2_0 = "#,###.##";
        public const string NUMBER_FORMAT_N2_1 = "#,##0.##";
        public const string NUMBER_FORMAT_N3_0 = "#,###.###";
        public const string NUMBER_FORMAT_N3_1 = "#,##0.###";
        public const string NUMBER_FORMAT_RATE = "#,##0.00";
        public const string NUMBER_FORMAT_RATE_3 = "#,##0.000";
        public const string NUMBER_FORMAT_RATE_4 = "#,##0.#0####";
        public const string NUMBER_FORMAT_RATE_5 = "#,##0.#0";
        public const string NUMBER_FORMAT_RATE_6 = "#,##0.#00";
        public const string NUMBER_FORMAT_N10 = "#,##0.##########";
    }

    public static class NumberFormatExtension
    {
        public static string ToNumberStringN0(this decimal number)
        {
            //return ((number > 0 && number < 1) ? number.ToString(NumberFormat.NUMBER_FORMAT_N0_1) : number.ToString(NumberFormat.NUMBER_FORMAT_N0_0));
            try
            {
                return Convert.ToInt64(number).ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string ToNumberStringN10(this decimal number)
        {
            //return ((number > 0 && number < 1) ? number.ToString(NumberFormat.NUMBER_FORMAT_N0_1) : number.ToString(NumberFormat.NUMBER_FORMAT_N0_0));
            return number.ToString(NumberFormat.NUMBER_FORMAT_N0_1);
        }

        // 1 digit after decimal point
        public static string ToNumberStringN1(this decimal number)
        {
            return ((number > 0 && number < 1) ? number.ToString(NumberFormat.NUMBER_FORMAT_N1_1) : number.ToString(NumberFormat.NUMBER_FORMAT_N1_0));
        }

        // 2 digits after decimal point
        public static string ToNumberStringN2(this decimal number)
        {
            return ((number > 0 && number < 1) ? number.ToString(NumberFormat.NUMBER_FORMAT_N2_1) : number.ToString(NumberFormat.NUMBER_FORMAT_N2_0));
        }

        // 3 digits after decimal point
        public static string ToNumberStringN3(this decimal number)
        {
            return ((number > 0 && number < 1) ? number.ToString(NumberFormat.NUMBER_FORMAT_N2_1) : number.ToString(NumberFormat.NUMBER_FORMAT_N3_0));
        }

        public static string ToNumberRate(this decimal number)
        {
            return number.ToString(NumberFormat.NUMBER_FORMAT_N10);
        }

        public static string ToNumberRate_3(this decimal number)
        {
            return number.ToString(NumberFormat.NUMBER_FORMAT_N10);
        }
        public static string ToNumberRate_4(this decimal number)
        {
            return (number == 0 ? "" : number.ToString(NumberFormat.NUMBER_FORMAT_RATE_4));
        }
        public static string ToNumberRate_41(this decimal number)
        {
            return (number == 0 ? "0" : number.ToString(NumberFormat.NUMBER_FORMAT_RATE_4));
        }
        /// <summary>
        /// Cắt 2 số sau dấu thập phân, không làm tròn 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ToNumberRate_5(this decimal number)
        {
            return number == 0 ? "" : (Math.Truncate(100 * number) / 100).ToString(NumberFormat.NUMBER_FORMAT_RATE_5);
        }

        /// <summary>
        /// Cắt 3 số sau dấu thập phân, có làm tròn
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ToNumberRate_6(this decimal number)
        {
            return (number == 0 ? "" : number.ToString(NumberFormat.NUMBER_FORMAT_RATE_6));
        }
    }
}
