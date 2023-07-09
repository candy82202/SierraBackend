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

namespace NiceAdmin.Controllers.Desserts
{
    [DirectToUnAuthorize(Roles = "admin,manager")]
    public class DessertTagsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: DessertTags
        [OverrideAuthorization]
        [DirectToUnAuthorize(Roles = "admin,manager,dessertSale,staff")]
        public ActionResult Index()
        {
            var dessertTags = db.DessertTags.Include(d => d.Dessert);
            return View(dessertTags.ToList());
        }

        // GET: DessertTags/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DessertTag dessertTag = db.DessertTags.Find(id);
            if (dessertTag == null)
            {
                return HttpNotFound();
            }
            return View(dessertTag);
        }

        // GET: DessertTags/Create
        public ActionResult Create()
        {
            ViewBag.DessertId = new SelectList(db.Desserts, "DessertId", "DessertName");
            return View();
        }

        // POST: DessertTags/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DessertTagId,DessertId,DessertTagName")] DessertTag dessertTag)
        {
            if (ModelState.IsValid)
            {
                db.DessertTags.Add(dessertTag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DessertId = new SelectList(db.Desserts, "DessertId", "DessertName", dessertTag.DessertId);
            return View(dessertTag);
        }

        // GET: DessertTags/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DessertTag dessertTag = db.DessertTags.Find(id);
            if (dessertTag == null)
            {
                return HttpNotFound();
            }
            ViewBag.DessertId = new SelectList(db.Desserts, "DessertId", "DessertName", dessertTag.DessertId);
            return View(dessertTag);
        }

        // POST: DessertTags/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DessertTagId,DessertId,DessertTagName")] DessertTag dessertTag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dessertTag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DessertId = new SelectList(db.Desserts, "DessertId", "DessertName", dessertTag.DessertId);
            return View(dessertTag);
        }

        // GET: DessertTags/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DessertTag dessertTag = db.DessertTags.Find(id);
            if (dessertTag == null)
            {
                return HttpNotFound();
            }
            return View(dessertTag);
        }

        // POST: DessertTags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DessertTag dessertTag = db.DessertTags.Find(id);
            db.DessertTags.Remove(dessertTag);
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
