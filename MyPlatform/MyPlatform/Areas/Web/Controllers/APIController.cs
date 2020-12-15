using MyPlatform.Model;
using MyPlatform.Utils;
using System;
using System.Collections.Generic;
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

        [HttpPost]
        public HttpResponseMessage List([FromBody]string codition)
        {
            ReturnData result = new ReturnData();
            try
            {
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
    }
}
