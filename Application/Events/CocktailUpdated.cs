namespace Application.Events;

public record CocktailUpdated(int Id) : INotification;
