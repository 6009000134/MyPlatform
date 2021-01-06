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
using MyPlatform.Common;

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
                string sqlApi = @"insert into sys_apis(Title ,
          ApiName ,
          Descprition ,
          CreatedBy ,
          CreatedDate           
          ) values(@title,@apiName,@description,'',getdate())";
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
                string sqlInput = "";
                string[] arrInput = (string[])dic["inputParam"];
                SqlParameter[] parInput = new SqlParameter[4];
                for (int i = 0; i < arrInput.Length; i++)
                {
                    if (i % 4 == 0)
                    {
                        sqlInput = @"insert into api_input( ApiID ,
          ParamName ,
          ParamType ,
          IsRequired ,
          Descprition ,
          OrderNo ,
          CreatedBy ,
          CreatedDate) values((SELECT IDENT_CURRENT('Sys_Apis')),@input" + i.ToString();
                        SqlParameter param = new SqlParameter("@input" + i.ToString(), arrInput[i]);
                        parInput[i % 4] = param;
                    }
                    else if (i % 4 == 3)
                    {
                        sqlInput += ",@input" + i.ToString() + "," + (i * 40).ToString() + ",'',getdate())";
                        SqlParameter param = new SqlParameter("@input" + i.ToString(), arrInput[i]);
                        parInput[i % 4] = param;
                        SqlCommandData cmdInput = new SqlCommandData();
                        cmdInput.CommandText = sqlInput;
                        cmdInput.Paras = parInput;
                        liSql.Add(cmdInput);
                        //重新声明新的输入参数信息
                        parInput = new SqlParameter[4];
                        sqlInput = "";
                    }
                    else
                    {
                        sqlInput += ",@input" + i.ToString();
                        SqlParameter param = new SqlParameter("@input" + i.ToString(), arrInput[i]);
                        parInput[i % 4] = param;
                    }
                }
                //保存输出参数信息
                string sqlOutput = "";
                string[] arrOutput = (string[])dic["outputParam"];
                int outputColumns = Convert.ToInt32(dic["outputColumns"]);
                SqlParameter[] parOutput = new SqlParameter[outputColumns];
                if (outputColumns == 4)
                {
                    for (int i = 0; i < arrOutput.Length; i++)
                    {
                        if (i % 4 == 0)
                        {
                            sqlOutput = @"insert into api_output( ApiID ,
          ParamName ,
          ParamType ,
          IsRequired ,
          Descprition ,
          OrderNo ,
          CreatedBy ,
          CreatedDate) values((SELECT IDENT_CURRENT('Sys_Apis')),@output" + i.ToString();
                            SqlParameter param = new SqlParameter("@output" + i.ToString(), arrOutput[i]);
                            parOutput[i % 4] = param;
                        }
                        else if (i % 4 == 3)
                        {
                            sqlOutput += ",@output" + i.ToString() + "," + (i * 40).ToString() + ",'',getdate())";
                            SqlParameter param = new SqlParameter("@output" + i.ToString(), arrOutput[i]);
                            parOutput[i % 4] = param;
                            SqlCommandData cmdOutput = new SqlCommandData();
                            cmdOutput.CommandText = sqlOutput;
                            cmdOutput.Paras = parOutput;
                            liSql.Add(cmdOutput);
                            //重新声明新的输入参数信息
                            parOutput = new SqlParameter[4];
                            sqlOutput = "";
                        }
                        else
                        {
                            sqlOutput += ",@output" + i.ToString();
                            SqlParameter param = new SqlParameter("@output" + i.ToString(), arrOutput[i]);
                            parOutput[i % 4] = param;
                        }
                    }
                }
                else if (outputColumns == 3)
                {
                    for (int i = 0; i < arrOutput.Length; i++)
                    {
                        if (i % 3 == 0)
                        {
                            sqlOutput = @"insert into api_output( ApiID ,
          ParamName ,
          ParamType ,
          IsRequired ,
          Descprition ,
          OrderNo ,
          CreatedBy ,
          CreatedDate)  values((SELECT IDENT_CURRENT('Sys_Apis')),@output" + i.ToString();
                            SqlParameter param = new SqlParameter("@output" + i.ToString(), arrOutput[i]);
                            parOutput[i % 3] = param;
                        }
                        else if (i % 3 == 2)
                        {
                            sqlOutput += ",'',@output" + i.ToString() + "," + (i * 40).ToString() + ",'',getdate())";
                            SqlParameter param = new SqlParameter("@output" + i.ToString(), arrOutput[i]);
                            parOutput[i % 3] = param;
                            SqlCommandData cmdOutput = new SqlCommandData();
                            cmdOutput.CommandText = sqlOutput;
                            cmdOutput.Paras = parOutput;
                            liSql.Add(cmdOutput);
                            //重新声明新的输入参数信息
                            parOutput = new SqlParameter[3];
                            sqlOutput = "";
                        }
                        else
                        {
                            sqlOutput += ",@output" + i.ToString();
                            SqlParameter param = new SqlParameter("@output" + i.ToString(), arrOutput[i]);
                            parOutput[i % 3] = param;
                        }
                    }
                }
                if (db.ExecuteTran(liSql))
                {
                    result.S = true;
                }
                else
                {
                    result.SetErrorMsg("添加失败！");
                }
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
