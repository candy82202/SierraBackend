using NiceAdmin.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.ViewModels.MembersVM
{
	public static class EmployeeExts
	{
		public static EmployeeIndexVM ToIndexVM(this Employee emp)
		{
			var db = new AppDbContext();
			var empId = emp.EmployeeId;
			var roleId = db.EmployeeToRoles.FirstOrDefault(r => r.EmployeeId == empId).RoleId;
			var roleName = db.Roles.FirstOrDefault(r => r.RoleId == roleId).RoleName;
			return new EmployeeIndexVM
			{
				EmployeeId = emp.EmployeeId,
				EmployeeName = emp.EmployeeName,
				CreateAt = emp.CreateAt,
				RoleName = roleName

			};
		}

		public static EmployeeCreateVM ToCreateVM(this Employee emp)
		{
			return new EmployeeCreateVM
			{
				EmployeeId = emp.EmployeeId,
				EmployeeName = emp.EmployeeName,
				EncryptedPassword = emp.EncryptedPassword,
			};
		}
		public static EmployeeEditVM ToEditVM(this Employee emp)
		{
			return new EmployeeEditVM
			{
				EmployeeId = emp.EmployeeId,
				EmployeeName = emp.EmployeeName,
			};
		}
		public static Employee ToEntity(this EmployeeCreateVM vm)
		{
			return new Employee
			{
				EmployeeId = vm.EmployeeId,
				EmployeeName = vm.EmployeeName,
				EncryptedPassword = vm.EncryptedPassword,
				CreateAt = DateTime.Now
			};
		}
		public static Employee ToEntity(this EmployeeEditVM vm)
		{
			return new Employee
			{
				EmployeeId = vm.EmployeeId,
				EmployeeName = vm.EmployeeName,
			};
		}

	}
}