using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sharearide.DTOs;
using Sharearide.Models;

namespace Sharearide.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    //[Authorize] TODO
    public class RideController : ControllerBase
    {
        private readonly IRideRepository _rideRepository;
        private readonly IUserRepository _userRepository;

        public RideController(IRideRepository rideRepository,IUserRepository userRepository)
        {
            _rideRepository = rideRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Get all rides
        /// </summary>
        /// <returns>List of all rides</returns>
        [HttpGet]
        public IEnumerable<RideDTO> GetAll()
        {
            return _rideRepository.GetAll();
        }
        /// <summary>
        /// Get all rides that are available for a user to join
        /// </summary>
        /// <param name="id">the id of the user</param>
        /// <returns>List of all rides that are availble for a user to join</returns>
        [HttpGet("user/{id}")]
        public IEnumerable<RideDTO> GetAllAvailableRidesForUser(int id)
        {
            return _rideRepository.GetAllAvailableRidesForUser(id);
        }
        /// <summary>
        /// Returns ride with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ride with given id</returns>
        [HttpGet("{id}")]
        public RideDTO GetById(int id)
        {
            return _rideRepository.GetByIdDTO(id);
        }
        /// <summary>
        /// Adds a user to a ride
        /// </summary>
        /// <param name="rideid"></param>
        /// <param name="userid"></param>
        [HttpPost("{rideid}/adduser/{userid}")]
        public ActionResult AddUserToRide(int rideid, int userid)
        {
            var ride = _rideRepository.GetById(rideid);
            var user = _userRepository.GetById(userid);
            user.AddRideToUser(ride);
            _userRepository.SaveChanges();
            return NoContent();
        }
        /// <summary>
        /// Adds a ride
        /// </summary>
        /// <param name="ride"></param>
        /// <returns>the created ride</returns>
        [HttpPost]
        public ActionResult<RideDTO> PostRide(RideDTO ride)
        {
            if (ride == null)
                return NotFound();

            _rideRepository.Add(ride);
            _rideRepository.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = ride.RideId },ride);
        }
    }
}