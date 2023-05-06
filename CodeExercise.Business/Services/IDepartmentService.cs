using CodeExercise.Dtos;

namespace CodeExercise.Services;

public interface IDepartmentService
{
    Task<DepartmentDto> CreateDepartment(CreateDepartmentDto department);
    Task<IList<DepartmentDto>> GetAll();
}