using Microsoft.EntityFrameworkCore;
using OutboxPatternSample.Domain;
using OutboxPatternSample.IntegrationEvents;
using Quartz;
using System.Text.Json;

namespace OutboxPatternSample.Services
{
    [DisallowConcurrentExecution]
    public class SendEmailsJob : IJob
    {
        private readonly ILogger<SendEmailsJob> _logger;
        private readonly EmailService _emailService;
        private readonly ApplicationDbContext _context;

        public SendEmailsJob(ILogger<SendEmailsJob> logger
            , EmailService emailService
            , ApplicationDbContext context)
        {
            _logger = logger;
            _emailService = emailService;
            _context = context;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Check messages!");
            var messages = await _context.Set<OutboxMessage>()
                .Where(c => c.Type == nameof(EmployeeCreatedEvent) 
                            && c.IsProcessed == false).OrderBy(c=> c.Data).ToListAsync();
            foreach (var message in messages)
            {
                var data = JsonSerializer.Deserialize<EmployeeCreatedEvent>(message.Data);
                _logger.LogInformation($"Sending to {data.Email}!");
                await _emailService.SendEmailAsync(data.Email, "Welcome on-board"
                    , $"It's great to work with us {data.Name}");

                message.MarkAsProcessed(DateTime.Now);
                _context.Update(message);
                await _context.SaveChangesAsync();
            }

        }
    }
}
