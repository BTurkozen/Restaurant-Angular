using Restaurant_Angular.Data.DataContext;
using Restaurant_Angular.Data.DataContracts;
using Restaurant_Angular.Data.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Angular.Data.Implementaion
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
