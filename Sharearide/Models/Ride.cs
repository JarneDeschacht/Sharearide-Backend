﻿using System;
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
        private int _availableSeats;
        #endregion

        #region Properties
        public int RideId { get; set; }
        public Location PickUpLocation { get; set; }
        public Location DropOffLocation { get; set; }
        public IEnumerable<Location> Stopovers => LocationRides.Select(lr => lr.Location).ToList();
        public ICollection<LocationRide> LocationRides { get; private set; }
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
                if (IsRoundTrip && (value.Date <= DateTime.Today || value.Date < _travelDate.Date))
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
        public int AvailableSeats
        {
            get => _availableSeats;
            set
            {
                if (value < 0 || value > TotalAvailableSeats)
                    throw new ArgumentException("Available seatsa cannot be les than zero and caanot exceed Total available seats");
                _availableSeats = value;
            }
        }
        public bool IsSoldOut { get; set; }
        #endregion

        #region Constructors
        public Ride(Location pickup,Location dropOff,ICollection<Location> stopovers
            ,DateTime travelDate,bool isRoundTrip, DateTime returnDate,double passengercontribution
            ,int totalAvailableSeats)
        {
            PickUpLocation = pickup;
            DropOffLocation = dropOff;
            LocationRides = new List<LocationRide>();
            if (stopovers != null)
            {
                foreach (var loc in stopovers)
                    LocationRides.Add(new LocationRide(loc, this));
            }
            TravelDate = travelDate;
            IsRoundTrip = isRoundTrip;
            ReturnDate = returnDate;
            PassengerContribution = passengercontribution;
            TotalAvailableSeats = totalAvailableSeats;
            AvailableSeats = totalAvailableSeats;
            IsSoldOut = false;
        }
        public Ride()
        {
            LocationRides = new List<LocationRide>();
        }
        #endregion

        #region Methods
        public Boolean CanUserBeAdded(User user)
        {
            if (!IsSoldOut)
            {
                if (AvailableSeats > 0)
                {
                    AvailableSeats--;
                    return true;
                }   

                if (AvailableSeats == 0)
                    IsSoldOut = true;
            }
            return false;
        }
        #endregion

    }
}
