using CodeExercise.Dtos;
using CodeExercise.Models;

namespace CodeExercise.Services;

public static class SqliteHelper
{
    public static string CreateConnectionString(string? databaseName = null)
    {
        if (string.IsNullOrWhiteSpace(databaseName))
            databaseName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "address_book.sqlite");

        return $"Data Source={databaseName}";
    }
}

public static class EntityHelper
{
    public static ContactDto? ConvertToDto(this Contact? contact)
    {
        if (contact == null)
            return null;

        return new ContactDto
        {
            Id = contact.Id,
            FirstName = contact.FirstName,
            LastName = contact.LastName,
            Email = contact.Email,
        };
    }

    public static DepartmentDto? ConvertToDto(this Department? department)
    {
        if(department == null) return null;

        return new DepartmentDto
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description,
        };
    }
}