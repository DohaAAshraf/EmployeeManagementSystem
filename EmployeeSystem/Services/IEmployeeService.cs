using EmployeeSystem.Models;

namespace EmployeeSystem.Services
{
    public interface IEmployeeService
    {
        EmployeeModel GetEmployee(int id);
        IEnumerable<EmployeeModel> GetAllEmployees();
        void CreateEmployee(EmployeeModel employee);
        void UpdateEmployee(EmployeeModel employee);
        void DeleteEmployee(int id);
    }
}
