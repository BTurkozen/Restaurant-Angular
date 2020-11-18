using Restaurant_Angular.Common;
using Restaurant_Angular.Data.DataContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Angular.Data.Implementaion
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

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
