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

namespace NiceAdmin.Controllers
{
    public class CouponsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Coupons
        public ActionResult Index()
        {
            IEnumerable<Coupon> coupons = db.Coupons;
            UpdateCouponStatus(coupons);
			IEnumerable<CouponIndexVM> couponVMs = db.Coupons.Include(c => c.CouponCategory)
                                                           .Include(c => c.DiscountGroup)
                                                           .ToList()
                                                           .Select(c => c.ToIndexVM());
                                                           
            return View(couponVMs);
        }

        // GET: Coupons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coupon coupon = db.Coupons.Find(id);
            CouponDetailVM detailVM = coupon.ToDetailVM();
            if (detailVM == null)
            {
                return HttpNotFound();
            }
            return View(detailVM);
        }

        // GET: Coupons/Create
        public ActionResult Create()
        {
            PrepareCouponCategoryAndDiscountGroupDataSource(null, null); 
            return View();
        }

        // POST: Coupons/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CouponCategoryId,DiscountGroupId,CouponName,CouponCode,LimitType,LimitValue,DiscountType,DiscountValue,StartAt,EndAt,Expiration")] CouponCreateVM vm)
        {
            //
            // ModelState.AddModelError()
            // return View(vm);
            // 都會在網頁那邊跳出錯誤
            if (ModelState.IsValid)
            {
                if (vm.DiscountGroupId == 4)
                {
					bool sameCouponCode = db.Coupons.Any(c => c.CouponCode == vm.CouponCode);
                    if (sameCouponCode)
                    {
                        //ModelState.AddModelError(string.Empty, "此優惠碼已經被使用");
                        //return View(vm);
                        return HttpNotFound();
					}
				}
                if (vm.StartAt != null)
                {
                    if (vm.StartAt < DateTime.Now)
                    {
                        //ModelState.AddModelError(string.Empty, "開始時間不可小於現在時間");
                        //return View(vm);
                        return HttpNotFound();
                    }
                    if (vm.StartAt > vm.EndAt)
                    {
                        //ModelState.AddModelError(string.Empty, "開始時間不可在結束時間以後");
                        //return View(vm);
                        return HttpNotFound();
                    }
                }
                Coupon coupon = vm.ToEntity();
                bool haveSameCouponCode = db.Coupons.Any(c => c.CouponCode== coupon.CouponCode);
                while (haveSameCouponCode)
                {
                    coupon.CouponCode = Guid.NewGuid().ToString();
                    haveSameCouponCode = db.Coupons.Any(c => c.CouponCode == coupon.CouponCode);
                }
                db.Coupons.Add(coupon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            PrepareCouponCategoryAndDiscountGroupDataSource(vm.CouponCategoryId, vm.DiscountGroupId);
            return View(vm);
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
        private void PrepareCouponCategoryAndDiscountGroupDataSource(int? couponCategoryId,int? discountGroupId)
        {
            var couponCategories = db.CouponCategories.ToList().Prepend(new CouponCategory());
            ViewBag.CouponCategoryId = new SelectList(couponCategories, "couponCategoryId", "couponCategoryName", couponCategoryId);

            var discountGroups = db.DiscountGroups.ToList().Prepend(new DiscountGroup());
            ViewBag.DiscountGroupId = new SelectList(discountGroups, "discountGroupId", "discountGroupName", discountGroupId);
        }
        private void UpdateCouponStatus(IEnumerable<Coupon>coupons)
        {
            foreach (var coupon in coupons)
            {
                if (coupon.DiscountGroupId == 2|| coupon.DiscountGroupId == 4)
                {
                    if (coupon.EndAt < DateTime.Now)
                    {
                        coupon.Status = false;
                    }
                }
			}
            db.SaveChanges();
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
