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
using NiceAdmin.Models.ViewModels.TeachersVM;
using NiceAdmin.Views.DessertOrders;

namespace NiceAdmin.Controllers.Orders
{
    [DirectToUnAuthorize(Roles = "admin")]
    public class DessertOrdersController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: DessertOrders
        [OverrideAuthorization]
        [DirectToUnAuthorize(Roles = "admin,staff")]
        public ActionResult Index(DessertOrderCriteria criteria)
        {
            PrepareOrderDataSource(criteria.OrderStatusId);
            ViewBag.Criteria = criteria;

            //查詢紀錄
            #region where
            var query = db.DessertOrders.Include(d => d.DessertOrderDetails).Include(d => d.Member).Include(d => d.OrderStatus);
            if (string.IsNullOrEmpty(criteria.MemberName) == false)
            {
                query = query.Where(d => d.Member.MemberName.Contains(criteria.MemberName));
            }
            if(criteria.OrderStatusId!=null && criteria.OrderStatusId.Value > 0) 
            {
                query = query.Where(d => d.OrderStatus.OrderStatusId == criteria.OrderStatusId.Value);
            }
            if (string.IsNullOrEmpty(criteria.Recipient) == false) 
            {
                query = query.Where(d=>d.Recipient.Contains(criteria.Recipient));
            }
            if (criteria.MinPrice.HasValue) 
            {
                query =query.Where(d=>d.DessertOrderTotal>=criteria.MinPrice.Value);
            }
            if (criteria.MaxPrice.HasValue) 
            {
                query = query.Where(d=>d.DessertOrderTotal<=criteria.MaxPrice.Value);
            }
            #endregion
            var dessertOrders = query.ToList().Select(d => d.TOIndexVM());
            return View(dessertOrders);
          

            //var dessertOrders = db.DessertOrders.Include(d=>d.DessertOrderDetails).Include(d => d.Member).Include(d => d.OrderStatus)
            //    .ToList()
            //    .Select(d => d.TOIndexVM());
            //return View(dessertOrders);
        }

        private void PrepareOrderDataSource(int? orderStatusId)
        {
            var status = db.OrderStatuses.ToList().Prepend(new OrderStatus());
            ViewBag.OrderStatusId = new SelectList(status, "OrderStatusId", "StatusName", orderStatusId);
        }
		[OverrideAuthorization]
		[DirectToUnAuthorize(Roles = "admin,staff")]
		public PartialViewResult TopSellingDesserts()//最熱銷前五名甜點
        {
            //var topDesserts = db.DessertOrders
            //    .SelectMany(o => o.DessertOrderDetails)
            //    .GroupBy(d => d.DessertName)
            //    .Select(g => new DessertOrderDetailStats { DessertName = g.Key, Quantity = g.Sum(d => d.Quantity) })
            //    .OrderByDescending(d => d.Quantity)
            //    .Take(5)
            //    .ToList();
            var topDesserts = db.DessertOrderDetails
        .Join(db.Desserts, od => od.DessertId, l => l.DessertId, (od, l) => new { od, l })
        .GroupBy(g => new { g.l.DessertName })
        .Select(g => new DessertOrderDetailStats
        {
            DessertName = g.Key.DessertName,
            Quantity = g.Sum(x => x.od.Quantity)
        })
        .OrderByDescending(x => x.Quantity)
        .ThenBy(x => x.DessertName)
        .Take(5)
        .ToList();

            return PartialView("TopSellingDesserts", topDesserts);
        }
		[OverrideAuthorization]
		[DirectToUnAuthorize(Roles = "admin,staff")]
		public PartialViewResult TopSellingDessertsOrder()//前十熱銷的甜點訂單
        {
            //var topSellingOrders = db.DessertOrders
            //    .Where(os => os.DessertOrderStatusId == 3)
            //    .OrderByDescending(o => o.DessertOrderTotal)
            //    .Take(10)
            //    .Select(o => new TopSellingDessertsOrder
            //    {
            //        OrderID = o.DessertOrderId,
            //        TotalAmount = o.DessertOrderTotal
            //    })
            //    .ToList();
            var topSellingOrders = db.DessertOrderDetails
                .Where(x=>x.DessertOrder.DessertOrderStatusId ==3)
                .GroupBy(x=>x.DessertId)
                .Select((g)=>new TopSellingDessertsOrder {
                    DessertId =g.Key,
                    DessertName =g.FirstOrDefault().DessertName,
                    TotalQuantity =g.Sum(x=>x.Quantity),
                    TotalAmount=g.Sum(x=>x.DessertOrder.DessertOrderTotal),
                })
                .OrderByDescending(x=>x.TotalAmount)
                .ThenByDescending(x=>x.TotalQuantity)
                .Take(10).ToList();
                


            return PartialView("TopSellingDessertsOrder", topSellingOrders);
        }

        // GET: DessertOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DessertOrder dessertOrder = db.DessertOrders.Find(id);
            if (dessertOrder == null)
            {
                return HttpNotFound();
            }
            return View(dessertOrder);
        }

        // GET: DessertOrders/Create
        public ActionResult Create()
        {
            ViewBag.CouponId = new SelectList(db.Coupons, "CouponId", "CouponName");
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "MemberName");
            ViewBag.DessertOrderStatusId = new SelectList(db.OrderStatuses, "OrderStatusId", "StatusName");
            return View();
        }

        // POST: DessertOrders/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DessertOrderId,MemberId,DessertOrderStatusId,CouponId,CreateTime,Recipient,RecipientPhone,RecipientAddress,ShippingFee,DessertOrderTotal,DeliveryMethod,Note,OrderCancellationReason,DiscountInfo")] DessertOrder dessertOrder)
        {
            if (ModelState.IsValid)
            {
                db.DessertOrders.Add(dessertOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CouponId = new SelectList(db.Coupons, "CouponId", "CouponName", dessertOrder.CouponId);
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "MemberName", dessertOrder.MemberId);
            ViewBag.DessertOrderStatusId = new SelectList(db.OrderStatuses, "OrderStatusId", "StatusName", dessertOrder.DessertOrderStatusId);
            return View(dessertOrder);
        }

        // GET: DessertOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DessertOrder dessertOrder = db.DessertOrders.Find(id);
            if (dessertOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.CouponId = new SelectList(db.Coupons, "CouponId", "CouponName", dessertOrder.CouponId);
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "MemberName", dessertOrder.MemberId);
            ViewBag.DessertOrderStatusId = new SelectList(db.OrderStatuses, "OrderStatusId", "StatusName", dessertOrder.DessertOrderStatusId);
            return View(dessertOrder);
        }

        // POST: DessertOrders/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DessertOrderId,MemberId,DessertOrderStatusId,CouponId,CreateTime,Recipient,RecipientPhone,RecipientAddress,ShippingFee,DessertOrderTotal,DeliveryMethod,Note,OrderCancellationReason,DiscountInfo")] DessertOrder dessertOrder)
        {
            if (ModelState.IsValid)
            {
                dessertOrder.CreateTime = DateTime.Now;
                db.Entry(dessertOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CouponId = new SelectList(db.Coupons, "CouponId", "CouponName", dessertOrder.CouponId);
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "MemberName", dessertOrder.MemberId);
            ViewBag.DessertOrderStatusId = new SelectList(db.OrderStatuses, "OrderStatusId", "StatusName", dessertOrder.DessertOrderStatusId);
            return View(dessertOrder);
        }

        // GET: DessertOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DessertOrder dessertOrder = db.DessertOrders.Find(id);
            if (dessertOrder == null)
            {
                return HttpNotFound();
            }
            return View(dessertOrder);
        }

        // POST: DessertOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            DessertOrder dessertOrder = db.DessertOrders.Find(id);
            dessertOrder.CreateTime = DateTime.Now;
            db.DessertOrders.Remove(dessertOrder);
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
