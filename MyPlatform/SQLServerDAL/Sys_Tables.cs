﻿using System;
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
            DbHelperSQL db = new DbHelperSQL();
            if (string.IsNullOrEmpty(DBName))
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
        /// 是否存在表,0-不存在，1-存在
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="dbName">数据库名</param>
        /// <param name="dbType">数据库类型</param>
        /// <returns></returns>
        public bool Exists(string tableName, string dbName, string dbType)
        {
            IDataBase db = CreateDBInstance(dbName, dbType);
            string sql = "";
            if (dbName.ToLower() == "sqlserver")
            {
                sql = " SELECT  1 FROM dbo.SysObjects WHERE ID = object_id(N'@tableName') AND OBJECTPROPERTY(ID, 'IsTable') = 1 ";
                SqlParameter[] paras = { new SqlParameter("@tableName", SqlDbType.VarChar, 30) };
                paras[0].Value = tableName;
                return Convert.ToInt32(db.ExecuteScalar(sql, paras)) > 0 ? true : false;
            }
            else
            {
                throw new NotImplementedException();
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
            sql.Append("Create table Sys_Users2 (");
            sql.Append(" ID int primary key identity(1,1),");
            sql.Append(" CreatedBy nvarchar(20),");
            sql.Append(" CreatedDate DATETIME DEFAULT(GETDATE()),");
            sql.Append(" UpdatedBy nvarchar(20) default(''),");
            sql.Append(" UpdatedDate datetime default(getdate()),");
            sql.Append(" Deleted int DEFAULT(0)");
            sql.Append(" )");
            SqlParameter[] paras = { new SqlParameter("@tableName", model.TableName) };
            SqlCommandData sc = new SqlCommandData();
            sc.CommandText = sql.ToString();
            //sc.Paras = paras;
            sqlCommmands.Add(sc);
            SqlCommandData sc2 = SqlFactory.CreateInsertSqlByRef<MyPlatform.Model.Sys_Tables>(model);
            sqlCommmands.Add(sc2);
            IDataBase db = new SqlServerDataBase();
            return db.ExecuteTran(sqlCommmands);
        }

        #endregion
        #region 辅助方法
        /// <summary>
        /// 创建数据库实例
        /// </summary>
        /// <param name="dbName">数据库连接名</param>
        /// <param name="dbType">数据库类型</param>
        /// <returns></returns>
        public IDataBase CreateDBInstance(string dbName, string dbType)
        {
            if (dbName.ToLower() == "sqlserver")
            {
                return new SqlServerDataBase(dbName);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        #endregion

    }
}

