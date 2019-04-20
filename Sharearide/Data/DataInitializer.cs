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
                User jarne = new User("Jarne", "Deschacht", "jarne.deschacht@student.hogent.be", "+32492554616", new DateTime(1999, 8, 9), Gender.Male);
                _dbContext.Users_Domain.Add(jarne);
                User ime = new User("Ime", "Van Daele", "imevandaele@gmail.com", "+32484977384", new DateTime(2000, 3, 8), Gender.Female);
                _dbContext.Users_Domain.Add(ime);
                User camiel = new User("Camiel", "Deschacht", "camiel.deschacht@student.hogent.be", "+32492554616", new DateTime(2001, 3, 9), Gender.Transgender);
                _dbContext.Users_Domain.Add(camiel);
                User dobby = new User("Dobby", "Deschacht", "dobby.deschacht@hotmail.com", "+32492554616", new DateTime(1999, 8, 9), Gender.Male);
                _dbContext.Users_Domain.Add(dobby);
                User dobbientje = new User("Dobbientje", "Van Daele", "dobbientjevandaele@gmail.com", "+32484977384", new DateTime(2000, 3, 8), Gender.Female);
                _dbContext.Users_Domain.Add(dobbientje);

                await CreateUser(jarne.Email, "P@ssword1111");
                await CreateUser(ime.Email, "P@ssword1111");
                await CreateUser(camiel.Email, "P@ssword1111");
                await CreateUser(dobby.Email, "P@ssword1111");
                await CreateUser(dobbientje.Email, "P@ssword1111");
                #endregion

                #region Create Cities
                City paris = new City("75000", "Parijs", Country.France);
                City antwerp = new City("2000", "Antwerpen",Country.Belgium);
                City washington = new City("20001", "Washington D.C.",Country.UnitedStates);
                City venice = new City("30100", "Venetië",Country.Italy);
                City bruges = new City("8000", "Brugge",Country.Belgium);
                City adam = new City("1000", "Amsterdam", Country.Netherlands);
                City bekegem = new City("8480", "Bekegem", Country.Belgium);
                City kastrup = new City("2770", "Kopenhagen", Country.Denmark);
                City zedelgem = new City("8210", "Zedelgem", Country.Belgium);
                City kaatsheuvel = new City("5171", "Kaatsheuvel", Country.Netherlands);

                _dbContext.Cities.AddRange(paris, antwerp, washington, venice, bruges,adam,kastrup,zedelgem,kaatsheuvel);

                #endregion

                #region Create Locations
                Location home = new Location("16", "Zilverstraat", bekegem);
                Location stationAdam = new Location("15", "Stationsplein", adam);
                Location wd = new Location("56", "Boulevard des Îles", paris, "Western Digital");
                Location sportp = new Location("119", "Schijnpoortweg", antwerp, "Sportpaleis");
                Location random = new Location("I502", "14th St NW", washington);
                Location stationBruges = new Location("5", "Stationsplein", bruges);
                Location airportCopenhagen = new Location("6", "Lufthavnsboulevarden", kastrup, "Kobenhavens lufthavne");
                Location imehome = new Location("1", "Molenstraat", zedelgem);
                Location efteling = new Location("1", "Europalaan", kaatsheuvel, "Efteling");

                _dbContext.Locations.AddRange(home,wd,sportp,random,stationAdam,stationBruges,airportCopenhagen,imehome,efteling);
                #endregion

                #region Create Rides
                Ride homeToAdam = new Ride(home, stationAdam, new HashSet<Location>() { stationBruges, sportp },
                    DateTime.Today.AddDays(5), 24.87, 3,jarne,new TimeSpan(8,15,00));
                Ride imehomeToSportP = new Ride(imehome, sportp, new HashSet<Location>() { stationBruges},
                    DateTime.Today.AddDays(1),11.50, 4,ime, new TimeSpan(17,30, 00));
                Ride brugesToParis = new Ride(stationBruges, wd, new HashSet<Location>() { },
                    DateTime.Today.AddDays(150), 27.75, 2, jarne, new TimeSpan(11, 45, 00));
                Ride imehomeToCopenHagen = new Ride(imehome, airportCopenhagen, new HashSet<Location>() { stationBruges, sportp, stationAdam },
                    DateTime.Today.AddDays(75), 77.00, 2, ime, new TimeSpan(10, 00, 00));
                Ride imehomeToEfteling = new Ride(imehome,efteling, new HashSet<Location>() { stationBruges, sportp },
                    DateTime.Today.AddDays(15),15.00,6,dobby,new TimeSpan(8,0,0));
                Ride CopenhagenTohome = new Ride(airportCopenhagen, home, new HashSet<Location>() { stationBruges, sportp },
                    DateTime.Today.AddDays(2),85.50, 6, camiel, new TimeSpan(11,55, 0));

                _dbContext.Rides.AddRange(homeToAdam,imehomeToSportP,brugesToParis,imehomeToCopenHagen,imehomeToEfteling,CopenhagenTohome);

                ime.AddRideToUser(homeToAdam);
                camiel.AddRideToUser(homeToAdam);
                dobby.AddRideToUser(homeToAdam);
                camiel.AddRideToUser(imehomeToSportP);
                jarne.AddRideToUser(imehomeToSportP);
                ime.AddRideToUser(brugesToParis);
                
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
