namespace Application;

public interface IRepository<in TId, TAggregate>
    where TAggregate : class
    where TId : notnull
{
    void Create(TAggregate aggregate);
    
    void Update(TAggregate aggregate);
    
    TAggregate Get(TId id);

    IEnumerable<TAggregate> GetAll();
    
    bool Delete(TId id);
}