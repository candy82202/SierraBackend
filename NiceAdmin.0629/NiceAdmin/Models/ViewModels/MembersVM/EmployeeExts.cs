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
            return new EmployeeIndexVM
            {
                EmployeeId = emp.EmployeeId,
                EmployeeName = emp.EmployeeName,
                CreateAt = emp.CreateAt
            };
        }

        public static EmployeeCreateVM ToCeateVM(this Employee emp)
        {
            return new EmployeeCreateVM
            {
                EmployeeId = emp.EmployeeId,
                EmployeeName = emp.EmployeeName,
                EncryptedPassword = emp.EncryptedPassword,
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
        // Edit 先pass
    }
}