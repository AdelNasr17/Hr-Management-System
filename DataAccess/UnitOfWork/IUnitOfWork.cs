using DataAccess.Repositories.DepartmentRepository;
using DataAccess.Repositories.EmployeeRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        public IEmployeeRepository employeeRepository { get;  }
        public IDepartmentRepository departmentRepository { get;  }
        public int Complete();
    }
}
