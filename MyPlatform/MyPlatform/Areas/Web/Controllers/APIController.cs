using MyPlatform.Common;
using MyPlatform.Model;
using MyPlatform.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace MyPlatform.Areas.Web.Controllers
{
    /// <summary>
    /// tushare的API接口
    /// </summary>
    public class APIController : ApiController
    {
        MyPlatform.BLL.Sys_Api bll = new MyPlatform.BLL.Sys_Api();
        string apiToken = "8637e17fe4cbc18f2c412229ad41f8628d0849598c73e4fd3332d8fa";
        string apiUrl = "http://api.tushare.pro";
        /// <summary>
        /// 创建接口参数
        /// </summary>
        /// <param name="apiID">接口ID</param>
        /// <param name="dicInput">传入参数及值</param>
        /// <returns></returns>
        public APIInputParam CreateInputStr(int apiID, Dictionary<string, string> dicInput)
        {

            APIInputParam input = new APIInputParam();
            input.token = apiToken;//设置Token
            input.@params = new Dictionary<string, string>();
            DataSet ds = bll.GetDetail(apiID);//获取api接口信息
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    input.api_name = ds.Tables[0].Rows[0]["ApiName"].ToString();
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    if (dicInput.Count > 0)//前端传参
                    {
                        foreach (KeyValuePair<string, string> item in dicInput)//校验前端传入的输入参数是否全部存在
                        {
                            if (!ds.Tables[1].Columns.Contains(item.Key))
                            {
                                throw new Exception("接口不存在" + item.Key + "输入参数，请核对接口输入参数信息！");
                            }
                        }
                        foreach (DataRow dr in ds.Tables[1].Rows)//设置传入参数及其值
                        {
                            if (dicInput.Keys.Contains(dr["ParamName"].ToString()))
                            {
                                input.@params.Add(dr["ParamName"].ToString(), dicInput[dr["ParamName"].ToString()]);
                            }
                            else
                            {
                                input.@params.Add(dr["ParamName"].ToString(), "");
                            }
                        }
                    }
                    else//无前端传参
                    {
                        foreach (DataRow dr in ds.Tables[1].Rows)
                        {
                            input.@params.Add(dr["ParamName"].ToString(), "");
                        }
                    }
                }
                else
                {
                    throw new Exception("未找到API接口的输入参数信息");
                }
                //设置输出fields
                if (ds.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[2].Rows)
                    {
                        input.fields += dr["ParamName"].ToString() + ",";
                    }
                    input.fields = input.fields.TrimEnd(',');
                }
                else
                {
                    throw new Exception("未找到API接口的输出参数信息");
                }
            }
            return input;
        }
        [HttpPost]
        public HttpResponseMessage GetApiResult([FromBody]JObject postData)
        {
            ReturnData result = new ReturnData();
            try
            {
                HttpHelper hh = new HttpHelper();
                TuShareResult apiResult = JSONUtil.ParseFromJson<TuShareResult>(hh.Post(postData.GetValue("url").ToString(), postData.GetValue("postData").ToJson()));
                if (apiResult.data == null)//API接口无返回数据
                {
                    result.S = false;
                    result.M = "API接口无数据返回！";
                }
                else
                {
                    result = bll.GetApiResult(apiResult, int.Parse(postData.GetValue("apiID").ToString()));
                }
            }
            catch (Exception ex)
            {
                result.SetErrorMsg(ex.Message);
            }
            return MyResponseMessage.SuccessJson(result);
        }
        private object lockObj = new object();
        public void GetDividend(object obj)
        {
            Dictionary<string, object> dic = (Dictionary<string, object>)obj;
            int apiID = (int)dic["apiID"];
            APIInputParam inputParam = (APIInputParam)dic["inputParam"];
            DataTable dt = (DataTable)dic["dt"];
            TuShareResult apiResultInsert = new TuShareResult();
            apiResultInsert.data = new TuShareResultData();
            apiResultInsert.data.items = new List<List<string>>();
            int index = 1;
            int min = DateTime.Now.Minute;
            int sec = DateTime.Now.Second;
            foreach (DataRow dr in dt.Rows)
            {
                if (index % 60 == 0)
                {
                    int min2 = DateTime.Now.Minute;
                    int sec2 = DateTime.Now.Second;

                    if (min2 - min == 1)
                    {
                        if (60 - sec + sec2 < 60)
                        {
                            Thread.Sleep((Math.Abs(sec2 - sec)) * 1000);
                        }
                    }
                    else if (min2 - min == 0)
                    {
                        Thread.Sleep(Math.Abs(60 - sec2 + sec) * 1000);
                    }
                    min = min2;
                    sec = sec2;
                }
                index += 1;
                HttpHelper hh = new HttpHelper();
                inputParam.@params["ts_code"] = dr["ts_code"].ToString();
                //postData.
                TuShareResult apiResult;
                string inputStr = "";
                lock (this)
                {
                    inputStr = inputParam.ToJson<APIInputParam>();
                }
                string responStr = hh.Post(apiUrl, inputStr);
                apiResult = JSONUtil.ParseFromJson<TuShareResult>(responStr);
                if (apiResult.data != null)//API接口无返回数据
                {
                    if (apiResultInsert.data.items.Count > 3000)
                    {
                        apiResultInsert.data.items.AddRange(apiResult.data.items);
                        bll.GetApiResult(apiResultInsert, apiID);
                        apiResultInsert.data.items = new List<List<string>>();
                    }
                    else
                    {
                        apiResultInsert.data.items.AddRange(apiResult.data.items);
                    }

                }
            }
            if (apiResultInsert.data.items.Count > 0)
            {
                bll.GetApiResult(apiResultInsert, apiID);
            }

        }
        /// <summary>
        /// 多线程获取分红数据
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetDividendResultMultiple([FromBody]Dictionary<string, object> dic)
        {
            int apiID = Convert.ToInt32(dic["apiID"]);
            APIInputParam inputParam = CreateInputStr(apiID, new Dictionary<string, string>());
            ReturnData result = new ReturnData();
            try
            {
                DataTable dt = bll.GetTsCode(dic["ts_code"].ToString()).Tables[0];
                TuShareResult apiResultInsert = new TuShareResult();
                apiResultInsert.data = new TuShareResultData();
                apiResultInsert.data.items = new List<List<string>>();
                if (dt.Rows.Count > 0)
                {
                    int count = dt.Rows.Count;
                    int thCount = count % 2 == 0 ? 2 : 3;
                    int perThCount = count / 2;
                    for (int i = 1; i < thCount + 1; i++)
                    {
                        Dictionary<string, object> dicTh = new Dictionary<string, object>();
                        dicTh.Add("apiID", apiID);
                        DataTable dtNew = new DataTable();
                        dtNew = dt.Copy();
                        dtNew.Clear();
                        for (int j = perThCount * (i - 1); j < i * perThCount; j++)
                        {
                            dtNew.Rows.Add(dt.Rows[j].ItemArray);
                        }
                        dicTh.Add("dt", dtNew);
                        dicTh.Add("inputParam", inputParam);
                        Thread td = new Thread(new ParameterizedThreadStart(GetDividend));
                        td.Start(dicTh);
                    }
                }
                else
                {
                    result.SetErrorMsg("找不到股票代码数据！");
                }
            }
            catch (Exception ex)
            {
                result.SetErrorMsg(ex.Message);
            }
            return MyResponseMessage.SuccessJson(result);
        }
        /// <summary>
        /// 获取分红数据
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetDividendResult([FromBody]Dictionary<string, object> dic)
        {
            int apiID = Convert.ToInt32(dic["apiID"]);
            APIInputParam inputParam = CreateInputStr(apiID, new Dictionary<string, string>());
            ReturnData result = new ReturnData();
            try
            {
                DataTable dt = bll.GetTsCode(dic["ts_code"].ToString()).Tables[0];
                TuShareResult apiResultInsert = new TuShareResult();
                apiResultInsert.data = new TuShareResultData();
                apiResultInsert.data.items = new List<List<string>>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        HttpHelper hh = new HttpHelper();
                        inputParam.@params["ts_code"] = dr["ts_code"].ToString();
                        //postData.
                        TuShareResult apiResult = JSONUtil.ParseFromJson<TuShareResult>(hh.Post(apiUrl, inputParam.ToJson<APIInputParam>()));
                        if (apiResult.data == null)//API接口无返回数据
                        {
                            result.S = false;
                            result.M = "API接口无数据返回！";
                        }
                        else
                        {
                            if (apiResultInsert.data.items.Count > 3000)
                            {
                                //foreach (List<string> item in apiResult.data.items)
                                //{
                                //    apiResultInsert.data.items[0].AddRange(item);
                                //}
                                apiResultInsert.data.items.AddRange(apiResult.data.items);
                                result = bll.GetApiResult(apiResultInsert, apiID);
                                apiResultInsert.data.items = new List<List<string>>();
                            }
                            else
                            {
                                apiResultInsert.data.items.AddRange(apiResult.data.items);
                            }

                        }
                    }
                    if (apiResultInsert.data.items.Count > 0)
                    {
                        result = bll.GetApiResult(apiResultInsert, apiID);
                    }

                }
                else
                {
                    result.SetErrorMsg("找不到股票代码数据！");
                }
            }
            catch (Exception ex)
            {
                result.SetErrorMsg(ex.Message);
            }
            return MyResponseMessage.SuccessJson(result);
        }
        /// <summary>
        /// 根据API信息，创建表
        /// </summary>
        /// <param name="apiID"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage CreateApiTable([FromBody]int apiID)
        {
            ReturnData result = new ReturnData();
            try
            {
                result = bll.CreateApiTable(apiID);
            }
            catch (Exception ex)
            {
                result.SetErrorMsg(ex.Message);
            }
            return MyResponseMessage.SuccessJson(result);
        }
        /// <summary>
        /// 获取API详情
        /// </summary>
        /// <param name="apiID">API ID</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetDetail([FromBody]int apiID)
        {
            ReturnData result = new ReturnData();
            try
            {
                result.S = true;
                result.D = bll.GetDetail(apiID);
            }
            catch (Exception ex)
            {
                result.SetErrorMsg(ex.Message);
            }
            return MyResponseMessage.SuccessJson(result);
        }
        /// <summary>
        /// 新增API
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Add(Dictionary<object, object> dic)
        {
            ReturnData result = new ReturnData();
            try
            {
                dic["inputParam"] = ((Newtonsoft.Json.Linq.JArray)dic["inputParam"]).ToObject<string[]>();
                dic["outputParam"] = ((Newtonsoft.Json.Linq.JArray)dic["outputParam"]).ToObject<string[]>();
                result = bll.Add(dic);
            }
            catch (Exception ex)
            {
                result.SetErrorMsg(ex.Message);
            }
            return MyResponseMessage.SuccessJson(result);
        }
        /// <summary>
        /// 获取API列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage List([FromBody]string condition)
        {
            ReturnData result = new ReturnData();
            try
            {
                DataSet ds = bll.GetList(condition);
                result.D = ds.Tables[0];
                result.S = true;
                //result.D = colBLL.GetList(tableID).Tables[0];
            }
            catch (Exception ex)
            {
                result.S = false;
                result.SetErrorMsg("获取表字段失败：" + ex.Message);
            }
            return MyResponseMessage.SuccessJson(result);
        }
        /// <summary>
        /// 测试是否能够调通tushare接口
        /// </summary>
        /// <param name="postData">接口地址和json数据</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Test(JObject postData)
        {
            ReturnData result = new ReturnData();
            try
            {
                HttpHelper hh = new HttpHelper();
                result.S = true;
                result.D = hh.Post(postData.GetValue("url").ToString(), postData.GetValue("postData").ToJson());
            }
            catch (Exception ex)
            {

                result.SetErrorMsg(ex.Message);
            }
            return MyResponseMessage.SuccessJson(result);
        }
        /// <summary>
        /// Get方式获取url的Html
        /// </summary>
        /// <param name="url">指定页面url</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetHtml([FromBody]string url)
        {
            ReturnData result = new ReturnData();
            try
            {
                HttpHelper hh = new HttpHelper();
                result.D = hh.Get(url, "");
                result.S = true;
            }
            catch (Exception ex)
            {
                result.SetErrorMsg(ex.Message);
            }
            return MyResponseMessage.SuccessJson(result);
        }
    }
}
