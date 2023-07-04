using NiceAdmin.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceAdmin.Models.Interfaces
{
    public interface IDessertOrderRepository
    {
        IEnumerable<DessertOrderIndexDto> Search();
    }
}
