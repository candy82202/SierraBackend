using Microsoft.Ajax.Utilities;
using NiceAdmin.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.ViewModels
{
    public static class DessertExts
    {
        public static DessertIndexVM ToIndexVM(this Dessert dessert) 
        {
            return new DessertIndexVM
            {
                DessertId=dessert.DessertId,
                DessertName=dessert.DessertName.Trim(),
                //CategoryName=dessert.Category.CategoryName,
                CategoryName = dessert.Category.CategoryName, // 修改此行
                Description =dessert.Description,
                UnitPrice=dessert.UnitPrice,
                Status=dessert.Status,
                CreateTime=dessert.CreateTime,

            };
        
        }
        public static DessertCreateVM ToCreatVM(this Dessert dessert)
        {
            return new DessertCreateVM
            { 
                DessertId=dessert.DessertId,
                DessertName=dessert.DessertName.Trim(),
                CategoryId=dessert.CategoryId,
                Description=dessert.Description,
                UnitPrice=dessert.UnitPrice,
                Status=dessert.Status,

            };
            
        }
        public static Dessert ToEntity(this DessertCreateVM vm) 
        {
            return new Dessert
            {
                DessertId = vm.DessertId,
                DessertName = vm.DessertName.Trim(),
                CategoryId = vm.CategoryId,
                Description = vm.Description,
                UnitPrice = vm.UnitPrice,
                Status = vm.Status
            };
           
        
        }
        public static DessertEditVM ToEditVM(this Dessert dessert) 
        {
            return new DessertEditVM 
            {
                DessertId = dessert.DessertId,
                DessertName=dessert.DessertName,
                CategoryId=dessert.CategoryId,
                Description=dessert.Description,
                UnitPrice=dessert.UnitPrice,
                Status=dessert.Status,

            
            };
        
        }
    }
}