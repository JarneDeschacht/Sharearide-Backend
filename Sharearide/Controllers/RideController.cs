using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sharearide.Models;

namespace Sharearide.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [AllowAnonymous] //temp
    public class RideController : ControllerBase
    {
        private readonly IRideRepository _rideRepository;

        public RideController(IRideRepository rideRepository)
        {
            _rideRepository = rideRepository;
        }

        [HttpGet]
        public IEnumerable<Ride> GetAllRides()
        {
            var rides = _rideRepository.GetAll();
            return rides;
        }
        [HttpGet("{id}/stopovers/")]
        public IEnumerable<Location> GetStopoversOfRide(int id)
        {
            return _rideRepository.GetById(id).Stopovers;
        }
        //[HttpGet("{id}/people/")]
        //public IEnumerable<User> GetPeopleOfRide(int id)
        //{
        //    return _rideRepository.GetById(id).People;
        //}
    }
}