
using Sharearide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sharearide.DTOs
{
    public class RideDTO
    {
        public int RideId { get; set; }
        public Location PickUpLocation { get; set; }
        public Location DropOffLocation { get; set; }
        public IEnumerable<Location> Stopovers { get; set; }
        public DateTime TravelDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public double PassengerContribution { get; set; }
        public int TotalAvailableSeats { get; set; }
        public int AvailableSeats { get; set; }
        public bool IsSoldOut { get; set; }
        public bool IsRoundTrip { get; set; }

        public RideDTO(Ride r)
        {
            AvailableSeats = r.AvailableSeats;
            DropOffLocation = r.DropOffLocation;
            IsSoldOut = r.IsSoldOut;
            PassengerContribution = r.PassengerContribution;
            PickUpLocation = r.PickUpLocation;
            ReturnDate = r.ReturnDate;
            RideId = r.RideId;
            Stopovers = r.Stopovers;
            TotalAvailableSeats = r.TotalAvailableSeats;
            TravelDate = r.TravelDate;
            IsRoundTrip = r.IsRoundTrip;
        }
    }
}
