namespace Application;

public interface IRepository<TId, TAggregate>
    where TAggregate : class
    where TId : notnull
{
    TId Create(TAggregate aggregate);
    
    void Update(TAggregate aggregate);
    
    TAggregate Get(TId id);

    IEnumerable<TAggregate> GetAll();
    
    bool Delete(TId id);
}