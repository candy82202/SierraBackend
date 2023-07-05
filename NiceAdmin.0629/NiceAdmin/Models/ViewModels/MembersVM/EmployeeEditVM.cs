using NiceAdmin.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace NiceAdmin.Models.ViewModels.MembersVM
{
    public class EmployeeEditVM
    {
        // 不能刪除,post back後要拿id做事情
        public int EmployeeId { get; set; }

        [Display(Name = "帳號")]
        [Required]
        [StringLength(50)]
        public string EmployeeName { get; set; }

        //[Display(Name = "角色")]
        //[Required]
        //[Range(1, int.MaxValue, ErrorMessage = "請選擇有效的角色")]
        //public int RoleId { get; set; }
        [Display(Name = "角色")]
        //[Required]
        public HashSet<Role> Roles { get; set; }

        [Display(Name = "請填選至少一個角色")]
        [Required]
        public int[] RoleIds { get; set; }
    }
}