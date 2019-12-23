using MyPlatform.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MyPlatform.Filter
{
    public class ApiAuthorization: AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // 对AllowAnonymousAttribute的Action不进行校验
            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
            {
                return;
            }
            // 校验token和权限
            string token = "";
            IEnumerable<string> iToken;
            if (actionContext.Request.Headers.TryGetValues("Token", out iToken))
            {
                token = iToken.First();
                //TODO:校验token，获取token
                bool TokenIsEffection = true;
                if (TokenIsEffection)
                {
                    //TODO: 验证是否有权限
                    bool IsAuthorized = true;
                    if (IsAuthorized)
                    {
                        base.OnAuthorization(actionContext);
                    }
                    else
                    {
                        actionContext.Response = MyPlatform.Utils.MyResponseMessage.UnAuthorized("访问的模块未授权，请联系管理员！");
                    }
                }
                else
                {
                    actionContext.Response = MyPlatform.Utils.MyResponseMessage.UnAuthorized("Token无效，请重新登录或联系管理员！");
                }
            }
            else
            {
                actionContext.Response=MyPlatform.Utils.MyResponseMessage.UnAuthorized("没有Token令牌，请重新登录或联系管理员！");
            }
        }
    }
}