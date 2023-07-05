using NiceAdmin.Models.DTOs;
using NiceAdmin.Models.EFModels;
using NiceAdmin.Models.ViewModels.TeachersVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.ViewModels.OrdersVM
{
    public static class DessertOrderExts
    {
        public static DessertOrderIndexVM TOIndexVM(this DessertOrderIndexDto dto)//ENTITY(資料庫的資料)轉成VM
        {
            return new DessertOrderIndexVM
            {
<<<<<<< HEAD
                DessertOrderId = entiey.DessertOrderId,
                MemberName = entiey.Member.MemberName,
                StatusName = entiey.OrderStatus.StatusName,
                CreateTime = entiey.CreateTime,
                Recipient = entiey.Recipient,
                RecipientPhone = entiey.RecipientPhone,
                RecipientAddress = entiey.RecipientAddress,
                ShippingFee= entiey.ShippingFee,
                DessertOrderTotal= entiey.DessertOrderTotal,
                DeliveryMethod = entiey.DeliveryMethod,
                DessertOrderDetails = entiey.DessertOrderDetails.ToList(),
=======
                DessertOrderId = dto.DessertOrderId,
               
                MemberName = dto.MemberName,
                StatusName = dto.StatusName,
                CreateTime = dto.CreateTime,
                Recipient = dto.Recipient,
                RecipientPhone = dto.RecipientPhone,
                RecipientAddress = dto.RecipientAddress,
                ShippingFee= dto.ShippingFee,
                DessertOrderTotal= dto.DessertOrderTotal,
                DeliveryMethod = dto.DeliveryMethod
>>>>>>> 7b25fefe37bb4b17a354d3f860c531aa5c962321
            };
        }

        public static DessertOrder TOIndexEntity(this DessertOrderIndexVM vm)//VM傳回給ENTITY
        {
            return new DessertOrder
            {
                DessertOrderId = vm.DessertOrderId,
                MemberId = Convert.ToInt16(vm.MemberName),
                DessertOrderStatusId = Convert.ToInt16(vm.StatusName),
                CreateTime = vm.CreateTime,
                Recipient = vm.Recipient,
                RecipientPhone = vm.RecipientPhone,
                RecipientAddress = vm.RecipientAddress,
                ShippingFee = vm.ShippingFee,
                DessertOrderTotal = vm.DessertOrderTotal,
                DeliveryMethod = vm.DeliveryMethod
            };
        }
        public static DessertOrderIndexDto TOIndexDto(this DessertOrder entity)
        {
            return new DessertOrderIndexDto
            {
                DessertOrderId = entity.DessertOrderId,
                MemberName = entity.Member.MemberName,
                StatusName = entity.OrderStatus.StatusName,
                //MemberId = entity.MemberId,
                //DessertOrderStatusId = entity.DessertOrderStatusId,
                CreateTime = entity.CreateTime,
                Recipient = entity.Recipient,
                RecipientPhone = entity.RecipientPhone,
                RecipientAddress = entity.RecipientAddress,
                ShippingFee = entity.ShippingFee,
                DessertOrderTotal = entity.DessertOrderTotal,
                DeliveryMethod = entity.DeliveryMethod
            };
        }
    }
}