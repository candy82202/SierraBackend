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

namespace NiceAdmin.Controllers.Orders
{
    [DirectToUnAuthorize(Roles = "admin,manager,dessertSale,lessonSale")]
    public class LessonOrderDetailsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: LessonOrderDetails
        [OverrideAuthorization]
        [DirectToUnAuthorize(Roles = "admin,manager,dessertSale,lessonSale,staff")]
        public ActionResult Index()
        {
            var lessonOrderDetails = db.LessonOrderDetails.Include(l => l.Lesson).Include(l => l.LessonOrder);
            return View(lessonOrderDetails.ToList());
        }

        // GET: LessonOrderDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LessonOrderDetail lessonOrderDetail = db.LessonOrderDetails.Find(id);
            if (lessonOrderDetail == null)
            {
                return HttpNotFound();
            }
            return View(lessonOrderDetail);
        }

        // GET: LessonOrderDetails/Create
        public ActionResult Create()
        {
            ViewBag.LessonId = new SelectList(db.Lessons, "LessonId", "LessonTitle");
            ViewBag.LessonOrderId = new SelectList(db.LessonOrders, "LessonOrderId", "Note");
            return View();
        }

        // POST: LessonOrderDetails/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LessonOrderDetailId,LessonOrderId,LessonId,LessonTitle,NumberOfPeople,LessonUnitPrice,Subtotal")] LessonOrderDetail lessonOrderDetail)
        {
            if (ModelState.IsValid)
            {
                db.LessonOrderDetails.Add(lessonOrderDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.LessonId = new SelectList(db.Lessons, "LessonId", "LessonTitle", lessonOrderDetail.LessonId);
            ViewBag.LessonOrderId = new SelectList(db.LessonOrders, "LessonOrderId", "Note", lessonOrderDetail.LessonOrderId);
            return View(lessonOrderDetail);
        }

        // GET: LessonOrderDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LessonOrderDetail lessonOrderDetail = db.LessonOrderDetails.Find(id);
            if (lessonOrderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.LessonId = new SelectList(db.Lessons, "LessonId", "LessonTitle", lessonOrderDetail.LessonId);
            ViewBag.LessonOrderId = new SelectList(db.LessonOrders, "LessonOrderId", "Note", lessonOrderDetail.LessonOrderId);
            return View(lessonOrderDetail);
        }

        // POST: LessonOrderDetails/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LessonOrderDetailId,LessonOrderId,LessonId,LessonTitle,NumberOfPeople,LessonUnitPrice,Subtotal")] LessonOrderDetail lessonOrderDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lessonOrderDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LessonId = new SelectList(db.Lessons, "LessonId", "LessonTitle", lessonOrderDetail.LessonId);
            ViewBag.LessonOrderId = new SelectList(db.LessonOrders, "LessonOrderId", "Note", lessonOrderDetail.LessonOrderId);
            return View(lessonOrderDetail);
        }

        // GET: LessonOrderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LessonOrderDetail lessonOrderDetail = db.LessonOrderDetails.Find(id);
            if (lessonOrderDetail == null)
            {
                return HttpNotFound();
            }
            return View(lessonOrderDetail);
        }

        // POST: LessonOrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LessonOrderDetail lessonOrderDetail = db.LessonOrderDetails.Find(id);
            db.LessonOrderDetails.Remove(lessonOrderDetail);
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
