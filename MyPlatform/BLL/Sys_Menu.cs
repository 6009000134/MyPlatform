using System; 
using System.Data;
using System.Collections.Generic; 
using MyPlatform.Common;
using MyPlatform.Model;
using MyPlatform.DALFactory;
using MyPlatform.IDAL;
namespace MyPlatform.BLL {
	 	//Sys_Menu
		public partial class Sys_Menu
	{
		private readonly ISys_Menu dal=DataAccess.CreateInstance<ISys_Menu>("Sys_Menu");
		public Sys_Menu()
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
		public int  Add(MyPlatform.Model.Sys_Menu model)
		{
			return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(MyPlatform.Model.Sys_Menu model)
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
		public MyPlatform.Model.Sys_Menu GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public MyPlatform.Model.Sys_Menu GetModelByCache(int ID)
		{
			
			string CacheKey = "Sys_MenuModel-" + ID;
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
			return (MyPlatform.Model.Sys_Menu)objModel;
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
		public List<MyPlatform.Model.Sys_Menu> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MyPlatform.Model.Sys_Menu> DataTableToList(DataTable dt)
		{
			List<MyPlatform.Model.Sys_Menu> modelList = new List<MyPlatform.Model.Sys_Menu>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MyPlatform.Model.Sys_Menu model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MyPlatform.Model.Sys_Menu();					
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
																												model.MenuName= dt.Rows[n]["MenuName"].ToString();
																												model.Uri= dt.Rows[n]["Uri"].ToString();
																						
				
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