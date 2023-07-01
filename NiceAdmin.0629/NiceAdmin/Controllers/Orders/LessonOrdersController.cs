using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NiceAdmin.Models.EFModels;

namespace NiceAdmin.Controllers.Orders
{
    public class LessonOrdersController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: LessonOrders
        public ActionResult Index()
        {
            var lessonOrders = db.LessonOrders.Include(l => l.Coupon).Include(l => l.Member).Include(l => l.OrderStatus);
            return View(lessonOrders.ToList());
        }

        // GET: LessonOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LessonOrder lessonOrder = db.LessonOrders.Find(id);
            if (lessonOrder == null)
            {
                return HttpNotFound();
            }
            return View(lessonOrder);
        }

        // GET: LessonOrders/Create
        public ActionResult Create()
        {
            ViewBag.CouponId = new SelectList(db.Coupons, "CouponId", "CouponName");
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "MemberName");
            ViewBag.LessonOrderStatusId = new SelectList(db.OrderStatuses, "OrderStatusId", "StatusName");
            return View();
        }

        // POST: LessonOrders/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LessonOrderId,MemberId,LessonOrderStatusId,CouponId,CreateTime,LessonOrderTotal,Note,OrderCancellationReason,DiscountInfo")] LessonOrder lessonOrder)
        {
            if (ModelState.IsValid)
            {
                db.LessonOrders.Add(lessonOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CouponId = new SelectList(db.Coupons, "CouponId", "CouponName", lessonOrder.CouponId);
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "MemberName", lessonOrder.MemberId);
            ViewBag.LessonOrderStatusId = new SelectList(db.OrderStatuses, "OrderStatusId", "StatusName", lessonOrder.LessonOrderStatusId);
            return View(lessonOrder);
        }

        // GET: LessonOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LessonOrder lessonOrder = db.LessonOrders.Find(id);
            if (lessonOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.CouponId = new SelectList(db.Coupons, "CouponId", "CouponName", lessonOrder.CouponId);
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "MemberName", lessonOrder.MemberId);
            ViewBag.LessonOrderStatusId = new SelectList(db.OrderStatuses, "OrderStatusId", "StatusName", lessonOrder.LessonOrderStatusId);
            return View(lessonOrder);
        }

        // POST: LessonOrders/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LessonOrderId,MemberId,LessonOrderStatusId,CouponId,CreateTime,LessonOrderTotal,Note,OrderCancellationReason,DiscountInfo")] LessonOrder lessonOrder)
        {
            if (ModelState.IsValid)
            {
                lessonOrder.CreateTime = DateTime.Now;
                db.Entry(lessonOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CouponId = new SelectList(db.Coupons, "CouponId", "CouponName", lessonOrder.CouponId);
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "MemberName", lessonOrder.MemberId);
            ViewBag.LessonOrderStatusId = new SelectList(db.OrderStatuses, "OrderStatusId", "StatusName", lessonOrder.LessonOrderStatusId);
            return View(lessonOrder);
        }

        // GET: LessonOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LessonOrder lessonOrder = db.LessonOrders.Find(id);
            if (lessonOrder == null)
            {
                return HttpNotFound();
            }
            return View(lessonOrder);
        }

        // POST: LessonOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LessonOrder lessonOrder = db.LessonOrders.Find(id);
            lessonOrder.CreateTime = DateTime.Now;
            db.LessonOrders.Remove(lessonOrder);
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
