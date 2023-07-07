using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NiceAdmin.Filters
{
	public class CustomAuthorizeAttribute : AuthorizeAttribute
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

		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			if (httpContext.User.Identity.IsAuthenticated)
			{
				// 檢查使用者是否同時擁有所有指定的角色
				if (!string.IsNullOrEmpty(Roles))
				{
					//以逗號分隔 建立陣列
					var requiredRoles = Roles.Split(',');

					foreach (var role in requiredRoles)
					{
						if (!httpContext.User.IsInRole(role))
						{
							return false; // 若有任一角色不符合要求，未授權存取
						}
					}
					return true; // 同時擁有所有指定的角色的使用者被授權存取
				}
			}
			return false; // 角色不符合要求，未授權存取
		}
	}
}