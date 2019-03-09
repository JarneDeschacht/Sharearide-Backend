using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CarpoolApplication.Models
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
                if (value == null)
                    throw new ArgumentException("Phonenumber is not valid");
                _phoneNumber = value;
            }
        }
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
        }
        public User()
        {

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
        #endregion
    }
}
