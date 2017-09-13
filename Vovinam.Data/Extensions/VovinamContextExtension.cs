using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Vovinam.Data.Extensions
{
    public static class DbContextExtension
    {
        public static void SetIsolationLevel(this DbContext context, IsolationLevel isolationLevel)
        {
            string sql;

            switch (isolationLevel)
            {
                case IsolationLevel.ReadUncommitted:
                    sql = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;";
                    break;
                case IsolationLevel.ReadCommitted:
                    sql = "SET TRANSACTION ISOLATION LEVEL READ COMMITTED;";
                    break;
                case IsolationLevel.Serializable:
                    sql = "SET TRANSACTION ISOLATION LEVEL Serializable;";
                    break;
                default:
                    throw new Exception("ISOLATION LEVEL is not defined in this method.");
            }

            //(context as IObjectContextAdapter).ObjectContext.ExecuteStoreCommand(sql, null);
            if (context.Database.Connection.State != ConnectionState.Open)
            {
                // Explicitly open the connection, this connection will close when context is disposed
                context.Database.Connection.Open();
            }

            context.Database.ExecuteSqlCommand(sql);
        }
    }

    public class DBUtility
    {

        /// <summary>
        /// hai.vu
        /// </summary>
        /// <param name="objectList"></param>
        /// <param name="paramNameList"></param>
        /// <returns></returns>
        ///trang.le:chi support input parameter 
        public static List<SqlParameter> BuildParameterList(object[] objectList, params string[] paramNameList)
        {
            var paramCollection = new List<SqlParameter>();
            for (var i = 0; i < paramNameList.Length; i++)
            {
                paramCollection.Add(new SqlParameter()
                {
                    ParameterName = paramNameList[i],
                    Value = objectList[i] ?? DBNull.Value
                });

            }

            return paramCollection;
        }


        /// <summary>
        /// hai.vu: 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paramLength"></param>
        /// <returns>{0}, {1}, {2}, {3}, {4}, {5}</returns>
        public static string BuilSqlCommand(string sql, int paramLength)
        {
            var paramDeclare = new StringBuilder();

            paramDeclare.Append(sql);

            // append parameter declare: {0}, {1}, {2}
            for (var i = 0; i < paramLength; i++)
            {
                paramDeclare.AppendFormat(" {{{0}}}", i);

                if (i < paramLength - 1)
                    paramDeclare.Append(",");
            }

            return paramDeclare.ToString();
        }


        /// <summary>
        /// hai.vu: 
        /// </summary>
        /// <param name="sql"></param>        
        /// <param name="parameters"> </param>
        /// <returns>@param1, @param2, @param3</returns>
        public static string BuilSqlCommand(string sql, List<SqlParameter> parameters, bool formatSQL = false)
        {
            var paramDeclare = new StringBuilder();

            if (formatSQL == false)
            {
                paramDeclare.Append(sql);
            }


            // append parameter declare: {0}, {1}, {2}
            for (var i = 0; i < parameters.Count; i++)
            {
                paramDeclare.AppendFormat(" {0}", parameters[i].ParameterName);
                if (parameters[i].Direction == System.Data.ParameterDirection.InputOutput)
                    paramDeclare.Append(" out ");

                if (i < parameters.Count - 1)
                    paramDeclare.Append(",");
            }

            if (formatSQL)
            {
                return string.Format(sql, paramDeclare.ToString());
            }

            return paramDeclare.ToString();
        }

        public static DataTable BuildObjectQuantityTable(long[] objectIDs)
        {
            return BuildObjectQuantityTable(objectIDs,
                new long[objectIDs.Length],
                null,
                null
            );
        }



        public static DataTable BuildObjectQuantityTable(long[] objectIDs, int[] quantity1, int[] quantity2, int[] quantity3)
        {
            return BuildObjectQuantityTable(objectIDs,
                quantity1 == null ? null : Array.ConvertAll(quantity1, x => (long)x),
                quantity2 == null ? null : Array.ConvertAll(quantity2, x => (long)x),
                quantity3 == null ? null : Array.ConvertAll(quantity3, x => (long)x)
            );
        }

        public static DataTable BuildObjectQuantityTable(long[] objectIDs, long[] quantity1, long[] quantity2, long[] quantity3)
        {
            var dt = new DataTable();
            dt.Columns.Add("ObjectID");
            dt.Columns.Add("Quantity1");
            dt.Columns.Add("Quantity2");
            dt.Columns.Add("Quantity3");

            for (int index = 0; index < objectIDs.Length; index++)
            {
                dt.Rows.Add(objectIDs[index],
                    quantity1 == null ? 0 : quantity1[index],
                    quantity2 == null ? 0 : quantity2[index],
                    quantity3 == null ? 0 : quantity3[index]
                    );
            }

            return dt;
        }

        public static DataTable BuildObjectLongQuantityTable(long[] objectIDs, long[] quantity1, long[] quantity2, long[] quantity3)
        {
            var dt = new DataTable();
            dt.Columns.Add("ObjectID");
            dt.Columns.Add("Quantity1");
            dt.Columns.Add("Quantity2");
            dt.Columns.Add("Quantity3");

            for (int index = 0; index < objectIDs.Length; index++)
            {
                dt.Rows.Add(objectIDs[index],
                    quantity1 == null ? 0 : quantity1[index],
                    quantity2 == null ? 0 : quantity2[index],
                    quantity3 == null ? 0 : quantity3[index]
                    );
            }

            return dt;
        }

        public static DataTable BuildObjectQuantityTable10(long[] objectIDs, params long?[][] arrays)
        {
            var dt = new DataTable();
            dt.Columns.Add("ObjectID");
            for (int i = 0; i < 10; i++)
            {
                dt.Columns.Add("Quantity" + (i + 1));
            }

            for (int index = 0; index < objectIDs.Length; index++)
            {
                var row = dt.NewRow();
                row["ObjectID"] = objectIDs[index];
                for (int i = 0; i < 10; i++)
                {
                    var value = i > arrays.Length - 1 ? 0 : arrays[i][index];
                    row["Quantity" + (i + 1)] = value;
                }

                dt.Rows.Add(row);
            }

            return dt;
        }

    }

    //public static class CTGroupContextExtension
    //{
    //    public static List<ChildUser> GetChildUser(this CTGroupBOProductEntities context, params object[] parameters)
    //    {
    //        var paramCollection = DBUtility.BuildParameterList(parameters, "@AdminID", "@IncludeCurrent");
    //        return context.Database.SqlQuery<ChildUser>(DBUtility.BuilSqlCommand("SELECT * from dbo.fnGetChildUser({0})", paramCollection, true), paramCollection.ToArray()).ToList();
    //    }
    //    public static List<DocumentPath> GetDocumentPath(this CTGroupBOProductEntities context)
    //    {
    //        return context.Database.SqlQuery<DocumentPath>("SELECT * FROM dbo.fnDocumentPathFull()").ToList();
    //    }

    //    public static List<Document> GetChildFolder(this CTGroupBOProductEntities context, params object[] parameters)
    //    {
    //        var paramCollection = DBUtility.BuildParameterList(parameters, "@DocumentId", "@IncludeCurrent");
    //        return context.Database.SqlQuery<Document>(DBUtility.BuilSqlCommand("SELECT * from dbo.fnGetChildFolder({0})", paramCollection, true), paramCollection.ToArray()).ToList();
    //    }

    //    public static CommandResult PaymentRequestProcess(this CTGroupBOProductEntities context,params object[] parameters)
    //    {
    //        var paramCollection = DBUtility.BuildParameterList(parameters, "@paymentrequestdetailid");
    //        return context.Database.SqlQuery<CommandResult>(DBUtility.BuilSqlCommand("exec spPaymentRequestProcess {0}", paramCollection, true), paramCollection.ToArray()).First();
    //    }
    //}
}
