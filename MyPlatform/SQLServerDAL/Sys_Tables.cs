using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using MyPlatform.DBUtility;
using MyPlatform.IDAL;
namespace MyPlatform.SQLServerDAL
{
    //Sys_Tables
    public partial class Sys_Tables : ISys_Tables
    {

        public Sys_Tables()
        {

        }
        #region Extend
        public string Test()
        {
            return "test322";
        }
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="DBName"></param>
        /// <returns></returns>
        public DataTable GetListByDBName(string DBName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Sys_Tables where 1=1 ");
            strSql.Append(" and DBName=@DBName");
            SqlParameter[] parameters = { new SqlParameter("@DBName", SqlDbType.VarChar, 30) };
            IDataBase db = new DbHelperSQL();
            return db.Query(strSql.ToString(), parameters).Tables[0];
        }
        #endregion      


    }
}

