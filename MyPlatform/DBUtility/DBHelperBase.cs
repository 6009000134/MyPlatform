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
        public DBHelperBase()
        {
            ConnectionString=GetDBConnection("");
        }
        public DBHelperBase(string dbName)
        {
            ConnectionString=GetDBConnection(dbName);
        }

        public string GetDBConnection(string dbName)
        {
            if (string.IsNullOrEmpty(dbName))
            {
                return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            }
            else
            {
                return ConfigurationManager.ConnectionStrings[dbName].ConnectionString;
            }
        }

    }
}
