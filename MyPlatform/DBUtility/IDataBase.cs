using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.DBUtility
{
    public interface IDataBase
    {
        bool Exists(string strSql,IDataParameter[] pars);
        DataSet Query(string sql);
        DataSet Query(string sql,IDataParameter[] pars);
        object GetSingle(string strSql,IDataParameter[] pars);
    }
}
