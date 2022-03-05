using System.ComponentModel.DataAnnotations;

namespace OutboxPatternSample.Dto
{
    public class EmployeeDto
    {
        [Required] public string Name { get; set; }
        [Required][EmailAddress] public string Email { get; set; }
    }
}
