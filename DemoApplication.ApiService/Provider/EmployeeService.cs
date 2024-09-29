using DemoApplication.ApiService.Dtos.Response;
using DemoApplication.ApiService.Interfaces;
using DemoApplication.ApiService.RequestModel;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication.ApiService.Provider
{
    public class EmployeeService : IEmployeeService
    {
        private readonly DemoContext _context;

        public EmployeeService(DemoContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetAll()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> GetById (long empId)
        {
            var empObj = await _context.Employees.FirstOrDefaultAsync(x => x.Id == empId);

            return empObj;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var employeeObj = await _context.Employees.AddAsync(employee);

            await _context.SaveChangesAsync();

            return employeeObj.Entity;
        }

        public async Task<bool> DeleteEmployee(long employeeId)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee == null)
            {
                return false; 
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true; 
        }

    }
}
