﻿using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using MyPlatform.DBUtility;
using MyPlatform.IDAL;
using MyPlatform.Model;
using System.Data.Common;
using MyPlatform.Common.Cache;

namespace MyPlatform.SQLServerDAL
{
    //Sys_Tables
    public partial class Sys_Tables : ISys_Tables
    {
        string defaultCon = "Default";
        public Sys_Tables()
        {

        }
        #region Extend
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="DBName"></param>
        /// <returns></returns>
        public DataTable GetListByDBName(Dictionary<string, object> dicCondition)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Sys_Tables where 1=1 ");
            IDataBase db = DBHelperFactory.CreateDBInstance(defaultCon);
            if (!string.IsNullOrEmpty("DBCon"))
            {
                strSql.Append(" and DBCon=@DBCon");
                SqlParameter[] parameters = { new SqlParameter("@DBCon", SqlDbType.VarChar, 30) };
                parameters[0].Value = "DBCon";
                return db.Query(strSql.ToString(), parameters).Tables[0];
            }
            else
            {
                return db.Query(strSql.ToString()).Tables[0];
            }
        }
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="DBName"></param>
        /// <returns></returns>
        public DataTable GetListByDBName(string DBCon)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Sys_Tables where 1=1 ");
            IDataBase db = DBHelperFactory.CreateDBInstance(defaultCon);
            if (!string.IsNullOrEmpty(DBCon))
            {
                strSql.Append(" and DBCon=@DBCon");
                SqlParameter[] parameters = { new SqlParameter("@DBCon", SqlDbType.VarChar, 30) };
                parameters[0].Value = DBCon;
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
        public ReturnData ExistsTable(string tableName, string dbCon)
        {
            ReturnData result = new ReturnData();
            try
            {
                IDataBase db = DBHelperFactory.CreateDBInstance(dbCon);
                Dictionary<string, string> dic = DBInfoCache.GetDBInfo(dbCon);
                if (dic == null)
                {
                    result.SetErrorMsg("找不到对应的数据库配置信息");
                }
                else
                {
                    string sql = "";
                    IDataParameter[] paras = new IDataParameter[1];
                    switch (dic["DBTypeCode"].ToLower())
                    {
                        case "sqlserver":
                            sql = " SELECT  1 FROM dbo.SysObjects WHERE ID = object_id(@tableName) AND OBJECTPROPERTY(ID, 'IsTable') = 1 ";
                            paras = new IDataParameter[1] { new SqlParameter("@tableName", SqlDbType.VarChar, 30) };
                            paras[0].Value = tableName;
                            break;
                        case "oracle"://TODO:集成oracle
                            result.SetErrorMsg("oracle数据库未实现！");
                            return result;
                            break;
                        case "mysql"://TODO:集成mysql
                            result.SetErrorMsg("oracle数据库未实现！");
                            return result;
                            break;
                        default:
                            break;
                    }
                    result.S = Convert.ToInt32(db.ExecuteScalar(sql, paras)) > 0;
                    if (!result.S)
                    {
                        result.M = "数据库已经存在表名为：" + tableName + "的表";
                    }
                    result.S = true;
                }

            }
            catch (SqlException ex)
            {
                result.SetErrorMsg(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 统计表数据记录数
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="dbCon">数据库连接名</param>
        /// <returns></returns>
        public int RecordCount(string tableName, string dbCon)
        {
            try
            {
                IDataBase db = DBHelperFactory.CreateDBInstance(dbCon);
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
        public ReturnData Add(MyPlatform.Model.Sys_Tables model)
        {
            ReturnData result = new ReturnData();
            try
            {
                List<SqlCommandData> sqlCommands = new List<SqlCommandData>();//事务参数
                StringBuilder sql = new StringBuilder();
                sql.Append("Create table {0} (");
                sql.Append(" ID int primary key identity(1,1),");
                sql.Append(" CreatedBy nvarchar(30) not null,");
                sql.Append(" CreatedDate DATETIME DEFAULT(GETDATE()) not null,");
                sql.Append(" UpdatedBy nvarchar(30) default(''),");
                sql.Append(" UpdatedDate datetime default(getdate()),");
                sql.Append(" Deleted bit DEFAULT(0)");
                sql.Append(" )");
                SqlCommandData sc = new SqlCommandData();
                sc.CommandText = string.Format(sql.ToString(), model.TableName);
                sqlCommands.Add(sc);
                IDataBase dbCreate = DBHelperFactory.CreateDBInstance(model.DBCon);
                if (dbCreate.ExecuteTran(sqlCommands))
                {

                    if (AddTableInfo(model))//记录表信息和列信息
                    {
                        result.S = true;
                    }
                    else
                    {
                        result.S = false;
                        result.SetErrorMsg("保存表和列信息到系统表失败");
                    }
                }
                else
                {
                    result.S = false;
                    result.SetErrorMsg("表格创建失败！");
                }
            }
            catch (Exception ex)
            {
                result.S = false;
                result.SetErrorMsg("保存失败：" + ex.Message);
            }
            return result;
        }
        /// <summary>
        /// 将表和信息记录到sys表
        /// </summary>
        /// <param name="model"></param>
        public bool AddTableInfo(MyPlatform.Model.Sys_Tables model)
        {
            List<SqlCommandData> sqlCommands = new List<SqlCommandData>();//事务参数
            IDataBase dbDefault = DBHelperFactory.CreateDBInstance(defaultCon);
            SqlCommandData sc2 = SqlFactory.CreateInsertSqlByRef<MyPlatform.Model.Sys_Tables>(model);
            sqlCommands.Add(sc2);
            //dbDefault.ExecuteTran(sqlCommands);
            //sqlCommands = new List<SqlCommandData>();
            //SqlCommandData scID = new SqlCommandData();
            //scID.CommandText = "select SCOPE_IDENTITY()";
            //sqlCommands.Add(scID);
            //int id = Convert.ToInt32(dbDefault.ExecuteScalar("select IDENT_CURRENT('Sys_Tables')"));
            SqlCommandData sc3 = new SqlCommandData();
            sc3.CommandText = @"INSERT INTO [dbo].[Sys_Columns]
           ([CreatedBy]           ,[CreatedDate]           ,[UpdatedBy]           ,[UpdatedDate]
           ,[Deleted]           ,[TableID]           ,[TableName]           ,[ColumnName]           ,[ColumnName_EN]
           ,[ColumnName_CN]           ,[ColumnType]           ,[Size]           ,[IsNullable]           ,[DefaultValue]
           ,[Remark])     VALUES ('" + model.CreatedBy + "','" + model.CreatedDate + "','" + model.UpdatedBy + "','" + model.UpdatedDate + "','0',(SELECT IDENT_CURRENT('Sys_Tables')),'"
           + model.TableName + "','CreatedBy','CreatedBy','创建人','NVarchar',30,0,'','')";
            sqlCommands.Add(sc3);
            SqlCommandData sc4 = new SqlCommandData();
            sc4.CommandText = @"INSERT INTO [dbo].[Sys_Columns]
           ([CreatedBy]           ,[CreatedDate]           ,[UpdatedBy]           ,[UpdatedDate]
           ,[Deleted]           ,[TableID]           ,[TableName]           ,[ColumnName]           ,[ColumnName_EN]
           ,[ColumnName_CN]           ,[ColumnType]           ,[Size]           ,[IsNullable]           ,[DefaultValue]
           ,[Remark])     VALUES ('" + model.CreatedBy + "','" + model.CreatedDate + "','" + model.UpdatedBy + "','" + model.UpdatedDate + "','0',(SELECT IDENT_CURRENT('Sys_Tables')),'"
           + model.TableName + "','CreatedDate','CreatedDate','创建时间','DateTime',0,0,'','')";
            sqlCommands.Add(sc4);
            SqlCommandData sc5 = new SqlCommandData();
            sc5.CommandText = @"INSERT INTO [dbo].[Sys_Columns]
           ([CreatedBy]           ,[CreatedDate]           ,[UpdatedBy]           ,[UpdatedDate]
           ,[Deleted]           ,[TableID]           ,[TableName]           ,[ColumnName]           ,[ColumnName_EN]
           ,[ColumnName_CN]           ,[ColumnType]           ,[Size]           ,[IsNullable]           ,[DefaultValue]
           ,[Remark])     VALUES ('" + model.CreatedBy + "','" + model.CreatedDate + "','" + model.UpdatedBy + "','" + model.UpdatedDate + "','0',(SELECT IDENT_CURRENT('Sys_Tables')),'"
           + model.TableName + "','UpdatedBy','UpdatedBy','更新人','NVarchar',30,1,'','')";
            sqlCommands.Add(sc5);
            SqlCommandData sc6 = new SqlCommandData();
            sc6.CommandText = @"INSERT INTO [dbo].[Sys_Columns]
           ([CreatedBy]           ,[CreatedDate]           ,[UpdatedBy]           ,[UpdatedDate]
           ,[Deleted]           ,[TableID]           ,[TableName]           ,[ColumnName]           ,[ColumnName_EN]
           ,[ColumnName_CN]           ,[ColumnType]           ,[Size]           ,[IsNullable]           ,[DefaultValue]
           ,[Remark])     VALUES ('" + model.CreatedBy + "','" + model.CreatedDate + "','" + model.UpdatedBy + "','" + model.UpdatedDate + "','0',(SELECT IDENT_CURRENT('Sys_Tables')),'"
           + model.TableName + "','UpdatedDate','UpdatedDate','更新时间','DateTime',0,1,'','')";
            sqlCommands.Add(sc6);
            SqlCommandData sc7 = new SqlCommandData();
            sc7.CommandText = @"INSERT INTO [dbo].[Sys_Columns]
           ([CreatedBy]           ,[CreatedDate]           ,[UpdatedBy]           ,[UpdatedDate]
           ,[Deleted]           ,[TableID]           ,[TableName]           ,[ColumnName]           ,[ColumnName_EN]
           ,[ColumnName_CN]           ,[ColumnType]           ,[Size]           ,[IsNullable]           ,[DefaultValue]
           ,[Remark])     VALUES ('" + model.CreatedBy + "','" + model.CreatedDate + "','" + model.UpdatedBy + "','" + model.UpdatedDate + "','0',(SELECT IDENT_CURRENT('Sys_Tables')),'"
           + model.TableName + "','Deleted','Deleted','是否已删除','Bit',0,1,0,'')";
            sqlCommands.Add(sc7);
            return dbDefault.ExecuteTran(sqlCommands);
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
        //TODO:分页
        public DataTable GetDetailListByTID(int tableID, Pagination page)
        {
            string sql = "select *from sys_columns a where a.tableID=@tableID";
            SqlParameter[] pars = { new SqlParameter("@tableID",SqlDbType.Int)
            };
            pars[0].Value = tableID;
            IDataBase db = new SqlServerDataBase();
            return db.Query(sql, pars).Tables[0];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableID"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ReturnData GetDetail(int tableID, Pagination page)
        {
            ReturnData result = new ReturnData();
            try
            {
                DataSet ds = new DataSet();
                int startIndex = DALUtils.CalStartIndex(page.PageSize, page.PageIndex);
                int endIndex = DALUtils.CalEndIndex(page.PageSize, page.PageIndex);
                string sql = "select * from sys_tables where ID=@ID;select * from (select ROW_NUMBER() OVER(ORDER BY orderNO)RN,* from sys_columns where tableID=@ID)t where t.rn>" + startIndex.ToString() + " and t.rn<" + endIndex.ToString() + ";select count(1)TotalCount from sys_columns where tableID=@ID";
                IDataBase db = DBHelperFactory.CreateDBInstance(defaultCon);
                SqlParameter[] pars = { new SqlParameter("@ID", tableID) };
                ds = db.Query(sql, pars);
                result.D = ds;
                result.S = true;
            }
            catch (Exception ex)
            {
                result.SetErrorMsg(ex.Message);
            }

            return result;
        }
        /// <summary>
        /// 刪除表以及相关信息
        /// </summary>
        /// <param name="tableID"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public bool Delete(int tableID)
        {
            IDataBase db = new SqlServerDataBase("Default");
            //无数据的表可以删除            
            string sql = "select ID,DBCon,TableName,DBTypeCode from Sys_tables where id=@tableID";
            SqlParameter[] pars = { new SqlParameter("@tableID", tableID) };
            DataTable dt = db.Query(sql, pars).Tables[0];
            if (dt.Rows.Count > 0)
            {
                IDataBase db2 = DBHelperFactory.CreateDBInstance(dt.Rows[0]["DBCon"].ToString());
                string sql2 = "select count(1) from " + dt.Rows[0]["TableName"].ToString();
                IDataParameter[] pars2 = new IDataParameter[1];
                //TODO:增加多类型数据库操作
                switch (dt.Rows[0]["DBTypeCode"].ToString().ToLower())
                {
                    case "sqlserver":
                        pars2 = new IDataParameter[1] { new SqlParameter("@tableName", dt.Rows[0]["TableName"]) };
                        break;
                    default:
                        break;
                }
                if (Convert.ToInt32(db2.ExecuteScalar(sql2)) == 0)
                {
                    string sqlDelete = "";
                    switch (dt.Rows[0]["DBTypeCode"].ToString().ToLower())
                    {
                        case "sqlserver":
                            sqlDelete = "drop table " + dt.Rows[0]["TableName"].ToString();
                            break;
                        default:
                            break;
                    }
                    db2.ExecuteNonQuery(sqlDelete);
                    IDataParameter[] pars4 = { new SqlParameter("@tableID", tableID) };
                    List<SqlCommandData> li = new List<SqlCommandData>();
                    SqlCommandData scd = new SqlCommandData();
                    scd.CommandText = "update sys_tables set deleted=1 where id=@tableID";
                    scd.Paras = new SqlParameter[1] { new SqlParameter("@tableID", tableID) };
                    li.Add(scd);
                    SqlCommandData scd2 = new SqlCommandData();
                    scd2.CommandText = "update sys_columns set deleted=1 where tableid=@tableID";
                    scd2.Paras = new SqlParameter[1] { new SqlParameter("@tableID", tableID) };
                    li.Add(scd2);
                    return db.ExecuteTran(li);
                }
                else
                {
                    return false;
                }
            }

            return true;
        }



        #endregion
        #region 辅助方法

        #endregion

    }
}

