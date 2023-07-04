using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace NiceAdmin.Models.ViewModels.DessertsVM
{
    public class DessertIndexPartVM
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
        public int UnitPrice { get; set; }

        public bool Status { get; set; }
        [Display(Name = "是否上架")]
        public string StatusText
        {
            get
            {
                return this.Status == true
                    ? "上架" : "下架";
            }
            private set { } // Add a private setter
        }

        [Display(Name = "創建時間")]
        public DateTime? CreateTime { get; set; }
        [Column("DessertImageName")]
        [StringLength(255)]
        [Display(Name = "照片")]
        public string DessertImageName { get; set; }

    }
}