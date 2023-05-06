using CodeExercise.Dtos;
using CodeExercise.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeExercise.Controllers;

[ApiController]
[Route("departments")]
public class DepartmentController : ControllerBase
{
    private IDepartmentService departmentService;
    private ILogger<DepartmentController> logger;

    [HttpPost("")]
    public async Task<IActionResult> Create(CreateDepartmentDto department)
    {
        try
        {
            var result = await departmentService.CreateDepartment(department);
            return Ok(result);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Could not create department");
            return BadRequest();
        }
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        var list = await departmentService.GetAll();
        return Ok(list);
    }

    public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger)
    {
        this.departmentService = departmentService;
        this.logger = logger;
    }
}