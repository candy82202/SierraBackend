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
using NiceAdmin.Models.ViewModels.PromotionsVM;

namespace NiceAdmin.Controllers
{
    [DirectToUnAuthorize(Roles = "admin,manager,dessertSale,lessonSale")]
    public class MemberCouponsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: MemberCoupons
        [OverrideAuthorization]
        [DirectToUnAuthorize(Roles = "admin,manager,dessertSale,lessonSale,staff")]
        public ActionResult Index()
        {
            var memberCoupons = db.Members.AsNoTracking()
                                          .Include(m => m.MemberCoupons)
                                          .ToList()
										  .Select(m => m.ToMemberCouponIndexVM());
            PrepareCouponDataSource(null);

			return View(memberCoupons);
        }

        // GET: MemberCoupons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            IEnumerable<MemberCouponInDetail> memberCoupons = db.MemberCoupons
                .Where(mc => mc.MemberId == id)
                .ToList()
                .Select(mc => mc.ToMemberCouponInDetail());
            MemberCouponDetailVM vm = new MemberCouponDetailVM()
            {
                MemberId = member.MemberId,
                MemberName = member.MemberName,
                MemberCoupons = memberCoupons
            };
            return View(vm);
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
        public JsonResult Deliver(int memberid,int couponid)
        {
            var couponInDb = db.Coupons.FirstOrDefault(c=>c.CouponId == couponid);
            if (couponInDb == null||couponInDb.CouponCategoryId!=1)
            {
                return Json("優惠券不存在或類別不是'指定發送'");
            }
            double expiration = (double)couponInDb.Expiration;
            var newMemberCoupon = new MemberCoupon()
            {
                CouponId = couponid,
                MemberId = memberid,
                //Status = 2,
                CreateAt = DateTime.Now,
                ExpireAt = DateTime.Now.AddDays(expiration),
                CouponName = couponInDb.CouponName,
            };
            db.MemberCoupons.Add(newMemberCoupon);
            db.SaveChanges();
            return Json(new {
                couponName=newMemberCoupon.CouponName,
            });
        }
		private void PrepareCouponDataSource(int? couponId)
		{
			var coupons = db.Coupons.Where(c => c.CouponCategoryId == 1)
									.ToList()
									.Prepend(new Coupon());
			ViewBag.CouponId = new SelectList(coupons, "couponId", "couponName", couponId);

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
