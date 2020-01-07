using MyPlatform.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyPlatform.Areas.Basic.Controllers
{
    public class DBController : ApiController
    {
        /// <summary>
        /// 获取数据库列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage List()
        {
            ReturnData result = new ReturnData();
            Dictionary<string, string> dicDBList = new Dictionary<string, string>();
            List<KeyValueData> li = new List<KeyValueData>();
            for (int i = 0; i < ConfigurationManager.ConnectionStrings.Count; i++)
            {
                string dbType = ConfigurationManager.AppSettings.Get(ConfigurationManager.ConnectionStrings[i].Name);
                if (!string.IsNullOrEmpty(dbType))
                {
                    KeyValueData kv = new KeyValueData();
                    kv.Key = ConfigurationManager.ConnectionStrings[i].Name;
                    kv.Value = ((MyPlatform.Utils.DBEnum)Convert.ToInt32(dbType)).ToString();
                    li.Add(kv);
                }
            }            
            result.S = true;
            result.D = li;
            return MyPlatform.Utils.MyResponseMessage.SuccessJson<ReturnData>(result);
        }


    }
}
