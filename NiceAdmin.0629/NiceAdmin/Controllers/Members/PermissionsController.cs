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

namespace NiceAdmin.Controllers.Members
{
    [DirectToUnAuthorize(Roles = "admin")]
    public class PermissionsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Permissions
        [OverrideAuthorization]
        [DirectToUnAuthorize(Roles = "admin,manager")]
        public ActionResult Index()
        {
            return View(db.Permissions.ToList());
        }

        //// GET: Permissions/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Permission permission = db.Permissions.Find(id);
        //    if (permission == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(permission);
        //}

        //// GET: Permissions/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Permissions/Create
        //// 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        //// 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "PermissionId,PermissionName")] Permission permission)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Permissions.Add(permission);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(permission);
        //}

        //// GET: Permissions/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Permission permission = db.Permissions.Find(id);
        //    if (permission == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(permission);
        //}

        //// POST: Permissions/Edit/5
        //// 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        //// 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "PermissionId,PermissionName")] Permission permission)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(permission).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(permission);
        //}

        //// GET: Permissions/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Permission permission = db.Permissions.Find(id);
        //    if (permission == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(permission);
        //}

        //// POST: Permissions/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Permission permission = db.Permissions.Find(id);
        //    db.Permissions.Remove(permission);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
