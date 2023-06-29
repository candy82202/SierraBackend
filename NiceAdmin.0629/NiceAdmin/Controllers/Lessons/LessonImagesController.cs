using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NiceAdmin.Models.EFModels;

namespace NiceAdmin.Controllers.Lessons
{
    public class LessonImagesController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: LessonImages
        public ActionResult Index()
        {
            var lessonImages = db.LessonImages.Include(l => l.Lesson);
            return View(lessonImages.ToList());
        }

        // GET: LessonImages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LessonImage lessonImage = db.LessonImages.Find(id);
            if (lessonImage == null)
            {
                return HttpNotFound();
            }
            return View(lessonImage);
        }

        // GET: LessonImages/Create
        public ActionResult Create()
        {
            ViewBag.LessonId = new SelectList(db.Lessons, "LessonId", "LessonTitle");
            return View();
        }

        // POST: LessonImages/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LessonImageId,LessonId,LessonImageName")] LessonImage lessonImage)
        {
            if (ModelState.IsValid)
            {
                db.LessonImages.Add(lessonImage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LessonId = new SelectList(db.Lessons, "LessonId", "LessonTitle", lessonImage.LessonId);
            return View(lessonImage);
        }

        // GET: LessonImages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LessonImage lessonImage = db.LessonImages.Find(id);
            if (lessonImage == null)
            {
                return HttpNotFound();
            }
            ViewBag.LessonId = new SelectList(db.Lessons, "LessonId", "LessonTitle", lessonImage.LessonId);
            return View(lessonImage);
        }

        // POST: LessonImages/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LessonImageId,LessonId,LessonImageName")] LessonImage lessonImage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lessonImage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LessonId = new SelectList(db.Lessons, "LessonId", "LessonTitle", lessonImage.LessonId);
            return View(lessonImage);
        }

        // GET: LessonImages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LessonImage lessonImage = db.LessonImages.Find(id);
            if (lessonImage == null)
            {
                return HttpNotFound();
            }
            return View(lessonImage);
        }

        // POST: LessonImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LessonImage lessonImage = db.LessonImages.Find(id);
            db.LessonImages.Remove(lessonImage);
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
