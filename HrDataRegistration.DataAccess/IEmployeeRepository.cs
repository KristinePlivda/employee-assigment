using HrDataRegistration.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HrDataRegistration.DataAccess
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployees();

        Task<Employee> InsertEmployee(Employee employee);
    }
}