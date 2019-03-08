using CarpoolApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarpoolApplication.Data
{
    public class CarpoolDataInitializer
    {
        private readonly CarpoolContext _dbContext;

        public CarpoolDataInitializer(CarpoolContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                User jarne = new User("Jarne", "Deschacht", "jarne.deschacht@student.hogent.be", "0492554616", new DateTime(1999, 8, 9), Gender.Male);
                _dbContext.Users.Add(jarne);
                User ime = new User("Ime", "Vandaele", "imevandaele@gmail.com", "0484977384", new DateTime(2000, 3, 8), Gender.Female);
                _dbContext.Users.Add(ime);
                User camiel = new User("Camiel", "Deschacht", "camiel.deschacht@student.hogent.be", "0492554616", new DateTime(2001, 3, 9), Gender.Transgender);
                _dbContext.Users.Add(camiel);

                _dbContext.SaveChanges();
            }
        }
    }
}
