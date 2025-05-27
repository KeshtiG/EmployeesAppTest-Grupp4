using EmployeesApp.Domain.Entities;

namespace EmployeesApp.Application.Employees.Interfaces
{
    public interface IEmployeeRepository
    {
        void Add(Employee employee);
        Employee[] GetAll();
        Employee? GetById(int id);
    }
}