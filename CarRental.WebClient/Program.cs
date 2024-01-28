using TAG8GJ_HFT_2023241;
using TAG8GJ_HFT_2023241.Logic;
using TAG8GJ_HFT_2023241.Models;
using TAG8GJ_HFT_2023241.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<CarRentalDbContext>();


builder.Services.AddTransient<IRepository<Car>,CarRepository>();
builder.Services.AddTransient<IRepository<Customer>, CustomerRepository>();
builder.Services.AddTransient<IRepository<Rental>, RentalRepository>();

builder.Services.AddTransient<ICustomerLogic,CustomerLogic>();
builder.Services.AddTransient<ICarLogic, CarLogic>();
builder.Services.AddTransient<IRentalLogic, RentalLogic>();

builder.Services.AddControllersWithViews();


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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
