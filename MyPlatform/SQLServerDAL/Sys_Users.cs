using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using MyPlatform.DBUtility;
using MyPlatform.IDAL;
using MyPlatform.Common;

namespace MyPlatform.SQLServerDAL
{
    //Sys_Users
    public partial class Sys_Users : ISys_Users
    {
        string dbCon = "Default";
        public MyPlatform.Model.Sys_Users GetModelByAccount(string account)
        {
            try
            {
                IDataBase db = new SqlServerDataBase(dbCon);
                MyPlatform.Model.Sys_Users user = new Model.Sys_Users();
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" select * from Sys_Users where deleted=0 and Account=@Account");
                SqlParameter[] parameters = { new SqlParameter("@Account", SqlDbType.VarChar, 30) };
                parameters[0].Value = account;
                DataTable dt = db.Query(strSql.ToString(), parameters).Tables[0];
                //DbHelperSQL.Query(strSql, parameters);
                user = ModelConverter<MyPlatform.Model.Sys_Users>.ConvertToModelEntity(dt);
                return user;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 登录验证账号密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Exists(MyPlatform.Model.Sys_Users model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from sys_users where deleted=0 and Account=@Account and password=@Password");
            SqlParameter[] parameters = { new SqlParameter("@Account", SqlDbType.VarChar, 30), new SqlParameter("@Password", SqlDbType.VarChar, 30) };
            parameters[0].Value = model.Account;
            parameters[1].Value = model.Password;
            IDataBase db = new SqlServerDataBase(dbCon);            
            return Convert.ToInt32(db.ExecuteScalar(strSql.ToString(), parameters)) == 0 ? false : true;
        }
        /// <summary>
        /// 检测账号是否存在
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public bool Exists(string Account)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Sys_Users");
            strSql.Append(" where ");
            strSql.Append(" deleted=0 and Account=@Account ");
            SqlParameter[] parameters = { new SqlParameter("@Account", SqlDbType.VarChar, 30) };
            parameters[0].Value = Account;
            IDataBase db = new SqlServerDataBase(dbCon);            
            return Convert.ToInt32(db.ExecuteScalar(strSql.ToString(), parameters)) == 0 ? false : true;

        }

        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Sys_Users");
            strSql.Append(" where ");
            strSql.Append(" ID = @ID  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)
            };
            parameters[0].Value = ID;
            IDataBase db = new SqlServerDataBase(dbCon);            
            return Convert.ToInt32(db.ExecuteScalar(strSql.ToString(), parameters)) == 0 ? false : true;
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(MyPlatform.Model.Sys_Users model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Sys_Users(");
            strSql.Append("CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,Deleted,Account,Password,UserName");
            strSql.Append(") values (");
            strSql.Append("@CreatedBy,@CreatedDate,@UpdatedBy,@UpdatedDate,@Deleted,@Account,@Password,@UserName");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@CreatedBy", SqlDbType.NVarChar,20) ,
                        new SqlParameter("@CreatedDate", SqlDbType.DateTime) ,
                        new SqlParameter("@UpdatedBy", SqlDbType.NVarChar,20) ,
                        new SqlParameter("@UpdatedDate", SqlDbType.DateTime) ,
                        new SqlParameter("@Deleted", SqlDbType.Int,4) ,
                        new SqlParameter("@Account", SqlDbType.VarChar,18) ,
                        new SqlParameter("@Password", SqlDbType.VarChar,20) ,
                        new SqlParameter("@UserName", SqlDbType.NVarChar,20)

            };

            parameters[0].Value = model.CreatedBy;
            parameters[1].Value = model.CreatedDate;
            parameters[2].Value = model.UpdatedBy;
            parameters[3].Value = model.UpdatedDate;
            parameters[4].Value = model.Deleted;
            parameters[5].Value = model.Account;
            parameters[6].Value = model.Password;
            parameters[7].Value = model.UserName;

            IDataBase db = new SqlServerDataBase(dbCon);
            return db.ExecuteNonQuery(strSql.ToString(), parameters);
        }
    }
}

