

namespace NiceAdmin.Models.ViewModels.MembersVM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Xml.Linq;
    public class EmployeeIndexVM
    {
        [Display(Name = "員工ID")]
        public int EmployeeId { get; set; }

        [Display(Name = "帳號")]
        [Required]
        [StringLength(50)]
        public string EmployeeName { get; set; }

        [Display(Name = "創建時間")]
        public DateTime CreateAt { get; set; }

		[Display(Name = "角色")]
		public string RoleName{ get; set; }
    }
}