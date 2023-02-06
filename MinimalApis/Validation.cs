namespace MinimalApis;

public class ValidationFilter<T> : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var argToValidate = context.GetArgument<T>(0);
        var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();

        if (validator is null) return await next.Invoke(context);
        var validationResult = await validator.ValidateAsync(argToValidate!);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary(),
                statusCode: (int)HttpStatusCode.BadRequest);
        }

        // Otherwise invoke the next filter in the pipeline
        return await next.Invoke(context);
    }
}

public class CreateBookRequestValidator : AbstractValidator<Book>
{
    public CreateBookRequestValidator()
    {
        RuleFor(book => book.Id).NotEmpty();
        RuleFor(book => book.Title).NotEmpty();
    }
}

public record Book(int Id, string Title);
