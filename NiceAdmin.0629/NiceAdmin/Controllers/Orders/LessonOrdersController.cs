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
using NiceAdmin.Models.ViewModels.OrdersVM;
using NiceAdmin.Views.DessertOrders;

namespace NiceAdmin.Controllers.Orders
{
    [DirectToUnAuthorize(Roles = "admin")]
    public class LessonOrdersController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: LessonOrders
        [OverrideAuthorization]
        [DirectToUnAuthorize(Roles = "admin,staff")]
        public ActionResult Index(LessonOrderCriteria criteria)
        {
            
            PrepareOrderDataSource(criteria.OrderStatusId);
            ViewBag.Criteria = criteria;
            //查詢紀錄
            #region where
            var query = db.LessonOrders.Include(l => l.LessonOrderDetails).Include(l => l.Coupon).Include(l => l.Member).Include(l => l.OrderStatus);
            if (string.IsNullOrEmpty(criteria.MemberName) == false)
            {
                query = query.Where(l => l.Member.MemberName.Contains(criteria.MemberName));
            }
            if (criteria.OrderStatusId != null && criteria.OrderStatusId.Value > 0)
            {
                query = query.Where(l => l.OrderStatus.OrderStatusId == criteria.OrderStatusId.Value);
            }
            if (criteria.MinPrice.HasValue)
            {
                query = query.Where(l => l.LessonOrderTotal >= criteria.MinPrice.Value);
            }
            if (criteria.MaxPrice.HasValue)
            {
                query = query.Where(l => l.LessonOrderTotal <= criteria.MaxPrice.Value);
            }
            #endregion
        
            var lessonOrders = query.ToList().Select(l => l.TOIndexVM());
            return View(lessonOrders);
       

            //var lessonOrders = db.LessonOrders.Include(l => l.LessonOrderDetails).Include(l => l.Coupon).Include(l => l.Member).Include(l => l.OrderStatus)
            //    .ToList()
            //    .Select(l => l.TOIndexVM());
            //return View(lessonOrders);
        }

        private void PrepareOrderDataSource(int? orderStatusId)
        {
            var status = db.OrderStatuses.ToList().Prepend(new OrderStatus());
            ViewBag.OrderStatusId = new SelectList(status, "OrderStatusId", "StatusName", orderStatusId);
        }
        public PartialViewResult TopSellingLessons()//最熱銷前五名課程
        {
            var topLessons = db.LessonOrderDetails
        .Join(db.Lessons, od => od.LessonId, l => l.LessonId, (od, l) => new { od, l })
        .GroupBy(g => new { g.l.LessonTitle })
        .Select(g => new TopSellingLessonsVM
        {
            LessonTitle = g.Key.LessonTitle,
            NumberOfPeople = g.Sum(x => x.od.NumberOfPeople)
        })
        .OrderByDescending(x => x.NumberOfPeople)
        .ThenBy(x => x.LessonTitle)
        .Take(5)
        .ToList();

            //var lessonIndexVMList = topLessons.Select(l => new TopSellingLessonsVM
            //{
            //    LessonTitle = l.LessonTitle,
            //    NumberOfPeople = l.NumberOfPeople
            //}).ToList();
            return PartialView("TopSellingLessons", topLessons);

            //return PartialView("TopSellingLessons", lessonIndexVMList);

        }
        public PartialViewResult TopSellingLessonsOrder()//前十熱銷的課程訂單
        {
            var topSellingOrders = db.LessonOrderDetails
                            .Where(x => x.LessonOrder.LessonOrderStatusId == 3)
                            .GroupBy(x => x.LessonId)
                            .Select((g) => new TopSellingLessonsOrder
                            {
                               
                                LessonId = g.Key,
                                LessonTitle = g.FirstOrDefault().LessonTitle,
                                TotalNumberOfPeople = g.Sum(x => x.NumberOfPeople),
                                TotalAmount = g.Sum(x => x.LessonOrder.LessonOrderTotal),
                            })
                        .OrderByDescending(x => x.TotalAmount)
                        .ThenByDescending(x => x.TotalNumberOfPeople)
                         .Take(10).ToList();




            return PartialView("TopSellingLessonsOrder", topSellingOrders);
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
