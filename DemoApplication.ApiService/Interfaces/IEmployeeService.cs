using DemoApplication.ApiService.RequestModel;

namespace DemoApplication.ApiService.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAll();
        Task<Employee> GetById(long empId);
        Task<Employee> AddEmployee(Employee employee);

        Task<bool> DeleteEmployee(long employeeId);
    }
}
