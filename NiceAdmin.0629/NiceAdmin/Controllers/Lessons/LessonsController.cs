using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NiceAdmin.Models.EFModels;
using NiceAdmin.Models.ViewModels;
using NiceAdmin.Models.ViewModels.LessonsVM;

namespace NiceAdmin.Controllers.Lessons
{
    public class LessonsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Lessons
        public ActionResult Index(LessonCriteria lessonCriteria)
        {
            PrepareCategoryDataSource(lessonCriteria.LessonCategoryId);
            //ViewBag.DessertCriteria = dessertCriteria;
            ViewBag.Criteria = lessonCriteria;

            var query = db.Lessons.Include(l => l.LessonCategory);
            #region where
            if (string.IsNullOrEmpty(lessonCriteria.LessonTitle) == false)
            {
                query = query.Where(l => l.LessonTitle.Contains(lessonCriteria.LessonTitle));
            }
            if (lessonCriteria.LessonCategoryId != null && lessonCriteria.LessonCategoryId.Value > 0)
            {
                query = query.Where(l => l.LessonCategoryId == lessonCriteria.LessonCategoryId.Value);
            }
            if (lessonCriteria.MinPrice.HasValue)
            {
                query = query.Where(l => l.LessonPrice >= lessonCriteria.MinPrice.Value);
            }
            if (lessonCriteria.MaxPrice.HasValue)
            {
                query = query.Where(l => l.LessonPrice <= lessonCriteria.MaxPrice.Value);
            }
            #endregion
            var lessons = query.ToList().Select(d => d.ToIndexVM());
            return View(lessons);
        }

        private void PrepareCategoryDataSource(int? lessonCategoryName)
        {
            var categories = db.LessonCategories.ToList().Prepend(new LessonCategory());
            ViewBag.LessonCategoryId = new SelectList(categories, "LessonCategoryId", "LessonCategoryName", lessonCategoryName);
        }

        // GET: Lessons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson lesson = db.Lessons.Find(id);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            return View(lesson);
        }

        // GET: Lessons/Create
        public ActionResult Create()
        {
            PrepareCategoryDataSource(null);
            return View();
        }

        // POST: Lessons/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Lesson lesson)//LessonCreateVM vm)
        {
            if (ModelState.IsValid)
            {
                db.Lessons.Add(lesson);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LessonCategoryId = new SelectList(db.LessonCategories, "LessonCategoryId", "LessonCategoryName", lesson.LessonCategoryId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "TeacherName", lesson.TeacherId);
            return View(lesson);
        }

        // GET: Lessons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson lesson = db.Lessons.Find(id);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            ViewBag.LessonCategoryId = new SelectList(db.LessonCategories, "LessonCategoryId", "LessonCategoryName", lesson.LessonCategoryId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "TeacherName", lesson.TeacherId);
            return View(lesson);
        }

        // POST: Lessons/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LessonId,LessonCategoryId,TeacherId,LessonTitle,LessonInfo,LessonDetail,LessonDessert,LessonTime,LessonHours,MaximumCapacity,LessonPrice,LessonStatus,CreateTime")] Lesson lesson)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lesson).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LessonCategoryId = new SelectList(db.LessonCategories, "LessonCategoryId", "LessonCategoryName", lesson.LessonCategoryId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "TeacherName", lesson.TeacherId);
            return View(lesson);
        }

        // GET: Lessons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson lesson = db.Lessons.Find(id);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            return View(lesson);
        }

        // POST: Lessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lesson lesson = db.Lessons.Find(id);
            db.Lessons.Remove(lesson);
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
