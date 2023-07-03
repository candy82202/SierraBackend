using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using NiceAdmin.Models.EFModels;
using NiceAdmin.Models.ViewModels.MembersVM;

namespace NiceAdmin.Controllers.Members
{
    public class EmployeesController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Employees
        public ActionResult Index()
        {
            var vm = db.Employees.ToList().Select(e => e.ToIndexVM());
            return View(vm);
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            //// 原本的寫法
            //ViewBag.RoleId = db.Roles.Select(r => new SelectListItem
            //{
            //    Value = r.RoleId.ToString(),
            //    Text = r.RoleName
            //});
            PrepareRolesDataSource(null);
			return View();
        }

        // POST: Employees/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee emp, EmployeeToRole relation)
        {
            if (ModelState.IsValid)
            {
                emp.CreateAt = DateTime.Now;
                db.Employees.Add(emp);
                db.SaveChanges();

                relation.EmployeeId = emp.EmployeeId;
                db.EmployeeToRoles.Add(relation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(emp);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            var relation = db.EmployeeToRoles.FirstOrDefault(r => r.EmployeeId == id);
            var roleId = relation.RoleId;
            var emp = db.Employees.Find(id).ToEditVM();
            
			if (emp == null)
            {
                return HttpNotFound();
            }
            PrepareRolesDataSource(roleId); 
			return View(emp);
        }

        // POST: Employees/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeEditVM vm, EmployeeToRole relation)
        {
            if (ModelState.IsValid)
            {
				// 因在有KeyAttribute的欄位無法修改,因此Edit的做法是先找到資料,刪除,再新增

                // 找到資料
				var relationInDb = db.EmployeeToRoles.FirstOrDefault(e => e.EmployeeId == vm.EmployeeId);

                //刪除
				db.EmployeeToRoles.Remove(relationInDb);
				db.SaveChanges();

                //新增
				relationInDb.RoleId = relation.RoleId; 
				db.EmployeeToRoles.Add(relationInDb);
                db.SaveChanges();

				return RedirectToAction("Index");
            }
			return View(vm);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
		private void PrepareRolesDataSource(int? roleId)
		{
			ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName", roleId );
		}
	}
}
