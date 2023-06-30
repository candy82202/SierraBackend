using NiceAdmin.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.ViewModels.PromotionsVM
{
    public static class CouponExts
    {
        public static CouponIndexVM ToIndexVM(this Coupon entity)
        {
            //因為entity.DiscountGroup有可能是null所以要分兩種，不然會出錯
            if (entity.DiscountGroupId == null)
            {
                return new CouponIndexVM()
                {
                    CouponId = entity.CouponId,
                    CouponName = entity.CouponName,
                    CouponCategoryName = entity.CouponCategory.CouponCategoryName,
                    DiscountGroupName = null,
                    CreateAt = entity.CreateAt,
                    EndAt = entity.EndAt
                };
            }
            else { 
                return new CouponIndexVM()
                {
                    CouponId = entity.CouponId,
                    CouponName = entity.CouponName,
                    CouponCategoryName = entity.CouponCategory.CouponCategoryName,
                    DiscountGroupName = entity.DiscountGroup.DiscountGroupName,
                    CreateAt = entity.CreateAt,
                    EndAt = entity.EndAt
                };
            }
        }
    }
}