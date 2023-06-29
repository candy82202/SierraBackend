using System;
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
    public class DessertOrderDetailsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: DessertOrderDetails
        public ActionResult Index()
        {
            var dessertOrderDetails = db.DessertOrderDetails.Include(d => d.Dessert).Include(d => d.DessertOrder);
            return View(dessertOrderDetails.ToList());
        }

        // GET: DessertOrderDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DessertOrderDetail dessertOrderDetail = db.DessertOrderDetails.Find(id);
            if (dessertOrderDetail == null)
            {
                return HttpNotFound();
            }
            return View(dessertOrderDetail);
        }

        // GET: DessertOrderDetails/Create
        public ActionResult Create()
        {
            ViewBag.DessertId = new SelectList(db.Desserts, "DessertId", "DessertName");
            ViewBag.DessertOrderId = new SelectList(db.DessertOrders, "DessertOrderId", "Recipient");
            return View();
        }

        // POST: DessertOrderDetails/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DessertOrderDetailId,DessertOrderId,DessertId,DessertName,Quantity,DessertUnitPrice,Subtotal")] DessertOrderDetail dessertOrderDetail)
        {
            if (ModelState.IsValid)
            {
                db.DessertOrderDetails.Add(dessertOrderDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DessertId = new SelectList(db.Desserts, "DessertId", "DessertName", dessertOrderDetail.DessertId);
            ViewBag.DessertOrderId = new SelectList(db.DessertOrders, "DessertOrderId", "Recipient", dessertOrderDetail.DessertOrderId);
            return View(dessertOrderDetail);
        }

        // GET: DessertOrderDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DessertOrderDetail dessertOrderDetail = db.DessertOrderDetails.Find(id);
            if (dessertOrderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.DessertId = new SelectList(db.Desserts, "DessertId", "DessertName", dessertOrderDetail.DessertId);
            ViewBag.DessertOrderId = new SelectList(db.DessertOrders, "DessertOrderId", "Recipient", dessertOrderDetail.DessertOrderId);
            return View(dessertOrderDetail);
        }

        // POST: DessertOrderDetails/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DessertOrderDetailId,DessertOrderId,DessertId,DessertName,Quantity,DessertUnitPrice,Subtotal")] DessertOrderDetail dessertOrderDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dessertOrderDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DessertId = new SelectList(db.Desserts, "DessertId", "DessertName", dessertOrderDetail.DessertId);
            ViewBag.DessertOrderId = new SelectList(db.DessertOrders, "DessertOrderId", "Recipient", dessertOrderDetail.DessertOrderId);
            return View(dessertOrderDetail);
        }

        // GET: DessertOrderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DessertOrderDetail dessertOrderDetail = db.DessertOrderDetails.Find(id);
            if (dessertOrderDetail == null)
            {
                return HttpNotFound();
            }
            return View(dessertOrderDetail);
        }

        // POST: DessertOrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DessertOrderDetail dessertOrderDetail = db.DessertOrderDetails.Find(id);
            db.DessertOrderDetails.Remove(dessertOrderDetail);
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
