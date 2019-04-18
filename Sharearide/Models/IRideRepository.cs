using Sharearide.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sharearide.Models
{
    public interface IRideRepository
    {
        IEnumerable<RideDTO> GetAll();
        RideDTO GetById(int id);
        void Add(Ride ride);
        void Delete(Ride ride);
        void Update(Ride ride);
        void SaveChanges();
    }
}
