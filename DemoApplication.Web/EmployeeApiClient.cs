namespace DemoApplication.Web
{
    public class EmployeeApiClient
    {
        private readonly HttpClient _httpClient;

        public EmployeeApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<EmployeeSSS>> GetEmployeesAsync(CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<List<EmployeeSSS>>("/api/employee", cancellationToken);
        }

        public async Task<EmployeeSSS> GetEmployeeById(long empId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<EmployeeSSS>($"/api/employee/empid?empid={empId}", cancellationToken);
        }

        public async Task<EmployeeSSS> AddEmployee(EmployeeSSS employeeSSS, CancellationToken cancellationToken = default)
        {
            // Send POST request
            var response = await _httpClient.PostAsJsonAsync("/api/employee/add", employeeSSS, cancellationToken);

            // Ensure the response is successful
            response.EnsureSuccessStatusCode();

            // Read and deserialize the response body
            var result = await response.Content.ReadFromJsonAsync<EmployeeSSS>(cancellationToken: cancellationToken);

            return result;
        }

        public async Task<EmployeeSSS> UpdateEmployee(long empId,EmployeeSSS employeeSSS, CancellationToken cancellationToken = default)
        {
            // Send POST request
            var response = await _httpClient.PutAsJsonAsync($"/api/employee/update/{empId}", employeeSSS, cancellationToken);

            // Ensure the response is successful
            response.EnsureSuccessStatusCode();

            // Read and deserialize the response body
            var result = await response.Content.ReadFromJsonAsync<EmployeeSSS>(cancellationToken: cancellationToken);

            return result;
        }

        public async Task<bool> DeleteEmployee(long empId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync($"/api/employee/delete/{empId}", cancellationToken);

            // Check if the request was successful
            if (response.IsSuccessStatusCode)
            {
                return true; // Employee deleted successfully
            }

            return false; // Deletion failed
        }

        public record EmployeeSSS
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public int Age { get; set; }
            public string Address { get; set; }
            public string Contact { get; set; }
        }
    }
}
