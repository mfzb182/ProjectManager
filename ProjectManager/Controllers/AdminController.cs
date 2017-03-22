using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjectManager.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        [Secure]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            WebClient webclient = new WebClient();
            string Code = string.Empty;
            string _Username = string.Empty;
            string Email = string.Empty;
            string WebApp = "http://abnt.ir/";
            string Key = "f26841cf-564a-484b-8701-2b024a1b7176";
            string Name = "projectmanager-11214";

            try
            {
                Code = Session["Code"].ToString();
                _Username = Session["Username"].ToString();
            }
            catch { }
            if (Code != "" && Code != null)
            {
                string Url = WebApp + "[webapp]/en-US/WebAppUser" + "?Name=" + Name + "&Key=" + Key + "&Code=" + Code + "&Export=";

                try
                {
                    _Username = webclient.DownloadString(HttpUtility.UrlDecode(Url + "Username"));
                }
                catch { }
                Session["Username"] = _Username;
                Session["Email"] = webclient.DownloadString(HttpUtility.UrlDecode(Url + "Email"));
                ViewBag.Username = _Username;

                if (_Username == "")
                {
                    if (_Username != "" && _Username != null)
                    {
                    }
                    else
                    {
                        Response.Redirect(WebApp + "[webapp]/en-US/WebAppAllow/Index" + "?Name=" + Name + "&Code=" + Code);
                    }
                }
            }
            else
            {
                try
                {
                    Session["Code"] = webclient.DownloadString(HttpUtility.UrlDecode(WebApp + "[webapp]/en-US/WebAppCode")).ToString();
                }
                catch { }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Remove("Username");
            return Redirect("~/Admin/Login");
        }

    }
}
