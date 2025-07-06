using System;
using System.Collections.Generic;

namespace CafeLab.API.Sensory_evaluation.Domain.Model.Aggregates
{
    public class CuppingSession
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public DateTime Date { get; private set; }
        public string Origin { get; private set; }
        public string Variety { get; private set; }
        public string Process { get; private set; }
        public string Lot { get; private set; }
        public string Profile { get; private set; }
        public CuppingSessionRatings Ratings { get; set; }
        public int UserId { get; private set; }

        public CuppingSession(string name, DateTime date, string origin, string variety, string process, string lot, string profile, CuppingSessionRatings ratings, int userId)
        {
            Name = name;
            Date = date;
            Origin = origin;
            Variety = variety;
            Process = process;
            Lot = lot;
            Profile = profile;
            Ratings = ratings;
            UserId = userId;
        }

        public CuppingSession() { }

        public void Update(string name, DateTime date, string origin, string variety, string process, string lot, string profile, CuppingSessionRatings ratings)
        {
            Name = name;
            Date = date;
            Origin = origin;
            Variety = variety;
            Process = process;
            Lot = lot;
            Profile = profile;
            Ratings = ratings;
        }
    }

    public class CuppingSessionRatings
    {
        public int Fragancia { get; private set; }
        public int Sabor { get; private set; }
        public int Acidez { get; private set; }
        public int Cuerpo { get; private set; }
        public int Balance { get; private set; }
        public int Postgusto { get; private set; }

        public CuppingSessionRatings(int fragancia, int sabor, int acidez, int cuerpo, int balance, int postgusto)
        {
            Fragancia = fragancia;
            Sabor = sabor;
            Acidez = acidez;
            Cuerpo = cuerpo;
            Balance = balance;
            Postgusto = postgusto;
        }

        public CuppingSessionRatings() { }
    }
} 