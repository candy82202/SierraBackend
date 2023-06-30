using NiceAdmin.Models.EFModels;
using NiceAdmin.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.Exts
{
	public static class DiscountGroupExts
	{
		public static DessertInDiscountGroup ToDessertInDiscountGroup(this Dessert entity)
		{
			return new DessertInDiscountGroup()
			{
				DessertId = entity.DessertId,
				DessertName = entity.DessertName,
				CategoryName = entity.Category.CategoryName,
				UnitPrice = entity.UnitPrice,
			};
		}
	}
}