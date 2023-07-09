using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NiceAdmin.Models.EFModels;
using NiceAdmin.Models.ViewModels.PromotionsVM;

namespace NiceAdmin.Controllers.Promotions
{
    public class CouponSettingsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: CouponSettings
        public ActionResult Index()
        {
            var couponSettings = db.CouponSettings.Include(c => c.Coupon);
            return View(couponSettings.ToList());
        }

        // GET: CouponSettings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CouponSetting couponSetting = db.CouponSettings.Find(id);
            if (couponSetting == null)
            {
                return HttpNotFound();
            }
            return View(couponSetting);
        }

        // GET: CouponSettings/Create
        public ActionResult Create()
        {
            ViewBag.CouponId = new SelectList(db.Coupons, "CouponId", "CouponName");
            return View();
        }

        // POST: CouponSettings/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CouponSettingId,CouponSettingName,CouponId,CouponType")] CouponSetting couponSetting)
        {
            if (ModelState.IsValid)
            {
                db.CouponSettings.Add(couponSetting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CouponId = new SelectList(db.Coupons, "CouponId", "CouponName", couponSetting.CouponId);
            return View(couponSetting);
        }

        // GET: CouponSettings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CouponSetting couponSetting = db.CouponSettings.Find(id);
            if (couponSetting == null)
            {
                return HttpNotFound();
            }
            ViewBag.CouponId = new SelectList(db.Coupons, "CouponId", "CouponName", couponSetting.CouponId);
            return View(couponSetting);
        }

        // POST: CouponSettings/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CouponSettingId,CouponSettingName,CouponId,CouponType")] CouponSetting couponSetting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(couponSetting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CouponId = new SelectList(db.Coupons, "CouponId", "CouponName", couponSetting.CouponId);
            return View(couponSetting);
        }

        // GET: CouponSettings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CouponSetting couponSetting = db.CouponSettings.Find(id);
            if (couponSetting == null)
            {
                return HttpNotFound();
            }
            return View(couponSetting);
        }

        // POST: CouponSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CouponSetting couponSetting = db.CouponSettings.Find(id);
            db.CouponSettings.Remove(couponSetting);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CouponForBirthday()
        {
            var birthdayCoupon = db.Coupons.Where(c=>c.CouponCategoryId==5)
                                           .ToList()
                                           .Select(c=>c.ToDragVM());
            return PartialView("CouponForBirthday", birthdayCoupon);
        }
        public ActionResult CouponForRegister()
        {
            var registerCoupon = db.Coupons.Where(c => c.CouponCategoryId == 6)
                                           .ToList()
                                           .Select(c => c.ToDragVM());
            return PartialView("CouponForRegister", registerCoupon);
        }
        public ActionResult CouponForGame()
        {
            var gameCoupon = db.Coupons.Where(c => c.CouponCategoryId == 3)
                                           .ToList()
                                           .Select(c => c.ToDragVM());
            return PartialView("CouponForGame", gameCoupon);
        }
        [HttpPost]
        public JsonResult Remove(int? couponSettingId)
        {
            if (couponSettingId == null ) return Json("刪除失敗");

            var CouponSettingInDb = db.CouponSettings.FirstOrDefault(cs=>cs.CouponSettingId== couponSettingId);
            if (CouponSettingInDb == null) return Json("刪除失敗");
            CouponSettingInDb.CouponId = null;
            
            db.SaveChanges();
            return Json(true);
        }
        [HttpPost]
        public JsonResult Add(int? couponSettingId,int? couponId)
        {
            if (couponSettingId == null|| couponId==null) return Json("加入失敗");

            var CouponSettingInDb = db.CouponSettings.FirstOrDefault(cs => cs.CouponSettingId == couponSettingId);
            if (CouponSettingInDb.Coupon!=null) return Json("加入失敗");
            CouponSettingInDb.CouponId = couponId;
            var couponInDb = db.Coupons.Find(couponId);
            var couponName = couponInDb.CouponName;
            db.SaveChanges();
            return Json(couponName);
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
