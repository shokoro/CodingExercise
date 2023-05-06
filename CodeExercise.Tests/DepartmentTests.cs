using Autofac;
using CodeExercise.Dtos;
using CodeExercise.Models;
using CodeExercise.Services;
using Microsoft.EntityFrameworkCore;

namespace CodeExercise.Tests;

public class DepartmentTests : BaseSharedContextFixture
{
    [Fact]
    [Trait("Category", "Department")]
    public async Task CreateDepartment_add_valid_department_should_create()
    {
        string deptName = "Test Department";

        await using (var scope = SharedContext.Container.BeginLifetimeScope())
        {
            var departmentService = scope.Resolve<IDepartmentService>();
            var testDept = new CreateDepartmentDto { Name = deptName, Description = "Test Department description" };
            await departmentService.CreateDepartment(testDept);
        }

        var db = Container.Resolve<AddressBookDbContext>();
        var verify = await db.Departments.Where(c => c.Name == deptName).FirstAsync();
        Assert.NotNull(verify);
    }

    [Fact]
    [Trait("Category", "Department")]
    public async Task CreateDepartment_should_assign_a_new_id()
    {
        string deptName = "Test Department 2";

        await using var scope = SharedContext.Container.BeginLifetimeScope();

        var departmentService = scope.Resolve<IDepartmentService>();
        var testDept = new CreateDepartmentDto { Name = deptName, Description = "Test Department description" };
        var result = await departmentService.CreateDepartment(testDept);

        Assert.True(result.Id > 0);
    }

    [Fact]
    [Trait("Category", "Department")]
    public async Task CreateDepartment_should_protect_against_null()
    {
        var service = Container.Resolve<IDepartmentService>();
        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
        {
            await service.CreateDepartment(null);
        });
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [Trait("Category", "Department")]
    public async Task CreateDepartment_department_name_is_a_mandatory_field(string departmentName)
    {
        var service = Container.Resolve<IDepartmentService>();
        await Assert.ThrowsAsync<ValidationException>(async () =>
        {
            await service.CreateDepartment(new CreateDepartmentDto { Name = departmentName, Description = "fake description" });
        });
    }

    [Fact]
    [Trait("Category", "Department")]
    public async Task GetAll_should_return_all_departments()
    {
        var service = Container.Resolve<IDepartmentService>();
        var departments = await service.GetAll();
        Assert.NotEmpty(departments);
    }

    public DepartmentTests(SharedContextFixture sharedContext) : base(sharedContext)
    {
    }
}