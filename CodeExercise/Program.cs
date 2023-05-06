using CodeExercise.Controllers;
using CodeExercise.Services;

namespace CodeExercise
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //ensure that we have a working file database
            CreateSqliteDbIfNotExist();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddBusinessServices();
            builder.Services.AddControllers()
                .AddPlainTextInputFormatter();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        static void CreateSqliteDbIfNotExist()
        {
            var workingFolder = AppDomain.CurrentDomain.BaseDirectory;
            var dbFile = Path.Combine(workingFolder, "address_book.sqlite");
            if (!File.Exists(dbFile))
            {
                File.Copy(Path.Combine(workingFolder, "address_book.template"), dbFile);
            }
        }
    }
}