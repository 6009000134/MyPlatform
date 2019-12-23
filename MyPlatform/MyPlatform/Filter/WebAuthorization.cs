using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPlatform.Filter
{
    public class WebAuthorization: AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //             filterContext.RequestContext.HttpContext.Request.Headers

            string s=filterContext.HttpContext.Request.Headers.Get("Content-Type");
            string s2 = filterContext.HttpContext.Request.Headers.Get("Token");
            // 
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                return;
            }
            // TODO:授权验证
            //filterContext.Result(new JsonResult() { Data="123"});
            if (!string.IsNullOrEmpty(filterContext.HttpContext.Request.Headers.Get("Token")))
            {
                base.OnAuthorization(filterContext);
            }
            else
            {
                filterContext.Result = new ContentResult() { Content = "没有Token" };
            }
            //if (filterContext.RequestContext.)
            //{

            //}
        }
    }
}