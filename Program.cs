using DriversManagement.Models.Data.Context;
using DriversManagement.Repositories.Implementations;
using DriversManagement.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DriversManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region services

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<DriversManagementDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DriversManagementDBConnection"));

            });

            #endregion

            #region ioc

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            #endregion

            //builder.Services.AddScoped<ILoggerRepository, LogRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}