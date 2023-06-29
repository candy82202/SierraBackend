﻿using System;
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
    public class DessertOrdersController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: DessertOrders
        public ActionResult Index()
        {
            var dessertOrders = db.DessertOrders.Include(d => d.Coupon).Include(d => d.Member).Include(d => d.OrderStatus);
            return View(dessertOrders.ToList());
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