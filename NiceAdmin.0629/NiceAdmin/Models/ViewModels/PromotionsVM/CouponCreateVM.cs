using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.ViewModels.PromotionsVM
{
    public class CouponCreateVM
    {
        [Display(Name = "優惠券類別")]
        [Required]
        public int CouponCategoryId { get; set; }
        [Display(Name = "優惠對象")]
        public int? DiscountGroupId { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "優惠券名稱")]
        public string CouponName { get; set; }
        [StringLength(50)]
        [Display(Name = "優惠碼")]
		[RequiredIf("CouponCategoryId", 4, ErrorMessage = "優惠碼為必填")]
		public string CouponCode { get; set; }
        [Display(Name = "優惠限制")]
        [Required]
        public int? LimitType { get; set; }
        [Display(Name = "限制數值")]
		[RequiredIf("LimitType", 1, 2, ErrorMessage = "當折扣限制為消費數量或消費金額時，限制數值為必填")]
		public int? LimitValue { get; set; }
        [Display(Name = "優惠類型")]
        [Required]
        public int DiscountType { get; set; }
		[Display(Name = "優惠數值")]
		[RequiredIf("DiscountType", 1, 2, ErrorMessage = "當折扣類型為折價或折扣時，折扣數值為必填")]
		public int? DiscountValue { get; set; }
        [Display(Name = "開始時間")]
		[RequiredIf("CouponCategoryId", 2,4, ErrorMessage = "開始時間為必填")]
		[DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? StartAt { get; set; }
        [Display(Name = "結束時間")]
		[RequiredIf("CouponCategoryId", 2, 4, ErrorMessage = "結束時間為必填")]
		[DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? EndAt { get; set; }
        [Display(Name = "使用期限")]
		[RequiredIf("CouponCategoryId", 1,3,4,5,6, ErrorMessage = "使用期限為必填")]
		public int? Expiration { get; set; }
    }
	
}