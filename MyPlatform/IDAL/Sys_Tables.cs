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
        string Test();
        #endregion
	} 
}