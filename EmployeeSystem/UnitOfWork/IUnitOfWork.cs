using EmployeeSystem.Models;
using EmployeeSystem.Repositories.IRepositories;

namespace EmployeeSystem
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<EmployeeModel> Employees { get; }
        void Save();
    }
}
