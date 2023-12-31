﻿using NiceAdmin.Models.EFModels;
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

		[DataType(DataType.Password)]
		[Display(Name = "密碼")]
        [Required]
        [StringLength(200)]
        public string Password { get; set; }
        [Display(Name = "頭像")]
        [StringLength(255)]
        public string ImageName { get; set; }
        [Display(Name = "角色")]
        // [Required]
        public HashSet<Role> Roles { get; set; }

        //[Display(Name = "請填選至少一個角色")]
        [Required(ErrorMessage = "請填選至少一個角色")]
        public int[] RoleIds { get; set; }
    }
}