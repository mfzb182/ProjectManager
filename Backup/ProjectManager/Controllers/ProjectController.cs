using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectManager.Models;

namespace ProjectManager.Controllers
{
    [Secure]
    public class ProjectController : Controller
    {
        static public string Date(int Year, int Month, int Day)
        {
            return "";
        }
        static public string Today = Date(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        private ModelContainer db = new ModelContainer();

        //
        // GET: /Project/

        public ActionResult Index(int Id = 10)
        {
            var projects = db.Projects.OrderByDescending(m => m.Id).Take(Id);
            return View(projects.ToList());
        }

        //
        // GET: /Project/Details/5

        public ActionResult Details(int id = 0)
        {
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        //
        // GET: /Project/Create

        public ActionResult Create()
        {
            ViewBag.EmployeeUsername = new SelectList(db.Employees, "Username", "Fullname");
            ViewBag.TypeId = new SelectList(db.Activities, "Id", "Title");
            return View();
        }

        //
        // POST: /Project/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Project project, int Day, int Month, int Year)
        {
            if (Year > 2014) {
                if (Month < 13)
                {
                    if (Day < 31)
                    {
                        project.Start = Date(Year, Month, Day);
                        project.End = Today;
                        if (ModelState.IsValid)
                        {
                            db.Projects.Add(project);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                    }
                }
            }


            ViewBag.EmployeeUsername = new SelectList(db.Employees, "Username", "Fullname", project.EmployeeUsername);
            ViewBag.TypeId = new SelectList(db.Activities, "Id", "Title", project.TypeId);
            return View(project);



        }

        //
        // GET: /Project/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeUsername = new SelectList(db.Employees, "Username", "Fullname", project.EmployeeUsername);
            ViewBag.TypeId = new SelectList(db.Activities, "Id", "Title", project.TypeId);
            return View(project);
        }

        //
        // POST: /Project/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeUsername = new SelectList(db.Employees, "Username", "Fullname", project.EmployeeUsername);
            ViewBag.TypeId = new SelectList(db.Activities, "Id", "Title", project.TypeId);
            return View(project);
        }

        //
        // GET: /Project/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        //
        // POST: /Project/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}