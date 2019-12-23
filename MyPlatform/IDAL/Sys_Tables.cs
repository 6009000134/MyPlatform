using System;
using System.Data;
namespace MyPlatform.IDAL {
	/// <summary>
	/// 接口层Sys_Tables
	/// </summary>
	public interface ISys_Tables
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists();
		/// <summary>
		/// 增加一条数据
		/// </summary>
		void  Add(MyPlatform.Model.Sys_Tables model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(MyPlatform.Model.Sys_Tables model);
		/// <summary>
		/// 删除数据
		/// </summary>
		bool Delete();
				/// <summary>
		/// 得到一个对象实体
		/// </summary>
		MyPlatform.Model.Sys_Tables GetModel();
		/// <summary>
		/// 获得数据列表
		/// </summary>
		DataSet GetList(string strWhere);
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		DataSet GetList(int Top,string strWhere,string filedOrder);
		/// <summary>
		/// 根据分页获得数据列表
		/// </summary>
		//DataSet GetList(int PageSize,int PageIndex,string strWhere);
		#endregion  成员方法
	} 
}