using Microsoft.EntityFrameworkCore;
using OutboxPatternSample.Services;

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
