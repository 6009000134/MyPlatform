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
    public class Sys_Api
    {
        string defaultCon = "Default";
        private readonly ISys_Api dal = DataAccess.CreateInstance<ISys_Api>("Sys_Api");
        public ReturnData CreateApiTable(int apiID)
        {
            IDataBase db = DBHelperFactory.CreateDBInstance(defaultCon);
            return dal.CreateApiTable(db,apiID);
        }
        public DataSet GetDetail(int apiID)
        {
            return dal.GetDetail(apiID);
        }
        /// <summary>
        /// 查询api信息
        /// </summary>
        /// <param name="condition">标题、api名称、描述关键字</param>
        /// <returns></returns>
        public DataSet GetList(string condition)
        {
            return dal.GetList(condition);
        }
        /// <summary>
        /// 新增API
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public ReturnData Add(Dictionary<object, object> dic)
        {
            return dal.Add(dic);
        }
    }
}
