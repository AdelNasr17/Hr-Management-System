

namespace DataAccess.Repositories.Shared
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        public void Add(T Entity);
        public IEnumerable<T> GetAll();
        public T? GetById(int id);
        public void Remove(T Entity);
        public void Update(T Entity);

        public IQueryable<T> GetAllQueryable();
    }
}
