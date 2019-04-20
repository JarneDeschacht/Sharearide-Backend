using Sharearide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sharearide.DTOs
{
    public class UserDTO
    {
        public int id { get; set; }
        //public IEnumerable<RideDTO> OfferedRides { get; set; } COMING SOON
        //public IEnumerable<RideDTO> ParticipatedRides { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Token { get; set; }
        public int NrOfParticipatedRides { get; set; }
        public int NrOfOfferedRides { get; set; }

        public UserDTO(User user)
        {
            if (user != null)
            {
                id = user.UserId;
                FirstName = user.FirstName;
                LastName = user.LastName;
                Email = user.Email;
                Gender = user.Gender;
                DateOfBirth = user.DateOfBirth;
                PhoneNumber = user.PhoneNumber;
                Token = user.Token;
            }
        }
    }
}
