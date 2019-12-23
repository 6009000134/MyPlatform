using MyPlatform.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyPlatform.Areas.Web.Controllers
{
    public class LoginController : ApiController
    {
        MyPlatform.BLL.Sys_Users userBLL = new MyPlatform.BLL.Sys_Users();
        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage Login(Sys_Users model)
        {
            ReturnData result = new ReturnData();
            if (userBLL.Exists(model))
            {                
                result.S = true;
                result.D = userBLL.GetModelByAccount(model.Account);                
            }
            else
            {
                result.SetErrorMsg("账号/密码错误！");
            }
            HttpResponseMessage rep = MyPlatform.Utils.MyResponseMessage.SuccessJson<ReturnData>(result);
            //rep.Headers.Add("Access-Control-Allow-Headers", "Token");
            rep.Headers.Add("Access-Control-Expose-Headers","Token");
            rep.Headers.Add("Token",model.Account);
            //rep.Headers.
            return rep;
        }
    }
}
 