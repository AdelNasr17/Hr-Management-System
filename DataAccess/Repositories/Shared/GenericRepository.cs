

namespace DataAccess.Repositories.Shared
{
    public class GenericRepository<T>(ApplicationDbContext dbContext) : IGenericRepository<T> where T : BaseEntity
    {
        public void Add(T Entity)
        {
            dbContext.Set<T>().Add(Entity);
            
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await dbContext.Set<T>().Where(E=> E.IsDeleted==false).ToListAsync().ConfigureAwait(false);

        public async Task<T?> GetByIdAsync(int id) => await dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync( e=> e.Id==id);
     
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
