using NiceAdmin.Models.DTOs.Desserts;
using NiceAdmin.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceAdmin.Models.Interfaces
{
    public interface IDessertRepository
    {
        IEnumerable<DessertsDto> Search(DessertCriteria criteria);
    }
}
