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
