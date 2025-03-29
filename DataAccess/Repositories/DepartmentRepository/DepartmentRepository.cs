using DataAccess.Data.DbContexts;
using DataAccess.Repositories.Shared;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories.DepartmentRepository
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
