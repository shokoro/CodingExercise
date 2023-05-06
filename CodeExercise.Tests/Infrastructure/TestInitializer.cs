using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using CodeExercise.Services;
using Autofac.Extensions.DependencyInjection;

namespace CodeExercise.Tests;

public static class TestInitializer
{
    [ModuleInitializer]
    public static void Initialize()
    {
        CreateSqliteDbIfNotExist();

        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .AddEnvironmentVariables()
            .AddUserSecrets(typeof(TestInitializer).Assembly)
            .Build();

        Configuration = config;

        IServiceCollection services = new ServiceCollection();
        var builder = new ContainerBuilder();

        services.AddLogging();
        services.AddBusinessServices();

        builder.Populate(services);
        Container = builder.Build();
    }

    public static IConfiguration Configuration { get; private set; }
    public static IContainer Container { get; private set; }

    static void CreateSqliteDbIfNotExist()
    {
        var workingFolder = AppDomain.CurrentDomain.BaseDirectory;
        var dbFile = Path.Combine(workingFolder, "address_book.sqlite");
        var templateFile = Path.Combine(workingFolder, "address_book.template");
        if (!File.Exists(templateFile))
        {
            throw new FileNotFoundException("Template database file not found.", templateFile);
        }

        File.Copy(Path.Combine(workingFolder, "address_book.template"), dbFile, true);
    }
}