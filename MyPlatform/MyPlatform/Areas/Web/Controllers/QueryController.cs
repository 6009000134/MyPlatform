using MyPlatform.BLL;
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
    public class QueryController : ApiController
    {
        private QueryBLL bll = new QueryBLL();
        [HttpPost]
        public HttpResponseMessage List([FromBody] Dictionary<string, string> dic)
        {
            ReturnData result = new ReturnData();
            try
            {

            }
            catch (Exception ex)
            {
                ex.Source += "QueryControll List";
                throw ex;
            }

            return MyResponseMessage.SuccessJson(result);
        }
        [HttpPost]
        public HttpResponseMessage Query([FromBody]int id)
        {
            ReturnData result = new ReturnData();
            try
            {
                //根据ID获取对象信息                
                result.D = bll.Query(id);
                result.S = true;
            }
            catch (Exception ex)
            {
                ex.Source += "QueryControll List";
                throw ex;
            }
            return MyResponseMessage.SuccessJson(result);
        }
    }
}
