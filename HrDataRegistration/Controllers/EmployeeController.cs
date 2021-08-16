using HrDataRegistration.DataAccess;
using HrDataRegistration.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HrDataRegistration.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeLogic)
        {
            _employeeRepository = employeeLogic;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> EmployeeList()
        {
            try
            {
                var employeeList = await _employeeRepository.GetEmployees();
                return Ok(employeeList);
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateEmployee([FromBody]Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var newEmployee = await _employeeRepository.InsertEmployee(employee);
                if (newEmployee == null)
                {
                    return BadRequest();
                }
                return Ok(newEmployee);
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }
    }
}