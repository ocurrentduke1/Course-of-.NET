var builder = WebApplication.CreateBuilder(args);

SemaphoreSlim gate = new(1, 1);

var app = builder.Build();

app.MapPost("/createUser", (HttpContext context) =>
{
    var user = new { Name = "John Doe", Age = 30 };
    return Results.Ok(user);
});

app.MapPost("/user", (HttpContext context) =>
{
    var user = new { Name = "John Doe", Age = 30 };
    return Results.Created($"/user/{user.Name}", user);
});

app.MapPost("/beer", (HttpContext context) =>
{
    gate.Wait();
    try
    {
        var requestBody = context.Request.ReadFromJsonAsync<Beer>().Result;
        if (requestBody == null)
        {
            return Results.BadRequest("Invalid beer data.");
        }

        Thread.Sleep(3000);

        var beer = new Beer(Guid.NewGuid(), requestBody.Name);

        return Results.Created($"/beer/{beer.id}", beer);
    }
    finally
    {
        gate.Release();
    }

});

app.MapPost("/wine", async (HttpContext context) =>
{
        var requestBody = await context.Request.ReadFromJsonAsync<Wine>();
        if (requestBody == null)
        {
            return Results.BadRequest("Invalid wine data.");
        }

        await Task.Delay(3000);

        var wine = new Wine(Guid.NewGuid(), requestBody.Name);

        return Results.Created($"/wine/{wine.id}", wine);
});

app.Run();

record Beer(Guid id, string Name);

record Wine(Guid id, string Name);