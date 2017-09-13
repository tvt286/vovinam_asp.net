using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
//using Vovinam.Resources;
using System.Data;
using System.Net;
using System.IO;


namespace Vovinam.Common
{
    public static class Utility
    {

        public static String NumberToTextVN(decimal total)
        {
            try
            {
                string rs = "";
                total = Math.Round(total, 0);
                string[] ch = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
                string[] rch = { "lẻ", "mốt", "", "", "", "lăm" };
                string[] u = { "", "mươi", "trăm", "ngàn", "", "", "triệu", "", "", "tỷ", "", "", "ngàn", "", "", "triệu" };
                string nstr = total.ToString();

                int[] n = new int[nstr.Length];
                int len = n.Length;
                for (int i = 0; i < len; i++)
                {
                    n[len - 1 - i] = Convert.ToInt32(nstr.Substring(i, 1));
                }

                for (int i = len - 1; i >= 0; i--)
                {
                    if (i % 3 == 2)// số 0 ở hàng trăm
                    {
                        if (n[i] == 0 && n[i - 1] == 0 && n[i - 2] == 0) continue;//nếu cả 3 số là 0 thì bỏ qua không đọc
                    }
                    else if (i % 3 == 1) // số ở hàng chục
                    {
                        if (n[i] == 0)
                        {
                            if (n[i - 1] == 0) { continue; }// nếu hàng chục và hàng đơn vị đều là 0 thì bỏ qua.
                            else
                            {
                                rs += " " + rch[n[i]]; continue;// hàng chục là 0 thì bỏ qua, đọc số hàng đơn vị
                            }
                        }
                        if (n[i] == 1)//nếu số hàng chục là 1 thì đọc là mười
                        {
                            rs += " mười"; continue;
                        }
                    }
                    else if (i != len - 1)// số ở hàng đơn vị (không phải là số đầu tiên)
                    {
                        if (n[i] == 0)// số hàng đơn vị là 0 thì chỉ đọc đơn vị
                        {
                            if (i + 2 <= len - 1 && n[i + 2] == 0 && n[i + 1] == 0) continue;
                            rs += " " + (i % 3 == 0 ? u[i] : u[i % 3]);
                            continue;
                        }
                        if (n[i] == 1)// nếu là 1 thì tùy vào số hàng chục mà đọc: 0,1: một / còn lại: mốt
                        {
                            rs += " " + ((n[i + 1] == 1 || n[i + 1] == 0) ? ch[n[i]] : rch[n[i]]);
                            rs += " " + (i % 3 == 0 ? u[i] : u[i % 3]);
                            continue;
                        }
                        if (n[i] == 5) // cách đọc số 5
                        {
                            if (n[i + 1] != 0) //nếu số hàng chục khác 0 thì đọc số 5 là lăm
                            {
                                rs += " " + rch[n[i]];// đọc số 
                                rs += " " + (i % 3 == 0 ? u[i] : u[i % 3]);// đọc đơn vị
                                continue;
                            }
                        }
                    }

                    rs += (rs == "" ? " " : ", ") + ch[n[i]];// đọc số
                    rs += " " + (i % 3 == 0 ? u[i] : u[i % 3]);// đọc đơn vị
                }
                if (rs[rs.Length - 1] != ' ')
                    rs += " đồng";
                else
                    rs += "đồng";

                if (rs.Length > 2)
                {
                    string rs1 = rs.Substring(0, 2);
                    rs1 = rs1.ToUpper();
                    rs = rs.Substring(2);
                    rs = rs1 + rs;
                }
                return rs.Trim().Replace("lẻ,", "lẻ").Replace("mươi,", "mươi").Replace("trăm,", "trăm").Replace("mười,", "mười");
            }
            catch
            {
                return "";
            }

        }

        public static String NumberToTextVN(decimal? total)
        {
            if (total.HasValue)
                return NumberToTextVN(total.Value);

            return string.Empty;
        }

        public static Dictionary<string, object> NvcToDictionary(NameValueCollection nvc, bool handleMultipleValuesPerKey)
        {
            var result = new Dictionary<string, object>();
            foreach (string key in nvc.Keys)
            {
                if (key.Contains("password"))
                {
                    continue;
                }

                if (handleMultipleValuesPerKey)
                {
                    string[] values = nvc.GetValues(key);
                    if (values.Length == 1)
                    {
                        result.Add(key, values[0]);
                    }
                    else
                    {
                        result.Add(key, values);
                    }
                }
                else
                {
                    result.Add(key, nvc[key]);
                }
            }

            return result;
        }
        public static int? ToNullableInt(this string s)
        {
            int i;
            if (int.TryParse(s, System.Globalization.NumberStyles.AllowThousands, null, out i)) return i;
            return null;
        }

        public static decimal? ToNullableDecimal(this string s)
        {
            decimal i;
            if (decimal.TryParse(s, System.Globalization.NumberStyles.AllowThousands, null, out i)) return i;
            return null;
        }

        public static decimal? ConvertoNullableDecimal(object o)
        {
            decimal i;
            if (o.GetType() == typeof(string))
            {
                if (decimal.TryParse(o.ToString(), System.Globalization.NumberStyles.AllowThousands, null, out i)) return i;
                return null;
            }
            try
            {
                return (decimal)(double)o;
            }
            catch
            {
                try
                {
                    return (decimal)(int)o;
                }
                catch { }
                return null;
            }
        }


        public static bool IsEmpty(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

        public static string MoneyToString(this decimal money)
        {
            if (money == 0)
            {
                return "0";
            }
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");   // try with "en-US"
            return money.ToString("#,###.##", cul.NumberFormat);
        }

        public static string MoneyToStringVN(this int money)
        {
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
            return money.ToString("#,##0", cul.NumberFormat);
        }

        public static string MoneyToStringVN(this double money)
        {
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
            return money.ToString("#,##0", cul.NumberFormat);
        }

        public static string MoneyToStringVN(this decimal money)
        {
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
            return money.ToString("#,##0", cul.NumberFormat);
        }

        public static string MoneyToStringUS(this decimal money, bool rounded = false)
        {
            CultureInfo cul = CultureInfo.GetCultureInfo("en-US");
            if (rounded)
            {
                return string.Format(cul, "{0:n0}", Math.Round(money, 0));
            }
            return string.Format(cul, "{0:n}", money);
        }

        public static string MoneyToStringUS(this double money, bool rounded = false)
        {
            CultureInfo cul = CultureInfo.GetCultureInfo("en-US");
            if (rounded)
            {
                return string.Format(cul, "{0:n0}", Math.Round(money, 0));
            }
            return string.Format(cul, "{0:n}", money);
        }
        public static string MoneyToStringUS(this int money, bool rounded = false)
        {
            CultureInfo cul = CultureInfo.GetCultureInfo("en-US");
            if (rounded)
            {
                return string.Format(cul, "{0:n0}", money);
            }
            return string.Format(cul, "{0:n}", money);
        }

        public static string ConvertCurrencyToWord(decimal? value, string currency)
        {
            string result = string.Empty;
            try
            {
                if (value.HasValue == false)
                    return result;
                switch (currency)
                {
                    case "USD":
                        result = value.Value.ToString("N2");
                        //result = String.Format("{0:#,###,###,##0.##}", value.Value);
                        break;
                    case "VND":
                        result = value.Value.ToString("N0");
                        //result = String.Format("{0:#,###,###,##0}", value.Value);
                        break;
                    default:
                        result = value.Value.ToString("N0");
                        //result = String.Format("{0:#,###,###,##0.##}", value.Value);
                        break;
                }
            }
            catch
            {
                return result;
            }
            return result;
        }

        public static string ConvertCurrencyToWord(decimal value, string currency)
        {
            string result = string.Empty;
            try
            {
                switch (currency)
                {
                    case "USD":
                        result = value.ToString("N2");
                        //result = String.Format("{0:#,###,###,##0.##}", value.Value);
                        break;
                    case "VND":
                        result = value.ToString("N0");
                        //result = String.Format("{0:#,###,###,##0}", value.Value);
                        break;
                    default:
                        result = value.ToString("N0");
                        //result = String.Format("{0:#,###,###,##0.##}", value.Value);
                        break;
                }
            }
            catch
            {
                return result;
            }
            return result;
        }


        public static bool NotEmpty(this string s)
        {
            return !string.IsNullOrWhiteSpace(s);
        }

        public static bool IsAllDigits(this string s)
        {
            return s.All(char.IsDigit);
        }

        public static string ConvertToUnsign(string str)
        {
            string[] signs = new string[]
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
            for (int i = 1; i < signs.Length; i++)
            {
                for (int j = 0; j < signs[i].Length; j++)
                {
                    str = str.Replace(signs[i][j], signs[0][i - 1]);
                }
            }
            return str;
        }

        public static string RemoveUnicodeCompound(string str)
        {
            string[] signs = new string[]
            {
                "aáàảãạ",
                "ăắằẳẵặ",
                "âấầẩẫậ",
                "eéèẻẽẹ",
                "êếềểễệ",
                "oóòỏõọ",
                "ôốồổỗọ",
                "ơớờởỡợ",
                "uúùủũụ",
                "ưứừửữự",
                "iíìỉĩị",
                "AÁÀẢÃẠ",
                "ĂẮẰẲẴẶ",
                "ÂẤẦẨẪẬ",
                "EÉÈẺẼẸ",
                "ÊẾỀỂỄỆ",
                "OÓÒỎÕỌ",
                "ÔỐỒỔỖỌ",
                "ƠỚỜỞỠỢ",
                "UÚÙỦŨỤ",
                "ƯỨỪỬỮỰ",
                "IÍÌỈĨỊ",
            };
            var sb = new StringBuilder(str.Length);
            sb.Append(str);
            foreach (string t in signs)
            {
                // sắc
                sb.Replace(t[0].ToString() + (char)769, t[1].ToString());
                // huyền
                sb.Replace(t[0].ToString() + (char)768, t[2].ToString());
                // hỏi
                sb.Replace(t[0].ToString() + (char)777, t[3].ToString());
                // ngã
                sb.Replace(t[0].ToString() + (char)771, t[4].ToString());
                // nặng
                sb.Replace(t[0].ToString() + (char)803, t[5].ToString());
            }
            return sb.ToString();
        }

        //public static string GetTitle(string table, string fieldName)
        //{
        //    var enumText = EnumText.ResourceManager.GetString(string.Format("{0}.{1}", table, fieldName));
        //    return enumText ?? fieldName;
        //}

        public static string CompareObjectDifferenceValueToString(object source, object target)
        {
            var propertiesSource =
                from property in source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                select new
                {
                    property.Name,
                    Value = property.GetValue(source, null)
                };

            var propertiesTarget =
                from property in target.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                select new
                {
                    property.Name,
                    Value = property.GetValue(target, null)
                };

            var builder = new StringBuilder();

            foreach (var propertyTarget in propertiesTarget)
            {
                // ReSharper disable once PossibleMultipleEnumeration
                var propertySource = propertiesSource.First(x => x.Name == propertyTarget.Name);
                if (propertySource.Value != null && propertyTarget.Value != null)
                {
                    if (propertySource.Value.ToString() != propertyTarget.Value.ToString())
                    {
                        builder
                        .Append(propertySource.Name + " cũ: ")
                        .Append(propertySource.Value)
                        .Append(" mới: ")
                        .Append(propertyTarget.Value)
                        .AppendLine();
                    }
                }
                else if ((propertySource.Value == null && propertyTarget.Value != null) || (propertySource.Value != null && propertyTarget.Value == null))
                {
                    builder
                        .Append(propertySource.Name + " cũ: ")
                        .Append(propertySource.Value)
                        .Append(" mới: ")
                        .Append(propertyTarget.Value)
                        .AppendLine();
                }

            }

            return builder.ToString();
        }

        public static bool IsValidEmailAddress(string emailaddress)
        {
            try
            {
                Regex rx = new Regex(
            @"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");
                return rx.IsMatch(emailaddress);
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool IsValidPhone(string phone)
        {
            var regex = new Regex("^09[0-9]{8}$|^01[0-9]{9}$");
            //var regex = new Regex(LiuLoConfiguration.CungMua.RegexMobile);
            return regex.IsMatch(phone);

            var telco = GetTelcoName(phone);
            switch (telco)
            {
                case "SFONE":
                case "VNMOBI":
                case "BEELINE":
                case "MOBI":
                case "VIETTEL":
                case "VINA":
                    return true;
                default:
                    return false;
            }
        }

        private static string GetTelcoName(string strPhone)
        {
            if (strPhone != null)
            {
                string strTelco = string.Empty;
                string strCheck;
                if (strPhone[1].Equals('9'))
                    strCheck = strPhone.Substring(0, 3);
                else
                    strCheck = strPhone.Substring(0, 4);

                switch (strCheck)
                {
                    case "095":
                        strTelco = "SFONE";
                        break;
                    //------------------------------------------------------
                    case "092":
                    case "0188":
                        strTelco = "VNMOBI";
                        break;
                    //------------------------------------------------------
                    case "0199":
                        strTelco = "BEELINE";
                        break;
                    //------------------------------------------------------
                    case "099":
                        strTelco = "GMOBILE";
                        break;
                    //------------------------------------------------------
                    case "090":
                    case "093":
                    case "0120":
                    case "0121":
                    case "0122":
                    case "0124":
                    case "0126":
                    case "0128":
                        strTelco = "MOBI";
                        break;
                    //------------------------------------------------------
                    case "096": // evn cu
                    case "097":
                    case "098":
                    case "0162":
                    case "0163":
                    case "0164":
                    case "0165":
                    case "0166":
                    case "0167":
                    case "0168":
                    case "0169":
                        strTelco = "VIETTEL";
                        break;
                    //------------------------------------------------------
                    case "091":
                    case "094":
                    case "0123":
                    case "0125":
                    case "0127":
                    case "0129":
                        strTelco = "VINA";
                        break;
                    default:
                        return "";
                }

                return strTelco;
            }
            else
                return "";
        }

        public static string Right(this string fileName, int maxLenght = 255)
        {
            if (maxLenght > 0 && fileName.Length > maxLenght)
                return fileName.Substring(fileName.Length - maxLenght);
            return fileName;

        }

        public static List<string> GetHrLevelId(string s)
        {
            return s.Split('-').ToList();
        }

        public static string FormatDate(this DateTime? dateTime, string format = "dd/MM/yyyy")
        {
            if (!dateTime.HasValue)
                return string.Empty;

            return FormatDate(dateTime.Value, format);
        }
        public static string FormatDate(this DateTime dateTime, string format = "dd/MM/yyyy")
        {
            return dateTime.ToString(format);
        }

        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        public static DataTable FindCoordinates(String Adress)
        {
            string url = "http://maps.google.com/maps/api/geocode/xml?address=" + Adress + "&sensor=false";
            WebRequest request = WebRequest.Create(url);
            using (WebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    DataSet dsResult = new DataSet();
                    dsResult.ReadXml(reader);
                    DataTable dtCoordinates = new DataTable();
                    dtCoordinates.Columns.AddRange(new DataColumn[4] { new DataColumn("Id", typeof(int)),
                        new DataColumn("Address", typeof(string)),
                        new DataColumn("Latitude",typeof(string)),
                        new DataColumn("Longitude",typeof(string)) });
                    foreach (DataRow row in dsResult.Tables["result"].Rows)
                    {
                        string geometry_id = dsResult.Tables["geometry"].Select("result_id = " + row["result_id"].ToString())[0]["geometry_id"].ToString();
                        DataRow location = dsResult.Tables["location"].Select("geometry_id = " + geometry_id)[0];
                        dtCoordinates.Rows.Add(row["result_id"], row["formatted_address"], location["lat"], location["lng"]);
                    }
                    return dtCoordinates;
                }
            }
        }

        public static string[] SplitSpaceString(string str)
        {

            return str.Split(' ');
        }

        public static string GetFirtsName(string str)
        {
            string[] name = str.Split(' ');
            
            var LName = "";
            for (int i = 0; i < name.Length - 1; i++)
            {
                LName += name[i] + " ";
            }
            return LName;     
        }

        public static string GetLastName(string str)
        {
            string[] name = str.Split(' ');
            return name.Last();
        }
    }
}
