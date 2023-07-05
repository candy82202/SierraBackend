using NiceAdmin.Models.DTOs;
using NiceAdmin.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.Services
{
    public class DessertOrderService
    {
        private IDessertOrderRepository _repo;

        public DessertOrderService(IDessertOrderRepository repo) 
        {
            _repo = repo;
        }
        public IEnumerable<DessertOrderIndexDto> Search() 
        {
            return _repo.Search();
        }
    }
}