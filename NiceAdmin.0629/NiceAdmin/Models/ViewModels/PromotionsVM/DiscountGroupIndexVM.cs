﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace NiceAdmin.Models.ViewModels
{
    public class DiscountGroupIndexVM
    {
        [Display(Name = "群組編號")]
        public int DiscountGroupId { get; set; }

        
        [Display(Name = "群組名稱")]
        public string DiscountGroupName { get; set; }

        [Display(Name = "群組商品")]
        public string DiscountGroupItems { get; set; }
    }
}