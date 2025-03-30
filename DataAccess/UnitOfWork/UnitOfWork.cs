using DataAccess.Repositories.DepartmentRepository;
using DataAccess.Repositories.EmployeeRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

       

        int IUnitOfWork.Complete()
        {
            return _dbContext.SaveChanges();
           
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
