using MyPlatform.Model;
using System;
using System.Collections.Generic;
using System.Data;
namespace MyPlatform.IDAL {
	/// <summary>
	/// 接口层Sys_Tables
	/// </summary>
	public interface ISys_Tables
	{
        #region Extend By liufei
        DataTable GetListByDBName(string DBName);
        DataTable GetListByDBName(Dictionary<string, object> dicCondition);
        bool Exists(string dbName);
        ReturnData ExistsTable(string tableName,string dbCon);
        ReturnData Add(MyPlatform.Model.Sys_Tables model);
        bool Edit(MyPlatform.Model.Sys_Tables model);
        bool Delete(int tableID );
        DataTable GetDetailListByTID(int tableID, MyPlatform.Model.Pagination page);
        #endregion
	} 
}