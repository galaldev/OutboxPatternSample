using OutboxPatternSample.Domain;
using OutboxPatternSample.Dto;

namespace OutboxPatternSample.Services
{
    public class EmployeeService
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailService _emailService;

        public EmployeeService(ApplicationDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task AddEmployee(EmployeeDto employeeDto)
        {
            Employee employee = new Employee(Guid.NewGuid(),
                employeeDto.Name, employeeDto.Email);
            _context.Add(employee);
            await _context.SaveChangesAsync();
            await _emailService.SendEmailAsync(employee.Email, "Welcome onboard"
                , $"It's great to work with us {employee.Name}");
        }
    }
}
