using System;

namespace Restaurant_Angular.Data.DataContracts
{
    public interface IUnitOfWork : IDisposable
    {
        IItemRepository itemRepository { get; }
        ICustomerRepository customerRepository { get; }
        IOrderRepository orderRepository { get; }
        IOrderItemRepository orderItemRepository { get; }
        void save();
    }
}
