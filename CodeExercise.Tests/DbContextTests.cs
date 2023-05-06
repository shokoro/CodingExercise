using Autofac;
using CodeExercise.Models;

namespace CodeExercise.Tests
{
    public class DbContextTests
    {
        [Fact]
        public void Can_Resolve_Db_context()
        {
            using var db = TestInitializer.Container.Resolve<AddressBookDbContext>();
            var contacts = db.Departments.ToList();
            Assert.NotEmpty(contacts);
        }
    }
}