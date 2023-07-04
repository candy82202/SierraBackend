using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace NiceAdmin.Models.ViewModels.PromotionsVM
{
	public class PromotionEditImageVM
	{
		public int PromotionId { get; set; }
		[StringLength(255)]
		[Display(Name = "原圖片")]
		public string PromotionImage { get; set; }
	}
}