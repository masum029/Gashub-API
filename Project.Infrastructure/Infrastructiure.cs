using InventoryApi.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Application.Interfaces;
using Project.Domail.Abstractions;
using Project.Infrastructure.DataContext;
using Project.Infrastructure.Implementation;
using Project.Infrastructure.Services;

namespace Project.Infrastructiure
{
    public static class Infrastructiure
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("dbcs"));
            });
            services.Configure<IdentityOptions>(options =>
            {
                // Default Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                // Default Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false; // For special character
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                // Default SignIn settings.
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.User.RequireUniqueEmail = true;
            });
            services.AddScoped<IUnitOfWorkDb, UnitOfWorkDb>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IBaseServices<PurchaseDTOs>, PurchaseService>();
            services.AddScoped<IPurchaseServices, PurchaseService>();
            return services;
        }
    }
}
