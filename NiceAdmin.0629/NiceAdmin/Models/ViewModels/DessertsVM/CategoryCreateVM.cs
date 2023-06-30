using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace NiceAdmin.Models.ViewModels.DessertsVM
{
    public class CategoryCreateVM
    {
        public int CategoryId { get; set; }
        [Display(Name = "類別名稱")]
        [StringLength(50)]
        public string CategoryName { get; set; }
    }
}