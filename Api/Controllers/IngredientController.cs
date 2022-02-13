using Application;
using Application.Events;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("ingredients")]
public class IngredientController : Controller
{
    private readonly IMediator _mediator;
    private readonly IRepository<int, Ingredient> _repository;

    public IngredientController(IMediator mediator, IRepository<int, Ingredient> repository)
    {
        _mediator = mediator;
        _repository = repository;
    }

    [HttpPut]
    public async Task<IActionResult> Update(Ingredient ingredient)
    {
        _repository.Update(ingredient);
        await _mediator.Publish(new IngredientUpdated(ingredient.Id));
        return Ok(ingredient.Id);
    }
}