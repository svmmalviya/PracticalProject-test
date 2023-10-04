using Microsoft.EntityFrameworkCore;
using PracticalProject.AppContext;
using PracticalProject.Controllers;
using PracticalProject.Interface_and_repos;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(
    options => { 
        options.UseSqlServer(builder.Configuration.GetConnectionString("ConnStr")); 
    });

builder.Services.AddScoped<IInvoice,InvoiceRepo>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=InvoiceList}/{id?}");

app.Run();
