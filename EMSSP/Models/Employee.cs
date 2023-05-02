using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EMSSP.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [DisplayName("Full Name")]
        [Required]
        public string Name { get; set; }
        [DisplayName("E-mail")]
        [Required]
        public string Email { get; set; }
        [DisplayName("Phone Number")]
        [Required]
        public string PhoneNo { get; set; }
        [DisplayName("Job Title")]
        [Required]
        public string JobTitle { get; set; }
    }
}
