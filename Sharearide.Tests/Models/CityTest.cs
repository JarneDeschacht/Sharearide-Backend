using Sharearide.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sharearide.Tests.Models
{
    public class CityTest
    {
        #region Constructor
        [Theory]
        [InlineData("1")]
        [InlineData("8480")]
        [InlineData("134655767")]
        [InlineData("0000000000")]
        [InlineData("1234567890")]
        public void NewCity_ValidData_CreatesCity(string postalCode)
        {
            City city = new City(postalCode, "Gent");
            Assert.Equal(postalCode, city.Postalcode);
            Assert.Equal("Gent", city.Name);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("abcd")]
        [InlineData("a123")]
        [InlineData("123a")]
        [InlineData("a")]
        [InlineData("12345678901")]
        [InlineData("a1234")]
        public void NewCity_InvalidPostalCode_ThrowsArgumentException(string postalCode)
        {
            Assert.Throws<ArgumentException>(() => new City(postalCode, "Gent"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890")]
        public void NewCity_InvalidName_ThrowsArgumentException(string name)
        {
            Assert.Throws<ArgumentException>(() => new City("9000", name));
        }

        #endregion

    }
}
