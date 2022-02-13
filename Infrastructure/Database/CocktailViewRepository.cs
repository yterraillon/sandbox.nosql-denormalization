using Application.Models;

namespace Infrastructure.Database;

public class CocktailViewRepository : ICocktailViewRepository
{
    private readonly ILiteCollection<CocktailViewModel> _collection;

    public CocktailViewRepository(DbContext dbContext) => _collection = dbContext.Database.GetCollection<CocktailViewModel>();
    
    public void Create(CocktailViewModel aggregate) => _collection.Insert(aggregate);

    public void Update(CocktailViewModel aggregate) => _collection.Update(aggregate);

    public CocktailViewModel Get(int id) => _collection.FindById(id);

    public IEnumerable<CocktailViewModel> GetAll() => _collection.FindAll();

    public bool Delete(int id) => _collection.Delete(id);
    
    public CocktailViewModel GetByCocktailId(int cocktailId) => _collection.FindOne(c => c.CocktailId == cocktailId);

    public IEnumerable<CocktailViewModel> GetAllWithIngredient(int ingredientId) =>
        _collection
            .Query()
            .Where(c => c.Ingredients
            .Select(i => i.Id)
            .Any(i => i == ingredientId))
            .ToEnumerable();
}