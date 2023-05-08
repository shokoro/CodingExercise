using System.Net.Mime;
using CodeExercise.Dtos;
using CodeExercise.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeExercise.Controllers;

[ApiController]
[Route("contacts")]
public class ContactController : ControllerBase
{
    private IContactService contactService;
    private ILogger<ContactController> logger;

    public ContactController(IContactService contactService, ILogger<ContactController> logger)
    {
        this.contactService = contactService ?? throw new ArgumentNullException(nameof(contactService));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="commaSeparatedInput"></param>
    /// <returns></returns>
    [HttpPost("")]
    [Consumes(MediaTypeNames.Text.Plain)]
    public async Task<IActionResult> Create([FromBody] string commaSeparatedInput)
    {
        /* Expects input to be plain text that will be comma separated with the first line to be column names.
         * example:
         * FirstName,LastName,Email
         * John,Smith,jsmith@email.com
         * Jane,Doe,jdoe@email.com
         *
         * Parse the input and create contacts
         * if you encounter an error return a bad request response. You should handle all errors and log the exceptions
         *
         * Use contactService.CreateContacts method to create the contacts. Return the result.
         */

        try
        {
            // Split the input by lines and remove any leading/trailing whitespace
            var lines = commaSeparatedInput.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).Select(line => line.Trim());

            // Get the column names from the first line of input
            var columns = lines.First().Split(',').Select(column => column.Trim());

            // Create a list to hold the contacts
            var contacts = new List<CreateContactDto>();

            // Loop through the remaining lines and create a contact for each one
            foreach (var line in lines.Skip(1))
            {
                var values = line.Split(',').Select(value => value.Trim());

                //// Check if the values are valid
                if (values?.Count() != 3 || values.Any(value => string.IsNullOrEmpty(value)))
                {
                    throw new ValidationException("Contact data is incorrect");
                }

                // Create a new contact and add it to the list
                var contact = new CreateContactDto
                {
                    FirstName = values.ElementAt(0),
                    LastName = values.ElementAt(1),
                    Email = values.ElementAt(2)
                };
                contacts.Add(contact);
            }

            var result = await contactService.CreateContacts(contacts);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Could not create contact");
            return BadRequest();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="lastName"></param>
    /// <returns></returns>
    [HttpGet("")]
    public async Task<IActionResult> GetContactsByLastName(string lastName)
    {
        var list = await contactService.GetContactsByLastName(lastName);
        return Ok(list);
    }

}