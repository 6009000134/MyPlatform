﻿using System;
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
        /// <param name="dbName">数据库名</param>
        /// <param name="dbType">数据库类型</param>
        /// <returns></returns>
        public bool ExistsTable(string tableName, string dbName)
        {
            return dal.ExistsTable(tableName, dbName);
        }
        /// <summary>
        /// 新增表信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(MyPlatform.Model.Sys_Tables model)
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
        #endregion
    }
}