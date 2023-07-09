using NiceAdmin.Models.EFModels;
using NiceAdmin.Models.ViewModels.TeachersVM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace NiceAdmin.Models.ViewModels.OrdersVM
{
    public static class DessertOrderExts
    {
        public static DessertOrderIndexVM TOIndexVM(this DessertOrder entity)//ENTITY(資料庫的資料)轉成VM
        {
            var dessertOrderDetails = entity.DessertOrderDetails.ToList();
            return new DessertOrderIndexVM
            {
                DessertOrderId = entity.DessertOrderId,
                MemberName = entity.Member.MemberName,
                StatusName = entity.OrderStatus.StatusName,
                OrderStatusId= entity.OrderStatus.OrderStatusId,
                CreateTime = entity.CreateTime,
                Recipient = entity.Recipient,
                RecipientPhone = entity.RecipientPhone,
                RecipientAddress = entity.RecipientAddress,
                ShippingFee = entity.ShippingFee,
                DessertOrderTotal = entity.DessertOrderTotal,
                DeliveryMethod = entity.DeliveryMethod,
                DessertOrderDetails = entity.DessertOrderDetails.ToList(),
                Quantity = entity.DessertOrderDetails.Sum(d => d.Quantity),
                DessertName = entity.DessertOrderDetails.Select(d => d.DessertName).FirstOrDefault()
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

        public static DessertOrderDetailIndexVM TOIndexVM(this DessertOrderDetail entiey)//ENTITY(資料庫的資料)轉成VM
        {
            return new DessertOrderDetailIndexVM
            {

                DessertName = entiey.DessertName,


                Quantity = entiey.Quantity,

                DessertUnitPrice = entiey.DessertUnitPrice,

                Subtotal = entiey.Subtotal
            };
        }
    }
}