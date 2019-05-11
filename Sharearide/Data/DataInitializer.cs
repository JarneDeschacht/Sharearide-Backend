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
            //_dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                #region Create Users and their account in Identity
                User jarne = new User("Jarne", "Deschacht", "jarne.deschacht@student.hogent.be", "+32492554616", new DateTime(1999, 8, 9), Gender.Male);
                _dbContext.Users_Domain.Add(jarne);
                User ime = new User("Ime", "Van Daele", "imevandaele@gmail.com", "+32484977384", new DateTime(2000, 3, 8), Gender.Female);
                _dbContext.Users_Domain.Add(ime);
                User geert = new User("Geert", "Deschacht", "geert.deschacht1@telenet.be", "+32492554616", new DateTime(1969,7,12), Gender.Male);
                _dbContext.Users_Domain.Add(geert);
                User natacha = new User("Natacha", "Vantyghem", "natacha.vantyghem@telenet.be", "+32492554616", new DateTime(1969, 7, 12), Gender.Female);
                _dbContext.Users_Domain.Add(natacha);
                User jelle = new User("Jelle", "Deschacht", "jalle.deschacht@gmail.com", "+32484977384", new DateTime(1997,7,21), Gender.Male);
                _dbContext.Users_Domain.Add(jelle);
                User camiel = new User("Camiel", "Deschacht", "camiel.deschacht@student.hogent.be", "+32492554616", new DateTime(1999, 8, 9), Gender.Male);
                _dbContext.Users_Domain.Add(camiel);
                User ruben = new User("Ruben", "Vanhee", "rubenvanhee@gmail.com", "+32484977384", new DateTime(2000, 3, 8), Gender.Male);
                _dbContext.Users_Domain.Add(ruben);
                User timon = new User("Timon", "Batsleer", "timon.batsleer@hotmail.com", "+32492554616", new DateTime(1969, 7, 12), Gender.Male);
                _dbContext.Users_Domain.Add(timon);
                User milan = new User("Milan", "Denolf", "milan.denolf@gmail.com", "+32492554616", new DateTime(1969, 7, 12), Gender.Male);
                _dbContext.Users_Domain.Add(milan);
                User robbe = new User("Robbe", "Vanrobaeys", "robbe.vanrobaeys@hotmail.com", "+32484977384", new DateTime(1997, 7, 21), Gender.Male);
                _dbContext.Users_Domain.Add(robbe);

                await CreateUser(jarne.Email, "P@ssword1111");
                await CreateUser(ime.Email, "P@ssword1111");
                await CreateUser(camiel.Email, "P@ssword1111");
                await CreateUser(geert.Email, "P@ssword1111");
                await CreateUser(natacha.Email, "P@ssword1111");
                await CreateUser(jelle.Email, "P@ssword1111");
                await CreateUser(ruben.Email, "P@ssword1111");
                await CreateUser(timon.Email, "P@ssword1111");
                await CreateUser(milan.Email, "P@ssword1111");
                await CreateUser(robbe.Email, "P@ssword1111");
                #endregion

                #region Create Cities
                City paris = new City("75000", "Parijs", Country.France);
                City antwerp = new City("2000", "Antwerpen",Country.Belgium);
                City venice = new City("30100", "Venetië",Country.Italy);
                City bruges = new City("8000", "Brugge",Country.Belgium);
                City adam = new City("1000", "Amsterdam", Country.Netherlands);
                City bekegem = new City("8480", "Bekegem", Country.Belgium);
                City kastrup = new City("2770", "Kopenhagen", Country.Denmark);
                City zedelgem = new City("8210", "Zedelgem", Country.Belgium);
                City kaatsheuvel = new City("5171", "Kaatsheuvel", Country.Netherlands);
                City brussels = new City("1020", "Brussel", Country.Belgium);

                _dbContext.Cities.AddRange(paris, antwerp, venice, bruges,adam,kastrup,zedelgem,kaatsheuvel,brussels);

                #endregion

                #region Create Locations
                Location home = new Location("16", "Zilverstraat", bekegem);
                Location stationAdam = new Location("15", "Stationsplein", adam);
                Location wd = new Location("56", "Boulevard des Îles", paris, "Western Digital");
                Location sportp = new Location("119", "Schijnpoortweg", antwerp, "Sportpaleis");
                Location random = new Location("I502", "14th St NW",brussels);
                Location stationBruges = new Location("5", "Stationsplein", bruges);
                Location airportCopenhagen = new Location("6", "Lufthavnsboulevarden", kastrup, "Kobenhavens lufthavne");
                Location imehome = new Location("1", "Molenstraat", zedelgem);
                Location efteling = new Location("1", "Europalaan", kaatsheuvel, "Efteling");
                Location atomium = new Location("1", "Atomiumsquare", brussels,"Atomium");

                _dbContext.Locations.AddRange(home,wd,sportp,random,stationAdam,stationBruges,airportCopenhagen,imehome,efteling,atomium);
                #endregion

                #region Create Rides
                Ride homeToAdam = new Ride(home, stationAdam, new HashSet<Location>() { stationBruges, sportp },
                    DateTime.Today.AddDays(5), 24.87, 3,jarne);
                Ride imehomeToSportP = new Ride(imehome, sportp, new HashSet<Location>() { stationBruges},
                    DateTime.Today.AddDays(1),11.50, 4,ime);
                Ride brugesToParis = new Ride(stationBruges, wd, new HashSet<Location>() { },
                    DateTime.Today.AddDays(150), 27.75, 2, jarne);
                Ride imehomeToCopenHagen = new Ride(imehome, airportCopenhagen, new HashSet<Location>() { stationBruges, sportp, stationAdam },
                    DateTime.Today.AddDays(75), 77.00, 2, ime);
                Ride imehomeToEfteling = new Ride(imehome,efteling, new HashSet<Location>() { stationBruges, sportp },
                    DateTime.Today.AddDays(15),15.00,6,jelle);
                Ride CopenhagenTohome = new Ride(airportCopenhagen, home, new HashSet<Location>() { stationBruges, sportp },
                    DateTime.Today.AddDays(2),85.50, 6, camiel);

                Ride homeToAtomium = new Ride(home, atomium, new HashSet<Location>() { stationBruges },
                    DateTime.Today.AddDays(3), 24.87, 3, jelle);
                Ride brugesToBrussels = new Ride(stationBruges, sportp, new HashSet<Location>() {},
                    DateTime.Today.AddDays(4), 11.50, 4, robbe);
                Ride parisTohomeime = new Ride(wd,imehome, new HashSet<Location>() {sportp,stationBruges},
                    DateTime.Today.AddDays(56), 27.75, 2, geert);
                Ride randomToantwerp = new Ride(random,sportp, new HashSet<Location>() {},
                    DateTime.Today.AddDays(89), 77.00, 2, timon);
                Ride brusselsAdam = new Ride(random,stationAdam, new HashSet<Location>() {},
                    DateTime.Today.AddDays(343), 15.00, 6, ruben);
                Ride ParisToCopenhagen = new Ride(wd,airportCopenhagen, new HashSet<Location>() { },
                    DateTime.Today.AddDays(23), 85.50, 6, geert);


                _dbContext.Rides.AddRange(homeToAdam,imehomeToSportP,brugesToParis,imehomeToCopenHagen,imehomeToEfteling,CopenhagenTohome,
                    homeToAtomium,brugesToBrussels,parisTohomeime,randomToantwerp,ParisToCopenhagen,brusselsAdam);

                ime.AddRideToUser(homeToAdam);
                camiel.AddRideToUser(homeToAdam);
                jelle.AddRideToUser(homeToAdam);
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
