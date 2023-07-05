using NiceAdmin.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.ViewModels.PromotionsVM
{
	public class MemberCouponIndexVM
	{
		public int MemberId { get; set; }
		[Display(Name ="會員名稱")]
		public string MemberName { get; set; }
		public virtual ICollection<MemberCoupon> MemberCoupons { get; set; }
		[Display(Name = "最新獲得")]
		public string NewestCouponName { get; set; }
		[Display(Name = "最近使用")]
		public string LastestUsedCouponName { get; set; }
	}
}