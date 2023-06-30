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
        public ActionResult Create()
        {

            var newDiscountGroup = new DiscountGroup()
            {
                DiscountGroupName = "新的優惠群組"
            };
            db.DiscountGroups.Add(newDiscountGroup);
            db.SaveChanges();

            return new RedirectResult("Index");
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
        public ActionResult Add(int? discountGroupId, int? DessertId)
        {
            if (discountGroupId == null || DessertId == null) return HttpNotFound();

            var selectedDiscountGroup = db.DiscountGroups.FirstOrDefault(dg => dg.DiscountGroupId == discountGroupId);
            if (selectedDiscountGroup == null) return HttpNotFound();

            return new EmptyResult();
        }
        private IEnumerable<DiscountGroupIndexVM> GetDiscountGroup()
        {


            return db.DiscountGroups
                .AsNoTracking()
                .Select(x => new DiscountGroupIndexVM
                {
                    DiscountGroupId = x.DiscountGroupId,
                    DiscountGroupName = x.DiscountGroupName,
                });
        }
    }
}