namespace Application.Models;

public class Cocktail
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Recipe { get; set; } = string.Empty;
    public IEnumerable<int> IngredientIds { get; set; } = Enumerable.Empty<int>();
}