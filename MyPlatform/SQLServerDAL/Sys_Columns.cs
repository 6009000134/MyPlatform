using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using MyPlatform.DBUtility;
using MyPlatform.IDAL;
using MyPlatform.Model;

namespace MyPlatform.SQLServerDAL
{
    //Sys_Columns
    public partial class Sys_Columns : ISys_Columns
    {
        /// <summary>
        /// 获取列集合
        /// </summary>
        /// <param name="tableID">表ID</param>
        /// <returns></returns>
        public DataSet GetList(string tableID)
        {
            string sql = "SELECT * FROM dbo.Sys_Columns where tableID=@tableID";
            IDataParameter[] pars = { new SqlParameter("tableID", SqlDbType.Int) };
            IDataBase db = DBHelperFactory.CreateDBInstance("DefaultConnection");
            return db.Query(sql, pars);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DBName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(string DBName, Model.Sys_Columns model)
        {
            IDataBase db = DBHelperFactory.CreateDBInstance(DBName);
            if (db.DBType.ToString() == "0")//SqlServer
            {                
                StringBuilder sb = new StringBuilder();
                sb.Append("alter table "+model.TableName+" add column "+model.ColumnName+" "+model.ColumnType+model.Size);                
                if (model.IsNullable)
                {
                    sb.Append("  null");
                }
                else
                {
                    sb.Append(" not null ");
                }
                //TODO:增加默认值
            }
            else if(db.DBType.ToString()=="1")//MySql
            {

            }
            else if (db.DBType.ToString() == "2")//Oracle
            {

            }
            else
            {
                throw new Exception(DBName + "数据库类型未知");
            }
            return true;
        }
    }
}

