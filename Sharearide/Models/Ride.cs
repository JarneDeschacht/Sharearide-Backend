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
        private double _passengerContribution;
        private int _totalAvailableSeats;
        private int _availableSeats;
        #endregion

        #region Properties
        public int RideId { get; set; }
        public Location PickUpLocation { get; set; }
        public User Owner { get; set; }
        public Location DropOffLocation { get; set; }
        public IEnumerable<Location> Stopovers => LocationRides.OrderBy(lr => lr.Index).Select(lr => lr.Location).ToList();
        public ICollection<LocationRide> LocationRides { get; private set; }
        public DateTime TravelDate
        {
            get => _travelDate;
            set
            {
                if (value == null /*|| value.Date <= DateTime.Today*/)
                    throw new ArgumentException("TravelDate is required");
                _travelDate = value;
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
        public bool IsSoldOut => AvailableSeats == 0;
        #endregion

        #region Constructors
        public Ride(Location pickup,Location dropOff,ICollection<Location> stopovers
            ,DateTime travelDate,double passengercontribution
            ,int totalAvailableSeats,User owner)
        {
            Owner = owner;
            PickUpLocation = pickup;
            DropOffLocation = dropOff;
            LocationRides = new List<LocationRide>();
            if (stopovers != null)
            {
                int index = 0;
                foreach (var loc in stopovers)
                    LocationRides.Add(new LocationRide(loc, this,index++));
            }
            TravelDate = travelDate;
            PassengerContribution = passengercontribution;
            TotalAvailableSeats = totalAvailableSeats;
            AvailableSeats = totalAvailableSeats;
        }
        public Ride()
        {
            LocationRides = new List<LocationRide>();
        }
        #endregion

        #region Methods
        public Boolean CanUserBeAdded(User user)
        {
            if (user.UserId == this.Owner.UserId) //check if user is not the owner
                return false;

            if (!IsSoldOut)
            {
                if (AvailableSeats > 0)
                {
                    AvailableSeats--;
                    return true;
                }
            }
            return false;
        }
        public void RemoveStopovers()
        {
            List<LocationRide> stops = LocationRides.Where(l => l.RideId == this.RideId).ToList();
            foreach(var stop in stops)
            {
                LocationRides.Remove(stop);
            }
        }
        #endregion

    }
}
