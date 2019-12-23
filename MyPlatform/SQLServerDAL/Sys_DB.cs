using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using MyPlatform.DBUtility;
using MyPlatform.IDAL;
namespace MyPlatform.SQLServerDAL  
{
	 	//Sys_DB
		public partial class Sys_DB: ISys_DB
	{
   		     
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Sys_DB");
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
		public int Add(MyPlatform.Model.Sys_DB model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Sys_DB(");			
            strSql.Append("CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,Deleted,DBName,DBType,DBTypeCode");
			strSql.Append(") values (");
            strSql.Append("@CreatedBy,@CreatedDate,@UpdatedBy,@UpdatedDate,@Deleted,@DBName,@DBType,@DBTypeCode");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@CreatedBy", SqlDbType.NVarChar,20) ,            
                        new SqlParameter("@CreatedDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@UpdatedBy", SqlDbType.NVarChar,20) ,            
                        new SqlParameter("@UpdatedDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@Deleted", SqlDbType.Int,4) ,            
                        new SqlParameter("@DBName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@DBType", SqlDbType.Int,4) ,            
                        new SqlParameter("@DBTypeCode", SqlDbType.VarChar,20)             
              
            };
			            
            parameters[0].Value = model.CreatedBy;                        
            parameters[1].Value = model.CreatedDate;                        
            parameters[2].Value = model.UpdatedBy;                        
            parameters[3].Value = model.UpdatedDate;                        
            parameters[4].Value = model.Deleted;                        
            parameters[5].Value = model.DBName;                        
            parameters[6].Value = model.DBType;                        
            parameters[7].Value = model.DBTypeCode;                        
			   
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
		public bool Update(MyPlatform.Model.Sys_DB model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Sys_DB set ");
			                                                
            strSql.Append(" CreatedBy = @CreatedBy , ");                                    
            strSql.Append(" CreatedDate = @CreatedDate , ");                                    
            strSql.Append(" UpdatedBy = @UpdatedBy , ");                                    
            strSql.Append(" UpdatedDate = @UpdatedDate , ");                                    
            strSql.Append(" Deleted = @Deleted , ");                                    
            strSql.Append(" DBName = @DBName , ");                                    
            strSql.Append(" DBType = @DBType , ");                                    
            strSql.Append(" DBTypeCode = @DBTypeCode  ");            			
			strSql.Append(" where ID=@ID ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4) ,            
                        new SqlParameter("@CreatedBy", SqlDbType.NVarChar,20) ,            
                        new SqlParameter("@CreatedDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@UpdatedBy", SqlDbType.NVarChar,20) ,            
                        new SqlParameter("@UpdatedDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@Deleted", SqlDbType.Int,4) ,            
                        new SqlParameter("@DBName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@DBType", SqlDbType.Int,4) ,            
                        new SqlParameter("@DBTypeCode", SqlDbType.VarChar,20)             
              
            };
						            
            parameters[0].Value = model.ID;                        
            parameters[1].Value = model.CreatedBy;                        
            parameters[2].Value = model.CreatedDate;                        
            parameters[3].Value = model.UpdatedBy;                        
            parameters[4].Value = model.UpdatedDate;                        
            parameters[5].Value = model.Deleted;                        
            parameters[6].Value = model.DBName;                        
            parameters[7].Value = model.DBType;                        
            parameters[8].Value = model.DBTypeCode;                        
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
			strSql.Append("delete from Sys_DB ");
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
			strSql.Append("delete from Sys_DB ");
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
		public MyPlatform.Model.Sys_DB GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate, Deleted, DBName, DBType, DBTypeCode  ");			
			strSql.Append("  from Sys_DB ");
			strSql.Append(" where ID=@ID");
						SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			
			MyPlatform.Model.Sys_DB model=new MyPlatform.Model.Sys_DB();
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
																																				model.DBName= ds.Tables[0].Rows[0]["DBName"].ToString();
																												if(ds.Tables[0].Rows[0]["DBType"].ToString()!="")
				{
					model.DBType=int.Parse(ds.Tables[0].Rows[0]["DBType"].ToString());
				}
																																				model.DBTypeCode= ds.Tables[0].Rows[0]["DBTypeCode"].ToString();
																										
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
			strSql.Append(" FROM Sys_DB ");
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
			strSql.Append(" FROM Sys_DB ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

   
	}
}

