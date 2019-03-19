using Microsoft.EntityFrameworkCore;
using Sharearide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sharearide.Data.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly SharearideContext _context;
        private readonly DbSet<Location> _locations;

        public LocationRepository(SharearideContext context)
        {
            _context = context;
            _locations = context.Locations;
        }

        public void Add(Location location)
        {
            _locations.Add(location);
        }

        public void Delete(Location location)
        {
            _locations.Remove(location);
        }
        public void Update(Location location)
        {
            _context.Update(location);
        }
        public IEnumerable<Location> GetAll()
        {
            return _locations
                .Include(l => l.City)
                .AsNoTracking()
                .OrderBy(l => l.City.Country)
                .ThenBy(l => l.City.Name)
                .ToList();
        }

        public IEnumerable<Location> GetByCountry(Country country)
        {
            return _locations
                .Include(l => l.City)
                .Where(l => l.City.Country == country)
                .AsNoTracking()
                .OrderBy(l => l.City.Country)
                .ThenBy(l => l.City.Name)
                .ToList();
        }

        public Location GetById(int id)
        {
            return _locations.Include(l => l.City).SingleOrDefault(l => l.LocationId == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        } 
    }
}
