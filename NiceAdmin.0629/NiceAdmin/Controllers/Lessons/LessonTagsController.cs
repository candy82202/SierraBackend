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
    public class LessonTagsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: LessonTags
        [OverrideAuthorization]
        [DirectToUnAuthorize(Roles = "admin,lessonSale,staff")]
        public ActionResult Index()
        {
            var lessonTags = db.LessonTags.Include(l => l.Lesson);
            return View(lessonTags.ToList());
        }

        // GET: LessonTags/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LessonTag lessonTag = db.LessonTags.Find(id);
            if (lessonTag == null)
            {
                return HttpNotFound();
            }
            return View(lessonTag);
        }

        // GET: LessonTags/Create
        public ActionResult Create()
        {
            ViewBag.LessonId = new SelectList(db.Lessons, "LessonId", "LessonTitle");
            return View();
        }

        // POST: LessonTags/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LessonTagId,LessonId,LessonTagName")] LessonTag lessonTag)
        {
            if (ModelState.IsValid)
            {
                db.LessonTags.Add(lessonTag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LessonId = new SelectList(db.Lessons, "LessonId", "LessonTitle", lessonTag.LessonId);
            return View(lessonTag);
        }

        // GET: LessonTags/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LessonTag lessonTag = db.LessonTags.Find(id);
            if (lessonTag == null)
            {
                return HttpNotFound();
            }
            ViewBag.LessonId = new SelectList(db.Lessons, "LessonId", "LessonTitle", lessonTag.LessonId);
            return View(lessonTag);
        }

        // POST: LessonTags/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LessonTagId,LessonId,LessonTagName")] LessonTag lessonTag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lessonTag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LessonId = new SelectList(db.Lessons, "LessonId", "LessonTitle", lessonTag.LessonId);
            return View(lessonTag);
        }

        // GET: LessonTags/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LessonTag lessonTag = db.LessonTags.Find(id);
            if (lessonTag == null)
            {
                return HttpNotFound();
            }
            return View(lessonTag);
        }

        // POST: LessonTags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LessonTag lessonTag = db.LessonTags.Find(id);
            db.LessonTags.Remove(lessonTag);
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
