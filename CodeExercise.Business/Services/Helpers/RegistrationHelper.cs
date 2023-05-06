using CodeExercise.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CodeExercise.Services;

public static class RegistrationHelper
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<AddressBookDbContext>(serviceProvider =>
        {
            var connectionString = SqliteHelper.CreateConnectionString();
            var optionsBuilder = new DbContextOptionsBuilder<AddressBookDbContext>();
            optionsBuilder.UseSqlite(connectionString);
            return new AddressBookDbContext(optionsBuilder.Options);
        });

        services.AddTransient<IDepartmentService, DepartmentService>();
        services.AddTransient<IContactService, ContactService>();

        return services;
    }
}