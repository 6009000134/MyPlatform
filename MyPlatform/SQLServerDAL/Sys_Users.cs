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
	public partial class Sys_Users: ISys_Users
	{
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool CreateUser(MyPlatform.Model.Sys_Users model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("  ");
            strSql.Append("  ");
            strSql.Append("  ");
            strSql.Append("  ");
            strSql.Append("  ");
            return true;
        }
        public MyPlatform.Model.Sys_Users GetModelByAccount(string account)
        {
            MyPlatform.Model.Sys_Users user = new Model.Sys_Users();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from Sys_Users where deleted=0 and Account=@Account");
            SqlParameter[] parameters =  {new SqlParameter("@Account",SqlDbType.VarChar,30) };
            parameters[0].Value = account;
            DataTable dt=DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0];
            //DbHelperSQL.Query(strSql, parameters);
            user = ModelConverter<MyPlatform.Model.Sys_Users>.ConvertToModelEntity(dt);
            return user;            
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
            SqlParameter[] parameters = { new SqlParameter("@Account", SqlDbType.VarChar, 30),new SqlParameter("@Password",SqlDbType.VarChar,30) };
            parameters[0].Value = model.Account;
            parameters[1].Value = model.Password;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
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
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
            
        }
   		     
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Sys_Users");
			strSql.Append(" where ");
			                                       strSql.Append(" ID = @ID  ");
                            			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(MyPlatform.Model.Sys_Users model)
		{
			StringBuilder strSql=new StringBuilder();
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
			   
			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);			
			if (obj == null)
			{
				return 0;
			}
			else
			{
				                    
            	return Convert.ToInt32(obj);
                                                                  
			}			   
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(MyPlatform.Model.Sys_Users model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Sys_Users set ");
			                                                
            strSql.Append(" CreatedBy = @CreatedBy , ");                                    
            strSql.Append(" CreatedDate = @CreatedDate , ");                                    
            strSql.Append(" UpdatedBy = @UpdatedBy , ");                                    
            strSql.Append(" UpdatedDate = @UpdatedDate , ");                                    
            strSql.Append(" Deleted = @Deleted , ");                                    
            strSql.Append(" Account = @Account , ");                                    
            strSql.Append(" Password = @Password , ");                                    
            strSql.Append(" UserName = @UserName  ");            			
			strSql.Append(" where ID=@ID ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4) ,            
                        new SqlParameter("@CreatedBy", SqlDbType.NVarChar,20) ,            
                        new SqlParameter("@CreatedDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@UpdatedBy", SqlDbType.NVarChar,20) ,            
                        new SqlParameter("@UpdatedDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@Deleted", SqlDbType.Int,4) ,            
                        new SqlParameter("@Account", SqlDbType.VarChar,18) ,            
                        new SqlParameter("@Password", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@UserName", SqlDbType.NVarChar,20)             
              
            };
						            
            parameters[0].Value = model.ID;                        
            parameters[1].Value = model.CreatedBy;                        
            parameters[2].Value = model.CreatedDate;                        
            parameters[3].Value = model.UpdatedBy;                        
            parameters[4].Value = model.UpdatedDate;                        
            parameters[5].Value = model.Deleted;                        
            parameters[6].Value = model.Account;                        
            parameters[7].Value = model.Password;                        
            parameters[8].Value = model.UserName;                        
            int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Sys_Users ");
			strSql.Append(" where ID=@ID");
						SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;


			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
				/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Sys_Users ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
				
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MyPlatform.Model.Sys_Users GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate, Deleted, Account, Password, UserName  ");			
			strSql.Append("  from Sys_Users ");
			strSql.Append(" where ID=@ID");
						SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			
			MyPlatform.Model.Sys_Users model=new MyPlatform.Model.Sys_Users();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
																																				model.CreatedBy= ds.Tables[0].Rows[0]["CreatedBy"].ToString();
																												if(ds.Tables[0].Rows[0]["CreatedDate"].ToString()!="")
				{
					model.CreatedDate=DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
				}
																																				model.UpdatedBy= ds.Tables[0].Rows[0]["UpdatedBy"].ToString();
																												if(ds.Tables[0].Rows[0]["UpdatedDate"].ToString()!="")
				{
					model.UpdatedDate=DateTime.Parse(ds.Tables[0].Rows[0]["UpdatedDate"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["Deleted"].ToString()!="")
				{
					model.Deleted=int.Parse(ds.Tables[0].Rows[0]["Deleted"].ToString());
				}
																																				model.Account= ds.Tables[0].Rows[0]["Account"].ToString();
																																model.Password= ds.Tables[0].Rows[0]["Password"].ToString();
																																model.UserName= ds.Tables[0].Rows[0]["UserName"].ToString();
																										
				return model;
			}
			else
			{
				return null;
			}
		}
		
		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM Sys_Users ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}
		
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM Sys_Users ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

   
	}
}

