using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sharearide.Models
{
    public class Ride
    {
        #region Fields
        private DateTime _travelDate;
        private DateTime _returnDate;
        private double _passengerContribution;
        private int _totalAvailableSeats;
        #endregion

        #region Properties
        public int RideId { get; set; }
        public Location PickUpLocation { get; set; }
        public Location DropOffLocation { get; set; }
        public ICollection<Location> Stopovers { get; set; }
        public DateTime TravelDate
        {
            get => _travelDate;
            set
            {
                if (value == null || value.Date <= DateTime.Today)
                    throw new ArgumentException("TravelDate must be in the future and the ride must" +
                        " be registered at least one day in advance");
                _travelDate = value;
            }
        }
        public bool IsRoundTrip { get; set; }
        public DateTime ReturnDate
        {
            get => _returnDate;
            set
            {
                if (value.Date <= DateTime.Today || value.Date <= _travelDate.Date)
                    throw new ArgumentException("ReturnDate must be in the future and the return must be later than the travelDate");
                _returnDate = value;
            }
        }
        public double PassengerContribution
        {
            get => _passengerContribution;
            set
            {
                if (value <= 0.0)
                    throw new ArgumentException("PassengerContribution must be strictly greater than zero");
                _passengerContribution = value;
            }
        }
        public int TotalAvailableSeats
        {
            get => _totalAvailableSeats;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("You cannot create a ride without available seats");
                _totalAvailableSeats = value;
            }
        }
        public ICollection<User> People { get; set; }
        public bool IsSoldOut { get; set; }
        #endregion

        #region Constructors
        public Ride(Location pickup,Location dropOff,ICollection<Location> stopovers
            ,DateTime travelDate,bool isRoundTrip, DateTime returnDate,double passengercontribution
            ,int totalAvailableSeats)
        {
            PickUpLocation = pickup;
            DropOffLocation = dropOff;
            Stopovers = stopovers == null ? new HashSet<Location>() : stopovers;
            TravelDate = travelDate;
            IsRoundTrip = isRoundTrip;
            ReturnDate = returnDate;
            PassengerContribution = passengercontribution;
            TotalAvailableSeats = totalAvailableSeats;
            People = new HashSet<User>();
            IsSoldOut = false;
        }
        public Ride()
        {
            Stopovers = new HashSet<Location>();
            People = new HashSet<User>();
        }
        #endregion

        #region Methods
        public void AddUserToRide(User user)
        {
            if (!IsSoldOut)
            {
                if (People.Count < TotalAvailableSeats)
                    People.Add(user);

                if (People.Count == TotalAvailableSeats)
                    IsSoldOut = true;
            }
            else
                throw new ArgumentException("Ride is sold out");
        }
        #endregion

    }
}
