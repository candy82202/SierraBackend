using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NiceAdmin.Filters
{
    public class DirectToUnAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                // 使用者已登入，但角色不符合要求，重新導向至無權使用頁面
                filterContext.Result = new ViewResult
                {
                    ViewName = "_Unauthorized" // 無權使用頁面的視圖名稱
                };
            }
            else
            {
                // 使用者未登入，重新導向至登入頁面
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}