using Microsoft.AspNetCore.Identity;
using Sharearide.Models;
using System;
using System.Collections.Generic;
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

                #region Create Cities
                City paris = new City("75000", "Paris",Country.France);
                City antwerp = new City("2000", "Antwerp",Country.Belgium);
                City washington = new City("20001", "Washington D.C.",Country.UnitedStates);
                City venice = new City("30100", "Venice",Country.Italy);
                City bruges = new City("8000", "Bruges",Country.Belgium);
                City adam = new City("1000", "Amsterdam", Country.Netherlands);

                _dbContext.Cities.AddRange(paris, antwerp, washington, venice, bruges,adam);

                #endregion

                #region Create Locations
                Location home = new Location("16", "Zilverstraat", bruges);
                Location stationAdam = new Location("15", "Stationsplein", adam);
                Location wd = new Location("56", "Boulevard des Îles", paris,"Western Digital");
                Location sportp = new Location("119", "Schijnpoortweg", antwerp, "Sportpaleis");
                Location random = new Location("I502", "14th St NW", washington);
                Location stationBruges = new Location("5", "Stationsplein", bruges);

                _dbContext.Locations.AddRange(home,wd,sportp,random,stationAdam,stationBruges);
                #endregion

                #region Create Rides
                Ride homeToAdam = new Ride(home, stationAdam, new HashSet<Location>() { stationBruges, sportp },
                    DateTime.Today.AddDays(5), true, DateTime.Today.AddDays(10), 49.75, 3);
                homeToAdam.AddUserToRide(jarne);
                homeToAdam.AddUserToRide(ime);
                Ride homeToSportP = new Ride(home, sportp, new HashSet<Location>() { stationBruges},
                    DateTime.Today.AddDays(1), true, DateTime.Today.AddDays(1), 15.50, 4);

                _dbContext.Rides.AddRange(homeToAdam,homeToSportP);
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
