﻿using MyPlatform.DBUtility;
using System;
using System.Data;
namespace MyPlatform.IDAL
{
    /// <summary>
    /// 接口层Sys_Menu
    /// </summary>
    public interface ISys_Menu
    {
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="model"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        bool Add(MyPlatform.Model.Sys_Menu model, IDataBase db);

        DataSet GetMenuTree(IDataBase db);
        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="model"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        bool Edit(MyPlatform.Model.Sys_Menu model, IDataBase db);
    }
}