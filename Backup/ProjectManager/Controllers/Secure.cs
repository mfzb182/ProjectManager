using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ProjectManager.Controllers
{
    class SecureAttribute : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool CanAccess = true;
            ModelContainer db = new ModelContainer();

            string Username = string.Empty;
            string Email = string.Empty;
            try
            {
                if (CanAccess)
                {
                    Username = filterContext.HttpContext.Session["Username"].ToString();
                    Email = filterContext.HttpContext.Session["Email"].ToString();
                }
            }
            catch
            {
                CanAccess = false;
            }


            if (CanAccess)
            {
                try
                {
                    var user = db.Employees.Find(Username);
                    if (user.Username.ToLower() != Username.ToLower())
                        CanAccess = false;
                }
                catch
                {
                    CanAccess = false;
                }
            }

            if (!CanAccess)
                filterContext.Result = new HttpStatusCodeResult(404);
        }
    }
}
