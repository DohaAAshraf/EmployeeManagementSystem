using EmployeeSystem.Models;
using EmployeeSystem.Repositories;
using EmployeeSystem.Repositories.IRepositories;

namespace EmployeeSystem.Services
{
   public class EmployeeService : IEmployeeService
{
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public EmployeeModel GetEmployee(int id)
    {
        return _unitOfWork.Employees.GetById(id);
    }

    public IEnumerable<EmployeeModel> GetAllEmployees()
    {
        return _unitOfWork.Employees.GetAll();
    }

    public void CreateEmployee(EmployeeModel employee)
    {
        _unitOfWork.Employees.Insert(employee);
        _unitOfWork.Save();  // Save changes through the UnitOfWork
    }

    public void UpdateEmployee(EmployeeModel employee)
    {
        _unitOfWork.Employees.Update(employee);
        _unitOfWork.Save();  // Save changes through the UnitOfWork
    }

    public void DeleteEmployee(int id)
    {
        var employee = _unitOfWork.Employees.GetById(id);
        if (employee != null)
        {
            _unitOfWork.Employees.Delete(employee);
            _unitOfWork.Save();  // Save changes through the UnitOfWork
        }
    }
}

}
