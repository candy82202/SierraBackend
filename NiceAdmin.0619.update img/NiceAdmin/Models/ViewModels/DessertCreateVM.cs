﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.ViewModels
{
    public class DessertCreateVM
    {
        public int DessertId { get; set; }
        [Display(Name = "甜點名稱")]
        [Required]
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
        public List<string> Images { get; set; }
        [Display(Name = "上架")]
        public bool Status { get; set; }

        public DessertCreateVM()
        {
            Images = new List<string>();
        }
    }

}