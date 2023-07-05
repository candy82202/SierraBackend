//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using NiceAdmin.Models.EFModels;

//namespace NiceAdmin.Controllers.Members
//{
//    public class RoleToPermissionsController : Controller
//    {
//        private AppDbContext db = new AppDbContext();

//        // GET: RoleToPermissions
//        public ActionResult Index()
//        {
//            return View(db.RoleToPermissions.ToList());
//        }

//        // GET: RoleToPermissions/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            RoleToPermission roleToPermission = db.RoleToPermissions.Find(id);
//            if (roleToPermission == null)
//            {
//                return HttpNotFound();
//            }
//            return View(roleToPermission);
//        }

//        // GET: RoleToPermissions/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: RoleToPermissions/Create
//        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
//        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "RoleId,PermissionId")] RoleToPermission roleToPermission)
//        {
//            if (ModelState.IsValid)
//            {
//                db.RoleToPermissions.Add(roleToPermission);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            return View(roleToPermission);
//        }

//        // GET: RoleToPermissions/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            RoleToPermission roleToPermission = db.RoleToPermissions.Find(id);
//            if (roleToPermission == null)
//            {
//                return HttpNotFound();
//            }
//            return View(roleToPermission);
//        }

//        // POST: RoleToPermissions/Edit/5
//        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
//        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "RoleId,PermissionId")] RoleToPermission roleToPermission)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(roleToPermission).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            return View(roleToPermission);
//        }

//        // GET: RoleToPermissions/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            RoleToPermission roleToPermission = db.RoleToPermissions.Find(id);
//            if (roleToPermission == null)
//            {
//                return HttpNotFound();
//            }
//            return View(roleToPermission);
//        }

//        // POST: RoleToPermissions/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            RoleToPermission roleToPermission = db.RoleToPermissions.Find(id);
//            db.RoleToPermissions.Remove(roleToPermission);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
