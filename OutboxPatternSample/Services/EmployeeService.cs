using OutboxPatternSample.Domain;
using OutboxPatternSample.Dto;
using OutboxPatternSample.IntegrationEvents;
using System.Text.Json;

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


            OutboxMessage outboxMessage = new OutboxMessage(Guid.NewGuid(),
                DateTime.Now, nameof(EmployeeCreatedEvent),
                JsonSerializer.Serialize(new EmployeeCreatedEvent { Name = employee.Name, Email = employee.Email }));
            _context.Add(employee);
            _context.Add(outboxMessage);


            await _context.SaveChangesAsync();

           
        }
    }
}
