

namespace DataAccess.Repositories.Shared
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        public int Add(T Entity);
        public IEnumerable<T> GetAll();
        public T? GetById(int id);
        public int Remove(T Entity);
        public int Update(T Entity);

        public IQueryable<T> GetAllQueryable();
    }
}
