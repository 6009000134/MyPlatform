using MyPlatform.Common.Cache;
using MyPlatform.Model.Enum;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.DBUtility
{
    public class DBHelperBase
    {
        public string ConnectionString;
        public DBEnum DBType { get; set; }

        public DBHelperBase()
        {
            //ConnectionString = GetDBConnection("DefaultConnection");
        }
        //TODO:获取连接字符串
        public string GetConStr(string dbCon)
        {
            DataCache cache = new DataCache();
            object DBList = cache.GetCache("Sys_DBList");
            if (DBList!=null)
            {
                List<Dictionary<string, string>> dbs = (List<Dictionary<string, string>>)DBList;                
                foreach (Dictionary<string,string> dic in dbs)
                {
                    if (dic["DBCon"] == "Default")
                    {
                        return "";
                    }
                }
            }
            return "";
        }
        //public DBHelperBase(string dbCon)
        //{
        //    ConnectionString = GetDBConnection(dbCon);
        //}
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="dbCon"></param>
        /// <returns></returns>
        //public string GetDBConnection(string dbCon)
        //{
        //    if (string.IsNullOrEmpty(dbCon))
        //    {
        //        return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //    }
        //    else
        //    {
        //        return ConfigurationManager.ConnectionStrings[dbCon].ConnectionString;
        //    }
        //}

    }
}
