using System;

namespace Restaurant_Angular.Data.DataContracts
{
    public interface IUnitOfWork : IDisposable
    {
        IItemRepository itemRepository { get; }
        void save();
    }
}
