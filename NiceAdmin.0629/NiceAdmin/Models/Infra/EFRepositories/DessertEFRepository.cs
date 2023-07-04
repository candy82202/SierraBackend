using NiceAdmin.Models.DTOs.Desserts;
using NiceAdmin.Models.EFModels;
using NiceAdmin.Models.Interfaces;
using NiceAdmin.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace NiceAdmin.Models.Infra.EFRepositories
{
    public class DessertEFRepository : IDessertRepository
    {
        private AppDbContext _db;
        public DessertEFRepository()
        {
            _db = new AppDbContext();
        }
        public IEnumerable<DessertsDto> Search(DessertCriteria criteria)
        {
            var query = _db.Desserts
                 .AsNoTracking()
                 .Include(d => d.Category)
                 .OrderBy(d => d.Category.CategoryId)
                 .Select(d => new DessertsDto
                 {
                     DessertId = d.DessertId,
                     DessertName = d.DessertName,
                     CategoryId = d.CategoryId,
                     CategoryName = d.Category.CategoryName,
                     UnitPrice = d.UnitPrice,
                     CreateTime = d.CreateTime,
                     Description = d.Description,
                     Status = d.Status,

                 });
            if (!string.IsNullOrEmpty(criteria.Name))
            {
                query = query.Where(d => d.DessertName.Contains(criteria.Name));
            }
            if (criteria.CategoryId != null && criteria.CategoryId.Value > 0)
            {
                query = query.Where(d => d.CategoryId == criteria.CategoryId.Value);
            }
            if (criteria.MinPrice.HasValue)
            {
                query = query.Where(d => d.UnitPrice >= criteria.MinPrice.Value);
            }
            if (criteria.MaxPrice.HasValue)
            {
                query = query.Where(d => d.UnitPrice <= criteria.MaxPrice.Value);
            }

            return query.ToList();
        }
    }
}