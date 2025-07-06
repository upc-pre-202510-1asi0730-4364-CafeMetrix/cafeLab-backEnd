namespace CafeLab.API.CoffeeProduction.Domain.Model.Queries;

public class SearchRoastProfilesByNameQuery
{
    public string ProfileName { get; set; } = string.Empty;
    public int UserId { get; set; }
} 