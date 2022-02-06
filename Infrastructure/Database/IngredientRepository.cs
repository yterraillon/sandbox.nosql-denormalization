using Application;
using Application.Models;

namespace Infrastructure.Database;

public class IngredientRepository : IRepository<int, Ingredient>
{
    private readonly ILiteCollection<Ingredient> _collection;

    public IngredientRepository(DbContext dbContext) => _collection = dbContext.Database.GetCollection<Ingredient>();

    public int Create(Ingredient aggregate)
    {
        _collection.Insert(aggregate);
        return aggregate.Id;
    }

    public void Update(Ingredient aggregate) => _collection.Update(aggregate);

    public Ingredient Get(int id) => _collection.FindById(id);

    public IEnumerable<Ingredient> GetAll() => _collection.FindAll();

    public bool Delete(int id) => _collection.Delete(id);
}