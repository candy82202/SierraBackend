using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NiceAdmin.Models.EFModels;
using NiceAdmin.Models.ViewModels;
using NiceAdmin.Models.ViewModels.TeachersVM;

namespace NiceAdmin.Controllers
{
    public class TeachersController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Teachers
        public ActionResult Index(TeacherCriteria criteria)
        {
           
            ViewBag.Criteria = criteria;
            //查詢紀錄，由於第一次進到網頁時，criteria是沒有值的
            var query = db.Teachers.ToList().Select(t=>t.TOIndexVM());//T指ENTITY
            if (string.IsNullOrEmpty(criteria.TeacherName) == false)
            {
                query = query.Where(t=>t.TeacherName.Contains(criteria.TeacherName)).ToList();
            }
            if (string.IsNullOrEmpty(criteria.TeacherStatusText) == false)
            {
                query = query.Where(t => t.TeacherStatusText.Contains(criteria.TeacherStatusText)).ToList();
            }
            // 清除搜尋條件
            if (string.IsNullOrEmpty(criteria.TeacherName) && string.IsNullOrEmpty(criteria.TeacherStatusText))
            {
                criteria = new TeacherCriteria();
            }
            //var teachers = db.Teachers.ToList();
            //var teacherViewModels = teachers.Select(t => t.TOIndexVM()).ToList();

            return View(query);
        }
        // 清除搜尋條件的動作方法
        public ActionResult ClearSearch()
        {
            return RedirectToAction("Index", new TeacherCriteria());
        }
        // GET: Teachers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // GET: Teachers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Teachers/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TeacherCreateVM vm,HttpPostedFileBase file1)
        {
            if (ModelState.IsValid)
            {
                //將file1存檔，並取得最後存檔檔案名稱
                string path = Server.MapPath("/Uploads");
                string fileName = SaveUploadedFile(path, file1);

                //將換完檔名的fileName存入vm裡
                vm.TeacherImage = fileName;
                //將view model轉型為Teacher
                Teacher teacher = vm.TOEntity();

                //新增一筆紀錄
                db.Teachers.Add(teacher);
                db.SaveChanges();

                
                return RedirectToAction("Index");
                //如果驗證失敗，停留在本網頁

                
            }

            
            return View(vm);
        }

        private string SaveUploadedFile(string path, HttpPostedFileBase file1)
        {
            //如果沒有上傳檔案或檔案是空的，就不處理，傳回string.empty
            if(file1 == null ||file1.ContentLength == 0) return string.Empty;
            //取得上傳檔案的副檔名
            string ext =System.IO.Path.GetExtension(file1.FileName);
            

            //如果副檔名不在允許的範圍裡，表示上傳不合理的檔案類型，就不處理，傳回string.empty
            string[] allowedExts = new string[] { ".jpg",".jpeg",".png",".tif"};
            if(allowedExts.Contains(ext.ToLower())==false) return string.Empty;
            //生成一個不會重複的檔名(newFileName檔案)
            string newFileName =Guid.NewGuid().ToString("N")+ext;
           
            string fullName =System.IO.Path.Combine(path,newFileName);
            //將上傳檔案存放到指定位置(fullName路徑)
            file1.SaveAs(fullName);
            //傳回存放的檔名(檔案)
            return newFileName;
        }

        // GET: Teachers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            

            return View(teacher.TOEditVM());
        }

        // POST: Teachers/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TeacherEditVM vm)
        {
            if (ModelState.IsValid)
            {

                var TeacherInDB = db.Teachers.Find(vm.TeacherId);
                TeacherInDB.TeacherName = vm.TeacherName;
                TeacherInDB.Specialty = vm.Specialty;
                TeacherInDB.Experience = vm.Experience;
                TeacherInDB.License = vm.License;
                TeacherInDB.Academic = vm.Academic;
                TeacherInDB.TeacherStatus = vm.TeacherStatus;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        public ActionResult EditImage(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }


            return View(teacher.TOEditImageVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditImage(TeacherEditImageVM vm, HttpPostedFileBase file1)
        {
            if (ModelState.IsValid)
            {
                //將file1存檔，並取得最後存檔檔案名稱
                string path = Server.MapPath("/Uploads");


                string fileName = SaveUploadedFile(path, file1);

                //將換完檔名的fileName存入vm裡
                vm.TeacherImage = fileName;
                //將view model轉型為Teacher
                Teacher teacher = vm.TOEntity();

                //teacher.TeacherImage = fileName;

                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();


                return RedirectToAction("Index");
                //如果驗證失敗，停留在本網頁


            }


            return View(vm);
        }

        



        // GET: Teachers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }

            TeacherDeleteVM vm = teacher.TODeleteVM(); 
            return View(vm);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            db.Teachers.Remove(teacher);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult UpTeachers(List<int> teacherIds)
        {
            if (teacherIds == null || teacherIds.Count == 0)
            {
                return View("Error");
                // 如果沒有選擇任何老師，可以在這裡給出提示或執行相應處理             
            }
            else
            {
                try
                {
                    // 根据选择的teacherIds进行在職处理
                    var teachesToUpdate = db.Teachers.Where(t => teacherIds.Contains(t.TeacherId)).ToList();
                    foreach (var teacher in teachesToUpdate)
                    {
                        teacher.TeacherStatus = true; // 在職
                    }
                    // 更新数据库
                    db.SaveChanges();
                    // 重新導向回教師清單
                    //return RedirectToAction("Index");
                    //return Json(new { success = true });
                    return Json(new { success = true, message = "上架成功" });
                }
                catch
                {
                    return Json(new { success = false, errorMessage = "查無教師資料" });
                }
            }
        }
        [HttpPost]
        public ActionResult DownTeachers(List<int> teacherIds)
        {
            if (teacherIds == null || teacherIds.Count == 0)
            {
                return View("Error");
                // 如果沒有選擇任何教師，可以在這裡給出提示或執行相應處理             
            }
            else
            {
                try
                {
                    // 根据选择的teacherIds进行下架处理
                    var teachersToUpdate = db.Teachers.Where(t => teacherIds.Contains(t.TeacherId)).ToList();
                    foreach (var teacher in teachersToUpdate)
                    {
                        teacher.TeacherStatus = false; // 離職
                    }
                    // 更新数据库
                    db.SaveChanges();
                    // 重新導向回師資清單
                    //return RedirectToAction("Index");
                    //return Json(new { success = true });
                    return Json(new { success = true, message = "下架成功" });
                }
                catch
                {
                    return Json(new { success = false, errorMessage = "查無教師資料" });
                }
            }
        }
       
        public ActionResult UpdateTeacherStatus(int teacherId, bool teacherStatus)
        {
            try
            {
                var teacherToUpdate = db.Teachers.Find(teacherId);
                if (teacherToUpdate == null)
                {
                    return Json(new { success = false, errorMessage = "Invalid teacher ID." });
                }

                bool previousStatus = teacherToUpdate.TeacherStatus; // Get the previous status
                teacherToUpdate.TeacherStatus = teacherStatus; // Set status based on the parameter

                // Join Lessons table to retrieve LessonTitle
                var teacherWithLessons = from teacher in db.Teachers
                                         join lesson in db.Lessons on teacher.TeacherId equals lesson.TeacherId
                                         where teacher.TeacherId == teacherId
                                         select new
                                         {
                                             Teacher = teacher,
                                             LessonTitle = lesson.LessonTitle
                                         };

                // Retrieve the LessonTitle from the joined result
                string lessonTitle = teacherWithLessons.FirstOrDefault()?.LessonTitle;

                db.SaveChanges();

                string message = teacherStatus ? "上架成功" : "下架成功";

                if (teacherStatus && previousStatus)
                {
                    message = "此教師已在職";
                }
                else if (!teacherStatus && !previousStatus)
                {
                    message = "此教師已離職";
                }

                return Json(new { success = true, message, lessonTitle });
            }
            catch
            {
                return Json(new { success = false, errorMessage = "查無教師資料" });
            }
        }


        public ActionResult DownTeachers()
        {
            var downTeachers = db.Teachers
        .Join(db.Lessons,
            teacher => teacher.TeacherId,
            lesson => lesson.TeacherId,
            (teacher, lesson) => new { Teacher = teacher, Lesson = lesson })
        .Where(joinResult => joinResult.Teacher.TeacherStatus == false)
        .OrderByDescending(joinResult => joinResult.Lesson.CreateTime)
        .ToList() // 执行查询并将结果映射到内存中的列表
        .Select(joinResult => joinResult.Teacher.ToIndexPartVM(joinResult.Lesson.CreateTime))
        .ToList();

            return PartialView("DownTeachers", downTeachers);
            
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
    

    
}
