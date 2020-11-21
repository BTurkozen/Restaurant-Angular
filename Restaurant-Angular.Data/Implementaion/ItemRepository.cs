using Restaurant_Angular.Data.DataContext;
using Restaurant_Angular.Data.DataContracts;
using Restaurant_Angular.Data.DbModels;

namespace Restaurant_Angular.Data.Implementaion
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        private readonly AppDbContext _context;

        public ItemRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
