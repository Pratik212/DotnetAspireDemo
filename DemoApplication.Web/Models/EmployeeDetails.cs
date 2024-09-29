using System.ComponentModel.DataAnnotations;

namespace DemoApplication.Web.Models
{
    public class EmployeeDetails
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        [Range(18, 65, ErrorMessage = "Age must be between 18 and 65.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Contact is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string Contact { get; set; }

    }
}
