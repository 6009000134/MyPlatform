using System;
using System.Data;
using System.Collections.Generic;
using MyPlatform.Common;
using MyPlatform.Model;
using MyPlatform.DALFactory;
using MyPlatform.IDAL;

namespace MyPlatform.BLL
{
    public class Sys_Api
    {
        private readonly ISys_Api dal = DataAccess.CreateInstance<ISys_Api>("Sys_Api");
        /// <summary>
        /// 查询api信息
        /// </summary>
        /// <param name="condition">标题、api名称、描述关键字</param>
        /// <returns></returns>
        public DataSet GetList(string condition)
        {
            return dal.GetList(condition);
        }
    }
}
