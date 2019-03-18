using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sharearide.Models
{
    public interface ILocationRepository
    {
        IEnumerable<Location> GetAll();
        IEnumerable<Location> GetByCountry(Country country);
        Location GetById(int id);
        void Add(Location location);
        void Delete(Location location);
        void Update(Location location);
        void SaveChanges();
    }
}
