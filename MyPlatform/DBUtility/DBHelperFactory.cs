using MyPlatform.Common.Cache;
using MyPlatform.DBUtility;
using MyPlatform.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.DBUtility
{
    public static class DBHelperFactory
    {
        /// <summary>
        /// 根据数据库连接名，返回对应DBHelper实例
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public static IDataBase CreateDBInstance(string dbName)
        {
            IDataBase db;
            DataCache cache = new DataCache();
            object DBList = cache.GetCache("Sys_DBList");
            string dbType = "";
            if (DBList == null)
            {
                dbType = ConfigurationManager.AppSettings.Get(dbName);                
            }
            else
            {
                List<KeyValueData> li = DBList as List<KeyValueData>;
                dbType = li.Where(m => m.Key == dbName).First().Value;
            }
            if (string.IsNullOrEmpty(dbType))
            {
                throw new Exception("不能打开数据，数据库连接为空！");
            }
            switch (dbType.ToLower())
            {
                case "sqlserver":
                    db = new SqlServerDataBase(dbName);
                    break;
                case "oracle":
                    throw new Exception("系统暂不支持Oracle数据库");
                    break;
                case "mysql":
                    throw new Exception("系统暂不支持MySql数据库");
                    break;
                default:
                    db = new SqlServerDataBase();
                    break;
            }
            return db;
        }
    }
}
