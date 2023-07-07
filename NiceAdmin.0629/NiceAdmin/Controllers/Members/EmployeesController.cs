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
using NiceAdmin.Filters;
using NiceAdmin.Models.EFModels;
using NiceAdmin.Models.Infra;
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
		[Authorize(Roles= "dessertSale,lessonSale")]
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
		[Authorize(Roles = "dessertSale,lessonSale")]
		public ActionResult Create(EmployeeCreateVM vm)
		{
			if (ModelState.IsValid == false)
			{
				HashSet<Role> roles = db.Roles.ToHashSet();
				ViewBag.Roles = roles;
				return View(); 
			}
			// bool isNameExist = db.Employees.ToList().Any(e => e.EmployeeName.ToLower() == vm.EmployeeName.ToLower());
			bool isNameExist = db.Employees.Any(e => e.EmployeeName.Equals(vm.EmployeeName, StringComparison.OrdinalIgnoreCase));
			if (isNameExist)
			{
				ModelState.AddModelError("EmployeeName", "帳號重複");
				HashSet<Role> roles = db.Roles.ToHashSet();
				ViewBag.Roles = roles;
				return View(vm);
			}
			// bool isNameExist = db.Employees.ToList().Select(x => x.EmployeeName).Contains(vm.EmployeeName);
			//bool isNameExist = db.Employees.ToList().Any(e => e.EmployeeName == vm.EmployeeName);
			//if (ModelState.IsValid == false || isNameExist)
			//{
			//	ModelState.AddModelError("Employee", "帳號重複");
			//	HashSet<Role> roles = db.Roles.ToHashSet();
			//	ViewBag.Roles = roles;
			//	return View(vm);
			//}

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
			var LT = db.Lessons.ToList();
			var LT2 = db.Lessons.ToList().Where(l => l.TeacherId == id);
			var LT22 = db.Lessons.ToList().Select(l => l.LessonTime);
			var LT23 = db.Lessons.ToList().Select(l => l.TeacherId);
			var LT3 = db.Lessons.Where(l => l.TeacherId == id).ToList();

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
		[CustomAuthorize(Roles= "dessertSale,lessonSale")]
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
		[CustomAuthorize(Roles = "dessertSale,lessonSale")]
		public ActionResult DeleteConfirmed(int id)
		{
			Employee employee = db.Employees.Find(id);
			db.Employees.Remove(employee);
			db.SaveChanges();
			return RedirectToAction("Index");
		}
		[HttpGet]
		[AllowAnonymous]
		public ActionResult Login()
		{
			var vm = new LoginVM() { Account = "Admin", Password = "123" };
			return View(vm);
		}
		
		[HttpPost]
		[ValidateAntiForgeryToken]
		[AllowAnonymous]
		public ActionResult Login(LoginVM vm)
		{
			if (ModelState.IsValid == false) return View(vm);

			// 驗證帳密
			Result result = ValidLogin(vm);

			// 驗證失敗
			if (result.ISuccess == false)
			{
				ModelState.AddModelError(string.Empty, result.ErrorMessage);
				return View(vm);
			}

			const bool rememberMe = false; // 泡麵哥證實不一定會記得

			// 驗證正確, 將登入帳號編碼後, 加入cookie
			var processResult = ProcessLogin(vm.Account, rememberMe);

			Response.Cookies.Add(processResult.cookie);
			return Redirect(processResult.returnUrl);
		}

		public ActionResult Logout()
		{
			Session.Abandon();
			FormsAuthentication.SignOut();
			return Redirect("/Employees/Login");
		}

		private (string returnUrl, HttpCookie cookie) ProcessLogin(string account, bool rememberMe)
		{
			// var roles = string.Empty; // 在本範例, 沒有用到角色權限,所以存入空白

			var roles = db.Employees.ToList()
						.FirstOrDefault(emp => string.Compare(emp.EmployeeName, account, true) == 0)
						.Roles.Select(r=>r.RoleName);
			var rolesStr = string.Join(",", roles);


			// 建立一張認證票
			var ticket =
				new FormsAuthenticationTicket(
					1,          // 版本別, 沒特別用處
					account,
					DateTime.Now,   // 發行日
					DateTime.Now.AddDays(2), // 到期日
					rememberMe,     // 是否續存
					rolesStr,          // userdata
					"/" // cookie位置
				);

			// 將它加密
			var value = FormsAuthentication.Encrypt(ticket);

			// 存入cookie
			var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, value);
			cookie.Expires = DateTime.Now.AddDays(3);

			// 取得return url
			var url = FormsAuthentication.GetRedirectUrl(account, true); //第二個引數沒有用處

			return (url, cookie);
		}

		private Result ValidLogin(LoginVM vm)
		{
			var emp = db.Employees.FirstOrDefault(e => e.EmployeeName == vm.Account);
			if (emp == null) return Result.Fail("帳密有誤");

			var salt = HashUtility.GetSalt();
			var hashPwd = HashUtility.ToSHA256(vm.Password, salt);

			return string.Compare(emp.EncryptedPassword, hashPwd) == 0
					? Result.Success()
					: Result.Fail("帳密有誤");
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
