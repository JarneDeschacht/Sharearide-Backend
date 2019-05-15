using Sharearide.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sharearide.Models
{
    public interface IRideRepository
    {
        IEnumerable<RideDTO> GetAllParticipated(int id);
        IEnumerable<RideDTO> GetAllOffered(int id);
        IEnumerable<RideDTO> GetAllAvailableRidesForUser(int id);
        RideDTO GetByIdDTO(int id);
        Ride GetById(int id);
        void Add(RideDTO r);
        void Delete(Ride ride);
        void SaveChanges();
    }
}
