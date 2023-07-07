using NiceAdmin.Models.EFModels;
using NiceAdmin.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using NiceAdmin.Models.Exts;
using Microsoft.Ajax.Utilities;

namespace NiceAdmin.Controllers
{
    public class DiscountGroupsController : Controller
    {
        // GET: DiscountGroup
        private AppDbContext db = new AppDbContext();
        public ActionResult Index()
        {
            IEnumerable<DiscountGroupIndexVM> discountGroups = GetDiscountGroup();
            return View(discountGroups);
        }
        [HttpPost]
        public JsonResult Create()
        {

            var newDiscountGroup = new DiscountGroup()
            {
                DiscountGroupName = "新的優惠群組"
            };
            bool haveSame = db.DiscountGroups.Any(dg=>dg.DiscountGroupName == newDiscountGroup.DiscountGroupName);
            if (haveSame)
            {
                return Json("請編輯先前新增的優惠群組");
            }
            db.DiscountGroups.Add(newDiscountGroup);
            db.SaveChanges();
            return Json("新增成功");
        }
        
        public ActionResult Delete(int discountGroupId)
        {
            var discountGroupInDb = db.DiscountGroups.FirstOrDefault(d => d.DiscountGroupId == discountGroupId);
            db.DiscountGroups.Remove(discountGroupInDb);
            db.SaveChanges();
            return new EmptyResult();
        }
        public ActionResult Edit(int? discountGroupId)
        {
            if (discountGroupId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            DiscountGroup discountGrouop = db.DiscountGroups.Find(discountGroupId);
            if (discountGrouop == null)
            {
                return HttpNotFound();
            }
            IEnumerable<DessertInDiscountGroup> desserts = db.Desserts.Include(d => d.Category)
                                                               .ToList()
                                                               .Select(d => d.ToDessertInDiscountGroup())
                                                               .ToList();
            IEnumerable<DiscountGroupItemVM> discountGroupItems = db.DiscountGroupItems.Include(dI => dI.DiscountGroupId)
                                                                                .Include(dI => dI.DessertId)
                                                                                .Where(dI => dI.DiscountGroupId == discountGroupId)
                                                                                .Select(dI => new DiscountGroupItemVM
                                                                                {
                                                                                    DiscountGroupItemId = dI.DiscountGroupItemId,
                                                                                    DiscountGroupId = dI.DiscountGroupId,
                                                                                    DessertId = dI.DessertId,
                                                                                    Dessert = new DessertInDiscountGroup
                                                                                    {
                                                                                        DessertId = dI.DessertId,
                                                                                        DessertName = dI.Dessert.DessertName,
                                                                                        CategoryName = dI.Dessert.Category.CategoryName,
                                                                                        UnitPrice = dI.Dessert.UnitPrice
                                                                                    }
                                                                                });
            DiscountGroupEditVM vm = new DiscountGroupEditVM()
            {
                DiscountGroupId = discountGrouop.DiscountGroupId,
                DiscountGroupName = discountGrouop.DiscountGroupName,
                Desserts = desserts,
                DiscountGroupItems = discountGroupItems
            };
            PrepareCategoryDataSource(null);

			return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DiscountGroupId,DiscountGroupName")] DiscountGroup discountGroup)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(discountGroup) = EntityState.Modified;
                //不知道為什麼錯
                var modifiedDiscountGroup = db.DiscountGroups.FirstOrDefault(dg => dg.DiscountGroupId == discountGroup.DiscountGroupId);
                if (modifiedDiscountGroup == null) return HttpNotFound();
                modifiedDiscountGroup.DiscountGroupName = discountGroup.DiscountGroupName;
                db.SaveChanges();
            }

            return new RedirectResult("Index");
        }
        [HttpPost]
        public JsonResult Add(int? discountGroupId, int? dessertId)
        {
            if (discountGroupId == null || dessertId == null) return Json("新增失敗");

            var selectedDiscountGroup = db.DiscountGroups.FirstOrDefault(dg => dg.DiscountGroupId == discountGroupId);
            if (selectedDiscountGroup == null) return Json("新增失敗");
            var itemsInSelectedDiscountGroup = db.DiscountGroupItems.Where(dgi => dgi.DiscountGroupId == discountGroupId);
            int[] itemsInSelectedDiscountGroupId = itemsInSelectedDiscountGroup.Select(x=>x.DessertId).ToArray();
            int index = Array.IndexOf(itemsInSelectedDiscountGroupId, dessertId);
            if (index != -1) return Json("新增失敗，群組中已有此商品");
            var newDiscountGroupItem = new DiscountGroupItem()
            {
                DiscountGroupId = (int)discountGroupId,
                DessertId = (int)dessertId
			};
            db.DiscountGroupItems.Add(newDiscountGroupItem);
            db.SaveChanges();
			return Json("新增成功");
        }
		[HttpPost]
		public JsonResult AddAll(int? discountGroupId, int[] dessertIds)
		{
			if (discountGroupId == null) return Json("找不到此群組");
            if(dessertIds == null) return Json("請輸入適當的搜尋條件");
			var selectedDiscountGroup = db.DiscountGroups.FirstOrDefault(dg => dg.DiscountGroupId == discountGroupId);
			if (selectedDiscountGroup == null) return Json("找不到此群組");
            var dessertIdsInSelectedDiscountGroup= selectedDiscountGroup.DiscountGroupItems?.Select(dgi => dgi.Dessert.DessertId).ToArray();
            var different = dessertIds.Except(dessertIdsInSelectedDiscountGroup);
            if (!different.Any()) return Json("此頁面所有產品都已加入群組");
            List<ReturnDessert> desserts = new List<ReturnDessert>();
            foreach(int dessertId in different)
            {
                var newDiscountGroupItem = new DiscountGroupItem()
                {
                    DiscountGroupId = (int)discountGroupId,
                    DessertId = dessertId
                };
				db.DiscountGroupItems.Add(newDiscountGroupItem);
				
				var dessertInDb = db.Desserts.Find(dessertId);
                var returnDessert = new ReturnDessert(dessertInDb.DessertName, dessertId);
				desserts.Add(returnDessert);
			}
			db.SaveChanges();
			return Json(desserts);
		}
		[HttpPost]
		public JsonResult AddCategory(int? discountGroupId, int? categoryId)
		{
			if (discountGroupId == null) return Json("找不到此群組");
			if (categoryId == null) return Json("請選擇類別");
			var selectedDiscountGroup = db.DiscountGroups.FirstOrDefault(dg => dg.DiscountGroupId == discountGroupId);
			if (selectedDiscountGroup == null) return Json("找不到此群組");
			var dessertIdsInSelectedDiscountGroup = selectedDiscountGroup.DiscountGroupItems?.Select(dgi => dgi.Dessert.DessertId).ToArray();
            var dessertsInCategory = db.Desserts.Where(d => d.CategoryId == categoryId).Select(d=>d.DessertId);
			var different = dessertsInCategory.Except(dessertIdsInSelectedDiscountGroup);
            if(!different.Any()) return Json("此類別所有產品都已加入群組");
			List<ReturnDessert> desserts = new List<ReturnDessert>();
			
			foreach (int dessertId in different)
		    {
			    var newDiscountGroupItem = new DiscountGroupItem()
			    {
				    DiscountGroupId = (int)discountGroupId,
				    DessertId = dessertId
			    };
			    db.DiscountGroupItems.Add(newDiscountGroupItem);
				
			    var dessertInDb = db.Desserts.Find(dessertId);
			    var returnDessert = new ReturnDessert(dessertInDb.DessertName, dessertId);
			    desserts.Add(returnDessert);
			}
			db.SaveChanges();
			return Json(desserts);
		}
		[HttpPost]
		public JsonResult Remove(int? discountGroupId, int? dessertId)
		{
			if (discountGroupId == null || dessertId == null) return Json("刪除失敗");

			var selectedDiscountGroup = db.DiscountGroups.FirstOrDefault(dg => dg.DiscountGroupId == discountGroupId);
			if (selectedDiscountGroup == null) return Json("刪除失敗");
			var itemInDb = db.DiscountGroupItems.FirstOrDefault(dgi=>dgi.DiscountGroupId == discountGroupId&&dgi.DessertId==dessertId);
			if (itemInDb == null) return Json("刪除失敗，群組沒有此商品");
			
			db.DiscountGroupItems.Remove(itemInDb);
			db.SaveChanges();
			return Json("刪除成功");
		}
		[HttpPost]
		public JsonResult RemoveAll(int? discountGroupId)
		{
			if (discountGroupId == null) return Json("刪除失敗");
			var selectedDiscountGroup = db.DiscountGroups.FirstOrDefault(dg => dg.DiscountGroupId == discountGroupId);
			if (selectedDiscountGroup == null) return Json("刪除失敗");
            var DiscountGroupItems = db.DiscountGroupItems.Where(dgi => dgi.DiscountGroupId == discountGroupId).ToList();
            for(int i = 0;i< DiscountGroupItems.Count(); i++)
            {
				db.DiscountGroupItems.Remove(DiscountGroupItems[i]); 
			};
			db.SaveChanges();
			return Json("刪除成功");
		}
		private void PrepareCategoryDataSource(int? categoryId)
		{
			ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", categoryId);
		}
		private class ReturnDessert
        {
            public string DessertName { get; set; }
            public int DessertId { get; set; }
            public ReturnDessert(string _dessertName, int _dessertId)
            {
                this.DessertName = _dessertName;
                this.DessertId = _dessertId;
            }
        }
		
        private IEnumerable<DiscountGroupIndexVM> GetDiscountGroup()
        {
            return db.DiscountGroups
                .ToList()
                .Select(x => x.ToIndexVM());
        }
    }
}