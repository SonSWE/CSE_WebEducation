using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace CommonLib
{
    public static class CommonFunc
    {

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return string.Empty;
        }

        public static bool ValidateIPAddress(string ip)
        {
            return Regex.IsMatch(ip, @"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");
        }

        public static string Encrypt_MD5(string toEncrypt)
        {
            try
            {
                var md5Hasher = new MD5CryptoServiceProvider();
                byte[] hashedBytes;
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(toEncrypt));
                StringBuilder s = new StringBuilder();
                foreach (byte _hashedByte in hashedBytes)
                {
                    s.Append(_hashedByte.ToString("x2"));
                }
                return s.ToString();
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return "";
            }
        }

        public static int GetFromToPaging(int pCurentPage, int pRecordOnPage, out int pToRecord)
        {
            pToRecord = -1;
            try
            {
                int _FromRecord = pRecordOnPage * (pCurentPage - 1) + 1;
                pToRecord = pRecordOnPage * pCurentPage;
                return _FromRecord;
            }
            catch (Exception ex)
            {
                Logger.nlog.Error(ex.ToString());
                pToRecord = -1;
                return -1;
            }
        }

        #region Pageing Ver 2

        public static string PagingData(int pCurPage, int pRecordOnPage, int pTotalRecord, string functionJavascript = "ChangePage", bool hasExportBtn = false)
        {
            try
            {
                int p_end = -1;
                int p_start = CommonFunc.GetFromToPaging(pCurPage, pRecordOnPage, out p_end);
                string pStrPaging = "";
                double _dobTotalRec = Convert.ToDouble(pTotalRecord);
                int _TotalPage = RoundUp(_dobTotalRec / pRecordOnPage);
                pStrPaging = WritePagingV2(p_start, p_end, _TotalPage, pCurPage, functionJavascript, pTotalRecord, pRecordOnPage, "bản ghi", hasExportBtn);
                return pStrPaging;
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return "";
            }
        }

        public static int RoundUp(double pDoubleNum)
        {
            try
            {
                return Convert.ToInt32(Math.Ceiling(pDoubleNum).ToString());
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return -1;
            }
        }

        public static string WritePagingV2(int p_start, int p_end, int iPageCount, int iCurrentPage, string functionJavascript, int iTotalRecords, int iPageSize, string pLoaiBanGhi, bool hasExportBtn = false)
        {
            try
            {
                string strPage = "";
                //strPage += "<div class='box-paging'><ul>";
                strPage += "<div class='box-paging'>";
                if (hasExportBtn)
                {
                    strPage += "<div><button id=\"btnExport\" class=\"btn-render\">Kết xuất <span><img src=\"../version_2/img/icon-render.svg\" alt=\"img\"></span></button></div>";
                }
                else
                {
                    strPage += "<div></div>";
                }
                strPage += "<div class=\"d-flex\" id='d_page'>";
                //strPage += "<div id='d_total_rec'>Tổng số " + iTotalRecords + " bản ghi </div>";
                if (iTotalRecords > 0)
                {
                    strPage += "<div class=\"number-record\" id='d_total_rec'>Hiển thị:  " + p_start + " - " + (p_end < iTotalRecords ? p_end : iTotalRecords) + " / " + iTotalRecords + " bản ghi </div>";
                }
                else
                {
                    strPage += "<div id='d_total_rec'>Hiển thị:  0 - " + (p_end < iTotalRecords ? p_end : iTotalRecords) + " / " + iTotalRecords + " bản ghi </div>";
                }
                strPage += "<div id='d_number_of_page'>";
                if (iPageCount <= 1)
                {
                    strPage += "</div>";
                    strPage += "</div>";
                    //strPage += "<ul>";
                    strPage += "</div>";
                    return strPage;
                }
                if (iCurrentPage > 1)
                {
                    strPage += "<button onclick=\"" + functionJavascript + "(1)\"><span id=\"first\"  href=\"\"><<</span></button>";
                    strPage += "<button onclick=\"" + functionJavascript + "(" + (iCurrentPage - 1) + ")\"><span id=\"back\"  href=\"\"><</span></button>";
                }
                if (iPageCount <= 5)
                {
                    for (int j = 0; j < iPageCount; j++)
                    {
                        if (iCurrentPage == (j + 1))
                        {
                            strPage += "<button onclick=\"" + functionJavascript + "(" + (j + 1) + ")\"><span class=\"a-active\" id=" + (j + 1) + "  href=\"\">" + (j + 1) + "</span></button>";
                        }
                        else
                        {
                            strPage += "<button onclick=\"" + functionJavascript + "(" + (j + 1) + ")\"><span id=" + (j + 1) + "  href=\"\">" + (j + 1) + "</span></button>";
                        }
                    }
                }
                else
                {
                    string cl = "";
                    int t;
                    int pagePreview = 0;
                    //nếu đang ở trang 2 thì vẽ đc có 1 trang trước nó nên sẽ vẽ thêm 3 trang phía sau
                    //default là vẽ 2 trang trc 2 trang sau
                    int soTrangVeLui = 2;
                    if ((iPageCount - iCurrentPage) == 1)
                    {
                        soTrangVeLui = soTrangVeLui + 1;
                    }
                    else if ((iPageCount - iCurrentPage) == 0)
                    {
                        soTrangVeLui = soTrangVeLui + 2;
                    }
                    for (t = iCurrentPage - soTrangVeLui; t <= iCurrentPage; t++) //ve truoc 2 trang
                    {
                        if (t < 1) continue;
                        cl = t == iCurrentPage ? "a-active" : "";
                        strPage += t == iCurrentPage ? "<button onclick=\"" + functionJavascript + "(" + (t) + ")\"><span class='" + cl + "' id=" + (t) + "  href=\"\">" + (t) + "</span></button>"
                                    : "<button  onclick=\"" + functionJavascript + "(" + (t) + ")\"><span class='" + cl + "' id=" + (t) + "  href=\"\">" + (t) + "</span></button>";
                        pagePreview++;
                    }
                    t = 0;
                    cl = "";
                    if (iCurrentPage == 1) //truong hop trang dau tien thi ve du 5 trang
                    {
                        for (t = iCurrentPage + 1; t < iCurrentPage + 5; t++)
                        {
                            if (t >= t + 5 || t > iPageCount) continue;
                            cl = t == iCurrentPage ? "a-active" : "";
                            strPage += t == iCurrentPage ? "<button onclick=\"" + functionJavascript + "(" + (t) + ")\"><span class='" + cl + "' id=" + (t) + "  href=\"\">" + (t) + "</span></button>"
                                     : "<button   onclick=\"" + functionJavascript + "(" + (t) + ")\"><span class='" + cl + "' id=" + (t) + "  href=\"\">" + (t) + "</span></button>";
                        }
                    }
                    else if (iCurrentPage > 1)  //truogn hop ma la trang lon hon 1 thi se ve 4 trang ke tiep + 1 trang truoc
                    {
                        int incr = 5 - (pagePreview - 1);
                        for (t = iCurrentPage + 1; t < iCurrentPage + incr; t++)
                        {
                            if (t >= t + incr || t > iPageCount) continue;
                            cl = t == iCurrentPage ? "a-active" : "";
                            strPage += t == iCurrentPage ? "<button onclick=\"" + functionJavascript + "(" + (t) + ")\"><span class='" + cl + "' id=" + (t) + "  href=\"\">" + (t) + "</span></button>"
                                     : "<button   onclick=\"" + functionJavascript + "(" + (t) + ")\"><span class='" + cl + "' id=" + (t) + "  href=\"\">" + (t) + "</span></button>";
                        }
                    }
                }
                if (iCurrentPage < iPageCount)
                {
                    strPage += "<button onclick=\"" + functionJavascript + "(" + (iCurrentPage + 1) + ")\"><span id=\"next\"  href=\"\">></span></button>";
                    strPage += "<button onclick=\"" + functionJavascript + "(" + iPageCount + ")\"><span id=\"end\"  href=\"\">>></span></button>";
                }
                strPage += "</div>";
                strPage += "</div>";
                //strPage += "<ul>";
                strPage += "</div>";
                return strPage;
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return string.Empty;
            }
        }

        #endregion

        static string[] strDigit = { "không ", "một ", "hai ", "ba ", "bốn ", "năm ", "sáu ", "bảy ", "tám ", "chín " };
        static string[] strGroup = { "nghìn ", "triệu ", "tỷ " };

        public static string ReadNum(string iNum)
        {
            short iG;
            byte k = 0;
            string st, s = "";
            try
            {
                iNum = iNum.Replace(" ", "");
                for (short i = (short)(iNum.Length - 6); i >= iNum.Length % 3; i -= 3)
                {
                    iG = short.Parse(iNum.Substring(i, 3));
                    st = Group3(iG);
                    if (st != "")
                    {
                        st += strGroup[k];
                    }
                    else if (k % 3 == 2)
                    {
                        st = strGroup[k];
                    }
                    k = (byte)((k + 1) % 3);
                    s = st + s;
                }
                if (iNum.Length % 3 != 0 && iNum.Length > 3)
                {
                    iG = short.Parse(iNum.Substring(0, iNum.Length % 3));
                    st = Group3(iG);
                    st += strGroup[k];
                    s = st + s;
                }
                s = s + Group3(short.Parse(iNum.Substring(Math.Max(iNum.Length - 3, 0))));
                if (s == "") s = "không";
                else if (s.Length > 13 && s.Substring(0, 13) == "không trăm lẻ") s = s.Substring(14);
                else if (s.Length > 11 && s.Substring(0, 11) == "không trăm ") s = s.Substring(11);
            }
            catch (Exception e)
            {
                s = e.Message;
            }
            //
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        private static string Group3(short iNum)
        {
            byte[] iDg = new byte[3];
            string[] sResult = new string[3];

            if (iNum == 0) return "";

            iDg[2] = (byte)(iNum / 100);
            iDg[1] = (byte)((iNum / 10) % 10);
            iDg[0] = (byte)(iNum % 10);

            sResult[2] = strDigit[iDg[2]] + "trăm ";
            if (iDg[1] >= 2)
            {
                sResult[1] = strDigit[iDg[1]] + "mươi ";
            }
            else if (iDg[1] == 1)
            {
                sResult[1] = "mười ";
            }
            else if (iDg[1] == 0)
            {
                sResult[1] = "lẻ ";
            }

            sResult[0] = strDigit[iDg[0]];

            if (iDg[0] == 0)
            {
                sResult[0] = "";
                if (iDg[1] == 0) sResult[1] = "";
            }
            else if (iDg[0] == 1 && iDg[1] >= 2)
            {
                sResult[0] = "mốt ";
            }
            else if (iDg[0] == 5 && iDg[1] >= 1)
            {
                sResult[0] = "lăm ";
            }

            return sResult[2] + sResult[1] + sResult[0];
        }


        // RandomString
        private static Random random = new Random();
        public static string RandomString(int length = 6)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static bool CheckPasswordStrength(string p_Password)
        {
            try
            {
                int _numberMatch = 0;
                if (p_Password.Length < 8)
                    return false;

                if (Regex.IsMatch(p_Password, @"\d+")) _numberMatch++;  //check co chua so khong
                if (Regex.IsMatch(p_Password, @"[a-z]")) _numberMatch++; //check co chua chu thuong khong
                if (Regex.IsMatch(p_Password, @"[A-Z]")) _numberMatch++; //check co chua chu hoa khong
                if (Regex.IsMatch(p_Password, @"[!@#\$%\^&\*\?_~\-\(\);\.\+:]+")) _numberMatch++;

                if (CheckIsUnicode(p_Password)) _numberMatch++;
                if (CheckIsUnicode_Upper(p_Password)) _numberMatch++;

                if (_numberMatch >= 3) return true;
                else return false;
            }
            catch
            {
                return false;
            }
        }

        private static bool CheckIsUnicode(string p_unicode)
        {
            try
            {
                bool _ck = false;
                string[] pattern = new string[7];
                pattern[0] = "á|ả|à|ạ|ã|ă|ắ|ẳ|ằ|ặ|ẵ|â|ấ|ẩ|ầ|ậ|ẫ";
                pattern[1] = "ó|ỏ|ò|ọ|õ|ô|ố|ổ|ồ|ộ|ỗ|ơ|ớ|ở|ờ|ợ|ỡ";
                pattern[2] = "é|è|ẻ|ẹ|ẽ|ê|ế|ề|ể|ệ|ễ";
                pattern[3] = "ú|ù|ủ|ụ|ũ|ư|ứ|ừ|ử|ự|ữ";
                pattern[4] = "í|ì|ỉ|ị|ĩ";
                pattern[5] = "ý|ỳ|ỷ|ỵ|ỹ";
                pattern[6] = "đ";

                // kiểm tra xem có chữ tiếng việt thường hay không
                for (int i = 0; i < pattern.Length; i++)
                {
                    MatchCollection matchs = Regex.Matches(p_unicode, pattern[i]);
                    if (matchs.Count > 0)
                        return true;
                }

                return _ck;
            }
            catch
            {
                return false;
            }
        }

        public static bool CheckIsUnicode_Upper(string p_unicode)
        {
            try
            {
                bool _ck = false;
                string[] pattern = new string[7];

                pattern[0] = "á|ả|à|ạ|ã|ă|ắ|ẳ|ằ|ặ|ẵ|â|ấ|ẩ|ầ|ậ|ẫ";
                pattern[1] = "ó|ỏ|ò|ọ|õ|ô|ố|ổ|ồ|ộ|ỗ|ơ|ớ|ở|ờ|ợ|ỡ";
                pattern[2] = "é|è|ẻ|ẹ|ẽ|ê|ế|ề|ể|ệ|ễ";
                pattern[3] = "ú|ù|ủ|ụ|ũ|ư|ứ|ừ|ử|ự|ữ";
                pattern[4] = "í|ì|ỉ|ị|ĩ";
                pattern[5] = "ý|ỳ|ỷ|ỵ|ỹ";
                pattern[6] = "đ";

                // kiểm tra xem có chữ tiếng việt thường hay không
                for (int i = 0; i < pattern.Length; i++)
                {
                    MatchCollection matchs = Regex.Matches(p_unicode, pattern[i].ToUpper());
                    if (matchs.Count > 0)
                        return true;
                }

                return _ck;
            }
            catch
            {
                return false;
            }
        }

        private static readonly string[] VietNamChar = new string[]
       {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
       };

        public static string RemoveVietnameseSign(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                for (int i = 1; i < VietNamChar.Length; i++)
                {
                    for (int j = 0; j < VietNamChar[i].Length; j++)
                        str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
                }
            }
            return str;
        }

        public static string ReplaceUnicodeString(string str, int maxlength = 100)
        {
            try
            {
                Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
                string temp = str.Normalize(NormalizationForm.FormD);
                temp = regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
                //temp = Regex.Replace(temp, @"[^A-Za-z0-9  -]+", "");

                temp = temp.Replace(" ", "-");
                while (temp.IndexOf("--") >= 0)
                {
                    temp = temp.Replace("--", "-");
                }

                if (maxlength > 0 && temp.Length > maxlength)
                {
                    temp = temp.Substring(0, maxlength);
                }

                return temp;
            }
            catch
            {
                return "";
            }
        }

        public static string EncryptUrlString(string textPlain, string key = "")
        {
            try
            {
                // Tam k mã hóa để test
                return textPlain;

                //if (string.IsNullOrEmpty(textPlain))
                //{
                //    return string.Empty;
                //}
                //if (string.IsNullOrEmpty(key))
                //{
                //    key = ConstData.keyEncDefault;
                //}
                //return EncryptString(textPlain, key).Replace("=", String.Empty).Replace('+', '-').Replace('/', '_');
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex);
            }
            return string.Empty;
        }

        public static List<int> LstChangeNumberDisplayOnPage()
        {
            List<int> clstRecordOnPage = new List<int>();
            clstRecordOnPage.Add(10);
            clstRecordOnPage.Add(20);
            clstRecordOnPage.Add(30);
            clstRecordOnPage.Add(40);
            clstRecordOnPage.Add(50);
            return clstRecordOnPage;
        }
        public static string Base64UrlEncode(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
            {
                return string.Empty;
            }
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes).Replace("=", String.Empty).Replace('+', '-').Replace('/', '_');
        }

        public static string Base64UrlDecode(string base64EncodedData)
        {
            if (string.IsNullOrEmpty(base64EncodedData))
            {
                return string.Empty;
            }
            base64EncodedData = base64EncodedData.Replace('-', '+').Replace('_', '/').PadRight(base64EncodedData.Length + (4 - base64EncodedData.Length % 4) % 4, '=');
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public static string Func_FindHrefs(string input)
        {
            string str = string.Empty;

            string strref = input;
            var regex = new Regex("<a [^>]*href=(?:'(?<href>.*?)')|(?:\"(?<href>.*?)\")", RegexOptions.IgnoreCase);
            var urls = regex.Matches(strref).OfType<Match>().Select(m => m.Groups["href"].Value).SingleOrDefault();
            if (!string.IsNullOrEmpty(urls))
                str = urls;
            else
                str = input;
            return str;
        }

        #region TCVN3ToUnicode

        private static char[] tcvnchars = {
            'µ', '¸', '¶', '·', '¹',
            '¨', '»', '¾', '¼', '½', 'Æ',
            '©', 'Ç', 'Ê', 'È', 'É', 'Ë',
            '®', 'Ì', 'Ð', 'Î', 'Ï', 'Ñ',
            'ª', 'Ò', 'Õ', 'Ó', 'Ô', 'Ö',
            '×', 'Ý', 'Ø', 'Ü', 'Þ',
            'ß', 'ã', 'á', 'â', 'ä',
            '«', 'å', 'è', 'æ', 'ç', 'é',
            '¬', 'ê', 'í', 'ë', 'ì', 'î',
            'ï', 'ó', 'ñ', 'ò', 'ô',
            '­', 'õ', 'ø', 'ö', '÷', 'ù',
            'ú', 'ý', 'û', 'ü', 'þ',
            '¡', '¢', '§', '£', '¤', '¥', '¦'
        };
        private static char[] unichars = {
            'à', 'á', 'ả', 'ã', 'ạ',
            'ă', 'ằ', 'ắ', 'ẳ', 'ẵ', 'ặ',
            'â', 'ầ', 'ấ', 'ẩ', 'ẫ', 'ậ',
            'đ', 'è', 'é', 'ẻ', 'ẽ', 'ẹ',
            'ê', 'ề', 'ế', 'ể', 'ễ', 'ệ',
            'ì', 'í', 'ỉ', 'ĩ', 'ị',
            'ò', 'ó', 'ỏ', 'õ', 'ọ',
            'ô', 'ồ', 'ố', 'ổ', 'ỗ', 'ộ',
            'ơ', 'ờ', 'ớ', 'ở', 'ỡ', 'ợ',
            'ù', 'ú', 'ủ', 'ũ', 'ụ',
            'ư', 'ừ', 'ứ', 'ử', 'ữ', 'ự',
            'ỳ', 'ý', 'ỷ', 'ỹ', 'ỵ',
            'Ă', 'Â', 'Đ', 'Ê', 'Ô', 'Ơ', 'Ư'
        };
        private static char[] convertTable;

        private static void MakeConverter()
        {
            convertTable = new char[256];
            for (int i = 0; i < 256; i++)
                convertTable[i] = (char)i;
            for (int i = 0; i < tcvnchars.Length; i++)
                convertTable[tcvnchars[i]] = unichars[i];
        }

        public static string TCVN3ToUnicode(string value)
        {
            MakeConverter();
            char[] chars = value.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
                if (chars[i] < (char)256)
                    chars[i] = convertTable[chars[i]];
            return new string(chars);
        }

        #endregion TCVN3ToUnicode

        #region OtherCharToUnicode

        private static char[] daus = new char[5] { '́', '̀', '̣', '̉', '̃' }; // Sac, Huyen, Nang, Hoi, Nga
        private static readonly string[] convertChars = new string[]
        {
            "aAâÂăĂeEêÊoOôÔơƠuUưƯiIyY",
            "áàạảã",
            "ÁÀẠẢÃ",
            "ấầậẩẫ",
            "ẤẦẬẨẪ",
            "ắằặẳẵ",
            "ẮẰẶẲẴ",
            "éèẹẻẽ",
            "ÉÈẸẺẼ",
            "ếềệểễ",
            "ẾỀỆỂỄ",
            "óòọỏõ",
            "ÓÒỌỎÕ",
            "ốồộổỗ",
            "ỐỒỘỔỖ",
            "ớờợởỡ",
            "ỚỜỢỞỠ",
            "úùụủũ",
            "ÚÙỤỦŨ",
            "ứừựửữ",
            "ỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
        };
        public static string OtherCharToUnicode(string value)
        {
            char[] chars = value.ToCharArray();
            char[] resultChars = new char[chars.Length];
            int resultIdx = 0;

            for (int i = 0; i < chars.Length; i++)
            {
                int charIdx = convertChars[0].IndexOf(chars[i]);
                int dauIdx = i != chars.Length - 1 ? Array.IndexOf(daus, chars[i + 1]) : -1;

                if (charIdx >= 0 && dauIdx >= 0)
                {
                    resultChars[resultIdx] = convertChars[charIdx + 1][dauIdx];
                    i++;
                }
                else
                {
                    resultChars[resultIdx] = chars[i];
                }
                resultIdx++;
            }

            return new string(resultChars, 0, resultIdx);
        }

        #endregion OtherCharToUnicode

        public static string NumberToTextVN(decimal v_tong_tien_thanh_toan)
        {
            string str = "";
            string s = v_tong_tien_thanh_toan.ToString("#");
            string[] so = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] hang = new string[] { "", "nghìn", "triệu", "tỷ" };
            int i, j, donvi, chuc, tram;

            bool booAm = false;
            decimal decS = 0;
            //Tung addnew
            try
            {
                decS = Convert.ToDecimal(s.ToString());
            }
            catch
            {
            }
            if (decS < 0)
            {
                decS = -decS;
                s = decS.ToString();
                booAm = true;
            }
            i = s.Length;
            if (i == 0)
                str = so[0] + str;
            else
            {
                j = 0;
                while (i > 0)
                {
                    donvi = Convert.ToInt32(s.Substring(i - 1, 1));
                    i--;
                    if (i > 0)
                        chuc = Convert.ToInt32(s.Substring(i - 1, 1));
                    else
                        chuc = -1;
                    i--;
                    if (i > 0)
                        tram = Convert.ToInt32(s.Substring(i - 1, 1));
                    else
                        tram = -1;
                    i--;
                    if ((donvi > 0) || (chuc > 0) || (tram > 0) || (j == 3))
                        str = hang[j] + str;
                    j++;
                    if (j > 3) j = 1;
                    if ((donvi == 1) && (chuc > 1))
                        str = "một " + str;
                    else
                    {
                        if ((donvi == 5) && (chuc > 0))
                            str = "lăm " + str;
                        else if (donvi > 0)
                            str = so[donvi] + " " + str;
                    }
                    if (chuc < 0)
                        break;
                    else
                    {
                        if ((chuc == 0) && (donvi > 0)) str = "lẻ " + str;
                        if (chuc == 1) str = "mười " + str;
                        if (chuc > 1) str = so[chuc] + " mươi " + str;
                    }
                    if (tram < 0) break;
                    else
                    {
                        if ((tram > 0) || (chuc > 0) || (donvi > 0)) str = so[tram] + " trăm " + str;
                    }
                    str = " " + str;
                }
            }
            if (booAm) str = "Âm " + str;
            if (str.Length > 0)
            {
                string str1 = str.Trim().Substring(0, 1);
                string str2 = str.Trim().Substring(1);

                str = str1.ToUpper() + str2;
            }

            return str.Trim();
        }

        public static string GetOrderByString(string[] sortTypesAllow, string[] sortColsAllow, string[] sortColsAsString, string OrderBy = "", string OrderType = "")
        {
            string strorder = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(OrderBy) && sortColsAllow.Contains(OrderBy, StringComparer.OrdinalIgnoreCase))
                {
                    if (sortColsAsString.Contains(OrderBy, StringComparer.OrdinalIgnoreCase))
                    {
                        // Sort as string
                        strorder = $"NLSSORT(UPPER({OrderBy}), 'NLS_SORT=BINARY_AI')";
                    }
                    else
                    {
                        // Sort default
                        strorder = OrderBy;
                    }
                    //
                    strorder += $" {(!string.IsNullOrEmpty(OrderType) && sortTypesAllow.Contains(OrderType, StringComparer.OrdinalIgnoreCase) ? OrderType : "")}";
                }
            }
            catch (Exception)
            {
                strorder = string.Empty;
            }

            return strorder;
        }

        public static string[] sortTypesAllow = new string[2] { "ASC", "DESC" };

        public static string GetOrderByString_New(string OrderBy, string OrderType, string[] sortColsAllow = null, string[] sortColsAsString = null)
        {
            try
            {
                string[] arr = OrderBy != null ? OrderBy.Split(",") : new string[0];
                string strorder = "";
                if (arr.Count() > 1)
                {

                    strorder = string.Join(",", arr.Select(item => (item + $" {(!string.IsNullOrEmpty(OrderType) && sortTypesAllow.Contains(OrderType, StringComparer.OrdinalIgnoreCase) ? OrderType : "")}")));
                }
                else
                {
                    strorder = OrderBy + $" {(!string.IsNullOrEmpty(OrderType) && sortTypesAllow.Contains(OrderType, StringComparer.OrdinalIgnoreCase) ? OrderType : "")}";
                }
                return strorder;
            }
            catch (Exception ex)
            {
                Logger.nlog.Error(ex.ToString());
                return "";
            }
        }

        public static string convertToUnSign2(string s)
        {
            string stFormD = s.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                System.Globalization.UnicodeCategory uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }
            sb = sb.Replace('Đ', 'D');
            sb = sb.Replace('đ', 'd');
            return (sb.ToString().Normalize(NormalizationForm.FormD));
        }
    }
}
