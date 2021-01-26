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
using System.Web.Http;

namespace MyPlatform.Areas.Web.Controllers
{
    /// <summary>
    /// tushare的API接口
    /// </summary>
    public class APIController : ApiController
    {
        MyPlatform.BLL.Sys_Api bll = new MyPlatform.BLL.Sys_Api();
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
