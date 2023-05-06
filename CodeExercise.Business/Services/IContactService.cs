using CodeExercise.Dtos;
using System.Collections.Generic;

namespace CodeExercise.Services;

public interface IContactService
{
    /// <summary>
    /// Given a collection of contacts. Save contacts to the data store and return
    /// the list of save contacts with the ID generated.
    /// The logic of generating ID is already included in the BaseEntity class.
    /// </summary>
    /// <param name="contacts"></param>
    /// <returns></returns>
    Task<IList<ContactDto>> CreateContacts(IEnumerable<CreateContactDto> contacts);

    /// <summary>
    /// Return a list of all contacts matching the provided last name.
    /// </summary>
    /// <param name="lastName"></param>
    /// <returns></returns>
    Task<IList<ContactDto>> GetContactsByLastName(string lastName);
}