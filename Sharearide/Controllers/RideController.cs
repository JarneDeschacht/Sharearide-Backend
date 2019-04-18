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
    public class RideController : ControllerBase
    {
        private readonly IRideRepository _rideRepository;

        public RideController(IRideRepository rideRepository)
        {
            _rideRepository = rideRepository;
        }

        [HttpGet]
        public IEnumerable<RideDTO> GetAllRides()
        {
            return _rideRepository.GetAll();
        }
    }
}