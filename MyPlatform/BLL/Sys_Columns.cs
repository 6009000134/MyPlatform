using System; 
using System.Data;
using System.Collections.Generic; 
using MyPlatform.Common;
using MyPlatform.Model;
using MyPlatform.DALFactory;
using MyPlatform.IDAL;
namespace MyPlatform.BLL {
	 	//Sys_Columns
		public partial class Sys_Columns
	{
		private readonly ISys_Columns dal=DataAccess.CreateInstance<ISys_Columns>("Sys_Columns");
		public Sys_Columns()
		{}
		
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists()
		{
			return dal.Exists();
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void  Add(MyPlatform.Model.Sys_Columns model)
		{
			dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(MyPlatform.Model.Sys_Columns model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete()
		{
			
			return dal.Delete();
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MyPlatform.Model.Sys_Columns GetModel()
		{
			
			return dal.GetModel();
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public MyPlatform.Model.Sys_Columns GetModelByCache()
		{
			
			string CacheKey = "Sys_ColumnsModel-";
			object objModel = MyPlatform.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel();
					if (objModel != null)
					{
						int ModelCache = MyPlatform.Common.ConfigHelper.GetConfigInt("ModelCache");
						MyPlatform.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MyPlatform.Model.Sys_Columns)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MyPlatform.Model.Sys_Columns> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MyPlatform.Model.Sys_Columns> DataTableToList(DataTable dt)
		{
			List<MyPlatform.Model.Sys_Columns> modelList = new List<MyPlatform.Model.Sys_Columns>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MyPlatform.Model.Sys_Columns model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MyPlatform.Model.Sys_Columns();					
									if(dt.Rows[n]["ID"].ToString()!="")
				{
					model.ID=int.Parse(dt.Rows[n]["ID"].ToString());
				}
																												model.CreatedBy= dt.Rows[n]["CreatedBy"].ToString();
																								if(dt.Rows[n]["CreatedDate"].ToString()!="")
				{
					model.CreatedDate=DateTime.Parse(dt.Rows[n]["CreatedDate"].ToString());
				}
																												model.UpdatedBy= dt.Rows[n]["UpdatedBy"].ToString();
																								if(dt.Rows[n]["UpdatedDate"].ToString()!="")
				{
					model.UpdatedDate=DateTime.Parse(dt.Rows[n]["UpdatedDate"].ToString());
				}
																								if(dt.Rows[n]["Deleted"].ToString()!="")
				{
					model.Deleted=int.Parse(dt.Rows[n]["Deleted"].ToString());
				}
																								if(dt.Rows[n]["TableID"].ToString()!="")
				{
					model.TableID=int.Parse(dt.Rows[n]["TableID"].ToString());
				}
																												model.TableName= dt.Rows[n]["TableName"].ToString();
																												model.ColumnName= dt.Rows[n]["ColumnName"].ToString();
																												model.ColumnName_EN= dt.Rows[n]["ColumnName_EN"].ToString();
																												model.ColumnName_CN= dt.Rows[n]["ColumnName_CN"].ToString();
																												model.ColumnType= dt.Rows[n]["ColumnType"].ToString();
																								if(dt.Rows[n]["Size"].ToString()!="")
				{
					model.Size=int.Parse(dt.Rows[n]["Size"].ToString());
				}
																												model.Remark= dt.Rows[n]["Remark"].ToString();
																						
				
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}
#endregion
   
	}
}