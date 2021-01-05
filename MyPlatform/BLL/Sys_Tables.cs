using System;
using System.Data;
using System.Collections.Generic;
using MyPlatform.Common;
using MyPlatform.Model;
using MyPlatform.DALFactory;
using MyPlatform.IDAL;
namespace MyPlatform.BLL
{
    //Sys_Tables
    public partial class Sys_Tables
    {
        private readonly ISys_Tables dal = DataAccess.CreateInstance<ISys_Tables>("Sys_Tables");
        public Sys_Tables()
        {
        }
        #region extend 
        public bool Delete(int tableID )
        {
            return dal.Delete(tableID);
        }
        public DataTable GetListByDBName(string DBName)
        {
            return dal.GetListByDBName(DBName);
        }

        public bool Exists(string tableName, string dbName, string dbTypeCode)
        {
            return dal.Exists(dbName);
        }
        /// <summary>
        /// 判断数据库中是否存在同名表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="dbCon">数据库名</param>
        /// <returns></returns>
        public ReturnData ExistsTable(string tableName, string dbCon)
        {
            return dal.ExistsTable(tableName, dbCon);
        }
        /// <summary>
        /// 新增表信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnData Add(MyPlatform.Model.Sys_Tables model)
        {
            return dal.Add(model);
        }
        /// <summary>
        /// 编辑表信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(MyPlatform.Model.Sys_Tables model)
        {
            return dal.Edit(model);
        }
        //TODO:分页
        public DataTable GetDetailListByTID(int tableID, Pagination page)
        {
            return dal.GetDetailListByTID(tableID, page);
        }
        #endregion
    }
}