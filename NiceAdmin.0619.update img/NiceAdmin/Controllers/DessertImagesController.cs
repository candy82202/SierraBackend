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
    public class DessertImagesController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: DessertImages
        public ActionResult Index()
        {
            return View(db.DessertImages.ToList());
        }

        // GET: DessertImages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DessertImage dessertImage = db.DessertImages.Find(id);
            if (dessertImage == null)
            {
                return HttpNotFound();
            }
            return View(dessertImage);
        }

        // GET: DessertImages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DessertImages/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ImageId,DessertId,DessertImage1")] DessertImage dessertImage)
        {
            if (ModelState.IsValid)
            {
                db.DessertImages.Add(dessertImage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dessertImage);
        }

        // GET: DessertImages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DessertImage dessertImage = db.DessertImages.Find(id);
            if (dessertImage == null)
            {
                return HttpNotFound();
            }
            return View(dessertImage);
        }

        // POST: DessertImages/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ImageId,DessertId,DessertImage1")] DessertImage dessertImage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dessertImage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dessertImage);
        }

        // GET: DessertImages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DessertImage dessertImage = db.DessertImages.Find(id);
            if (dessertImage == null)
            {
                return HttpNotFound();
            }
            return View(dessertImage);
        }

        // POST: DessertImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DessertImage dessertImage = db.DessertImages.Find(id);
            db.DessertImages.Remove(dessertImage);
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
