namespace Application.CocktailViews;

public class UpdateCocktailView: INotificationHandler<CocktailUpdated>
{
    private readonly IRepository<int, Ingredient> _ingredientRepository;
    private readonly IRepository<int, Cocktail> _cocktailRepository;
    private readonly ICocktailViewRepository _cocktailViewRepository;
    
    public UpdateCocktailView(IRepository<int, Ingredient> ingredientRepository, IRepository<int, Cocktail> cocktailRepository, ICocktailViewRepository cocktailViewRepository)
    {
        _ingredientRepository = ingredientRepository;
        _cocktailRepository = cocktailRepository;
        _cocktailViewRepository = cocktailViewRepository;
    }
    
    public Task Handle(CocktailUpdated notification, CancellationToken cancellationToken)
    {
        var cocktail = _cocktailRepository.Get(notification.Id);
        var ingredients = GetIngredients(cocktail.IngredientIds);
        
        var cocktailView = _cocktailViewRepository.GetByCocktailId(notification.Id);
        cocktailView.Name = cocktail.Name;
        cocktailView.Recipe = cocktail.Recipe;
        cocktailView.SetIngredients(ingredients);
        
        _cocktailViewRepository.Update(cocktailView);
        
        return Task.CompletedTask;
    }
    
    private IEnumerable<Ingredient> GetIngredients(IEnumerable<int> ingredientIds) => 
        ingredientIds.Select(ingredientId => _ingredientRepository.Get(ingredientId));
}