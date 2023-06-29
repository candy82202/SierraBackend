using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace NiceAdmin.Models.ViewModels
{
    public class DessertEditVM
    {
        public int DessertId { get; set; }
        [Display(Name = "甜點名稱")]
        [StringLength(50)]
        public string DessertName { get; set; }
        [Display(Name = "甜點類別")]
        public int CategoryId { get; set; }
        [Display(Name = "售價")]
        public int UnitPrice { get; set; }
        [Display(Name = "描述")]
        [StringLength(200)]
        public string Description { get; set; }
        [Display(Name = "圖片")]
        public List<string> DessertImageNames { get; set; }
        [Display(Name = "是否上架")]
        public bool Status { get; set; }
        public DessertEditVM()
        {
            DessertImageNames = new List<string>();
        }
    }
}