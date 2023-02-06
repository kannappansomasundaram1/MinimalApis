using MinimalApis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IMessageProvider, MessageProvider>();

var app = builder.Build();

app.MapGet("/", () => "Hello NDC");

app.MapGet("/{firstname}",
    async (string firstname, string? lastname, IMessageProvider messageProvider) =>
        await messageProvider.GetMessage(firstname + " " + lastname));

app.MapGet("/special",
    async (HttpRequest request, HttpResponse response, CancellationToken CancellationToken) =>
    {
        await response.WriteAsJsonAsync(request.Query, CancellationToken);
    });

var _books = new ConcurrentDictionary<int, Book>
{
    [1] = new(1, "Code that fits in your head")
};

app.MapGet("/books/{id}",
        async (int id) => _books.TryGetValue(id, out var book) ? Results.Ok(book) : Results.NotFound())
    .Produces<Book>()
    .Produces(StatusCodes.Status404NotFound)
    .WithName("GetBookbyID").WithTags("Getters");

app.MapPost("/books",
        async ([FromBody] Book addbook, HttpResponse response) =>
        {
            _books.TryAdd(addbook.Id, addbook);
            response.StatusCode = 201;
            response.Headers.Location = $"books/{addbook.Id}";
        })
    .Accepts<Book>("application/json")
    .Produces<Book>(StatusCodes.Status201Created);

app.Run();
