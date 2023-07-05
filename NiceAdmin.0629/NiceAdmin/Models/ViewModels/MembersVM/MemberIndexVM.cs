using NiceAdmin.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.ViewModels.MembersVM
{
    public class MemberIndexVM
    {
		[Display(Name ="會員Id")]
		public int MemberId { get; set; }

		[Display(Name = "會員帳號")]
		[Required]
		[StringLength(50)]
		public string MemberName { get; set; }

		[Display(Name = "信箱")]
		[Required]
		[StringLength(50)]
		public string Email { get; set; }

		//[Required]
		//[StringLength(200)]
		//public string EncryptedPassword { get; set; }

		[Display(Name = "地址")]
		[Required]
		[StringLength(50)]
		public string Address { get; set; }

		[Display(Name = "電話")]
		[Required]
		[StringLength(10)]
		public string Phone { get; set; }

		[Display(Name = "註冊時間")]
		public DateTime CreateAt { get; set; }

		[Display(Name = "生日")]
		[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
		public DateTime Birth { get; set; }

		[Display(Name = "性別")]
		public bool Gender { get; set; }
		[Display(Name = "性別")]
		public string GenderText => Gender ? "男" : "女";

        [Display(Name = "圖片")]
		[StringLength(255)]
		public string ImageName { get; set; }

		[Display(Name = "是否黑名單")]
		public bool IsBan { get; set; }
		[Display(Name = "帳號狀態")]
		public string IsBanText => IsBan ? "黑名單" : "正常";

        [Display(Name = "訂單取消次數")]
		public int CancelTimes { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<DessertOrder> DessertOrders { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<LessonOrder> LessonOrders { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<MemberCoupon> MemberCoupons { get; set; }
	}
}