using Microsoft.AspNetCore.Identity;
using Sharearide.Models;
using System;
using System.Threading.Tasks;

namespace Sharearide.Data
{
    public class DataInitializer
    {
        private readonly SharearideContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public DataInitializer(SharearideContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                #region Create Users and their account in Identity
                User jarne = new User("Jarne", "Deschacht", "jarne.deschacht@student.hogent.be", "0492554616", new DateTime(1999, 8, 9), Gender.Male);
                _dbContext.Users_Domain.Add(jarne);
                User ime = new User("Ime", "Vandaele", "imevandaele@gmail.com", "0484977384", new DateTime(2000, 3, 8), Gender.Female);
                _dbContext.Users_Domain.Add(ime);
                User camiel = new User("Camiel", "Deschacht", "camiel.deschacht@student.hogent.be", "0492554616", new DateTime(2001, 3, 9), Gender.Transgender);
                _dbContext.Users_Domain.Add(camiel);

                await CreateUser(jarne.Email, "P@ssword1111");
                await CreateUser(ime.Email, "P@ssword1111");
                await CreateUser(camiel.Email, "P@ssword1111");
                #endregion

                #region Create cities
                City paris = new City("75000", "Paris");
                City antwerp = new City("2000", "Antwerp");
                City washington = new City("20001", "Washington D.C.");
                City venice = new City("30100", "Venice");
                City bruges = new City("8000", "Bruges");

                _dbContext.Cities.AddRange(paris, antwerp, washington, venice, bruges);

                #endregion

                #region Create Locations
                Location home = new Location("16", "Zilverstraat", bruges, Country.Belgium);

                _dbContext.Locations.AddRange(home);
                #endregion

                _dbContext.SaveChanges();
            }
        }
        private async Task CreateUser(string email, string password)
        {
            var user = new IdentityUser { UserName = email, Email = email };
            await _userManager.CreateAsync(user, password);
        }
    }
}
