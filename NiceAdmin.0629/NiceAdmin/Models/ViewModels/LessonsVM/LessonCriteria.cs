using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.ViewModels.LessonsVM {
    public class LessonCriteria {
            public string LessonTitle { get; set; }
            public int? LessonCategoryId { get; set; }
            public int? MinPrice { get; set; }
            public int? MaxPrice { get; set; }
        
    }
}