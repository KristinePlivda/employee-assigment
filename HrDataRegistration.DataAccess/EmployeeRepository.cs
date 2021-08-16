using HrDataRegistration.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrDataRegistration.DataAccess
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            return await context.Employees.Where(e => e.EndDate == null || e.EndDate > DateTime.Now).ToListAsync();
        }

        public async Task<Employee> InsertEmployee(Employee employee)
        {
            context.Employees.Add(employee);
            await context.SaveChangesAsync();

            return employee;
        }
    }
}