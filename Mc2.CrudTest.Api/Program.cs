using System.Reflection;
using Mc2.CrudTest.Application;
using Mc2.CrudTest.Common.Swagger;
using Mc2.CrudTest.Infrastructure.Models;
using Mc2.CrudTest.Infrastructure.ModelsRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerVersion(builder.Configuration, Assembly.GetExecutingAssembly());
builder.Services.AddSwaggerDoc(builder.Configuration, Assembly.GetExecutingAssembly());
builder.Services.AddSwaggerVersion(builder.Configuration, Assembly.GetExecutingAssembly());
builder.Services.InstallMediatr();
builder.Services.AddDbContext<CustomerDbContext>(optionsAction => { optionsAction.UseSqlServer("DefaultConnection"); });
builder.Services.AddScoped<ICustomerRepository, CustomerSqlRepository>();
var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSwaggerDoc(builder.Environment);
app.UseRouting();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();