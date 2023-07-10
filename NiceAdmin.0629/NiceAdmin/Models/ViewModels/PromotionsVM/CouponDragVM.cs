using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.ViewModels.PromotionsVM
{
    public class CouponDragVM
    {
        public int CouponId { get; set; }
        public int CouponCategoryId { get; set; }
        public string CouponName { get; set; }
    }
}