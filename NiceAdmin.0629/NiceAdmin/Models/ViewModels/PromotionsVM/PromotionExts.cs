using NiceAdmin.Controllers.Promotions;
using NiceAdmin.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.ViewModels.PromotionsVM
{
    public static class PromotionExts
    {
        public static PromotionIndexVM ToIndexVM(this Promotion entity)
        {
            return new PromotionIndexVM()
            {
                PromotionImage=entity.PromotionImage,
                PromotionId=entity.PromotionId,
                PromotionName=entity.PromotionName,
                CouponName = entity.Coupon==null?"無":entity.Coupon.CouponName,
                Description=entity.Description,
                LaunchAt=entity.LaunchAt,
                StartAt=entity.StartAt,
                EndAt=entity.EndAt,
                CreateAt=entity.CreateAt
            };
        }
        public static Promotion ToEntity(this PromotionCreateVM vm)
        {
            return new Promotion()
            {
                PromotionImage = vm.PromotionImage,
                PromotionName = vm.PromotionName,
                CouponId = vm.CouponId==0?null: vm.CouponId,
                Description = vm.Description,
                LaunchAt = vm.LaunchAt,
                StartAt = vm.StartAt,
                EndAt = vm.EndAt,
                CreateAt = DateTime.Now
            };
        }
		public static PromotionEditVM ToEditVM(this Promotion entity)
		{
			return new PromotionEditVM()
			{
				PromotionImage = entity.PromotionImage,
                CouponId= entity.Coupon==null?0: entity.Coupon.CouponId,
				PromotionId = entity.PromotionId,
				PromotionName = entity.PromotionName,
				
				Description = entity.Description,
				LaunchAt = entity.LaunchAt,
				StartAt = entity.StartAt,
				EndAt = entity.EndAt,
				
			};
		}
		public static PromotionEditImageVM ToEditImageVM(this Promotion entity)
		{
			return new PromotionEditImageVM()
			{
				PromotionImage = entity.PromotionImage,
				PromotionId = entity.PromotionId,
				
			};
		}
        public static PromotionDetailVM ToDetailVM(this Promotion entity)
        {
            return new PromotionDetailVM()
            {
                PromotionImage = entity.PromotionImage,
                PromotionId = entity.PromotionId,
                PromotionName = entity.PromotionName,
                CouponName = entity.Coupon == null ? "無" : entity.Coupon.CouponName,
                Description = entity.Description,
                LaunchAt = entity.LaunchAt,
                StartAt = entity.StartAt,
                EndAt = entity.EndAt
            };
        }
        public static PromotionSlideVM ToSlideVM(this Promotion entity)
        {
            return new PromotionSlideVM()
            {
                PromotionImage = entity.PromotionImage,
                PromotionId = entity.PromotionId
            };
        }
    }
}