using sharearideApplication.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace sharearideApplication.Tests.Models
{
    public class UserTest
    {
        private const int _userId = 1;
        private const string _firstName = "Jarne";
        private const string _lastName = "Deschacht";
        private const string _email = "jarne.deschacht@hotmail.com";
        private const string _phoneNumber = "0492554616";
        private DateTime _dateOfBirth = new DateTime(1999,8,9);

        #region Constructor
        [Fact]
        public void NewUser_ValidData_CreatesUser()
        {
            var user = new User(_firstName, _lastName, _email, _phoneNumber, _dateOfBirth, Gender.Male) { UserId = _userId};
            Assert.Equal(_lastName, user.LastName);
            Assert.Equal(_firstName, user.FirstName);
            Assert.Equal(_phoneNumber, user.PhoneNumber);
            Assert.Equal(_dateOfBirth, user.DateOfBirth);
            Assert.Equal(_email, user.Email);
            Assert.Equal(Gender.Male, user.Gender);
            Assert.Equal(_userId, user.UserId);
        }
        [Fact]
        public void NewUser_ValidDataUsingDefaultCTOR_CreatesUser()
        {
            var user = new User()
            {
                UserId = _userId,
                FirstName = _firstName,
                LastName = _lastName,
                Email = _email,
                Gender = Gender.Male,
                PhoneNumber = _phoneNumber,
                DateOfBirth = new DateTime(1999,8,9)

            };
            Assert.Equal(_lastName, user.LastName);
            Assert.Equal(_firstName, user.FirstName);
            Assert.Equal(_phoneNumber, user.PhoneNumber);
            Assert.Equal(_dateOfBirth, user.DateOfBirth);
            Assert.Equal(_email, user.Email);
            Assert.Equal(Gender.Male, user.Gender);
            Assert.Equal(_userId, user.UserId);
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData("jarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnej")] // > 100 chars
        public void NewUser_FirstNameNotValid_ThrowsArgumentException(string firstName)
        {
            Assert.Throws<ArgumentException>(() => new User(firstName, _lastName,_email,_phoneNumber,_dateOfBirth,Gender.Male) { UserId = _userId });
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData("jarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnej")] // > 100 chars
        public void NewUser_LastNameNotValid_ThrowsArgumentException(string lastName)
        {
            Assert.Throws<ArgumentException>(() => new User(_firstName, lastName, _email, _phoneNumber, _dateOfBirth, Gender.Male) { UserId = _userId });
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData("jarne.deschacht&hotmail.com")]
        [InlineData("jarne+deschacht@hotmail+com")]
        [InlineData("jarne.deschachthotmail.com")]
        [InlineData("jarnedeschacht@hotmailcom")]
        [InlineData("jarnedeschachthotmailcom")]
        [InlineData("jarne.deschacht@hotmail.")]
        [InlineData("jarne+deschacht@hotmail%com")]
        // ...
        public void NewUser_EmailNotValid_ThrowsArgumentException(string email)
        {
            Assert.Throws<ArgumentException>(() => new User(_firstName, _lastName, email, _phoneNumber, _dateOfBirth, Gender.Male) { UserId = _userId });
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData("0000000000000000")] // > 15 chars
        public void NewUser_PhoneNumberNotValid_ThrowsArgumentException(string phoneNumber)
        {
            Assert.Throws<ArgumentException>(() => new User(_firstName, _lastName, _email, phoneNumber, _dateOfBirth, Gender.Male) { UserId = _userId });
        }
        [Theory]
        [InlineData(2100,9,8)]
        [InlineData(2015, 9, 8)]
        public void NewUser_DateTimeNotValid_ThrowsArgumentException(int year,int month,int day)
        {
            DateTime date = new DateTime(year, month, day);
            Assert.Throws<ArgumentException>(() => new User(_firstName, _lastName, _email, _phoneNumber, date, Gender.Male) { UserId = _userId });
        }
        #endregion

        #region Methods
        [Fact]
        public void EditUser_ValidData_CreatesUser()
        {
            User user = new User("Ime", "Van Daele", "imevandaele@gmail.com", "0492554619", new DateTime(2000, 3, 8), Gender.Female){UserId = _userId};
            user.EditUser(_firstName, _lastName,_email,_phoneNumber,_dateOfBirth,Gender.Male);
            Assert.Equal(_lastName, user.LastName);
            Assert.Equal(_firstName, user.FirstName);
            Assert.Equal(_phoneNumber, user.PhoneNumber);
            Assert.Equal(_dateOfBirth, user.DateOfBirth);
            Assert.Equal(_email, user.Email);
            Assert.Equal(Gender.Male, user.Gender);
            Assert.Equal(_userId, user.UserId);
        }
        #endregion
    }
}
