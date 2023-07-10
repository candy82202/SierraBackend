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
using NiceAdmin.Models.ViewModels.DessertsVM;
using System.Runtime.InteropServices.ComTypes;
using NiceAdmin.Models.ViewModels.MembersVM;
using NiceAdmin.Models.ViewModels.PromotionsVM;
namespace NiceAdmin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.CurrentTime = DateTime.Now;
            ViewBag.HotProducts = GetHotProducts();
            ViewBag.TotalDessertsCount = GetTotalDessertsCount();
            ViewBag.TotalLessonsCount = GetTotalDessertsCount();
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
                    //var desserts = result.Desserts.Where(d => d.Status).ToList();
                    var desserts = result.Desserts.Where(d => d.Status || (d.ScheduledPublishDate.HasValue && d.ScheduledPublishDate <= DateTime.Now)).ToList();
                    foreach (var dessert in desserts)
                    {
                        DessertFrontIndexVM item = new DessertFrontIndexVM
                        {
                            DessertId = dessert.DessertId,
                            DessertName = dessert.DessertName,
                            CategoryName = result.CategoryName,
                            UnitPrice = dessert.UnitPrice,
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
                        UnitPrice = dessert.UnitPrice,
                        Description = dessert.Description,
                        DessertImageName = dessert.DessertImages.FirstOrDefault()?.DessertImageName
                    };
                    dvm.Add(item);
                }
            }

            ViewBag.count = dvm.Count;


            return View(dvm);
        }
        public ActionResult NewDesserts(int? cId)
        {
            List<DessertIndexPartVM> dvm = new List<DessertIndexPartVM>();

            // 判斷如果有傳入類別編號，就篩選那個類別的商品出來
            if (cId != null)
            {
                var result = db.Categories.SingleOrDefault(x => x.CategoryId == cId);
                if (result != null)
                {
                    var currentTime = DateTime.Now;
                    var desserts = result.Desserts
                        .Where(d => d.Status == true && d.CreateTime <= currentTime)
                        .ToList();
                    foreach (var dessert in desserts)
                    {
                        DessertIndexPartVM item = new DessertIndexPartVM
                        {
                            DessertId = dessert.DessertId,
                            DessertName = dessert.DessertName,
                            CategoryName = result.CategoryName,
                            UnitPrice = dessert.UnitPrice,
                            //Description = dessert.Description,
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
                                         .Where(d => d.Status == true)
                                         .OrderByDescending(d => d.CreateTime)
                                         .Take(5)
                                         .ToList();
                foreach (var dessert in desserts)
                {
                    DessertIndexPartVM item = new DessertIndexPartVM
                    {
                        DessertId = dessert.DessertId,
                        DessertName = dessert.DessertName,
                        CategoryName = dessert.Category.CategoryName,
                        UnitPrice = dessert.UnitPrice,
                        // Description = dessert.Description,
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
        public ActionResult RecentDesserts()
        {
            var currentTime = DateTime.Now;
            var recentDesserts = db.Desserts
              .Where(d => d.Status && d.CreateTime <= currentTime) // Filter by status and creation time
              .OrderByDescending(d => d.CreateTime) // Sort by descending creation time
              .Take(5) // Get the latest 5 desserts
              .ToList()
              .Select(d => d.ToIndexPartVM())
              .ToList();

            return PartialView("RecentUpDesserts", recentDesserts);
        }
        public ActionResult RecentEmployees()
        {
            var vm = db.Employees
                    .OrderByDescending(e => e.CreateAt)
                    .Take(3)
                    .ToList()
                    .Select(e => e.ToIndexVM());
            return PartialView("RecentUpEmployees", vm);
        }
        public ActionResult RecentMembers()
        {
            var vm = db.Members
                    .OrderByDescending(e => e.CreateAt)
                    .Take(3)
                    .ToList()
                    .Select(e => e.ToIndexVM());
            return PartialView("RecentUpMembers", vm);
        }
        public ActionResult ExistingPromotions()
        {
            IEnumerable<PromotionSlideVM> existPromotions = db.Promotions.Where(p => p.StartAt < DateTime.Now && p.EndAt > DateTime.Now)
                                                                         .ToList()
                                                                         .Select(p => p.ToSlideVM());
            return PartialView("ExistingPromotions", existPromotions);
        }
        public ActionResult SierraIndex()
        {

            ViewBag.HotProducts = GetHotProducts();

            return View();
        }              
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult FAQ()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult TSDessertsIndexPV()
        {
            ViewBag.HotProducts = GetHotProducts();
            return PartialView("TSDessertsIndexPV", ViewBag.HotProducts);
        }
        public ActionResult TotalDesserts()
        {
            ViewBag.totalDessertsCount = GetTotalDessertsCount();
            return PartialView("TotalDesserts", ViewBag.TotalDessertsCount);
        }
        public ActionResult TotalLessons()
        {
            ViewBag.totalLessonsCount = GetTotalLessonsCount();
            return PartialView("TotalLessons", ViewBag.TotalLessonsCount);
        }
        private int GetTotalDessertsCount()
        {
            using (var dbContext = new AppDbContext())
            {
                int totalDessertsCount = dbContext.Desserts.Count();
                return totalDessertsCount;
            }
        }
        private int GetTotalLessonsCount()
        {
            using (var dbContext = new AppDbContext())
            {
                int totalLessonsCount = dbContext.Lessons.Count();
                return totalLessonsCount;
            }
        }
        //抓到熱門銷售甜點
        private List<DessertFrontIndexVM> GetHotProducts()
        {
            using (var dbContext = new AppDbContext())
            {
                var hotProductIds = dbContext.DessertOrderDetails
                    .GroupBy(d => d.DessertId)
                    .OrderByDescending(g => g.Sum(d => d.Quantity))
                    .Take(3)
                    .Select(g => g.Key)
                    .ToList();

                var hotProductsIdsDict = hotProductIds
    .Select((id, index) => new { Id = id, Index = index })
    .ToDictionary(x => x.Id, x => x.Index);

                var hotProducts = dbContext.Desserts
                    .Where(d => hotProductIds.Contains(d.DessertId))
                    .AsEnumerable()
                    .OrderBy(d => hotProductsIdsDict[d.DessertId])
                    .Select(d => new DessertFrontIndexVM
                    {
                        DessertId = d.DessertId,
                        DessertName = d.DessertName,
                        DessertImageName = d.DessertImages.FirstOrDefault()?.DessertImageName,
                        UnitPrice = d.UnitPrice,
                    })
                    .ToList();
                return hotProducts;
            }
        }
        public ActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public ActionResult Login(int a)
		{
			return View();
		}
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }


	
}

