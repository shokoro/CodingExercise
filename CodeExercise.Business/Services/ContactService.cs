using CodeExercise.Dtos;
using CodeExercise.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeExercise.Services;

public class ContactService : IContactService
{
    private AddressBookDbContext db;

    public ContactService(AddressBookDbContext db)
    {
        this.db = db;
    }

    public Task<IList<ContactDto>> CreateContacts(IEnumerable<CreateContactDto> contacts)
    {
        throw new NotImplementedException();
    }

    public Task<IList<ContactDto>> GetContactsByLastName(string lastName)
    {
        throw new NotImplementedException();
    }
}