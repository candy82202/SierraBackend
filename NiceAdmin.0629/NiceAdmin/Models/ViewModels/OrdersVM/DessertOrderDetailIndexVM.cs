using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.ViewModels.OrdersVM
{
    public class DessertOrderDetailIndexVM
    {
        public int DessertOrderDetailId { get; set; }
        [Display(Name = "甜點名稱")]
        public string DessertName { get; set; }
        [Display(Name = "數量")]
        public int Quantity { get; set; }
        [Display(Name = "單價")]
        public int DessertUnitPrice { get; set; }
        [Display(Name = "小計")]
        public int Subtotal { get; set; }
    }
}