using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Angular.Common
{
    public interface IUnitOfWork : IDisposable
    {
        void save();
    }
}
