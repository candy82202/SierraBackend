using Dapper;
using NiceAdmin.Models.DTOs;
using NiceAdmin.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.Infra.DapperRepositories
{
    public class DessertOrderDapperRepository : IDessertOrderRepository
    {
        private string _connStr;
        public DessertOrderDapperRepository()
        {
            _connStr = System.Configuration.ConfigurationManager
                .ConnectionStrings["AppDbContext"].ToString();

        }
        public IEnumerable<DessertOrderIndexDto> Search()
        {
            

            using (var conn = new SqlConnection(_connStr))
            {
                string sql = @"SELECT	O.DessertOrderId,O.MemberId,O.DessertOrderStatusId,O.CreateTime,O.Recipient,
O.RecipientPhone,O.RecipientAddress,O.ShippingFee,O.DessertOrderTotal,O.DeliveryMethod,
D.DessertName,D.Quantity,D.DessertUnitPrice,D.Subtotal
FROM DessertOrders as O
INNER JOIN DessertOrderDetails as D
on D.DessertOrderId = O.DessertOrderId";

                return conn.Query<DessertOrderIndexDto>(sql);
            }
        }
    }
}