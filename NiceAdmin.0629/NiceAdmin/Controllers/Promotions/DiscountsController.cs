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

namespace NiceAdmin.Controllers.Promotions
{
    [DirectToUnAuthorize(Roles = "admin,marketing")]
    public class DiscountsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Discounts
        [OverrideAuthorization]
        [DirectToUnAuthorize(Roles = "admin,marketing,staff")]
        public ActionResult Index()
        {
            var discounts = db.Discounts.Include(d => d.DiscountGroup);
            return View(discounts.ToList());
        }

        // GET: Discounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discount discount = db.Discounts.Find(id);
            if (discount == null)
            {
                return HttpNotFound();
            }
            return View(discount);
        }

        // GET: Discounts/Create
        public ActionResult Create()
        {
            ViewBag.DiscountGroupId = new SelectList(db.DiscountGroups, "DiscountGroupId", "DiscountGroupName");
            return View();
        }

        // POST: Discounts/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DiscountId,DiscountGroupId,DiscountName,LimitType,LimitValue,DiscountType,DiscountValue,StartAt,EndAt,CreateAt")] Discount discount)
        {
            if (ModelState.IsValid)
            {
                db.Discounts.Add(discount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DiscountGroupId = new SelectList(db.DiscountGroups, "DiscountGroupId", "DiscountGroupName", discount.DiscountGroupId);
            return View(discount);
        }

        // GET: Discounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discount discount = db.Discounts.Find(id);
            if (discount == null)
            {
                return HttpNotFound();
            }
            ViewBag.DiscountGroupId = new SelectList(db.DiscountGroups, "DiscountGroupId", "DiscountGroupName", discount.DiscountGroupId);
            return View(discount);
        }

        // POST: Discounts/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DiscountId,DiscountGroupId,DiscountName,LimitType,LimitValue,DiscountType,DiscountValue,StartAt,EndAt,CreateAt")] Discount discount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(discount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DiscountGroupId = new SelectList(db.DiscountGroups, "DiscountGroupId", "DiscountGroupName", discount.DiscountGroupId);
            return View(discount);
        }

        // GET: Discounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discount discount = db.Discounts.Find(id);
            if (discount == null)
            {
                return HttpNotFound();
            }
            return View(discount);
        }

        // POST: Discounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Discount discount = db.Discounts.Find(id);
            db.Discounts.Remove(discount);
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
