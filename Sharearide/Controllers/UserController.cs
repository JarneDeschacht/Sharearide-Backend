using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sharearide.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Sharearide.DTOs;

namespace Sharearide.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IRideRepository _rideRepository;

        public UserController(IUserRepository userRepository,IRideRepository rideRepository)
        {
            _userRepository = userRepository;
            _rideRepository = rideRepository;
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
        /// Get user with given email
        /// </summary>
        /// <param name="email">the email of the user</param>
        /// <returns>a single object of user</returns>
        [HttpGet("{email}")]
        public ActionResult<UserDTO> GetUser(string email)
        {
            var user = _userRepository.GetByEmail(email);
            if (user == null)
                return NotFound();

            return user;
        }

        /// <summary>
        /// Get user with given id
        /// </summary>
        /// <param name="id">the id of the user</param>
        /// <returns>a single object of user</returns>
        [HttpGet("{id}/rides/")]
        public IEnumerable<RideDTO> GetRidesOfUser(int id)
        {
            var user = _userRepository.GetById(id);
            ICollection<RideDTO> rides = new List<RideDTO>();
            foreach (Ride r in user.ParticipatedRides)
            {
                rides.Add(_rideRepository.GetById(r.RideId));
            }
            return rides;
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
        public ActionResult<UserDTO> PutUser(int id, UserDTO user)
        {
            if (id != user.id)
                return BadRequest();

            var usr = _userRepository.Update(user);
            _userRepository.SaveChanges();
            return usr;
        }
        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="id">the id of the user to be deleted</param>
        [HttpDelete("{id}")]
        public ActionResult<User> DeleteUser(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
                return NotFound();

            _userRepository.Delete(user);
            _userRepository.SaveChanges();
            return user;
        }

    }
}