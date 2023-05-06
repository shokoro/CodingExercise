using Autofac;
using CodeExercise.Dtos;
using CodeExercise.Models;
using CodeExercise.Services;
using Microsoft.EntityFrameworkCore;

namespace CodeExercise.Tests;

public class ContactServiceTests : BaseSharedContextFixture
{
    [Fact]
    public async Task CreateContacts_should_create_then_list()
    {
        var input = new List<CreateContactDto>()
        {
            new CreateContactDto { FirstName = "James", LastName = "Test01", Email = "mymail01@mail.com" },
            new CreateContactDto { FirstName = "George", LastName = "Test01", Email = "mymail02@mail.com" }
        };

        await using (var scope = SharedContext.Container.BeginLifetimeScope())
        {
            var contactService = scope.Resolve<IContactService>();
            await contactService.CreateContacts(input);
        }

        var db = Container.Resolve<AddressBookDbContext>();
        var verify = await db.Contacts.Where(c => c.LastName == "Test01").ToListAsync();

        Assert.True(verify.Count >= 2);
    }

    [Fact]
    public async Task CreateContacts_should_protect_against_null()
    {
        var service = Container.Resolve<IContactService>();
        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
        {
            await service.CreateContacts(null);
        });
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task CreateContacts_should_ensure_email_is_provided_always(string badEmail)
    {
        var input = new List<CreateContactDto>()
        {
            new CreateContactDto { FirstName = "James", LastName = "Test02", Email = badEmail },
            new CreateContactDto { FirstName = "George", LastName = "Test02", Email = "mymail04@mail.com" }
        };

        var service = Container.Resolve<IContactService>();
        await Assert.ThrowsAsync<ValidationException>(async () =>
        {
            await service.CreateContacts(input);
        });
    }

    [Fact]
    public async Task GetContactsByLast_should_filter_by_last_name()
    {
        var input = new List<Contact>()
        {
            new() { FirstName = "Ferre", LastName = "Test04", Email = "kaskito1@mail.com" },
            new() { FirstName = "Patricia", LastName = "Test04", Email = "kaskito2@mail.com" }
        };

        await using (var scope = SharedContext.Container.BeginLifetimeScope())
        {
            var db = scope.Resolve<AddressBookDbContext>();
            await db.AddRangeAsync(input);
            await db.SaveChangesAsync();
        }

        var service = Container.Resolve<IContactService>();
        var verify = await service.GetContactsByLastName("Test04");

        Assert.True(verify.Count >= 2);
    }

    public ContactServiceTests(SharedContextFixture sharedContext) : base(sharedContext)
    {
    }
}