using Microsoft.EntityFrameworkCore;
using Sharearide.DTOs;
using Sharearide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sharearide.Data.Repositories
{
    public class RideRepository : IRideRepository
    {
        private readonly SharearideContext _context;
        private readonly DbSet<Ride> _rides;
        private readonly DbSet<Location> _locations;
        private readonly DbSet<City> _cities;
        private readonly IUserRepository _userRepository;

        public RideRepository(SharearideContext context,IUserRepository userRepository)
        {
            _context = context;
            _rides = context.Rides;
            _locations = context.Locations;
            _cities = context.Cities;
            _userRepository = userRepository;
        }
        public void Add(RideDTO r)
        {
            //solve possible duplicate data and errors
            //locations and cities will not be recreated when they already exist with the same properties
            List<Location> hulp = r.Stopovers.ToList();
            foreach (Location l in r.Stopovers)
            {
                if (CityExists(l.City))
                    _cities.Attach(l.City);
                if (LocationExists(l))
                {
                    int index = hulp.IndexOf(l);
                    hulp[index] = GetLocation(l);
                    _locations.Attach(hulp[index]);
                }
            }

            Ride newR = new Ride(r.PickUpLocation, r.DropOffLocation, hulp, r.TravelDate,
                r.PassengerContribution, r.TotalAvailableSeats,_userRepository.GetById(r.Owner.id));

            //solve possible duplicate data and errors
            //locations and cities will not be recreated when they already exist with the same properties
            if(CityExists(newR.PickUpLocation.City))
                _cities.Attach(newR.PickUpLocation.City);
            if (CityExists(newR.DropOffLocation.City))
                _cities.Attach(newR.DropOffLocation.City);
            if (LocationExists(newR.PickUpLocation))
            {
                newR.PickUpLocation = GetLocation(newR.PickUpLocation);
                _locations.Attach(newR.PickUpLocation);
            }
            if (LocationExists(newR.DropOffLocation))
            {
                newR.DropOffLocation = GetLocation(newR.DropOffLocation);
                _locations.Attach(newR.DropOffLocation);
            }

            _rides.Add(newR);
        }

        public void Delete(Ride ride)
        {
            ride.RemoveStopovers();
            _rides.Remove(ride);
        }

        public IEnumerable<RideDTO> GetAllAvailableRidesForUser(int id)
        {
            var user = _userRepository.GetById(id);
            IList<int> availablerides = _rides.
                Where(r => r.Owner.UserId != id && r.IsSoldOut == false &&
                !user.CheckIfUserIsNotAlreadyJoinedToARide(r) && r.TravelDate.Date > DateTime.Now.Date)
                .Select(r => r.RideId).ToList();
            return ToRideDTO(availablerides);
        }
        public IEnumerable<RideDTO> GetAllOffered(int id)
        {
            var user = _userRepository.GetById(id);
            IList<int> offeredRides = _rides.Where(r => r.Owner.UserId == id).Select(r => r.RideId).ToList();
            return ToRideDTO(offeredRides);
        }
        public IEnumerable<RideDTO> GetAllParticipated(int id)
        {
            var user = _userRepository.GetById(id);
            return ToRideDTO(user.ParticipatedRides.Select(r => r.RideId).ToList());
        }
        public Ride GetById(int id)
        {
            return _rides
                .Include(r => r.Owner)
                .Include(r => r.DropOffLocation).ThenInclude(drop => drop.City)
                .Include(r => r.PickUpLocation).ThenInclude(pick => pick.City)
                .Include(r => r.LocationRides)
                    .ThenInclude(lr => lr.Location)
                    .ThenInclude(l => l.City)
                .SingleOrDefault(r => r.RideId == id);
        }
        public RideDTO GetByIdDTO(int id)
        {
            Ride ride = _rides
                .Include(r => r.Owner)
                .Include(r => r.DropOffLocation).ThenInclude(drop => drop.City)
                .Include(r => r.PickUpLocation).ThenInclude(pick => pick.City)
                .Include(r => r.LocationRides)
                    .ThenInclude(lr => lr.Location)
                    .ThenInclude(l => l.City)
                .SingleOrDefault(r => r.RideId == id);
            return new RideDTO(ride);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        private ICollection<RideDTO> ToRideDTO(IEnumerable<int> rides)
        {
            ICollection<RideDTO> ridesDTO = new List<RideDTO>();
            foreach(int rideId in rides)
            {
                ridesDTO.Add(GetByIdDTO(rideId));
            }
            return ridesDTO.OrderBy(r => r.TravelDate).ThenBy(r => r.RideId).ToList();
        }
        private bool CityExists(City city) => _cities.Contains(city);
        private bool LocationExists(Location location) => GetLocation(location) != null;
        private Location GetLocation(Location location) => _locations.SingleOrDefault(l => l.City.Equals(location.City) &&
        l.CompanyName.Equals(location.CompanyName, StringComparison.OrdinalIgnoreCase) &&
        l.Number.Equals(location.Number, StringComparison.CurrentCultureIgnoreCase) &&
        l.Street.Equals(location.Street, StringComparison.OrdinalIgnoreCase));


    }
}
