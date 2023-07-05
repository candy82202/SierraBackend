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

            //var teachers = db.Teachers.ToList();
            //var teacherViewModels = teachers.Select(t => t.TOIndexVM()).ToList();

            return View(query);
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
