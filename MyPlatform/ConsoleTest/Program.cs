using MyPlatform.Common;
using MyPlatform.DBUtility;
using MyPlatform.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Add();
            //Sys_Tables dal = new Sys_Tables();
            //MyPlatform.Model.Sys_Tables model = new MyPlatform.Model.Sys_Tables();
            //model.CreatedBy = "1";
            //model.DBName = "aa";
            //string sql = CreateInsertSqlByRef<MyPlatform.Model.Sys_Tables>(model);
            //DataSet ds = ExecProcedure();
            //string s=JSONUtil.GetJson(ds.Tables[0]);
            //DataRow dr = ds.Tables[0].Rows[0];
            //string ss = JSONUtil.GetJson(dr);
            //string sss = JSONUtil.GetJson(dr.ItemArray);

        }
        #region 测试DAL
        private static void Add()
        {
            Sys_Tables dal = new Sys_Tables();
            MyPlatform.Model.Sys_Tables model = new MyPlatform.Model.Sys_Tables();
            model.CreatedBy = "1";
            model.DBName = "aa";
            dal.Add(model);
        }
        #endregion

        #region 辅助方法
        private static string CreateInsertSqlByRef<T>(T t)
        {
            PropertyInfo[] pis = t.GetType().GetProperties();
            StringBuilder sql = new StringBuilder();
            sql.Append("Insert Into " + t.GetType().Name);
            sql.Append(" (");
            foreach (PropertyInfo pi in pis)
            {
                sql.Append(pi.Name + ",");
            }
            sql.Remove(sql.Length - 1, 1);
            sql.Append(" )");
            sql.Append(" values (");
            foreach (PropertyInfo pi in pis)
            {
                sql.Append(" @" + pi.Name + ",");
            }
            sql.Remove(sql.Length - 1, 1);
            sql.Append(" )");
            SqlParameter[] paras = new SqlParameter[pis.Length];
            return sql.ToString();
        }
        #endregion
        #region DBUtility
        private void RunTran()
        {
            IDataBase db = new SqlServerDataBase();
            SqlParameter[] par = { new SqlParameter("ID", SqlDbType.Int, 32), new SqlParameter("Count", SqlDbType.Int, 32) };
            par[0].Value = 3;
            par[1].Direction = ParameterDirection.Output;
            DataSet ds = db.ExecProcedure("sp_Test", par);
            int count = Convert.ToInt32(par[1].Value); ;
            List<string> li = new List<string>();
            li.Add(@"select * from sys_users");
            li.Add(@"select * from sys_users");
            db.ExecuteTran(li);
        }
        private static DataSet ExecProcedure()
        {
            IDataBase db = new SqlServerDataBase();
            SqlParameter[] par = { new SqlParameter("ID", SqlDbType.Int, 32), new SqlParameter("Count", SqlDbType.Int, 32) };
            par[0].Value = 3;
            par[1].Direction = ParameterDirection.Output;
            DataSet ds = db.ExecProcedure("sp_Test", par);
            return ds;
        }
        #endregion

    }
}
