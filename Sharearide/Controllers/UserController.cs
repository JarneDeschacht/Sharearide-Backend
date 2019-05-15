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
    /// <summary>
    /// controller to control the user
    /// </summary>
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IRideRepository _rideRepository;

        /// <summary>
        /// constructor to create controller and initialize the repo's
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="rideRepository"></param>
        public UserController(IUserRepository userRepository,IRideRepository rideRepository)
        {
            _userRepository = userRepository;
            _rideRepository = rideRepository;
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
            user.NrOfOfferedRides = _rideRepository.GetAllOffered(user.id).Count();
            return user;
        }

        /// <summary>
        /// Get all rides that the given user in participated
        /// </summary>
        /// <param name="id">the id of the user</param>
        /// <returns>List of all participad rides of the given user</returns>
        [HttpGet("{id}/participatedrides/")]
        public IEnumerable<RideDTO> GetParticipatedRidesOfUser(int id)
        {
            return _rideRepository.GetAllParticipated(id).OrderBy(r => r.TravelDate);
        }
        /// <summary>
        /// Get all rides that the given user offered
        /// </summary>
        /// <param name="id">the id of the user</param>
        /// <returns>List of all offered rides of the given user</returns>
        [HttpGet("{id}/offeredrides/")]
        public IEnumerable<RideDTO> GetOfferedRidesOfUser(int id)
        {
            return _rideRepository.GetAllOffered(id).OrderBy(r => r.TravelDate);
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
            return CreatedAtAction(nameof(GetUser), new { id = user.Email }, user);
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
            usr.NrOfOfferedRides = _rideRepository.GetAllOffered(user.id).Count();
            return usr;
        }
    }
}