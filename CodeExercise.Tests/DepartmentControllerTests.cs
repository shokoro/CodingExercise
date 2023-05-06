using Autofac;
using CodeExercise.Controllers;
using CodeExercise.Dtos;
using CodeExercise.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodeExercise.Tests;

public class DepartmentControllerTests : BaseSharedContextFixture
{
    [Fact]
    [Trait("Category", "Department")]
    public async Task Create_should_return_ok_with_object()
    {
        var service = Container.Resolve<IDepartmentService>();
        var logger = Container.Resolve<ILogger<DepartmentController>>();
        var departmentController = new DepartmentController(service, logger);

        var result = (OkObjectResult)
            (await departmentController.Create(new CreateDepartmentDto { Name = "Shock Dept", Description = "Shock " }));

        Assert.NotNull(result.Value);
        Assert.True(((DepartmentDto)result.Value).Id > 0);

    }

    [Fact]
    [Trait("Category", "Department")]
    public async Task Create_should_return_bad_request_when_error_encountered()
    {
        var service = Container.Resolve<IDepartmentService>();
        var logger = Container.Resolve<ILogger<DepartmentController>>();
        var departmentController = new DepartmentController(service, logger);

        var result = (StatusCodeResult)
            (await departmentController.Create(new CreateDepartmentDto { Name = "", Description = "Shock " }));

        Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
    }

    [Fact]
    [Trait("Category", "Department")]
    public async Task GetAll_should_return_list_of_departments()
    {
        var service = Container.Resolve<IDepartmentService>();
        var logger = Container.Resolve<ILogger<DepartmentController>>();
        var departmentController = new DepartmentController(service, logger);

        var result = (OkObjectResult)
            (await departmentController.GetAll());

        Assert.NotNull(result.Value);
        Assert.NotEmpty((IList<DepartmentDto>) result.Value);
    }

    public DepartmentControllerTests(SharedContextFixture sharedContext) : base(sharedContext)
    {
    }
}