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

        public static Coupon ToEntity(this CouponCreateVM vm)
        {
            return new Coupon()
            {
                CouponCategoryId = vm.CouponCategoryId,
                DiscountGroupId = vm.DiscountGroupId==0?null: vm.DiscountGroupId,
                CouponName = vm.CouponName,
                CouponCode = vm.CouponCode==null? Guid.NewGuid().ToString(): vm.CouponCode,
                LimitType = vm.LimitType==0?null: vm.LimitType,
                LimitValue = vm.LimitValue,
                DiscountType = vm.DiscountType,
                DiscountValue = vm.DiscountValue,
                StartAt = vm.StartAt,
                EndAt = vm.EndAt,
                Expiration = vm.Expiration,
                CreateAt= DateTime.Now
            };
        }
        public static CouponDetailVM ToDetailVM(this Coupon entity)
        {
                return new CouponDetailVM()
                {
                    CouponId = entity.CouponId,
                    CouponName = entity.CouponName,
                    CouponCategoryId=entity.CouponCategoryId,
                    CouponCategoryName = entity.CouponCategory.CouponCategoryName,
                    DiscountGroupName = entity.DiscountGroup==null?null: entity.DiscountGroup.DiscountGroupName,
                    CouponCode=entity.CouponCode,
                    LimitType=entity.LimitType,
                    LimitValue=entity.LimitValue,
                    DiscountType=entity.DiscountType,
                    DiscountValue=entity.DiscountValue,
                    StartAt = entity.StartAt,
                    EndAt = entity.EndAt,
                    Expiration = entity.Expiration,
                    CreateAt = entity.CreateAt
                };
        }

    }
}