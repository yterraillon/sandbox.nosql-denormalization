using Application;
using Application.Models;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure();
builder.Services.AddApplication();
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

// Ingredients
app.MapGet("/ingredients", (IRepository<int, Ingredient> repository) => repository.GetAll());
app.MapGet("/ingredients/{id}", (IRepository<int, Ingredient> repository, int id) =>
    repository.Get(id) switch
    {
        Ingredient ingredient => Results.Ok(ingredient),
        null => Results.NotFound()
    });
app.MapPost("/ingredients", (IRepository<int, Ingredient> repository, Ingredient ingredient) =>
{
    repository.Create(ingredient);
    return Results.Created($"/ingredients/{ingredient.Id}", ingredient.Id);
});
// app.MapPut("ingredients", (IRepository<int, Ingredient> repository, Ingredient ingredient) =>
// {
//     repository.Update(ingredient);
//     return Results.NoContent();
// });
app.MapDelete("ingredients/{id}", (IRepository<int, Ingredient> repository, int id) => 
    repository.Delete(id) ? Results.NoContent() : Results.Problem()); // TODO need rework

// Cocktails
app.MapGet("/cocktails", (IRepository<int, Cocktail> repository) => repository.GetAll());
app.MapGet("/cocktails/{id}", (IRepository<int, Cocktail> repository, int id) =>
    repository.Get(id) switch
    {
        { } cocktail => Results.Ok(cocktail),
        null => Results.NotFound()
    });
// app.MapPost("/cocktails", async ([FromServices]IRepository<int, Cocktail> repository, [FromServices] Mediator mediator, [FromBody] Cocktail cocktail) =>
// {
//     repository.Create(cocktail);
//     await mediator.Publish(new CocktailCreated(cocktail.Id));
//     return Results.Created($"/cocktails/{cocktail.Id}", cocktail.Id);
// });
// app.MapPut("cocktails", (IRepository<int, Cocktail> repository, Cocktail cocktail) =>
// {
//     repository.Update(cocktail);
//     return Results.NoContent();
// });
app.MapDelete("cocktails/{id}", (IRepository<int, Cocktail> repository, int id) => 
    repository.Delete(id) ? Results.NoContent() : Results.Problem()); // TODO need rework

// CocktailViewModels
app.MapGet("/cocktailViews", (ICocktailViewRepository repository) => repository.GetAll());

app.Run();