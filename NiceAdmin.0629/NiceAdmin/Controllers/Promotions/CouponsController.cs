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
    public class CouponsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Coupons
        public ActionResult Index()
        {
            var coupons = db.Coupons.Include(c => c.CouponCategory).Include(c => c.DiscountGroup);
            return View(coupons.ToList());
        }

        // GET: Coupons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coupon coupon = db.Coupons.Find(id);
            if (coupon == null)
            {
                return HttpNotFound();
            }
            return View(coupon);
        }

        // GET: Coupons/Create
        public ActionResult Create()
        {
            ViewBag.CouponCategoryId = new SelectList(db.CouponCategories, "CouponCategoryId", "CouponCategoryName");
            ViewBag.DiscountGroupId = new SelectList(db.DiscountGroups, "DiscountGroupId", "DiscountGroupName");
            return View();
        }

        // POST: Coupons/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CouponId,CouponCategoryId,DiscountGroupId,CouponName,CouponCode,LimitType,LimitValue,DiscountType,DiscountValue,StartAt,EndAt,Expiration,CreateAt")] Coupon coupon)
        {
            if (ModelState.IsValid)
            {
                db.Coupons.Add(coupon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CouponCategoryId = new SelectList(db.CouponCategories, "CouponCategoryId", "CouponCategoryName", coupon.CouponCategoryId);
            ViewBag.DiscountGroupId = new SelectList(db.DiscountGroups, "DiscountGroupId", "DiscountGroupName", coupon.DiscountGroupId);
            return View(coupon);
        }

        // GET: Coupons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coupon coupon = db.Coupons.Find(id);
            if (coupon == null)
            {
                return HttpNotFound();
            }
            ViewBag.CouponCategoryId = new SelectList(db.CouponCategories, "CouponCategoryId", "CouponCategoryName", coupon.CouponCategoryId);
            ViewBag.DiscountGroupId = new SelectList(db.DiscountGroups, "DiscountGroupId", "DiscountGroupName", coupon.DiscountGroupId);
            return View(coupon);
        }

        // POST: Coupons/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CouponId,CouponCategoryId,DiscountGroupId,CouponName,CouponCode,LimitType,LimitValue,DiscountType,DiscountValue,StartAt,EndAt,Expiration,CreateAt")] Coupon coupon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coupon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CouponCategoryId = new SelectList(db.CouponCategories, "CouponCategoryId", "CouponCategoryName", coupon.CouponCategoryId);
            ViewBag.DiscountGroupId = new SelectList(db.DiscountGroups, "DiscountGroupId", "DiscountGroupName", coupon.DiscountGroupId);
            return View(coupon);
        }

        // GET: Coupons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coupon coupon = db.Coupons.Find(id);
            if (coupon == null)
            {
                return HttpNotFound();
            }
            return View(coupon);
        }

        // POST: Coupons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Coupon coupon = db.Coupons.Find(id);
            db.Coupons.Remove(coupon);
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
