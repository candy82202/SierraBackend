using NiceAdmin.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using NiceAdmin.Models.ViewModels;

namespace NiceAdmin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.CurrentTime = DateTime.Now;
            return View();
        }
        private AppDbContext db = new AppDbContext();
        public ActionResult Sierras(int? cId)
        {
            List<DessertFrontIndexVM> dvm = new List<DessertFrontIndexVM>();

            // 判斷如果有傳入類別編號，就篩選那個類別的商品出來
            if (cId != null)
            {
                var result = db.Categories.SingleOrDefault(x => x.CategoryId == cId);
                if (result != null)
                {
                    var desserts = result.Desserts.Where(d => d.Status).ToList();
                    foreach (var dessert in desserts)
                    {
                        DessertFrontIndexVM item = new DessertFrontIndexVM
                        {
                            DessertId = dessert.DessertId,
                            DessertName = dessert.DessertName,
                            CategoryName = result.CategoryName,
                            Description = dessert.Description,
                            DessertImageName = db.DessertImages.FirstOrDefault(di => di.DessertId == dessert.DessertId)?.DessertImageName
                        };
                        dvm.Add(item);
                    }
                }
            }
            else
            {
                var desserts = db.Desserts.Include("Category")
                                         .Include("DessertImages")
                                         .Where(d => d.Status)
                                         .ToList();
                foreach (var dessert in desserts)
                {
                    DessertFrontIndexVM item = new DessertFrontIndexVM
                    {
                        DessertId = dessert.DessertId,
                        DessertName = dessert.DessertName,
                        CategoryName = dessert.Category.CategoryName,
                        Description = dessert.Description,
                        DessertImageName = dessert.DessertImages.FirstOrDefault()?.DessertImageName
                    };
                    dvm.Add(item);
                }
            }

            ViewBag.count = dvm.Count;

            return View(dvm);
        }
        public ActionResult CurrentTime()
        {
            ViewBag.CurrentTime = DateTime.Now;
            //這裡刻意取不同名字，來去跳轉到不同的頁面
            return this.PartialView("_CurrentTimePartial");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Sierra() 
        {
            return View();
        }
        public ActionResult FormsLayouts() { return View(); }
        public ActionResult UsersProfile() { return View(); }
        public ActionResult FormEdit() { return View(); }
        public ActionResult DataTable() { return View(); }
    }
}