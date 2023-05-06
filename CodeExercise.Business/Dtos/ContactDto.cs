using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeExercise.Dtos
{
    /// <summary>
    /// Contact data transfer object
    /// </summary>
    public class ContactDto
    {
        public long Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}
