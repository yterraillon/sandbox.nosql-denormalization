namespace Application.Models;

public class Cocktail
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Recipe { get; set; } = string.Empty;
    public IEnumerable<Ingredient> Ingredients { get; set; } = Enumerable.Empty<Ingredient>();
}