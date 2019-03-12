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
        private readonly sharearideContext _context;
        private readonly DbSet<User> _users;

        public UserRepository(sharearideContext dbContext)
        {
            _context = dbContext;
            _users = dbContext.Users;
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

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
