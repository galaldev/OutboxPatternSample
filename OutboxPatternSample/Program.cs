using Microsoft.EntityFrameworkCore;
using OutboxPatternSample.Services;
using Quartz;

var builder = WebApplication.CreateBuilder(args);
var folder = Environment.SpecialFolder.LocalApplicationData;
var path = Environment.GetFolderPath(folder);
var dbPath = System.IO.Path.Join(path, "employees.db");
builder.Services.AddDbContextFactory<ApplicationDbContext>(opt =>
    opt.UseSqlite($"Data Source={dbPath}"));
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddTransient<EmployeeService>();
builder.Services.AddTransient<EmailService>();
builder.Services.AddQuartz(q =>
{
    //// Use a Scoped container to create jobs. I'll touch on this later
    q.UseMicrosoftDependencyInjectionJobFactory();
    var jobKey = new JobKey(nameof(SendEmailsJob));
    // Register the job with the DI container
    q.AddJob<SendEmailsJob>(opts => opts.WithIdentity(jobKey));
    // Create a trigger for the job
    q.AddTrigger(opts => opts
        .ForJob(jobKey) // link to the HelloWorldJob
        .WithIdentity($"{nameof(SendEmailsJob)}-trigger") // give the trigger a unique name
        .WithCronSchedule("0/10 * * * * ?")); // run every 5 seconds
});
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
