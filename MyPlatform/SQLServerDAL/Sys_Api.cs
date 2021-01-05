using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyPlatform.IDAL;
using System.Threading.Tasks;
using MyPlatform.Model;
using System.Data;
using MyPlatform.DBUtility;
using System.Data.SqlClient;

namespace MyPlatform.SQLServerDAL
{
    public class Sys_Api : ISys_Api
    {
        string defaultCon = "Default";
        /// <summary>
        /// 新增API
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnData Add(Dictionary<object, object> dic)
        {
            ReturnData result = new ReturnData();
            try
            {
                IDataBase db = DBHelperFactory.CreateDBInstance(defaultCon);
                //保存API信息
                string sqlApi = "insert into sys_apis values(@title,@apiName,@description,'',getdate())";
                List<SqlCommandData> liSql = new List<SqlCommandData>();
                SqlCommandData cmdApi = new SqlCommandData();
                cmdApi.CommandText = sqlApi;
                cmdApi.Paras = new SqlParameter[]{
                    new SqlParameter("@title", dic["title"]),
                new SqlParameter("@apiName", dic["apiName"]),
                new SqlParameter("@description", dic["description"])
                };
                liSql.Add(cmdApi);
                //保存输入参数信息
                SqlCommandData cmdInput = new SqlCommandData();
                string sqlInput = "";
                string[] arrInput = (string[])dic["inputParam"];
                SqlParameter[arrInput.Length] parInput;
                for (int i = 0; i < arrInput.Length; i++)
                {
                    if (i % 4 == 0)
                    {
                        sqlInput = "insert into api_input values(@input" + i.ToString();
                    }
                }
                cmdInput.CommandText = sqlInput;
                //保存输出参数信息
                db.ExecuteTran(liSql);
                result.S = true;
            }
            catch (Exception ex)
            {
                result.SetErrorMsg(ex.Message);
            }
            return result;
        }
        /// <summary>
        /// 删除API
        /// </summary>
        /// <param name="apiID"></param>
        /// <returns></returns>
        public ReturnData Delete(int apiID)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 修改API
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnData Edit(Sys_API model)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 查询API信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public DataSet GetList(string condition)
        {
            DataSet ds = new DataSet();
            condition = "%" + condition + "%";
            string sql = "select * from Sys_APIs WHERE PATINDEX(@condition,Title+ApiName+Descprition)>0";
            IDataBase db = DBHelperFactory.CreateDBInstance(defaultCon);
            IDataParameter[] pars = { new SqlParameter("@condition", condition) };
            return db.Query(sql, pars);

        }
    }
}
