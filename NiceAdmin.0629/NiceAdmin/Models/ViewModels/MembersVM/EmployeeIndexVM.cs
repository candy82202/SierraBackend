

namespace NiceAdmin.Models.ViewModels.MembersVM
{
    using NiceAdmin.Models.EFModels;
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

        [Display(Name = "員工帳號")]
        [Required]
        [StringLength(50)]
        public string EmployeeName { get; set; }

        [Display(Name = "入職時間")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")]
        public DateTime CreateAt { get; set; }

        [Display(Name = "大頭貼")]
        [StringLength(255)]
        public string ImageName { get; set; }

        [Display(Name = "角色")]
        public HashSet<Role> Roles { get; set; }
    }
}