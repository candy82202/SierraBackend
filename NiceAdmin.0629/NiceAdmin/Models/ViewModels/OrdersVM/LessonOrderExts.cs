using NiceAdmin.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.ViewModels.OrdersVM
{
    public static class LessonOrderExts
    {
        public static LessonOrderIndexVM TOIndexVM(this LessonOrder entity)//ENTITY(資料庫的資料)轉成VM
        {
            var lessonOrderDetails = entity.LessonOrderDetails.ToList();
            return new LessonOrderIndexVM
            {
                LessonOrderId = entity.LessonOrderId,
                MemberName = entity.Member.MemberName,
                StatusName = entity.OrderStatus.StatusName,
                OrderStatusId = entity.OrderStatus.OrderStatusId,
                CreateTime = entity.CreateTime,
                LessonOrderTotal = entity.LessonOrderTotal,
                LessonOrderDetails = entity.LessonOrderDetails.ToList(),
                NumberOfPeople = entity.LessonOrderDetails.Sum(d => d.NumberOfPeople),
                LessonTitle = entity.LessonOrderDetails.Select(d => d.LessonTitle).FirstOrDefault()
            };
        }
    }
}