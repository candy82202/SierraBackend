using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NiceAdmin.Controllers
{
    public class ErrorHandlerController : Controller
    {
        // GET: ErrorHandler
        public ActionResult NotFound()
        {
            return View();
        }
        
      
    }
}