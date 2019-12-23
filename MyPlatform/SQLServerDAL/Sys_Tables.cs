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
		public partial class Sys_Tables: ISys_Tables
	{
   		     
		public bool Exists()
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Sys_Tables");
			strSql.Append(" where ");
						SqlParameter[] parameters = {
			};

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MyPlatform.Model.Sys_Tables model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Sys_Tables(");			
            strSql.Append("ID,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,Deleted,TableName,TableName_EN,TableName_CN,Remark");
			strSql.Append(") values (");
            strSql.Append("@ID,@CreatedBy,@CreatedDate,@UpdatedBy,@UpdatedDate,@Deleted,@TableName,@TableName_EN,@TableName_CN,@Remark");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4) ,            
                        new SqlParameter("@CreatedBy", SqlDbType.NVarChar,20) ,            
                        new SqlParameter("@CreatedDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@UpdatedBy", SqlDbType.NVarChar,20) ,            
                        new SqlParameter("@UpdatedDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@Deleted", SqlDbType.Int,4) ,            
                        new SqlParameter("@TableName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@TableName_EN", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@TableName_CN", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@Remark", SqlDbType.NVarChar,200)             
              
            };
			            
            parameters[0].Value = model.ID;                        
            parameters[1].Value = model.CreatedBy;                        
            parameters[2].Value = model.CreatedDate;                        
            parameters[3].Value = model.UpdatedBy;                        
            parameters[4].Value = model.UpdatedDate;                        
            parameters[5].Value = model.Deleted;                        
            parameters[6].Value = model.TableName;                        
            parameters[7].Value = model.TableName_EN;                        
            parameters[8].Value = model.TableName_CN;                        
            parameters[9].Value = model.Remark;                        
			            DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(MyPlatform.Model.Sys_Tables model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Sys_Tables set ");
			                        
            strSql.Append(" ID = @ID , ");                                    
            strSql.Append(" CreatedBy = @CreatedBy , ");                                    
            strSql.Append(" CreatedDate = @CreatedDate , ");                                    
            strSql.Append(" UpdatedBy = @UpdatedBy , ");                                    
            strSql.Append(" UpdatedDate = @UpdatedDate , ");                                    
            strSql.Append(" Deleted = @Deleted , ");                                    
            strSql.Append(" TableName = @TableName , ");                                    
            strSql.Append(" TableName_EN = @TableName_EN , ");                                    
            strSql.Append(" TableName_CN = @TableName_CN , ");                                    
            strSql.Append(" Remark = @Remark  ");            			
			strSql.Append(" where  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4) ,            
                        new SqlParameter("@CreatedBy", SqlDbType.NVarChar,20) ,            
                        new SqlParameter("@CreatedDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@UpdatedBy", SqlDbType.NVarChar,20) ,            
                        new SqlParameter("@UpdatedDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@Deleted", SqlDbType.Int,4) ,            
                        new SqlParameter("@TableName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@TableName_EN", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@TableName_CN", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@Remark", SqlDbType.NVarChar,200)             
              
            };
						            
            parameters[0].Value = model.ID;                        
            parameters[1].Value = model.CreatedBy;                        
            parameters[2].Value = model.CreatedDate;                        
            parameters[3].Value = model.UpdatedBy;                        
            parameters[4].Value = model.UpdatedDate;                        
            parameters[5].Value = model.Deleted;                        
            parameters[6].Value = model.TableName;                        
            parameters[7].Value = model.TableName_EN;                        
            parameters[8].Value = model.TableName_CN;                        
            parameters[9].Value = model.Remark;                        
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
		public bool Delete()
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Sys_Tables ");
			strSql.Append(" where ");
						SqlParameter[] parameters = {
			};


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
		/// 得到一个对象实体
		/// </summary>
		public MyPlatform.Model.Sys_Tables GetModel()
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate, Deleted, TableName, TableName_EN, TableName_CN, Remark  ");			
			strSql.Append("  from Sys_Tables ");
			strSql.Append(" where ");
						SqlParameter[] parameters = {
			};

			
			MyPlatform.Model.Sys_Tables model=new MyPlatform.Model.Sys_Tables();
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
																																				model.TableName= ds.Tables[0].Rows[0]["TableName"].ToString();
																																model.TableName_EN= ds.Tables[0].Rows[0]["TableName_EN"].ToString();
																																model.TableName_CN= ds.Tables[0].Rows[0]["TableName_CN"].ToString();
																																model.Remark= ds.Tables[0].Rows[0]["Remark"].ToString();
																										
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
			strSql.Append(" FROM Sys_Tables ");
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
			strSql.Append(" FROM Sys_Tables ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

   
	}
}

