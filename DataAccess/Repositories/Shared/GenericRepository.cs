

namespace DataAccess.Repositories.Shared
{
    public class GenericRepository<T>(ApplicationDbContext dbContext) : IGenericRepository<T> where T : BaseEntity
    {
        public int Add(T Entity)
        {
            dbContext.Set<T>().Add(Entity);
            return dbContext.SaveChanges();
        }

        public IEnumerable<T> GetAll() => dbContext.Set<T>().Where(E=> E.IsDeleted==false).ToList();

       

        public T? GetById(int id) => dbContext.Set<T>().Find(id);

     

        public int Remove(T Entity)
        {
            //dbContext.Set<T>().Remove(Entity);
            //return dbContext.SaveChanges();
           Entity.IsDeleted = true;
            dbContext.Set<T>().Update(Entity);
            return dbContext.SaveChanges();
        }

        public int Update(T Entity)
        {
            dbContext.Set<T>().Update(Entity);
            return dbContext.SaveChanges();
        }

        public IQueryable<T> GetAllQueryable()
        {
            return dbContext.Set<T>();
        }
    }
}
