using System; 
using System.Data;
using System.Collections.Generic; 
using MyPlatform.Common;
using MyPlatform.Model;
using MyPlatform.DALFactory;
using MyPlatform.IDAL;
namespace MyPlatform.BLL {
	 	//Sys_Role
		public partial class Sys_Role
	{
		private readonly ISys_Role dal=DataAccess.CreateInstance<ISys_Role>("Sys_Role");
		public Sys_Role()
		{}
		
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			return dal.Exists(ID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(MyPlatform.Model.Sys_Role model)
		{
			return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(MyPlatform.Model.Sys_Role model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ID)
		{
			
			return dal.Delete(ID);
		}
				/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			return dal.DeleteList(IDlist );
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MyPlatform.Model.Sys_Role GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public MyPlatform.Model.Sys_Role GetModelByCache(int ID)
		{
			
			string CacheKey = "Sys_RoleModel-" + ID;
			object objModel = MyPlatform.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ID);
					if (objModel != null)
					{
						int ModelCache = MyPlatform.Common.ConfigHelper.GetConfigInt("ModelCache");
						MyPlatform.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (MyPlatform.Model.Sys_Role)objModel;
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
		public List<MyPlatform.Model.Sys_Role> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MyPlatform.Model.Sys_Role> DataTableToList(DataTable dt)
		{
			List<MyPlatform.Model.Sys_Role> modelList = new List<MyPlatform.Model.Sys_Role>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MyPlatform.Model.Sys_Role model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MyPlatform.Model.Sys_Role();					
									if(dt.Rows[n]["ID"].ToString()!="")
				{
					model.ID=int.Parse(dt.Rows[n]["ID"].ToString());
				}
																								if(dt.Rows[n]["PID"].ToString()!="")
				{
					model.PID=int.Parse(dt.Rows[n]["PID"].ToString());
				}
																												model.RoleName= dt.Rows[n]["RoleName"].ToString();
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