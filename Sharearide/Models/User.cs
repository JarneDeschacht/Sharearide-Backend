﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sharearide.Models
{
    public class User
    {
        #region Fields
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _phoneNumber;
        private DateTime _dateOfBirth;
        #endregion

        #region Properties
        public int UserId { get; set; }
        public ICollection<UserRide> UserRides { get; private set; }
        public IEnumerable<Ride> ParticipatedRides => UserRides != null ? UserRides.Select(r => r.Ride).ToList() : new List<Ride>();
        public string LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length > 100)
                    throw new ArgumentException("LastName cannot be empty and should not exceed 100 characters");
                _lastName = value;
            }
        }
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length > 100)
                    throw new ArgumentException("FirstName cannot be empty and should not exceed 100 characters");
                _firstName = value;
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                Regex emailcheck = new Regex(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}");
                if (value == null || !emailcheck.IsMatch(value))
                    throw new ArgumentException("Email address is not valid");
                _email = value;
            }
        }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                TimeSpan age = DateTime.Now - value.Date;
                if (value == null || value.Date >= DateTime.Today || !IsValidBornDate(value))
                    throw new ArgumentException("Date of birth must be a date in the past and you must be at least 18 years old");
                _dateOfBirth = value;
            }
        }
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (value == null || string.IsNullOrWhiteSpace(value) || value.Length > 15)
                    throw new ArgumentException("Phonenumber is not valid");
                _phoneNumber = value;
            }
        }
        public string Token { get; set; }
        public string URL { get; set; }
        #endregion

        #region Constructors
        public User(string firstName, string lastName, string email,string phoneNumber,DateTime dateOfBirth,Gender gender)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            UserRides = new HashSet<UserRide>();
            URL = "Resources/Images/profilepic.png";
    }
        public User()
        {
            UserRides = new HashSet<UserRide>();
            URL = "Resources/Images/profilepic.png";
        }
        #endregion

        #region Methods
        private bool IsValidBornDate(DateTime bornDate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - bornDate.Year;
            if (bornDate > today.AddYears(-age))
                age--;

            return age < 18 ? false : true;
        }
        public void EditUser(string firstName, string lastName, string email, string phoneNumber, DateTime dateOfBirth, Gender gender)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            DateOfBirth = dateOfBirth;
            Gender = gender;
        }
        public void AddRideToUser(Ride ride)
        {
            if(!CheckIfUserIsNotAlreadyJoinedToARide(ride))
                if (ride.CanUserBeAdded(this))
                    UserRides.Add(new UserRide(this, ride));
        }
        public bool CheckIfUserIsNotAlreadyJoinedToARide(Ride ride)
        {
            return UserRides.Select(ur => ur.RideId).ToList().Contains(ride.RideId);
        }
        public void RemoveFromRide(Ride ride)
        {
            UserRides.Remove(UserRides.SingleOrDefault(ur => ur.RideId == ride.RideId && ur.UserId == this.UserId));
        }
        public bool ParticipatedInRide(int rideid)
        {
            return UserRides.Where(ur => ur.RideId == rideid).ToList().Count > 0;
        }
        #endregion
    }
}
