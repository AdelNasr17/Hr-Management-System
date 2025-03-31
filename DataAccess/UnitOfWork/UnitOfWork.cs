
namespace DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public ApplicationDbContext _dbContext { get; }
        public IEmployeeRepository employeeRepository=> new EmployeeRepository(_dbContext);
            
        public IDepartmentRepository departmentRepository => new DepartmentRepository(_dbContext);
        public UnitOfWork(ApplicationDbContext dbcontext)
        {
            _dbContext = dbcontext;
            //employeeRepository = new EmployeeRepository(_dbContext);
            //departmentRepository = new DepartmentRepository(_dbContext);
        }

       

         public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync().ConfigureAwait(false);
           
        }

        

        public async ValueTask DisposeAsync()
        {
           await _dbContext.DisposeAsync().ConfigureAwait(false);
        }
    }
}
