using NiceAdmin.Models.DTOs.Desserts;
using NiceAdmin.Models.Interfaces;
using NiceAdmin.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.Services.Desserts
{
    public class DessertService
    {
        private IDessertRepository _repo;
        public DessertService(IDessertRepository repo)
        {
            _repo = repo;
        }
        public IEnumerable<DessertsDto> Search(DessertCriteria criteria)
        {
            return _repo.Search(criteria);
        }
    }
}