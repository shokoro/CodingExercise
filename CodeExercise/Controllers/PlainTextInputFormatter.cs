using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace CodeExercise.Controllers;

public class PlainTextInputFormatter : InputFormatter
{
    private const string ContentType = "text/plain";

    public PlainTextInputFormatter()
    {
        SupportedMediaTypes.Add(ContentType);
    }

    public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
    {
        var request = context.HttpContext.Request;
        using var reader = new StreamReader(request.Body);
        var content = await reader.ReadToEndAsync();
        return await InputFormatterResult.SuccessAsync(content);
    }

    public override bool CanRead(InputFormatterContext context)
    {
        var contentType = context.HttpContext.Request.ContentType;
        return contentType.StartsWith(ContentType);
    }
}

public static class InputFormatterExtensions
{
    public static IMvcBuilder AddPlainTextInputFormatter(this IMvcBuilder builder)
    {
        if (builder == null) throw new ArgumentNullException(nameof(builder));
        var options = new MvcOptions();
        return builder.AddMvcOptions(opts =>
        {
            opts.InputFormatters.Add(new PlainTextInputFormatter());
        });
    }
}