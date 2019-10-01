using System.Linq;

namespace Employee.Domain
{
    public interface IBaseRepository<T> where T: class, IBaseEntity
    {        
        IQueryable<T> All();
        void Insert(T entity);
        void Update(T entityToUpdate);
        void Delete(T entityToDelete);
        T Find(int? id);
    }
}
