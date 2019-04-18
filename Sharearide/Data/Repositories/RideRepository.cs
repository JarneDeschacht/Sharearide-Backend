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

        public RideRepository(SharearideContext context)
        {
            _context = context;
            _rides = context.Rides;
        }

        public void Add(Ride ride)
        {
            _rides.Add(ride);
        }

        public void Delete(Ride ride)
        {
            _rides.Remove(ride);
        }

        public IEnumerable<RideDTO> GetAll()
        {
            var rides = _rides
                .Include(r => r.DropOffLocation).ThenInclude(drop => drop.City)
                .Include(r => r.PickUpLocation).ThenInclude(pick => pick.City)
                .Include(r => r.LocationRides)
                .ThenInclude(lr => lr.Location)
                .ThenInclude(l => l.City)
                .ToList();

            ICollection<RideDTO> ridesDTO = new List<RideDTO>();
            foreach (Ride r in rides)
            {
                ridesDTO.Add(new RideDTO(r));
            }
            return ridesDTO;
        }

        public RideDTO GetById(int id)
        {
            Ride ride = _rides
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

        public void Update(Ride ride)
        {
            _context.Update(ride);
        }
    }
}
