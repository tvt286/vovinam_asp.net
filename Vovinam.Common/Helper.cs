using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Reflection;
using System.Text;


namespace Vovinam.Common
{
    public class ExcelHelper
    {
        public static DataTable ReadExcelFile(string sheetName,
                string path,
                ref StringBuilder message)
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection())
                {
                    DataTable dt = new DataTable();
                    string Import_FileName = path;
                    string fileExtension = Path.GetExtension(Import_FileName);
                    if (fileExtension == ".xls")
                        conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Import_FileName + ";" + "Extended Properties='Excel 8.0;HDR=YES;'";
                    if (fileExtension == ".xlsx")
                        conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Import_FileName + ";" + "Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                    using (OleDbCommand comm = new OleDbCommand())
                    {
                        comm.CommandText = "Select * from [" + sheetName + "$]";

                        comm.Connection = conn;

                        using (OleDbDataAdapter da = new OleDbDataAdapter())
                        {
                            da.SelectCommand = comm;
                            da.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message.Append(string.Format("Read excel loi: {0}",
                        ex.Message));
            }
            return new DataTable();
        }

        /// <summary>
        /// Get first sheet of excel file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static DataTable ReadExcelFile(string path)
        {
            using (
                var connect =
                    new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path +
                                        @";Extended Properties=""Excel 12.0;HDR=YES;"""))
            {
                connect.Open();
                var tables = connect.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (tables == null)
                {
                    return null;
                }

                String[] excelSheets = new String[tables.Rows.Count];

                // Add the sheet name to the string array.
                for (int j = 0; j < tables.Rows.Count; j++)
                {
                    excelSheets[j] = tables.Rows[j]["TABLE_NAME"].ToString();
                }

                var command = new OleDbCommand(string.Format("select * from [{0}]", excelSheets[0]), connect);
                var da = new OleDbDataAdapter(command);
                var dset = new DataSet();
                da.Fill(dset);
                if (dset.Tables.Count > 0)
                {
                    return dset.Tables[0];
                }
                else
                {
                    return null;
                }
            }
        }
    }

    public class EnumHelper
    {
        public static string GetDescription(Enum en)
        {
            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return en.ToString();
        }
    }

   
}
