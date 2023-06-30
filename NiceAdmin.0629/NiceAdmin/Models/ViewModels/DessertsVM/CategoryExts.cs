using NiceAdmin.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.ViewModels.DessertsVM
{
    public static class CategoryExts
    {
        public static CategoryCreateVM ToCreateVM(this Category category)
        {
            return new CategoryCreateVM
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
            };
        }
        public static Category ToEntity(this CategoryCreateVM vm)
        {
            return new Category
            {
                CategoryId = vm.CategoryId,
                CategoryName = vm.CategoryName,
            };
        }
    }
}