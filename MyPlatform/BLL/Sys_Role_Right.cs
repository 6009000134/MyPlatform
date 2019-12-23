using System; 
using System.Data;
using System.Collections.Generic; 
using MyPlatform.Common;
using MyPlatform.Model;
using MyPlatform.DALFactory;
using MyPlatform.IDAL;
namespace MyPlatform.BLL {
	 	//Sys_Role_Right
		public partial class Sys_Role_Right
	{
		private readonly ISys_Role_Right dal=DataAccess.CreateInstance<ISys_Role_Right>("Sys_Role_Right");
		public Sys_Role_Right()
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
		public int  Add(MyPlatform.Model.Sys_Role_Right model)
		{
			return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(MyPlatform.Model.Sys_Role_Right model)
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
		public MyPlatform.Model.Sys_Role_Right GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public MyPlatform.Model.Sys_Role_Right GetModelByCache(int ID)
		{
			
			string CacheKey = "Sys_Role_RightModel-" + ID;
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
			return (MyPlatform.Model.Sys_Role_Right)objModel;
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
		public List<MyPlatform.Model.Sys_Role_Right> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MyPlatform.Model.Sys_Role_Right> DataTableToList(DataTable dt)
		{
			List<MyPlatform.Model.Sys_Role_Right> modelList = new List<MyPlatform.Model.Sys_Role_Right>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MyPlatform.Model.Sys_Role_Right model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MyPlatform.Model.Sys_Role_Right();					
									if(dt.Rows[n]["ID"].ToString()!="")
				{
					model.ID=int.Parse(dt.Rows[n]["ID"].ToString());
				}
																								if(dt.Rows[n]["ROleID"].ToString()!="")
				{
					model.ROleID=int.Parse(dt.Rows[n]["ROleID"].ToString());
				}
																								if(dt.Rows[n]["RightID"].ToString()!="")
				{
					model.RightID=int.Parse(dt.Rows[n]["RightID"].ToString());
				}
																						
				
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