using CodeExercise.Dtos;
using CodeExercise.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CodeExercise.Services;

public class ContactService : IContactService
{
    private AddressBookDbContext db;

    public ContactService(AddressBookDbContext db)
    {
        this.db = db;
    }

    public async Task<IList<ContactDto>> CreateContacts(IEnumerable<CreateContactDto> contacts)
    {
        var contactList = new List<Contact>();
        List<ContactDto> contactDto = new List<ContactDto>();
        if (!contacts.Any()) throw new ArgumentNullException(nameof(contacts));
        
        foreach (var item in contacts)
        {
            if(string.IsNullOrWhiteSpace(item.Email)) throw new ValidationException("Email is a mandatory field.");

            var contact = new Contact
            {
                FirstName = item.FirstName,
                LastName = item.LastName,
                Email = item.Email
            };
            contactDto.Add(contact.ConvertToDto()!);

            await db.AddAsync(contact);
        }
        
        await db.SaveChangesAsync();

        return contactDto;
    }

    public async Task<IList<ContactDto>> GetContactsByLastName(string lastName)
    {
        List<ContactDto> contactList = new List<ContactDto>();
        var contacts = await db.Contacts.
            Where(x => !string.IsNullOrEmpty(x.LastName) && 
            x.LastName.ToLower().Contains(lastName.ToLower())).ToListAsync();

        foreach (var item in contacts)
        {
            contactList.Add(item.ConvertToDto()!);
        }

        return contactList;
    }
}