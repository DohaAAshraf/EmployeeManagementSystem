using EmployeeSystem.ApplicationDbContexts;
using EmployeeSystem.Models;
using EmployeeSystem.Repositories;
using EmployeeSystem.Repositories.IRepositories;

namespace EmployeeSystem
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IRepository<EmployeeModel> _employees;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IRepository<EmployeeModel> Employees
        {
            get
            {
                return _employees ??= new Repository<EmployeeModel>(_context);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
