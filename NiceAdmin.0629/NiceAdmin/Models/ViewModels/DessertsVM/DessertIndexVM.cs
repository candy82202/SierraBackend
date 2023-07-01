using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.ViewModels
{
    public class DessertIndexVM
    {
        [Display(Name = "甜點編號")]
        public int DessertId { get; set; }
        [Display(Name = "甜點名稱")]
        [StringLength(50)]
        public string DessertName { get; set; }
        [Display(Name = "甜點類別")]
        //public string CategoryName { get; set; }
        public string CategoryName { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,#}")]
        [Display(Name = "售價")]
        public int? UnitPrice { get; set; }
     
        public string Description { get; set; }
        [Display(Name = "描述")]
       
        public string DescriptionText { get 
            {
                return this.Description.Length > 10
                        ? this.Description.Substring(0, 10) + "..."
                        : this.Description;
            } }
      
        public bool Status { get; set; }
        [Display(Name = "是否上架")]
        public string StatusText { get 
            {
                return this.Status == true
                    ? "上架" : "下架";
            } }

        [Display(Name = "創建時間")]
        public DateTime? CreateTime { get; set; }

    }
}