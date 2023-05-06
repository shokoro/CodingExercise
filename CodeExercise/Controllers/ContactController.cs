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

        return Ok();
    }

}