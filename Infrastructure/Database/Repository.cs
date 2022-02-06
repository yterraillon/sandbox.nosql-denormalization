namespace Infrastructure.Database;
using Application.Models;

public class Repository<T> : IRepository<int, T>
    where T : class
{
    private readonly ILiteCollection<T> _collection;

    public Repository(DbContext dbContext) => _collection = dbContext.Database.GetCollection<T>();

    public void Create(T aggregate) => _collection.Insert(aggregate);

    public void Update(T aggregate) => _collection.Update(aggregate);

    public T Get(int id) => _collection.FindById(id);

    public IEnumerable<T> GetAll() => _collection.FindAll();

    public bool Delete(int id) => _collection.Delete(id);
}