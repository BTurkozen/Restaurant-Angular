using System;

namespace Restaurant_Angular.Data.DataContracts
{
    public interface IUnitOfWork : IDisposable
    {
        IItemRepository itemRepository { get; }
        ICustomerRepository customerRepository { get; }
        void save();
    }
}
