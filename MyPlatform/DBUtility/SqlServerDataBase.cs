using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.DBUtility
{
    public class SqlServerDataBase : IDataBase
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public SqlServerDataBase()
        {
        }
        public SqlServerDataBase(string dbname)
        {
            connectionString= ConfigurationManager.ConnectionStrings[dbname].ConnectionString;
        }

        public int ExecuteNonQuert(string sql, IDataParameter[] paras)
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery(string sql)
        {
            throw new NotImplementedException();
        }

        public SqlDataReader ExecuteReader(string sql)
        {
            throw new NotImplementedException();
        }

        public object ExecuteScalar(string sql)
        {
            throw new NotImplementedException();
        }

        public DataSet Query(string sql)
        {
            throw new NotImplementedException();
        }
    }
}
