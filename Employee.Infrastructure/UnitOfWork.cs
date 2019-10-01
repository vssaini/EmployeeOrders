using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Employee.Domain;
using Employee.Domain.Entities;
using Employee.Infrastructure.Repositories;

namespace Employee.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _isDisposed = false;
        private DatabaseContext _context;
        private IBaseRepository<SalesOrder> _salesOrders;
        private IBaseRepository<Customer> _customers;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        public IBaseRepository<SalesOrder> SalesOrders
        {
            get
            {
                if (_salesOrders == null)
                    _salesOrders = new BaseRepository<SalesOrder>(_context);
                return _salesOrders;
            }
        }

        public IBaseRepository<Customer> Customers
        {
            get
            {
                if (_customers == null)
                    _customers = new BaseRepository<Customer>(_context);
                return _customers;
            }
        }

        public async Task<int> SaveAsync()
        {
            try
            {
                int changes = await _context.SaveChangesAsync();
                return changes;
            }

            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);

                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed && disposing)
            {
                _context.Dispose();
            }

            _isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
