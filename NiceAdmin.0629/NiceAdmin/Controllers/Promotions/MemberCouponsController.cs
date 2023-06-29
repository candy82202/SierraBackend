using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NiceAdmin.Models.EFModels;

namespace NiceAdmin.Controllers
{
    public class MemberCouponsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: MemberCoupons
        public ActionResult Index()
        {
            var memberCoupons = db.MemberCoupons.Include(m => m.Coupon).Include(m => m.Member);
            return View(memberCoupons.ToList());
        }

        // GET: MemberCoupons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberCoupon memberCoupon = db.MemberCoupons.Find(id);
            if (memberCoupon == null)
            {
                return HttpNotFound();
            }
            return View(memberCoupon);
        }

        // GET: MemberCoupons/Create
        public ActionResult Create()
        {
            ViewBag.CouponId = new SelectList(db.Coupons, "CouponId", "CouponName");
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "MemberName");
            return View();
        }

        // POST: MemberCoupons/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberCouponId,MemberId,CouponId,Status,CreateAt,UseAt,ExpireAt,CouponName")] MemberCoupon memberCoupon)
        {
            if (ModelState.IsValid)
            {
                db.MemberCoupons.Add(memberCoupon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CouponId = new SelectList(db.Coupons, "CouponId", "CouponName", memberCoupon.CouponId);
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "MemberName", memberCoupon.MemberId);
            return View(memberCoupon);
        }

        // GET: MemberCoupons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberCoupon memberCoupon = db.MemberCoupons.Find(id);
            if (memberCoupon == null)
            {
                return HttpNotFound();
            }
            ViewBag.CouponId = new SelectList(db.Coupons, "CouponId", "CouponName", memberCoupon.CouponId);
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "MemberName", memberCoupon.MemberId);
            return View(memberCoupon);
        }

        // POST: MemberCoupons/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberCouponId,MemberId,CouponId,Status,CreateAt,UseAt,ExpireAt,CouponName")] MemberCoupon memberCoupon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberCoupon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CouponId = new SelectList(db.Coupons, "CouponId", "CouponName", memberCoupon.CouponId);
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "MemberName", memberCoupon.MemberId);
            return View(memberCoupon);
        }

        // GET: MemberCoupons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberCoupon memberCoupon = db.MemberCoupons.Find(id);
            if (memberCoupon == null)
            {
                return HttpNotFound();
            }
            return View(memberCoupon);
        }

        // POST: MemberCoupons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MemberCoupon memberCoupon = db.MemberCoupons.Find(id);
            db.MemberCoupons.Remove(memberCoupon);
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
