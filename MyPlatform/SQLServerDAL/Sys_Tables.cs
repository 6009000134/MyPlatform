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
            IDataBase db = DBHelperFactory.CreateDBInstance("Default");
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
        public bool ExistsTable(string tableName, string dbCon)
        {
            try
            {
                IDataBase db = DBHelperFactory.CreateDBInstance(dbCon);
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
        public bool Add(MyPlatform.Model.Sys_Tables model)
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
            SqlCommandData sc2 = SqlFactory.CreateInsertSqlByRef<MyPlatform.Model.Sys_Tables>(model);
            sqlCommands.Add(sc2);
            IDataBase db = DBHelperFactory.CreateDBInstance("Default");
            if (db.ExecuteTran(sqlCommands))
            {
                SqlCommandData scID = new SqlCommandData();
                scID.CommandText = "select SCOPE_IDENTITY()";
                sqlCommands.Add(scID);
                int id = Convert.ToInt32(db.ExecuteScalar("select IDENT_CURRENT('Sys_Tables')"));
                sqlCommands = new List<SqlCommandData>();
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
            }

            return db.ExecuteTran(sqlCommands);
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
        /// 刪除表以及相关信息
        /// </summary>
        /// <param name="tableID"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public bool Delete(int tableID, string dbName)
        {
            //无数据的表可以删除            
            string sql = "select 1 from @tableName";

            return true;
        }



        #endregion
        #region 辅助方法

        #endregion

    }
}

