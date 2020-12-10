using System;
using System.Data;
namespace MyPlatform.IDAL {
	/// <summary>
	/// 接口层Sys_Tables
	/// </summary>
	public interface ISys_Tables
	{
        #region Extend By liufei
        DataTable GetListByDBName(string DBName);
        bool Exists(string dbName);
        bool ExistsTable(string tableName,string dbName);
        bool Add(MyPlatform.Model.Sys_Tables model);
        bool Edit(MyPlatform.Model.Sys_Tables model);
        DataTable GetDetailListByTID(int tableID, MyPlatform.Model.Pagination page);
        #endregion
	} 
}