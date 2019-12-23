using System; 
using System.Data;
using System.Collections.Generic; 
using MyPlatform.Common;
using MyPlatform.Model;
using MyPlatform.DALFactory;
using MyPlatform.IDAL;
namespace MyPlatform.BLL {
	 	//Sys_Tables
		public partial class Sys_Tables
	{
		private readonly ISys_Tables dal=DataAccess.CreateInstance<ISys_Tables>("Sys_Tables");
		public Sys_Tables()
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
		public void  Add(MyPlatform.Model.Sys_Tables model)
		{
			dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(MyPlatform.Model.Sys_Tables model)
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
		public MyPlatform.Model.Sys_Tables GetModel()
		{
			
			return dal.GetModel();
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public MyPlatform.Model.Sys_Tables GetModelByCache()
		{
			//TODO:待阅读代码
			string CacheKey = "Sys_TablesModel-";
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
			return (MyPlatform.Model.Sys_Tables)objModel;
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
		public List<MyPlatform.Model.Sys_Tables> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MyPlatform.Model.Sys_Tables> DataTableToList(DataTable dt)
		{
			List<MyPlatform.Model.Sys_Tables> modelList = new List<MyPlatform.Model.Sys_Tables>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MyPlatform.Model.Sys_Tables model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MyPlatform.Model.Sys_Tables();					
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
																												model.TableName= dt.Rows[n]["TableName"].ToString();
																												model.TableName_EN= dt.Rows[n]["TableName_EN"].ToString();
																												model.TableName_CN= dt.Rows[n]["TableName_CN"].ToString();
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