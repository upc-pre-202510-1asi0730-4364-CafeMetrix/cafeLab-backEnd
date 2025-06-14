namespace CafeLab.API.CuppingSessions.Domain.Model;

public class Ratings
{
    public int Fragancia { get; set; }
    public int Sabor { get; set; }
    public int Acidez { get; set; }
    public int Cuerpo { get; set; }
    public int Balance { get; set; }
    public int Postgusto { get; set; }

    public Ratings(int fragancia, int sabor, int acidez, int cuerpo, int balance, int postgusto)
    {
        Fragancia = fragancia;
        Sabor = sabor;
        Acidez = acidez;
        Cuerpo = cuerpo;
        Balance = balance;
        Postgusto = postgusto;
    }

    // Constructor sin parámetros para Entity Framework Core
    public Ratings() { }
} 