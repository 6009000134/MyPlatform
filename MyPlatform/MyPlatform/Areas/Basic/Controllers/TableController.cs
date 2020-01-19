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

            ReturnData result = new ReturnData();
            //校验是否存在同名表
            if (tableBLL.Exists(model.TableName, model.DBName, model.DBType))
            {
                result.SetErrorMsg("数据库已经存在同名表！！");
            }
            else
            {
                //创建表
                //tableBLL.Add(model);
            }
            return MyResponseMessage.SuccessJson<ReturnData>(result);
        }

        [HttpPost]
        public HttpResponseMessage List([FromBody]string DBName)
        {
            ReturnData result = new ReturnData();            
            result.D=tableBLL.GetListByDBName(DBName);
            result.S = true;
            return MyResponseMessage.SuccessJson<ReturnData>(result);
        }
    }
}
