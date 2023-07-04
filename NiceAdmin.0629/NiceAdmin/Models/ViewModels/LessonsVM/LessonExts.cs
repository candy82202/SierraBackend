using NiceAdmin.Models.EFModels;
using NiceAdmin.Models.ViewModels.LessonsVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.ViewModels.LessonsVM {
    public static class LessonExts {

        public static LessonIndexVM ToIndexVM(this Lesson lesson)
        {
            return new LessonIndexVM
            {
                LessonId = lesson.LessonId,
                LessonTitle = lesson.LessonTitle.Trim(),
                //CategoryName=dessert.Category.CategoryName,
                LessonCategoryName = lesson.LessonCategory.LessonCategoryName, // 修改此行
                LessonInfo = lesson.LessonInfo,
                LessonDetail= lesson.LessonDetail,
                LessonPrice = lesson.LessonPrice,
                LessonStatus = lesson.LessonStatus,
                LessonHours = lesson.LessonHours,
                LessonTime = lesson.LessonTime,
                LessonDessert = lesson.LessonDessert,
                TeacherName = lesson.Teacher.TeacherName,
                MaximumCapacity = lesson.MaximumCapacity,
                CreateTime = lesson.CreateTime,

            };

        }
        public static LessonCreateVM ToCreatVM(this Lesson lesson)
        {
            return new LessonCreateVM
            {
                //LessonId = lesson.LessonId,
                LessonTitle = lesson.LessonTitle.Trim(),
                LessonCategoryId = lesson.LessonCategoryId, // 修改此行
                LessonInfo = lesson.LessonInfo,
                LessonDetail = lesson.LessonDetail,
                LessonPrice = lesson.LessonPrice,
                LessonStatus = lesson.LessonStatus,
                LessonHours = lesson.LessonHours,
                LessonTime = lesson.LessonTime,
                LessonDessert = lesson.LessonDessert,
                TeacherId = lesson.TeacherId,
                MaximumCapacity = lesson.MaximumCapacity,

            };

        }
        public static Lesson ToEntity(this LessonCreateVM vm)
        {
            return new Lesson
            {
                //LessonId = vm.LessonId,
                LessonTitle = vm.LessonTitle.Trim(),
                LessonCategoryId = vm.LessonCategoryId, // 修改此行
                LessonInfo = vm.LessonInfo,
                LessonDetail = vm.LessonDetail,
                LessonPrice = vm.LessonPrice,
                LessonStatus = vm.LessonStatus,
                LessonHours = vm.LessonHours,
                LessonTime = vm.LessonTime,
                LessonDessert = vm.LessonDessert,
                TeacherId = vm.TeacherId,
                MaximumCapacity = vm.MaximumCapacity,
            };


        }
        public static DessertEditVM ToEditVM(this Dessert dessert)
        {
            return new DessertEditVM
            {
                DessertId = dessert.DessertId,
                DessertName = dessert.DessertName,
                CategoryId = dessert.CategoryId,
                Description = dessert.Description,
                UnitPrice = dessert.UnitPrice,
                Status = dessert.Status,


            };

        }
    }
}