using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NiceAdmin.Filters;
using NiceAdmin.Models.EFModels;

namespace NiceAdmin.Controllers.Lessons
{
    [DirectToUnAuthorize(Roles = "admin,lessonSale")]
    public class LessonCategoriesController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: LessonCategories
        [OverrideAuthorization]
        [DirectToUnAuthorize(Roles = "admin,lessonSale,staff")]
        public ActionResult Index()
        {
            return View(db.LessonCategories.ToList());
        }

        // GET: LessonCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LessonCategory lessonCategory = db.LessonCategories.Find(id);
            if (lessonCategory == null)
            {
                return HttpNotFound();
            }
            return View(lessonCategory);
        }

        // GET: LessonCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LessonCategories/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LessonCategoryId,LessonCategoryName")] LessonCategory lessonCategory)
        {
            if (ModelState.IsValid)
            {
                db.LessonCategories.Add(lessonCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lessonCategory);
        }

        // GET: LessonCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LessonCategory lessonCategory = db.LessonCategories.Find(id);
            if (lessonCategory == null)
            {
                return HttpNotFound();
            }
            return View(lessonCategory);
        }

        // POST: LessonCategories/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LessonCategoryId,LessonCategoryName")] LessonCategory lessonCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lessonCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lessonCategory);
        }

        // GET: LessonCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LessonCategory lessonCategory = db.LessonCategories.Find(id);
            if (lessonCategory == null)
            {
                return HttpNotFound();
            }
            return View(lessonCategory);
        }

        // POST: LessonCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LessonCategory lessonCategory = db.LessonCategories.Find(id);
            db.LessonCategories.Remove(lessonCategory);
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
