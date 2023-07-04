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
		[Display(Name = "相關優惠券")]
		public int? CouponId { get; set; }
		[StringLength(255)]
		[Display(Name = "圖片")]
		public string PromotionImage { get; set; }
		[Required]
		[StringLength(50)]
		[Display(Name = "活動名稱")]
		public string PromotionName { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "活動詳情")]
		public string Description { get; set; }
		[Display(Name = "發佈時間")]
		public DateTime LaunchAt { get; set; }
		[Display(Name = "開始時間")]
		public DateTime StartAt { get; set; }
		[Display(Name = "結束時間")]
		public DateTime EndAt { get; set; }
		public virtual Coupon Coupon { get; set; }
	}
}