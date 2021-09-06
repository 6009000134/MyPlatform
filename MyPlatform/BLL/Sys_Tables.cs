using System;
using System.Data;
using System.Collections.Generic;
using MyPlatform.Common;
using MyPlatform.Model;
using MyPlatform.DALFactory;
using MyPlatform.IDAL;
using MyPlatform.DBUtility;

namespace MyPlatform.BLL
{
    //Sys_Tables
    public partial class Sys_Tables
    {
        private readonly ISys_Tables dal = DataAccess.CreateInstance<ISys_Tables>("Sys_Tables");
        private string defaultCon = "Default";
        public Sys_Tables()
        {

        }
        #region extend
        public bool Delete(int tableID )
        {
            ////判断是否存在表
            //IDataBase db = DBUtility.DBHelperFactory.Create(defaultCon);
            //Model.Sys_Tables tableInfo = dal.GetModel(db);
            //if (tableInfo==null)
            //{
            //    throw new Exception("未找到表信息，无法删除！");
            //}
            //ReturnData result = ExistsTable(tableInfo.DBCon, tableInfo.TableName);
            //if (result.S)
            //{
            //    if (tableInfo.DBCon==defaultCon)
            //    {

            //    }
            //    IDataBase acDB = DBHelperFactory.Create(tableInfo.DBCon);
            //}
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
        public ReturnData ExistsTable(string dbCon,string tableName)
        {
            IDataBase db = DBUtility.DBHelperFactory.Create(dbCon);
            if (db==null)
            {
                throw new Exception("数据库"+dbCon+"连接错误！");
            }
            return dal.ExistsTable(db, tableName);
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
        /// <summary>
        /// 获取表详情（表信息及列信息）
        /// </summary>
        /// <param name="tableID"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ReturnData GetDetail(int tableID, Pagination page)
        {
            return dal.GetDetail(tableID,page);
        }
        #endregion
    }
}