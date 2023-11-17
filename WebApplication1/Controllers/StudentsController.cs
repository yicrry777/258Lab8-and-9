using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class StudentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private object students;

        public object GetStudents()
        {
            return students;
        }

        // GET: Students
        public ActionResult Index(string category, string search, object students)
        {
            //instantiate a new view model
            StudentsIndexViewModel viewModel = new StudentsIndexViewModel();
            var products = db.Students.Include(p => p.Campus);
            //find the products where either the product name field contains search,the product 
            //description contains search, or the product's category name contains search
            if (!String.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.Name.Contains(search) ||
                p.Address.Contains(search) ||
                p.Campus.Name.Contains(search));
                // ViewBag.Search = search;
                viewModel.Search = search;
            }
            //group search results into categories and count how many items in each category
            viewModel.CatsWithCount = from matchingStudents in products
                                      where
                                      matchingStudents.CampusID != null
                                      group matchingStudents by
                                      matchingStudents.Campus.Name into
                                      catGroup
                                      select new CampusWithCount()
                                      {
                                          CampusName = catGroup.Key,
                                          StudentCount = catGroup.Count()
                                      };
            if (!String.IsNullOrEmpty(category))
            {
                products = products.Where(p => p.Campus.Name == category);
            }
            //ViewBag.Category = new SelectList(categories); 
            viewModel.Students = products;
            // return View(products.ToList());
            return View(viewModel);
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.CampusID = new SelectList(db.Campus, "ID", "Name");
            return View();
        }

        // POST: Students/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性；有关
        // 更多详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Address,CampusID")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CampusID = new SelectList(db.Campus, "ID", "Name", student.CampusID);
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.CampusID = new SelectList(db.Campus, "ID", "Name", student.CampusID);
            return View(student);
        }

        // POST: Students/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性；有关
        // 更多详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Address,CampusID")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CampusID = new SelectList(db.Campus, "ID", "Name", student.CampusID);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
