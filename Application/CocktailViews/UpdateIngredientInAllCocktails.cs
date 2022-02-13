namespace Application.CocktailViews;

public class UpdateIngredientInAllCocktails : INotificationHandler<IngredientUpdated>
{
    private readonly IRepository<int, Ingredient> _ingredientRepository;
    private readonly ICocktailViewRepository _cocktailViewRepository;

    public UpdateIngredientInAllCocktails(IRepository<int, Ingredient> ingredientRepository, ICocktailViewRepository cocktailViewRepository)
    {
        _ingredientRepository = ingredientRepository;
        _cocktailViewRepository = cocktailViewRepository;
    }

    public Task Handle(IngredientUpdated notification, CancellationToken cancellationToken)
    {
        var allCocktailViewsWithIngredient = _cocktailViewRepository.GetAllWithIngredient(notification.Id);
        var ingredient = _ingredientRepository.Get(notification.Id);
        
        UpdateIngredientInCocktailViewModels(allCocktailViewsWithIngredient, ingredient);
        
        return Task.CompletedTask;
    }

    private void UpdateIngredientInCocktailViewModels(IEnumerable<CocktailViewModel> cocktailViewModels, Ingredient ingredient)
    {
        foreach (var cocktailViewModel in cocktailViewModels)
        {
            cocktailViewModel.UpdateIngredient(ingredient);
            _cocktailViewRepository.Update(cocktailViewModel);
        }
    }
}