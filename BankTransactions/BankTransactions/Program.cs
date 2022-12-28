using BankTransactions.DBTables;
using BankTransactions.Models;
using BankTransactions.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//dependency injections for DbContext
builder.Services.AddDbContext<TransactionDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection"))
);

//Services 
builder.Services.AddScoped<TransactionService, TransactionService>();

// DB Tables
builder.Services.AddScoped<TransactionTable, TransactionTable>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Transaction}/{action=Index}/{id?}");

app.Run();
