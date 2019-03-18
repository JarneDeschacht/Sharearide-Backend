using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sharearide.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Sharearide.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Get all users ordered by firstname
        /// </summary>
        /// <returns>array of users</returns>
        [HttpGet]
        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        /// <summary>
        /// Get user with given id
        /// </summary>
        /// <param name="id">the id of the user</param>
        /// <returns>a single object of user</returns>
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
                return NotFound();
            return user;
        }

        /// <summary>
        /// Adds a new user to the database
        /// </summary>
        /// <param name="user">the new user</param>
        [HttpPost]
        public ActionResult<User> PostUser(User user)
        {
            if (user == null)
                return NotFound();

            _userRepository.Add(user);
            _userRepository.SaveChanges();
            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
        }
        /// <summary>
        /// Modifies a user
        /// </summary>
        /// <param name="id">id of the user to be modified</param>
        /// <param name="user">the modified user</param>
        [HttpPut("{id}")]
        public IActionResult PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }
            _userRepository.Update(user);
            _userRepository.SaveChanges();
            return NoContent();
        }
        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="id">the id of the user to be deleted</param>
        [HttpDelete("{id}")]
        public ActionResult<User> DeleteUser(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null) { return NotFound(); }
            _userRepository.Delete(user);
            _userRepository.SaveChanges();
            return user;
        }

    }
}