namespace Application.CocktailViews;

public class CreateCocktailView : INotificationHandler<CocktailCreated>
{
    private readonly IRepository<int, Ingredient> _ingredientRepository;
    private readonly IRepository<int, Cocktail> _cocktailRepository;
    private readonly ICocktailViewRepository _cocktailViewRepository;

    public CreateCocktailView(IRepository<int, Ingredient> ingredientRepository, IRepository<int, Cocktail> cocktailRepository, ICocktailViewRepository cocktailViewRepository)
    {
        _ingredientRepository = ingredientRepository;
        _cocktailRepository = cocktailRepository;
        _cocktailViewRepository = cocktailViewRepository;
    }
    
    public Task Handle(CocktailCreated notification, CancellationToken cancellationToken)
    {
        var cocktail = _cocktailRepository.Get(notification.Id);
        var ingredients = GetIngredients(cocktail.IngredientIds);
        
        var cocktailView = CocktailViewModel.Create(cocktail);
        cocktailView.SetIngredients(ingredients);
        
        _cocktailViewRepository.Create(cocktailView);
        return Task.CompletedTask;
    }

    private IEnumerable<Ingredient> GetIngredients(IEnumerable<int> ingredientIds) => 
        ingredientIds.Select(ingredientId => _ingredientRepository.Get(ingredientId));
}
