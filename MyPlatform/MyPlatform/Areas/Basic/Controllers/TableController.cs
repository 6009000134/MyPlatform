using MyPlatform.Model;
using MyPlatform.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyPlatform.Areas.Basic.Controllers
{
    public class TableController : ApiController
    {
        MyPlatform.BLL.Sys_Tables tableBLL = new MyPlatform.BLL.Sys_Tables();
        /// <summary>
        /// 创建系统表，默认创建ID、Deleted、CreateBy、CreateDate、UpdateBy、UpdateDate字段
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Add(Sys_Tables model)
        {
            //校验是否存在同名表

            //创建表
            ReturnData result = new ReturnData();
            return MyResponseMessage.SuccessJson<ReturnData>(result);
        }
    }
}
