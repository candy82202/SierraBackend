using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
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

            //// 後來的寫法
            //PrepareRolesDataSource(null);

            // 最後的寫法
            HashSet<Role> roles = db.Roles.ToHashSet();
            ViewBag.Roles = roles;
           
            return View();
		}

		// POST: Employees/Create
		// 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
		// 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(EmployeeCreateVM vm)
		{
			if (ModelState.IsValid == false) 
			{
                HashSet<Role> roles = db.Roles.ToHashSet();
                ViewBag.Roles = roles;
                return View(vm); 
			}

			var emp = vm.ToEntity();
			emp.Roles = db.Roles.Where(r => vm.RoleIds.Contains(r.RoleId)).ToList();
            db.Employees.Add(emp);
			//var newEmp = db.Employees.OrderByDescending(e => e.EmployeeId).FirstOrDefault();
   //         newEmp.Roles = db.Roles.Where(r=> vm.RoleIds.Contains(r.RoleId)).ToList();

            db.SaveChanges();
			return RedirectToAction("Index");
		}

		// GET: Employees/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var vm = db.Employees.Find(id).ToEditVM();
			if (vm == null)
			{
				return HttpNotFound();
			}

			HashSet<Role> roles = db.Roles.ToHashSet();
			ViewBag.Roles = roles;

			return View(vm);
		}

		// POST: Employees/Edit/5
		// 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
		// 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(EmployeeEditVM vm)
		{
			if (ModelState.IsValid == false)
			{
                HashSet<Role> roles = db.Roles.ToHashSet();
                ViewBag.Roles = roles;
                return View(vm);
			}
			var empInDb = db.Employees.Find(vm.EmployeeId);

            if (empInDb == null) return HttpNotFound();
			// 之後補上Result類別後改成下面這行
			// if (empInDb == null) return Result.Fail("找不到要修改的會員記錄");

			// 因可能被主鍵約束限制,先清除,再塞資料
			empInDb.Roles.Clear();
            empInDb.Roles = db.Roles.Where(r => vm.RoleIds.Contains(r.RoleId)).ToList();
			db.SaveChanges();

			return RedirectToAction("Index");

			// 因post back後, vm繫結不到表單傳回來的RoleId, 因此自己宣告一個來繫結
			// 也因此下面的原來寫法是不行的(也沒有先Clear掉)
            // empInDb.Roles = vm.Roles;
		}

		// GET: Employees/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Employee emp = db.Employees.Find(id);
			if (emp == null)
			{
				return HttpNotFound();
			}
			var vm = emp.ToIndexVM();
			return View(vm);
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

		public ActionResult Login()
		{
			return View();
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
			var roleList = db.Roles.ToList().Prepend(new Role());
			ViewBag.RoleId = new SelectList(roleList, "RoleId", "RoleName", roleId);
		}
	}
}
