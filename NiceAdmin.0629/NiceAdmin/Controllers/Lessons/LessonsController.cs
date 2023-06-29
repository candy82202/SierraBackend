﻿using System;
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
    public class LessonsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Lessons
        public ActionResult Index()
        {
            var lessons = db.Lessons.Include(l => l.LessonCategory).Include(l => l.Teacher);
            return View(lessons.ToList());
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
            ViewBag.LessonCategoryId = new SelectList(db.LessonCategories, "LessonCategoryId", "LessonCategoryName");
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "TeacherName");
            return View();
        }

        // POST: Lessons/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LessonId,LessonCategoryId,TeacherId,LessonTitle,LessonInfo,LessonDetail,LessonDessert,LessonTime,LessonHours,MaximumCapacity,LessonPrice,LessonStatus,CreateTime")] Lesson lesson)
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