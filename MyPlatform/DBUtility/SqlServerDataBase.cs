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

        public DataSet ExecuteSql(string sql)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string strSql, IDataParameter[] pars)
        {
            throw new NotImplementedException();
        }

        public object GetSingle(string strSql, IDataParameter[] pars)
        {
            throw new NotImplementedException();
        }

        public DataSet Query(string sql)
        {
            throw new NotImplementedException();
        }

        public DataSet Query(string sql, IDataParameter[] pars)
        {
            throw new NotImplementedException();
        }
    }
}
