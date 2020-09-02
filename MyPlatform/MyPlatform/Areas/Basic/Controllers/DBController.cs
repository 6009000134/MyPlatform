using MyPlatform.Model.Enum;
using MyPlatform.Common.Cache;
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
    /// <summary>
    /// 数据库连接信息
    /// </summary>
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
            try
            {
                Dictionary<string, string> dicDBList = new Dictionary<string, string>();
                List<KeyValueData> li = new List<KeyValueData>();
                DataCache cache = new DataCache();
                object DBList = cache.GetCache("Sys_DBList");
                if (DBList == null)
                {
                    for (int i = 0; i < ConfigurationManager.ConnectionStrings.Count; i++)
                    {
                        string dbType = ConfigurationManager.AppSettings.Get(ConfigurationManager.ConnectionStrings[i].Name);
                        if (!string.IsNullOrEmpty(dbType))
                        {
                            KeyValueData kv = new KeyValueData();
                            kv.Key = ConfigurationManager.ConnectionStrings[i].Name;
                            kv.Value = ((DBEnum)Convert.ToInt32(dbType)).ToString();
                            li.Add(kv);
                        }
                    }
                    cache.SetCache("Sys_DBList", li);
                    result.D = li;
                }
                else
                {
                    result.D = DBList;
                }
                result.S = true;
            }
            catch (Exception ex)
            {
                result.S = false;
                result.SetErrorMsg("获取数据库信息失败："+ex.Message);                
            }

            return MyPlatform.Utils.MyResponseMessage.SuccessJson<ReturnData>(result);
        }


    }
}
