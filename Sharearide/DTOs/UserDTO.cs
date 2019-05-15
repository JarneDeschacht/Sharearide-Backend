using Sharearide.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sharearide.DTOs
{
    public class UserDTO
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [StringLength(15)]
        [Required]
        public string PhoneNumber { get; set; }
        public string Token { get; set; }
        public int NrOfParticipatedRides { get; set; }
        public int NrOfOfferedRides { get; set; }
        public string URL { get; set; }

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
                URL = user.URL;
            }
        }
    }
}
