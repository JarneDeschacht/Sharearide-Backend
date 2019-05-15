using Sharearide.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sharearide.Models
{
    public interface IUserRepository
    {
        UserDTO GetByEmail(string email);
        User GetById(int id);
        void Add(User user);
        UserDTO Update(UserDTO user);
        void SaveChanges();
        void RemoveRideForAllUsers(Ride ride);
    }
}
