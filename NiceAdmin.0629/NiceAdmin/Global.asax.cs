using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace NiceAdmin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Authenticaterequest()
        {
            if (!Request.IsAuthenticated) return;

			// 取得FormsIdentity
			var id = (FormsIdentity)User.Identity;
            // 取出驗證票
            var ticket = id.Ticket;
            string rolesStr = ticket.UserData;
            string[] rolesAry = rolesStr.Split(',');

            IPrincipal currentUser = new GenericPrincipal(User.Identity, rolesAry);
            Context.User = currentUser;
        }

	}
}
