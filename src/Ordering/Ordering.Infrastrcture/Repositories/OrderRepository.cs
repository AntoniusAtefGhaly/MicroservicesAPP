using Microsoft.EntityFrameworkCore;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;
using Ordering.Infrastrcture.Data;
using Ordering.Infrastrcture.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastrcture.Repositories
{
   public class OrderRepository:Repository<Order>, IOrderRepository
    {
        public OrderRepository( OrderContext context):base(context)
        {

        }

        public async Task<IEnumerable<Order>> GetOrderByUserName(string userName)
        {
            var orderlist = await _dbContext.Orders.Where(o => o.UserName == userName).ToListAsync();
            return orderlist;
        }
    }
}
