using Sharearide.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public void Update(User user)
        {
            _context.Update(user);
        }
        public void Delete(User user)
        {
            _users.Remove(user);
        }
        public IEnumerable<User> GetAll()
        {
            return _users.OrderBy(u => u.FirstName).ToList();
        }
        public User GetByEmail(string email)
        {
            return _users.SingleOrDefault(u => u.Email.Equals(email));
        }
        public User GetById(int id)
        {
            return _users.SingleOrDefault(u => u.UserId == id);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
