using NiceAdmin.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.ViewModels.PromotionsVM
{
	public class PromotionEditVM
	{
		public int PromotionId { get; set; }
		public int? CouponId { get; set; }
		[Required]
		[StringLength(255)]
		public string PromotionImage { get; set; }

		[Required]
		[StringLength(50)]
		public string PromotionName { get; set; }

		[Required]
		[StringLength(300)]
		public string Description { get; set; }

		public DateTime LaunchAt { get; set; }

		public DateTime StartAt { get; set; }

		public DateTime EndAt { get; set; }
		public virtual Coupon Coupon { get; set; }
	}
}