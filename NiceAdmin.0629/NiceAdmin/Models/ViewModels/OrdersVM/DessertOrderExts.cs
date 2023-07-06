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
        public static DessertOrderIndexVM TOIndexVM(this DessertOrder entiey)//ENTITY(資料庫的資料)轉成VM
        {
            return new DessertOrderIndexVM
            {
                DessertOrderId = entiey.DessertOrderId,
                MemberName = entiey.Member.MemberName,
                StatusName = entiey.OrderStatus.StatusName,
                CreateTime = entiey.CreateTime,
                Recipient = entiey.Recipient,
                RecipientPhone = entiey.RecipientPhone,
                RecipientAddress = entiey.RecipientAddress,
                ShippingFee = entiey.ShippingFee,
                DessertOrderTotal = entiey.DessertOrderTotal,
                DeliveryMethod = entiey.DeliveryMethod,
                DessertOrderDetails = entiey.DessertOrderDetails.ToList(),
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