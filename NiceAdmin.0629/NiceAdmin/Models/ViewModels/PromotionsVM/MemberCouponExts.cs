using Microsoft.Ajax.Utilities;
using NiceAdmin.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Member = NiceAdmin.Models.EFModels.Member;

namespace NiceAdmin.Models.ViewModels.PromotionsVM
{
	public static class MemberCouponExts
	{
		public static MemberCouponIndexVM ToMemberCouponIndexVM(this Member entity)
		{
			if (entity.MemberCoupons.Any()) {
				return new MemberCouponIndexVM()
				{
					MemberId = entity.MemberId,
					MemberName = entity.MemberName,
					NewestCouponName = entity.MemberCoupons.OrderByDescending(mc => mc.CreateAt).Last().CouponName,
					LastestUsedCouponName = entity.MemberCoupons.Where(mc => mc.UseAt.HasValue).Any() ? entity.MemberCoupons.Where(mc => mc.UseAt.HasValue)
					.OrderByDescending(mc => mc.UseAt).Last().CouponName : null
				};
			}
			else
			{
				return new MemberCouponIndexVM()
				{
					MemberId = entity.MemberId,
					MemberName = entity.MemberName,
					NewestCouponName = null,
					LastestUsedCouponName = null
				};
			}
		}
		public static MemberCouponInDetail ToMemberCouponInDetail(this MemberCoupon entity)
		{

			return new MemberCouponInDetail()
			{
				MemberId= entity.MemberId,
				CouponId= entity.CouponId,
				CouponName= entity.CouponName,
				CreateAt= entity.CreateAt,
				UseAt= entity.UseAt,
				ExpireAt= entity.ExpireAt,
				Coupon = entity.Coupon
			};

		}

	}
}