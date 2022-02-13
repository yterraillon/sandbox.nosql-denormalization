namespace Application.Events;

public record CocktailCreated(int Id) : INotification;
