using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManager.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        static Random random = new Random();
        public static string GetRandomHexNumber(int digits)
        {
            byte[] buffer = new byte[digits / 2];
            random.NextBytes(buffer);
            string result = String.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
            if (digits % 2 == 0)
                return result;
            return result + random.Next(16).ToString("X");
        }

        public ActionResult Index()
        {
            using (ModelContainer db = new ModelContainer())
            {
                string Stat = "[";
                Employee[] em = db.Employees.ToList().ToArray();
                foreach (Employee pl in em)
                {
                    try
                    {
                        Stat += "{" + "value: " + db.Projects.Where(m => m.EmployeeUsername == pl.Username).Sum(x => x.Score).ToString() + ", color: \"" + "#" + GetRandomHexNumber(6) + "\", label: \"" + pl.Fullname + "\" },";
                    }
                    catch { }
                }
                Stat = Stat + "]";
                Stat = Stat.Replace(",]", "]");
                ViewBag.Stat = Stat;
            }
            return View();
        }

    }
}
