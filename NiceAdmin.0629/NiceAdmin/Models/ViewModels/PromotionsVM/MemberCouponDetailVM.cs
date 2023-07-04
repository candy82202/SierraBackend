using NiceAdmin.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.ViewModels.PromotionsVM
{
	public class MemberCouponDetailVM
	{
		public int MemberId { get; set; }
		public string MemberName { get; set; }
		public IEnumerable<MemberCouponInDetail> MemberCoupons { get; set; }
	}

	public class MemberCouponInDetail
	{
		public int MemberId { get; set; }
		public int CouponId { get; set; }

		public string CouponName { get; set; }
		public DateTime CreateAt { get; set; }

		public DateTime? UseAt { get; set; }

		public DateTime ExpireAt { get; set; }
		public string StatusText
		{
			get { 
				if (this.UseAt != null) return "已使用";
				if (this.ExpireAt < DateTime.Now) return "已過期";
				if (this.Coupon.StartAt > this.CreateAt) return "未啟用";
				else return "可使用";
			}
		}
		public virtual Coupon Coupon { get; set; }
	}
}