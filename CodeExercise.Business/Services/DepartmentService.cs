using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeExercise.Dtos;
using CodeExercise.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeExercise.Services
{
    public class DepartmentService : IDepartmentService
    {
        private AddressBookDbContext db;

        public DepartmentService(AddressBookDbContext db)
        {
            this.db = db;
        }

        public async Task<DepartmentDto> CreateDepartment(CreateDepartmentDto department)
        {
            if (department == null) throw new ArgumentNullException(nameof(department));

            if (string.IsNullOrWhiteSpace(department.Name))
                throw new ValidationException("Department Name is a mandatory field.");

            var entity = new Department
            {
                Name = department.Name,
                Description = department.Description
            };

            await db.AddAsync(entity);
            await db.SaveChangesAsync();

            return entity.ConvertToDto()!;
        }

        public async Task<IList<DepartmentDto>> GetAll()
        {
            List<DepartmentDto> deptList = new List<DepartmentDto>();
            var depts = await db.Departments.ToListAsync();
            foreach (var department in depts)
            {
                deptList.Add(department.ConvertToDto()!);
            }

            return deptList;
        }
    }
}
