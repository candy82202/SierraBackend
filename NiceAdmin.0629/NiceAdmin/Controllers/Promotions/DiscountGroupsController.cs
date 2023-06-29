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
    public class DiscountGroupsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: DiscountGroups
        public ActionResult Index()
        {
            return View(db.DiscountGroups.ToList());
        }

        // GET: DiscountGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiscountGroup discountGroup = db.DiscountGroups.Find(id);
            if (discountGroup == null)
            {
                return HttpNotFound();
            }
            return View(discountGroup);
        }

        // GET: DiscountGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DiscountGroups/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DiscountGroupId,DiscountGroupName")] DiscountGroup discountGroup)
        {
            if (ModelState.IsValid)
            {
                db.DiscountGroups.Add(discountGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(discountGroup);
        }

        // GET: DiscountGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiscountGroup discountGroup = db.DiscountGroups.Find(id);
            if (discountGroup == null)
            {
                return HttpNotFound();
            }
            return View(discountGroup);
        }

        // POST: DiscountGroups/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DiscountGroupId,DiscountGroupName")] DiscountGroup discountGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(discountGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(discountGroup);
        }

        // GET: DiscountGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiscountGroup discountGroup = db.DiscountGroups.Find(id);
            if (discountGroup == null)
            {
                return HttpNotFound();
            }
            return View(discountGroup);
        }

        // POST: DiscountGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DiscountGroup discountGroup = db.DiscountGroups.Find(id);
            db.DiscountGroups.Remove(discountGroup);
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
