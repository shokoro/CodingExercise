using Autofac;
using CodeExercise.Controllers;
using CodeExercise.Dtos;
using CodeExercise.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodeExercise.Tests;

public class ContactControllerTests : BaseSharedContextFixture
{
    [Fact]
    public async Task Create_should_return_ok_with_object()
    {
        var service = Container.Resolve<IContactService>();
        var logger = Container.Resolve<ILogger<ContactController>>();
        var departmentController = new ContactController(service, logger);
        string input =
            "FirstName,LastName,Email\nGeorge,Test1010,gweya@mail.com\nKoffi,Test1010,koffi@mail.com\nSam,Test1010,sam@mail.com\nMatt,Test1010,mlasota@mail.com";

        var result = (OkObjectResult)
            (await departmentController.Create(input));

        Assert.NotNull(result.Value);
        Assert.True(((IList<ContactDto>)result.Value).Count == 4);
    }

    [Fact]
    public async Task Create_new_created_contacts_should_have_valid_Ids()
    {
        var service = Container.Resolve<IContactService>();
        var logger = Container.Resolve<ILogger<ContactController>>();
        var departmentController = new ContactController(service, logger);
        string input =
            "FirstName,LastName,Email\nGeorge,Test1010,gweya@mail.com\nKoffi,Test1010,koffi@mail.com\nSam,Test1010,sam@mail.com\nMatt,Test1010,mlasota@mail.com";

        var result = (OkObjectResult)
            (await departmentController.Create(input));

        Assert.NotNull(result.Value);
        var createdContacts = (IList<ContactDto>)result.Value;
        Assert.True(createdContacts.All(c => c.Id > 0));
    }

    [Fact]
    public async Task Create_should_return_bad_request_on_error()
    {
        var service = Container.Resolve<IContactService>();
        var logger = Container.Resolve<ILogger<ContactController>>();
        var departmentController = new ContactController(service, logger);
        string input =
            "FirstName,LastName,Email\nGeorge,Test1010,gweya@mail.com\nKoffi,Test1010, \nSam,Test1010,sam@mail.com\nMatt,Test1010,mlasota@mail.com";

        var result = (StatusCodeResult)
            (await departmentController.Create(input));

        Assert.NotNull(result);
        Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
    }

    //[Fact]
    //public async Task GetContactsByLastName_should_return_a_list_of_contacts_filtered_last_name()
    //{
    //    var service = Container.Resolve<IContactService>();
    //    var logger = Container.Resolve<ILogger<ContactController>>();
    //    var departmentController = new ContactController(service, logger);

    //    var result = (OkObjectResult)
    //        (await departmentController.GetByLastName("Bell"));

    //    Assert.NotNull(result.Value);
    //    var foundContacts = (IList<ContactDto>)result.Value;
    //    Assert.True(foundContacts.Count >= 2);
    //}

    public ContactControllerTests(SharedContextFixture sharedContext) : base(sharedContext)
    {
    }
}