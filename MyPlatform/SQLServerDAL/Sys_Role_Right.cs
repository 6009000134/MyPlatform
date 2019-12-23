using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using MyPlatform.DBUtility;
using MyPlatform.IDAL;
namespace MyPlatform.SQLServerDAL  
{
	 	//Sys_Role_Right
		public partial class Sys_Role_Right: ISys_Role_Right
	{
   		     
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Sys_Role_Right");
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
		public int Add(MyPlatform.Model.Sys_Role_Right model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Sys_Role_Right(");			
            strSql.Append("ROleID,RightID");
			strSql.Append(") values (");
            strSql.Append("@ROleID,@RightID");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@ROleID", SqlDbType.Int,4) ,            
                        new SqlParameter("@RightID", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.ROleID;                        
            parameters[1].Value = model.RightID;                        
			   
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
		public bool Update(MyPlatform.Model.Sys_Role_Right model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Sys_Role_Right set ");
			                                                
            strSql.Append(" ROleID = @ROleID , ");                                    
            strSql.Append(" RightID = @RightID  ");            			
			strSql.Append(" where ID=@ID ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4) ,            
                        new SqlParameter("@ROleID", SqlDbType.Int,4) ,            
                        new SqlParameter("@RightID", SqlDbType.Int,4)             
              
            };
						            
            parameters[0].Value = model.ID;                        
            parameters[1].Value = model.ROleID;                        
            parameters[2].Value = model.RightID;                        
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
			strSql.Append("delete from Sys_Role_Right ");
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
			strSql.Append("delete from Sys_Role_Right ");
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
		public MyPlatform.Model.Sys_Role_Right GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID, ROleID, RightID  ");			
			strSql.Append("  from Sys_Role_Right ");
			strSql.Append(" where ID=@ID");
						SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			
			MyPlatform.Model.Sys_Role_Right model=new MyPlatform.Model.Sys_Role_Right();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["ROleID"].ToString()!="")
				{
					model.ROleID=int.Parse(ds.Tables[0].Rows[0]["ROleID"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["RightID"].ToString()!="")
				{
					model.RightID=int.Parse(ds.Tables[0].Rows[0]["RightID"].ToString());
				}
																														
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
			strSql.Append(" FROM Sys_Role_Right ");
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
			strSql.Append(" FROM Sys_Role_Right ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

   
	}
}

