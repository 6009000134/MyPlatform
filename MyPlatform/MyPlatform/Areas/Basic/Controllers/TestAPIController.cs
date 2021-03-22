using MyPlatform.Model;
using MyPlatform.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyPlatform.Areas.Basic.Controllers
{
    /// <summary>
    /// 学习时编写测试代码
    /// </summary>
    public class TestAPIController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage List(List<QueryParam> qd)
        {
            ReturnData result = new ReturnData();
            result.S = true;
            result.D = qd;
            return MyResponseMessage.SuccessJson<ReturnData>(result);
        }
        [HttpPost]
        public HttpResponseMessage List2(JObject obj)
        {
            List<QueryParam> qd = obj["qd"].ToObject<List<QueryParam>>();
            string name = obj["name"].ToString();
            ReturnData result = new ReturnData();
            result.S = true;
            //result.D = qd;
            return MyResponseMessage.SuccessJson<ReturnData>(result);
        }

        public HttpResponseMessage List3(JObject obj)
        {
            ReturnData result = new ReturnData();
            List<QueryParam> qd = obj["qd"].ToObject<List<QueryParam>>();
            List<QueryParam> qd2 = obj["qd2"].ToObject<List<QueryParam>>();
            result.S = true;
            result.D = obj;
            return MyResponseMessage.SuccessJson<ReturnData>(result);
        }

        public HttpResponseMessage List4()
        {
            ReturnData result = new ReturnData();
            result.S = true;
            return MyResponseMessage.SuccessJson<ReturnData>(result);
        }
    }
}
