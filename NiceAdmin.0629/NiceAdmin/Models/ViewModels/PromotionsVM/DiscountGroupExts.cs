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
        public static DiscountGroupIndexVM ToIndexVM(this DiscountGroup entity)
        {
			IEnumerable<DiscountGroupItem> items = entity.DiscountGroupItems;
			if (!items.Any())
			{
				return new DiscountGroupIndexVM()
                {
                    DiscountGroupId = entity.DiscountGroupId,
                    DiscountGroupName = entity.DiscountGroupName,
                    DiscountGroupItems = "無"
                };
            }
			else { 
				IEnumerable<string> dessertsName = items.Select(x => x.Dessert.DessertName);
				int count = items.Count();
				string countString = $"[共{count}項產品]";
				string itemString = string.Join(", ", dessertsName) ;
				string discountGroupItems = $"{countString}{itemString}";

				return new DiscountGroupIndexVM()
				{
					DiscountGroupId=entity.DiscountGroupId,
					DiscountGroupName=entity.DiscountGroupName,
					DiscountGroupItems = discountGroupItems
				};
            }
        }
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