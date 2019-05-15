
using Sharearide.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sharearide.DTOs
{
    public class RideDTO
    {
        public int RideId { get; set; }
        [Required]
        public Location PickUpLocation { get; set; }
        [Required]
        public Location DropOffLocation { get; set; }
        [Required]
        public IEnumerable<Location> Stopovers { get; set; }
        [Required]
        public DateTime TravelDate { get; set; }
        [Required]
        public double PassengerContribution { get; set; }
        [Required]
        public int TotalAvailableSeats { get; set; }
        [Required]
        public int AvailableSeats { get; set; }
        public bool IsSoldOut { get; set; }
        [Required]
        public UserDTO Owner { get; set; }

        public RideDTO(Ride r)
        {
            if (r != null)
            {
                AvailableSeats = r.AvailableSeats;
                DropOffLocation = r.DropOffLocation;
                IsSoldOut = r.IsSoldOut;
                PassengerContribution = r.PassengerContribution;
                PickUpLocation = r.PickUpLocation;
                RideId = r.RideId;
                Stopovers = r.Stopovers;
                TotalAvailableSeats = r.TotalAvailableSeats;
                TravelDate = r.TravelDate;
                Owner = new UserDTO(r.Owner);
            }
        }
        public RideDTO() { }
    }
}
