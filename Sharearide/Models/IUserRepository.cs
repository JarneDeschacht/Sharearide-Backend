using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sharearide.Models
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetByEmail(string email);
        void Add(User user);
        void Delete(User user);
        void Update(User user);
        void SaveChanges();
    }
}
