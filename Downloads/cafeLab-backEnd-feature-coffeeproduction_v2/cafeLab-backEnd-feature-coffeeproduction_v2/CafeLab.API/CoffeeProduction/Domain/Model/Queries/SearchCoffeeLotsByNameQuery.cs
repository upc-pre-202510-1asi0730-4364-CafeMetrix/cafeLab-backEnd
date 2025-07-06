namespace CafeLab.API.CoffeeProduction.Domain.Model.Queries;

public class SearchCoffeeLotsByNameQuery
{
    public string LotName { get; set; } = string.Empty;
    public int UserId { get; set; }
} 