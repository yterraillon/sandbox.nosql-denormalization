using Application;
using Application.Models;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/ingredients", (IRepository<int, Ingredient> repository) => repository.GetAll());
app.MapGet("/ingredients/{id}", (IRepository<int, Ingredient> repository, int id) =>
    repository.Get(id) switch
    {
        Ingredient ingredient => Results.Ok(ingredient),
        null => Results.NotFound()
    });
app.MapPost("/ingredients", (IRepository<int, Ingredient> repository, Ingredient ingredient) =>
{
    var id = repository.Create(ingredient);
    return Results.Created($"/ingredients/{id}", id);
});
app.MapPut("ingredients", (IRepository<int, Ingredient> repository, Ingredient ingredient) =>
{
    repository.Update(ingredient);
    return Results.NoContent();
});
app.MapDelete("ingredients/{id}", (IRepository<int, Ingredient> repository, int id) => 
    repository.Delete(id) ? Results.NoContent() : Results.Problem());

app.Run();