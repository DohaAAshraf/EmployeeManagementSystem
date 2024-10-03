using EmployeeSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeSystem.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        // GET: Employee
        public IActionResult Index()
        {
            var employees = _unitOfWork.Employees.GetAll();
            var displayEmployees = employees.Select((e, index) => new
            {
                DisplayID = index + 1, // Custom sequential ID starting from 1
                e.EmployeeID,
                e.Name,
                e.Position,
                e.Department,
                e.Email,
                e.Salary 
            }).ToList();

           return View(displayEmployees);
        }

        // GET: Employee/Create
            [Authorize(Roles = "Admin")]

        public IActionResult Create()
        {
            return View();
        }

        
        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeModel employee)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Employees.Insert(employee);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employee/Edit/5
            [Authorize(Roles = "Admin")]

        public IActionResult Edit(int id)
        {
            var employee = _unitOfWork.Employees.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EmployeeModel employee)
        {
            if (id != employee.EmployeeID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Employees.Update(employee);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employee/Delete/5
            [Authorize(Roles = "Admin")]

        public IActionResult Delete(int id)
        {
            var employee = _unitOfWork.Employees.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var employee = _unitOfWork.Employees.GetById(id);
                if (employee == null)
                {
                    return NotFound();
                }

                _unitOfWork.Employees.Delete(employee);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging framework)
                ModelState.AddModelError("", $"Error deleting employee: {ex.Message}");
                return View(); // Optionally return the same view with an error message
            }
        }

        // GET: Employee/Details/5
        public IActionResult Details(int id)
        {
            var employee = _unitOfWork.Employees.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
    }
}
    
