using Sharearide.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sharearide.DTOs;

namespace Sharearide.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SharearideContext _context;
        private readonly DbSet<User> _users;

        public UserRepository(SharearideContext dbContext)
        {
            _context = dbContext;
            _users = dbContext.Users_Domain;
        }
        public void Add(User user)
        {
            _users.Add(user);
        }
        public UserDTO Update(UserDTO user)
        {
            var usr = GetById(user.id);
            usr.EditUser(user.FirstName, user.LastName, user.Email, user.PhoneNumber, user.DateOfBirth, user.Gender);
            _context.Update(usr);
            _context.SaveChanges();
            return GetByEmail(usr.Email);
        }
        public void Delete(User user)
        {
            _users.Remove(user);
        }
        public IEnumerable<User> GetAll()
        {
            return _users.OrderBy(u => u.FirstName).ToList();
        }
        public UserDTO GetByEmail(string email)
        {
            var user= _users.SingleOrDefault(u => u.Email.Equals(email));
            UserDTO dto = new UserDTO(user);
            dto.NrOfParticipatedRides = GetById(user.UserId).ParticipatedRides.Count();
            return dto;
        }
        public User GetById(int id)
        {
            return _users.Include(u =>u.UserRides).ThenInclude(ur => ur.Ride).SingleOrDefault(u => u.UserId == id);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
