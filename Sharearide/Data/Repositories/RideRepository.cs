using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<Ride> GetAll()
        {
            return _rides
                .Include(r => r.DropOffLocation).ThenInclude(drop => drop.City)
                .Include(r => r.PickUpLocation).ThenInclude(pick => pick.City)
                .ToList();
        }

        public Ride GetById(int id)
        {
            return _rides
                .Include(r => r.LocationRides)
                .ThenInclude(lr => lr.Location)
                .ThenInclude(l => l.City)
                //.Include(r => r.UserRides)
                //.ThenInclude(ur => ur.User)
                .SingleOrDefault(r => r.RideId == id);
        }

        public IEnumerable<Ride> GetByUser(User user)
        {
            //TODO
            return _rides;
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
