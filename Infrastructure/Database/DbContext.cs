using Application.Models;

namespace Infrastructure.Database;

public class DbContext
{
    public LiteDatabase Database { get; }

    public DbContext()
    {
        Database = new LiteDatabase("Cocktails.db");

        Database.GetCollection<Ingredient>()
            .EnsureIndex(i => i.Id);
    }
}