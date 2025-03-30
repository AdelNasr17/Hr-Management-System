

namespace DataAccess.Repositories.Shared
{
    public class GenericRepository<T>(ApplicationDbContext dbContext) : IGenericRepository<T> where T : BaseEntity
    {
        public void Add(T Entity)
        {
            dbContext.Set<T>().Add(Entity);
            
        }

        public IEnumerable<T> GetAll() => dbContext.Set<T>().Where(E=> E.IsDeleted==false).ToList();

       

        public T? GetById(int id) => dbContext.Set<T>().Find(id);

     

        public void Remove(T Entity)
        {
            //dbContext.Set<T>().Remove(Entity);
            //return dbContext.SaveChanges();
           Entity.IsDeleted = true;
            dbContext.Set<T>().Update(Entity);
           
        }

        public void Update(T Entity)
        {
            dbContext.Set<T>().Update(Entity);
           
        }

        public IQueryable<T> GetAllQueryable()
        {
            return dbContext.Set<T>();
        }
    }
}
