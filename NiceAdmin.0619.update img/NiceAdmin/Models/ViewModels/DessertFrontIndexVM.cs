using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace NiceAdmin.Models.ViewModels
{
    public class DessertFrontIndexVM
    {
        public int DessertId { get; set; }
        [Display(Name = "甜點名稱")]
        [StringLength(50)]
        public string DessertName { get; set; }
        [Display(Name = "甜點類別")]

        public string CategoryName { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,#}")]
        [Display(Name = "售價")]

        public string Description { get; set; }
        [Display(Name = "描述")]
        public string DescriptionText
        {
            get
            {
                return this.Description.Length > 10
                        ? this.Description.Substring(0, 10) + "..."
                        : this.Description;
            }
        }
        [Column("DessertImageName")]
        [StringLength(255)]
        [Display(Name = "照片")]
        public string DessertImageName { get; set; }


    }
}