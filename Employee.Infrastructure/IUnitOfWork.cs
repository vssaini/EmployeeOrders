using Employee.Domain;
using Employee.Domain.Entities;
using System.Threading.Tasks;

namespace Employee.Infrastructure
{
    public interface IUnitOfWork
    {
        IBaseRepository<SalesOrder> SalesOrders { get; }
        IBaseRepository<Customer> Customers { get; }
        Task<int> SaveAsync();
    }
}
