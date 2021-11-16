using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Ordering.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Infrastrcture.Data
{
    public class OrderContext :DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options):base(options)
        {

        }
        public DbSet<Order> Orders { get; set; }
    }
}
