using Sharearide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Sharearide.Tests.Models
{
    public class RideTest
    {
        #region TestData
        private const int _rideId = 1;
        private readonly Location _pickupLocation = 
            new Location("16","Zilverstraat",new City("8480","Ichtegem",Country.Belgium));
        private readonly Location _dropOffLocation = 
            new Location("1", "Molenstraat", new City("8210", "Zedelgem", Country.Belgium),"I.M.E");
        private readonly ICollection<Location> _stopovers = new HashSet<Location>()
        {
            new Location("88", "Eernegemweg", new City("8490", "Jabbeke",Country.Belgium)),
        };
        private readonly DateTime _travelDate = DateTime.Today.AddDays(5);
        private readonly DateTime _returnDate = DateTime.Today.AddDays(10);
        private const double _passengerContribution = 3.50;
        private const int _totalAvailableSeats = 3;
        private readonly User _jarne = new User("Jarne", "Deschacht", "jarne.deschacht@student.hogent.be",
            "0492554616", new DateTime(1999, 8, 9), Gender.Male);
        private readonly User _ime = new User("Ime", "Vandaele", "imevandaele@gmail.com",
            "0484977384",new DateTime(2000, 3, 8), Gender.Female);
        private readonly User _camiel = new User("Camiel", "Deschacht", "camiel.deschacht@student.hogent.be",
            "0492554616", new DateTime(2001, 3, 9), Gender.Transgender);
        #endregion

        #region Constructors
        [Fact]
        public void NewRide_ValidDataWithRoundTrip_CreatesRide()
        {
            var ride = new Ride(_pickupLocation,_dropOffLocation,_stopovers,_travelDate,true,
                _returnDate,_passengerContribution,_totalAvailableSeats) { RideId = _rideId};
            Assert.Equal(_pickupLocation, ride.PickUpLocation);
            Assert.Equal(_dropOffLocation, ride.DropOffLocation);
            Assert.True(ride.IsRoundTrip);
            Assert.False(ride.IsSoldOut);
            Assert.Equal(_travelDate, ride.TravelDate);
            Assert.Equal(_returnDate, ride.ReturnDate);
            Assert.Equal(1, ride.LocationRides.Count);
            Assert.Empty(ride.People);
            Assert.Equal(_rideId, ride.RideId);
            Assert.Equal(_passengerContribution, ride.PassengerContribution);
            Assert.Equal(_totalAvailableSeats, ride.TotalAvailableSeats);
        }
        [Fact]
        public void NewRide_ValidDataWithoutRoundTrip_CreatesRide()
        {
            var ride = new Ride(_pickupLocation, _dropOffLocation, _stopovers, _travelDate, false,
               DateTime.MinValue, _passengerContribution, _totalAvailableSeats){ RideId = _rideId };
            Assert.Equal(_pickupLocation, ride.PickUpLocation);
            Assert.Equal(_dropOffLocation, ride.DropOffLocation);
            Assert.False(ride.IsRoundTrip);
            Assert.False(ride.IsSoldOut);
            Assert.Equal(_travelDate, ride.TravelDate);
            Assert.Equal(DateTime.MinValue,ride.ReturnDate);
            Assert.Equal(1, ride.LocationRides.Count);
            Assert.Empty(ride.People);
            Assert.Equal(_rideId, ride.RideId);
            Assert.Equal(_passengerContribution, ride.PassengerContribution);
            Assert.Equal(_totalAvailableSeats, ride.TotalAvailableSeats);
        }
        [Fact]
        public void NewRide_ValidDataWithoutStopovers_CreatesRide()
        {
            var ride = new Ride(_pickupLocation, _dropOffLocation, null, _travelDate, false,
               DateTime.MinValue, _passengerContribution, _totalAvailableSeats)
            { RideId = _rideId };
            Assert.Equal(_pickupLocation, ride.PickUpLocation);
            Assert.Equal(_dropOffLocation, ride.DropOffLocation);
            Assert.False(ride.IsRoundTrip);
            Assert.False(ride.IsSoldOut);
            Assert.Equal(_travelDate, ride.TravelDate);
            Assert.Equal(DateTime.MinValue, ride.ReturnDate);
            Assert.Empty(ride.Stopovers);
            Assert.Empty(ride.People);
            Assert.Equal(_rideId, ride.RideId);
            Assert.Equal(_passengerContribution, ride.PassengerContribution);
            Assert.Equal(_totalAvailableSeats, ride.TotalAvailableSeats);
        }
        [Fact]
        public void NewRide_ValidDataUsingDefaultCTOR_CreatesRide()
        {
            var ride = new Ride()
            {
                RideId = _rideId,
                DropOffLocation = _dropOffLocation,
                IsRoundTrip = true,
                PassengerContribution = _passengerContribution,
                PickUpLocation = _pickupLocation,
                ReturnDate = _returnDate,
                TotalAvailableSeats = _totalAvailableSeats,
                TravelDate = _travelDate
            };
            Assert.Equal(_pickupLocation, ride.PickUpLocation);
            Assert.Equal(_dropOffLocation, ride.DropOffLocation);
            Assert.True(ride.IsRoundTrip);
            Assert.False(ride.IsSoldOut);
            Assert.Equal(_travelDate, ride.TravelDate);
            Assert.Equal(_returnDate, ride.ReturnDate);
            Assert.Empty(ride.LocationRides);
            Assert.Empty(ride.People);
            Assert.Equal(_rideId, ride.RideId);
            Assert.Equal(_passengerContribution, ride.PassengerContribution);
            Assert.Equal(_totalAvailableSeats, ride.TotalAvailableSeats);
        }
        [Fact]
        public void NewRide_TravelDateInPast_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Ride(_pickupLocation, _dropOffLocation, _stopovers,new DateTime(1999,8,9), true,
                _returnDate, _passengerContribution, _totalAvailableSeats)
            { RideId = _rideId });
        }
        [Fact]
        public void NewRide_TravelDateToday_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Ride(_pickupLocation, _dropOffLocation, _stopovers, DateTime.Today, true,
                _returnDate, _passengerContribution, _totalAvailableSeats)
            { RideId = _rideId });
        }
        [Fact]
        public void NewRide_ReturnDateInPast_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Ride(_pickupLocation, _dropOffLocation, _stopovers, _travelDate, true,
                new DateTime(1999,8,9), _passengerContribution, _totalAvailableSeats)
            { RideId = _rideId });
        }
        [Fact]
        public void NewRide_ReturnDateToday_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Ride(_pickupLocation, _dropOffLocation, _stopovers, _travelDate, true,
                DateTime.Today, _passengerContribution, _totalAvailableSeats)
            { RideId = _rideId });
        }
        [Fact]
        public void NewRide_ReturnDateBeforeTravelDate_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Ride(_pickupLocation, _dropOffLocation, _stopovers, _travelDate, true,
                new DateTime(_travelDate.Year -5,8,9), _passengerContribution, _totalAvailableSeats)
            { RideId = _rideId });
        }
        [Theory]
        [InlineData(-5)]
        [InlineData(0.0)]
        [InlineData(-0)]
        [InlineData(-1.439424894801843)]
        [InlineData(-9999999999999999)]
        public void NewRide_InvalidPassengerContribution_ThrowsArgumentException(double contr)
        {
            Assert.Throws<ArgumentException>(() => new Ride(_pickupLocation, _dropOffLocation, _stopovers, _travelDate, true,
                _returnDate, contr, _totalAvailableSeats)
            { RideId = _rideId });
        }
        [Theory]
        [InlineData(-5)]
        [InlineData(0)]
        public void NewRide_InvalidTotalAvailableSeats_ThrowsArgumentException(int seats)
        {
            Assert.Throws<ArgumentException>(() => new Ride(_pickupLocation, _dropOffLocation, _stopovers, _travelDate, true,
                _returnDate, _passengerContribution, seats)
            { RideId = _rideId });
        }
        #endregion

        #region Methods
        [Fact]
        public void AddUserToRide_ValidUser_AddsUserToRide()
        {
            var ride = new Ride(_pickupLocation, _dropOffLocation, _stopovers, _travelDate, true,
                _returnDate, _passengerContribution, _totalAvailableSeats){ RideId = _rideId };
            Assert.Empty(ride.People);
            ride.AddUserToRide(_jarne);
            Assert.Equal(1, ride.UserRides.Count);
        }
        [Fact]
        public void AddUserToRide_MultipleValidUsers_AddsUsersToRide()
        {
            var ride = new Ride(_pickupLocation, _dropOffLocation, _stopovers, _travelDate, true,
                _returnDate, _passengerContribution, _totalAvailableSeats)
            { RideId = _rideId };
            Assert.Empty(ride.People);
            ride.AddUserToRide(_jarne);
            ride.AddUserToRide(_ime);
            ride.AddUserToRide(_camiel);
            Assert.Equal(3, ride.UserRides.Count);
        }
        [Fact]
        public void AddUserToRide_UnvalidUser_ThrowsArgumentException()
        {
            var ride = new Ride(_pickupLocation, _dropOffLocation, _stopovers, _travelDate, true,
                _returnDate, _passengerContribution, _totalAvailableSeats)
            { RideId = _rideId };
            Assert.Empty(ride.People);
            Assert.Throws<ArgumentException>(() => ride.AddUserToRide(null));
        }
        [Fact]
        public void AddUserToRide_IsSoldOut_ThrowsArgumentException()
        {
            var ride = new Ride(_pickupLocation, _dropOffLocation, _stopovers, _travelDate, true,
                _returnDate, _passengerContribution, _totalAvailableSeats)
            { RideId = _rideId,IsSoldOut = true };
            Assert.Empty(ride.People);
            Assert.Throws<ArgumentException>(() => ride.AddUserToRide(_jarne));
        }
        [Fact]
        public void AddUserToRide_Add2Users1PlaceLeft_ThrowsArgumentExceptionAndSetsIsSoldOutTrue()
        {
            var ride = new Ride(_pickupLocation, _dropOffLocation, _stopovers, _travelDate, true,
                _returnDate, _passengerContribution, 2)
            { RideId = _rideId};
            Assert.Empty(ride.People);
            ride.AddUserToRide(_jarne);
            ride.AddUserToRide(_ime);
            Assert.Throws<ArgumentException>(() => ride.AddUserToRide(_camiel));
            Assert.True(ride.IsSoldOut);
        }
        #endregion
    }
}
