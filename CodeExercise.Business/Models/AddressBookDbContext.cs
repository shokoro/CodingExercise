using Microsoft.EntityFrameworkCore;

namespace CodeExercise.Models;

public class AddressBookDbContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Department> Departments { get; set; }

    public AddressBookDbContext(DbContextOptions<AddressBookDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ContactConfiguration());
        modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
    }
}