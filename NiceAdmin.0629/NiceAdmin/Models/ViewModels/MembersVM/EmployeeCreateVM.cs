using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace NiceAdmin.Models.ViewModels.MembersVM
{
    public class EmployeeCreateVM
    {
        public int EmployeeId { get; set; }

        [Display(Name = "帳號")]
        [Required]
        [StringLength(50)]
        public string EmployeeName { get; set; }

        [Display(Name = "密碼")]
        [Required]
        [StringLength(200)]
        public string EncryptedPassword { get; set; }

        [Display(Name = "角色")]
        [Required]
        public int RoleId { get; set; }
    }
}