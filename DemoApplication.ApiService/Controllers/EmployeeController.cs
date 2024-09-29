using DemoApplication.ApiService.Dtos;
using DemoApplication.ApiService.Dtos.Response;
using DemoApplication.ApiService.Interfaces;
using DemoApplication.ApiService.RequestModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly DemoContext _context;

        public EmployeeController(IEmployeeService employeeService, DemoContext context)
        {
            _employeeService = employeeService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _employeeService.GetAll();
            return Ok(employees);
        }

        [HttpGet("empid")]
        public async Task<IActionResult> GetById(long empid)
        {
            var employees = await _employeeService.GetById(empid);
            return Ok(employees);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> NewEmployee(EmployeeDto employeeDto)
        {
            var employees = new Employee()
            {
                Name = employeeDto.Name,
                Email = employeeDto.Email,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                Contact = employeeDto.Contact,
            };

            var empObj = await _employeeService.AddEmployee(employees);

            return Ok(empObj);
        }

        [HttpPut("Update/{empId}")]
        public async Task<IActionResult> UpdateEmployee(long empId, EmployeeDto employeeDto)
        {
            var emp = await _context.Employees.FirstOrDefaultAsync(x => x.Id == empId);

            if (emp == null)
            {
                return BadRequest(new ResponseModel { ErrorCode = "U008", Message = "Employee Not Found." });
            };

            emp.Name = employeeDto.Name;
            emp.Email = employeeDto.Email;
            emp.Age = employeeDto.Age;
            emp.Address = employeeDto.Address;
            emp.Contact = employeeDto.Contact;

            await _context.SaveChangesAsync();

            return Ok(emp);
        }

        [HttpDelete("Delete/{empId}")]
        public async Task<IActionResult> DeleteEmp(long empId)
        {
            var result = await _employeeService.DeleteEmployee(empId);

            if (result)
            {
                return Ok(new { Message = "Employee deleted successfully." });
            }
            else
            {
                return NotFound(new { Message = "Employee not found." });
            }
        }
    }
}
