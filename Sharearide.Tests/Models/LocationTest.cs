using Sharearide.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sharearide.Tests.Models
{
    public class LocationTest
    {
        #region TestData
        private const int _locationId = 1;
        private const string _number = "16";
        private const string _street = "Zilverstraat";
        private const string _companyName = "I.M.E";
        private readonly City _city = new City("8480", "Ichtegem");
        #endregion

        #region Constructor
        [Fact]
        public void NewLocation_ValidDataWithoutCompanyName_CreatesLocation()
        {
            var location = new Location(_number, _street, _city, Country.Belgium) { LocationId = _locationId};
            Assert.Equal(_number, location.Number);
            Assert.Equal(_street, location.Street);
            Assert.Equal(String.Empty, location.CompanyName);
            Assert.Equal(_city, location.City);
            Assert.Equal(Country.Belgium, location.Country);
            Assert.Equal(_locationId, location.LocationId);
        }
        [Fact]
        public void NewLocation_ValidDataWithCompanyName_CreatesLocation()
        {
            var location = new Location(_number, _street, _city, Country.Belgium,_companyName) { LocationId = _locationId };
            Assert.Equal(_number, location.Number);
            Assert.Equal(_street, location.Street);
            Assert.Equal(_companyName, location.CompanyName);
            Assert.Equal(_city, location.City);
            Assert.Equal(Country.Belgium, location.Country);
            Assert.Equal(_locationId, location.LocationId);
        }
        [Fact]
        public void NewLocation_ValidDataUsingDefaultCTOR_CreatesLocation()
        {
            var location = new Location
            {
                Number = _number,
                Street =_street,
                City = _city,
                Country = Country.Belgium,
                CompanyName = _companyName,
                LocationId = _locationId
            };
            Assert.Equal(_number, location.Number);
            Assert.Equal(_street, location.Street);
            Assert.Equal(_companyName, location.CompanyName);
            Assert.Equal(_city, location.City);
            Assert.Equal(Country.Belgium, location.Country);
            Assert.Equal(_locationId, location.LocationId);
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData("|@@{#[^^{}{}´[][@{@~´")] // > 20 chars
        [InlineData("azertyuiopqsdxcvbnqmq")] // > 20 chars
        [InlineData("123456789012345678901")] // > 20 chars
        public void NewLocation_NumberNotValid_ThrowsArgumentException(string number)
        {
            Assert.Throws<ArgumentException>(() => new Location(number, _street, _city, Country.Belgium, _companyName) { LocationId = _locationId });
        }
        [Theory]
        [InlineData(null)]
        [InlineData("jarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnej")] // > 100 chars
        public void NewLocation_CompanyNameNotValid_ThrowsArgumentException(string companyName)
        {
            Assert.Throws<ArgumentException>(() => new Location(_number, _street, _city, Country.Belgium, companyName) { LocationId = _locationId });
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData("jarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnejarnej")] // > 100 chars
        public void NewLocation_StreetNotValid_ThrowsArgumentException(string street)
        {
            Assert.Throws<ArgumentException>(() => new Location(_number, street, _city, Country.Belgium, _companyName) { LocationId = _locationId });
        }
        #endregion
    }
}
