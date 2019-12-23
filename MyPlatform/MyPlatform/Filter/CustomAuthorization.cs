using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace MyPlatform.Filter
{
    public class CustomAuthorization:AuthorizeAttribute
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            // 
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute),true)||filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute),true))
            {                
                return;
            }
            // TODO:授权验证
        }


        //public void OnAuthorization(HttpActionContext actionContext)
        //{
        //    // 对AllowAnonymousAttribute的Action不进行校验
        //    if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
        //    {
        //        return;
        //    }
        //    // 校验token和权限
        //    string token = "";
        //    IEnumerable<string> iToken;
        //    if (actionContext.Request.Headers.TryGetValues("Token", out iToken))
        //    {
        //        token = iToken.ToString();
        //    }
        //    else
        //    {
        //        return actionContext.
        //    }
        //}
    }
}