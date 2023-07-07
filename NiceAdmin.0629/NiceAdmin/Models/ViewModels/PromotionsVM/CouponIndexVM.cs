using NiceAdmin.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.ViewModels.PromotionsVM
{
	public class CouponIndexVM
	{
		public int CouponId { get; set; }
		[Display(Name ="優惠券名稱")]
		public string CouponName { get; set; }
		[Display(Name = "優惠券類別")]
		public string CouponCategoryName { get; set; }
		
		public string DiscountGroupName { get; set; }
        [Display(Name = "優惠對象")]
		public string DiscountTarget
		{
			get { return this.DiscountGroupName == null ? "全部商品" : this.DiscountGroupName; }
		}
        [Display(Name = "創建時間")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
		public DateTime CreateAt { get; set; }
		
		public DateTime? EndAt { get; set; }
		[Display(Name = "結束時間")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
		public string EndAtText
		{
			get
			{
				if (this.EndAt == null) return "無";
				else
				{
					DateTime endAtNotNull = (DateTime)this.EndAt;
					return endAtNotNull.ToString("yyyy-MM-dd");
				}
			}
		}
		public bool Status { get; set; }
		[Display(Name = "是否上架")]
        public string StatusText {
			get
			{
				if (this.Status) return "上架";
				else return "下架";
			}
		}
		public virtual CouponCategory CouponCategory { get; set; }

		public virtual DiscountGroup DiscountGroup { get; set; }
	}
}