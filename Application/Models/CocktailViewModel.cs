namespace Application.Models;

public class CocktailViewModel
{
    public int Id { get; set; }
    public int CocktailId { get; init; }
    public string Name { get; set; } = string.Empty;
    public string Recipe { get; set; } = string.Empty;
    public IEnumerable<Ingredient> Ingredients { get; private set; } = Enumerable.Empty<Ingredient>();

    public static CocktailViewModel Create(Cocktail cocktail) => new()
        {
            CocktailId = cocktail.Id,
            Name = cocktail.Name,
            Recipe = cocktail.Recipe
        };

    public void SetIngredients(IEnumerable<Ingredient> ingredients) => Ingredients = ingredients;

    public void UpdateIngredient(Ingredient updatedIngredient)
    {
        var current = Ingredients
            .First(i => i.Id == updatedIngredient.Id);
        
        var updated = current with {Name = updatedIngredient.Name, Brand = updatedIngredient.Brand, IsAlcohol = updatedIngredient.IsAlcohol};
        
        Ingredients = Ingredients
            .Where(i => i.Id != updatedIngredient.Id)
            .Append(updated);
    }
}