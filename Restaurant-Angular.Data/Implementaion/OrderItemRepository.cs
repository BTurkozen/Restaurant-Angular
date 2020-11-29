using Restaurant_Angular.Data.DataContext;
using Restaurant_Angular.Data.DataContracts;
using Restaurant_Angular.Data.DbModels;

namespace Restaurant_Angular.Data.Implementaion
{
    public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
    {
        private readonly AppDbContext _context;

        public OrderItemRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
