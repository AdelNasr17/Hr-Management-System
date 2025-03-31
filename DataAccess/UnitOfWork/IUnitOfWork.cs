
namespace DataAccess.UnitOfWork
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        public IEmployeeRepository employeeRepository { get;  }
        public IDepartmentRepository departmentRepository { get;  }
        public Task<int> CompleteAsync();
    }
}
