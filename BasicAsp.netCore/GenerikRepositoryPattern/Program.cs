using AppDataAccess.Data;
using AppDataAccess.GenerikInterface;
using AppDataAccess.Manager;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DBConnection");


builder.Services.AddDbContext<AppDbContext>(options=>
options.UseSqlServer(connectionString, b => b.MigrationsAssembly("AppDataAccess")));

// best practice  to register generik interface
builder.Services.AddTransient(typeof(IGenerik<>), typeof(GenerikManager<>));

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".AdventureWorks.Session";
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.IsEssential = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapRazorPages();
app.MapDefaultControllerRoute();
app.Run();
