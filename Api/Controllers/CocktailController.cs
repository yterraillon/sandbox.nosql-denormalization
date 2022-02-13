using Application;
using Application.Events;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("cocktails")]
public class CocktailController : Controller
{
    private readonly IMediator _mediator;
    private readonly IRepository<int, Cocktail> _repository;

    public CocktailController(IMediator mediator, IRepository<int, Cocktail> repository)
    {
        _mediator = mediator;
        _repository = repository;
    }
    
    // Should be in a handler, but kept it simple for the sake of testing & learning
    [HttpPost]
    public async Task<IActionResult> Create(Cocktail cocktail)
    {
        _repository.Create(cocktail);
        await _mediator.Publish(new CocktailCreated(cocktail.Id));
        return Ok(cocktail.Id);
    }
    
    [HttpPut]
    public async Task<IActionResult> Update(Cocktail cocktail)
    {
        _repository.Update(cocktail);
        await _mediator.Publish(new CocktailUpdated(cocktail.Id));
        return Ok(cocktail.Id);
    }
}