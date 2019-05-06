using Sharearide.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sharearide.Models
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        UserDTO GetByEmail(string email);
        User GetById(int id);
        void Add(User user);
        void Delete(User user);
        UserDTO Update(UserDTO user);
        void SaveChanges();
        void RemoveRideForAllUsers(Ride ride);
    }
}
