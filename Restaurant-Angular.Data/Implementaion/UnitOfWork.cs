using Restaurant_Angular.Data.DataContext;
using Restaurant_Angular.Data.DataContracts;

namespace Restaurant_Angular.Data.Implementaion
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            itemRepository = new ItemRepository(_context);
            customerRepository = new CustomerRepository(_context);
        }

        public IItemRepository itemRepository { get; private set; }

        public ICustomerRepository customerRepository { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void save()
        {
            _context.SaveChanges();
        }
    }
}
