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
//    public class EmployeeToRolesController : Controller
//    {
//        private AppDbContext db = new AppDbContext();

//        // GET: EmployeeToRoles
//        public ActionResult Index()
//        {
//            return View(db.EmployeeToRoles.ToList());
//        }

//        // GET: EmployeeToRoles/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            EmployeeToRole employeeToRole = db.EmployeeToRoles.Find(id);
//            if (employeeToRole == null)
//            {
//                return HttpNotFound();
//            }
//            return View(employeeToRole);
//        }

//        // GET: EmployeeToRoles/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: EmployeeToRoles/Create
//        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
//        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "EmployeeId,RoleId")] EmployeeToRole employeeToRole)
//        {
//            if (ModelState.IsValid)
//            {
//                db.EmployeeToRoles.Add(employeeToRole);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            return View(employeeToRole);
//        }

//        // GET: EmployeeToRoles/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            EmployeeToRole employeeToRole = db.EmployeeToRoles.Find(id);
//            if (employeeToRole == null)
//            {
//                return HttpNotFound();
//            }
//            return View(employeeToRole);
//        }

//        // POST: EmployeeToRoles/Edit/5
//        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
//        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "EmployeeId,RoleId")] EmployeeToRole employeeToRole)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(employeeToRole).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            return View(employeeToRole);
//        }

//        // GET: EmployeeToRoles/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            EmployeeToRole employeeToRole = db.EmployeeToRoles.Find(id);
//            if (employeeToRole == null)
//            {
//                return HttpNotFound();
//            }
//            return View(employeeToRole);
//        }

//        // POST: EmployeeToRoles/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            EmployeeToRole employeeToRole = db.EmployeeToRoles.Find(id);
//            db.EmployeeToRoles.Remove(employeeToRole);
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
