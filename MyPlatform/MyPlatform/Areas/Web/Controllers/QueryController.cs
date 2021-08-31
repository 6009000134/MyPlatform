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
        [HttpPost]
        public HttpResponseMessage List([FromBody] Dictionary<string,string>dic)
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
    }
}
