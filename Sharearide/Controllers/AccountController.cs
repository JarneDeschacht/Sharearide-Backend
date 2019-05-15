using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Sharearide.DTOs;
using Sharearide.Models;
using System.Linq;

namespace Sharearide.Controllers
{
    /// <summary>
    /// controller to control the accounts 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IRideRepository _rideRepository;
        private readonly IConfiguration _config;

        /// <summary>
        /// constructor to create the AccountController and initializes the repo
        /// </summary>
        /// <param name="signInManager"></param>
        /// <param name="userManager"></param>
        /// <param name="userRepository"></param>
        /// <param name="config"></param>
        /// <param name="rideRepository"></param>
        public AccountController(
          SignInManager<IdentityUser> signInManager,
          UserManager<IdentityUser> userManager,
          IUserRepository userRepository,
          IConfiguration config,
          IRideRepository rideRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userRepository = userRepository;
            _config = config;
            _rideRepository = rideRepository;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="model">the login details</param>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<UserDTO>> CreateToken(LoginDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            UserDTO userIn = _userRepository.GetByEmail(model.Email);
            userIn.NrOfOfferedRides = _rideRepository.GetAllOffered(userIn.id).Count();

            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                if (result.Succeeded)
                {
                    userIn.Token = GetToken(user,userIn.FirstName);

                    return Ok(userIn); //returns the user                 
                }
            }
            return BadRequest();
        }


        /// <summary>
        /// Register a user
        /// </summary>
        /// <param name="model">the user details</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO model)
        {
            IdentityUser user = new IdentityUser { UserName = model.Email, Email = model.Email };
            User usr = new User { Email = model.Email, FirstName = model.FirstName, LastName = model.LastName, DateOfBirth = model.DateOfBirth, Gender = model.Gender, PhoneNumber = model.PhoneNumber };
            var result = await _userManager.CreateAsync(user, model.Password);


            if (result.Succeeded)
            {
                _userRepository.Add(usr);
                _userRepository.SaveChanges();
                usr.Token = GetToken(user,model.FirstName);
                return Ok(_userRepository.GetByEmail(usr.Email));
            }
            return BadRequest();
        }

        private String GetToken(IdentityUser user,string name)
        {
            // Create the token
            var claims = new[]
            {
              new Claim(JwtRegisteredClaimNames.Sub,name), //name
              new Claim(JwtRegisteredClaimNames.UniqueName,name) //name
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              null, null,
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        /// <summary>
        /// chacks if an email is already used
        /// </summary>
        /// <param name="email"></param>
        /// <returns>true or false depending if the email is already used</returns>
        [AllowAnonymous]
        [HttpGet("checkusername")]
        public async Task<ActionResult<Boolean>> CheckAvailableUserName(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            return user == null;
        }

        /// <summary>
        /// Changes the password of a user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="oldPassw"></param>
        /// <param name="newPassw"></param>
        /// <returns>true or false depending if the operation succeeded</returns>
        [HttpPut("ChangePassword/{email}/{oldPassw}/{newPassw}")]
        public async Task<ActionResult<Boolean>> ChangePassword(string email, string oldPassw, string newPassw)
        {
            var identityUser = await _userManager.FindByNameAsync(email);
            if (identityUser == null)
                return NotFound();

            var result = await _userManager.ChangePasswordAsync(identityUser, oldPassw, newPassw);
            return result.Succeeded;
        }
    }
}
