using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NiceAdmin.Models.EFModels;

namespace NiceAdmin.Controllers.CourseController
{
    public class CourseController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Course
        public ActionResult Index()
        {
            var courses = db.Courses.Include(c => c.CoursesCategory).Include(c => c.Teacher);
            return View(courses.ToList());
        }

        // GET: Course/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            return View(cours);
        }

        // GET: Course/Create
        public ActionResult Create()
        {
            ViewBag.CourseCategoryId = new SelectList(db.CoursesCategories, "CourseCategoryId", "CoursesCategoryName");
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "TeacherName");
            return View();
        }

        // POST: Course/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseId,CourseCategoryId,TeacherId,CourseTitle,CourseInfo,CourseDetail,CourseDessert,CourseTime,CourseHours,MaximumCapacity,CoursePrice,CourseStatus,CreateTime")] Cours cours)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(cours);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseCategoryId = new SelectList(db.CoursesCategories, "CourseCategoryId", "CoursesCategoryName", cours.CourseCategoryId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "TeacherName", cours.TeacherId);
            return View(cours);
        }

        // GET: Course/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseCategoryId = new SelectList(db.CoursesCategories, "CourseCategoryId", "CoursesCategoryName", cours.CourseCategoryId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "TeacherName", cours.TeacherId);
            return View(cours);
        }

        // POST: Course/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseId,CourseCategoryId,TeacherId,CourseTitle,CourseInfo,CourseDetail,CourseDessert,CourseTime,CourseHours,MaximumCapacity,CoursePrice,CourseStatus,CreateTime")] Cours cours)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cours).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseCategoryId = new SelectList(db.CoursesCategories, "CourseCategoryId", "CoursesCategoryName", cours.CourseCategoryId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "TeacherName", cours.TeacherId);
            return View(cours);
        }

        // GET: Course/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            return View(cours);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cours cours = db.Courses.Find(id);
            db.Courses.Remove(cours);
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
