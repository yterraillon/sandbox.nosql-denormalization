namespace Application.Events;

public record IngredientUpdated(int Id) : INotification;
