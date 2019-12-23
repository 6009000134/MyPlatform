using System;
using System.Data;
using MyPlatform.Model;
namespace MyPlatform.IDAL {
	/// <summary>
	/// 接口层Sys_Users
	/// </summary>
	public interface ISys_Users
	{
        #region Extend by liufei
        bool CreateUser(Sys_Users model);
        /// <summary>
        /// 验证账号是否注册
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        bool Exists(string Account);
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Exists(Sys_Users model);
        /// <summary>
        /// 通过账号获取账号信息
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        Sys_Users GetModelByAccount(string Account);
        #endregion
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int ID);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int  Add(MyPlatform.Model.Sys_Users model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(MyPlatform.Model.Sys_Users model);
		/// <summary>
		/// 删除数据
		/// </summary>
		bool Delete(int ID);
				bool DeleteList(string IDlist );
				/// <summary>
		/// 得到一个对象实体
		/// </summary>
		MyPlatform.Model.Sys_Users GetModel(int ID);
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