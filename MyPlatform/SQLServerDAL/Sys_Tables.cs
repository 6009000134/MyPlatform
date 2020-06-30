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
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="DBName"></param>
        /// <returns></returns>
        public DataTable GetListByDBName(string DBName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Sys_Tables where 1=1 ");
            IDataBase db = DBHelperFactory.CreateDBInstance("DefaultConnection");
            if (!string.IsNullOrEmpty(DBName))
            {
                strSql.Append(" and DBName=@DBName");
                SqlParameter[] parameters = { new SqlParameter("@DBName", SqlDbType.VarChar, 30) };
                return db.Query(strSql.ToString(), parameters).Tables[0];
            }
            else
            {
                return db.Query(strSql.ToString()).Tables[0];
            }
        }
        /// <summary>
        /// 判断数据是否存在
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public bool Exists(string dbName)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 是否存在表,0-不存在，1-存在
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="dbName">数据库名</param>
        /// <param name="dbTypeCode">数据库类型</param>
        /// <returns></returns>
        public bool ExistsTable(string tableName, string dbName)
        {
            try
            {
                IDataBase db = DBHelperFactory.CreateDBInstance(dbName);
                string sql = "";
                sql = " SELECT  1 FROM dbo.SysObjects WHERE ID = object_id(@tableName) AND OBJECTPROPERTY(ID, 'IsTable') = 1 ";
                SqlParameter[] paras = { new SqlParameter("@tableName", SqlDbType.VarChar, 30) };
                paras[0].Value = tableName;
                return Convert.ToInt32(db.ExecuteScalar(sql, paras)) > 0 ? true : false;
            }
            catch (SqlException ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 统计表数据记录数
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="dbName">数据库连接名</param>
        /// <returns></returns>
        public int RecordCount(string tableName, string dbName)
        {
            try
            {
                IDataBase db = DBHelperFactory.CreateDBInstance(dbName);
                string sql = "";
                sql = "select count(1) from @tableName";
                SqlParameter[] pars = { new SqlParameter("@tableName", SqlDbType.VarChar, 100) };
                pars[0].Value = tableName;
                return Convert.ToInt32(db.ExecuteScalar(sql, pars));
            }
            catch (SqlException ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 创建表以及默认字段CreatedBy、CreatedDate、UpdatedDate、CreatedDate、Deleted
        /// </summary>
        /// <param name="model">表信息</param>
        /// <returns></returns>
        public bool Add(MyPlatform.Model.Sys_Tables model)
        {
            List<SqlCommandData> sqlCommmands = new List<SqlCommandData>();//事务参数
            StringBuilder sql = new StringBuilder();
            sql.Append("Create table {0} (");
            sql.Append(" ID int primary key identity(1,1),");
            sql.Append(" CreatedBy nvarchar(30),");
            sql.Append(" CreatedDate DATETIME DEFAULT(GETDATE()),");
            sql.Append(" UpdatedBy nvarchar(40) default(''),");
            sql.Append(" UpdatedDate datetime default(getdate()),");
            sql.Append(" Deleted int DEFAULT(0)");
            sql.Append(" )");
            SqlCommandData sc = new SqlCommandData();
            sc.CommandText = string.Format(sql.ToString(), model.TableName);
            sqlCommmands.Add(sc);
            SqlCommandData sc2 = SqlFactory.CreateInsertSqlByRef<MyPlatform.Model.Sys_Tables>(model);
            sqlCommmands.Add(sc2);
            IDataBase db = new SqlServerDataBase();
            return db.ExecuteTran(sqlCommmands);
        }
        /// <summary>
        /// 编辑表信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(MyPlatform.Model.Sys_Tables model)
        {
            //if (RecordCount(model.TableName,model.DBName)>0)
            //{
            //    return false;
            //}
            //当表中无数据时，允许修改表名、列名信息
            string sql = "";
            sql = "UPDATE dbo.Sys_Tables SET TableName_EN=@TableName_EN,TableName_CN=@TableName_CN,Remark=@Remark,UpdatedBy=@UpdatedBy,UpdatedDate=GETDATE() where ID=@ID";
            SqlParameter[] pars = { new SqlParameter("@TableName_EN",SqlDbType.VarChar,50)
            ,new SqlParameter("@TableName_EN",SqlDbType.VarChar,50)
            ,new SqlParameter("@TableName_CN",SqlDbType.VarChar,100)
            ,new SqlParameter("@Remark",SqlDbType.VarChar,100)
            ,new SqlParameter("@UpdatedBy",SqlDbType.VarChar,400)
            ,new SqlParameter("@ID",SqlDbType.Int)
            };
            pars[0].Value = model.TableName_EN;
            pars[1].Value = model.TableName_CN;
            pars[2].Value = model.Remark;
            pars[3].Value = model.UpdatedBy;
            pars[4].Value = model.ID;
            IDataBase db = new SqlServerDataBase();
            return db.ExecuteNonQuery(sql) > 0 ? true : false;
        }

        #endregion
        #region 辅助方法

        #endregion

    }
}

