using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace NiceAdmin.Models.ViewModels
{
	public class DiscountGroupEditVM
	{
		public int DiscountGroupId { get; set; }

		[Required]
		[StringLength(50)]
		[Display(Name = "群組名稱")]
		public string DiscountGroupName { get; set; }

		public IEnumerable<DessertInDiscountGroup> Desserts { get; set; }

		public IEnumerable<DiscountGroupItemVM> DiscountGroupItems { get; set; }

	}
	public class DessertInDiscountGroup
	{
		public int? DessertId { get; set; }

		public string DessertName { get; set; }

		public string CategoryName { get; set; }

		public int? UnitPrice { get; set; }
	}
	public class DiscountGroupItemVM
	{
		public int DiscountGroupItemId { get; set; }

		public int DiscountGroupId { get; set; }

		public int? DessertId { get; set; }

		public DessertInDiscountGroup Dessert { get; set; }
	}

}