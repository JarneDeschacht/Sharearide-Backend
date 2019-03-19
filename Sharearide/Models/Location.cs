using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sharearide.Models
{
    public class Location
    {
        #region Fields
        private string _number;
        private string _street;
        private string _companyName;
        #endregion

        #region Properties
        public int LocationId { get; set; }
        public string Number
        {
            get => _number;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length > 20)
                    throw new ArgumentException("Number of location cannot be empty and should not exceed 20 characters");
                _number = value;
            }
        }
        public string CompanyName
        {
            get => _companyName;
            set
            {
                if ( value == null || value.Length > 100)
                    throw new ArgumentException("CompanyName of location cannot be null and should not exceed 100 characters");
                _companyName = value;
            }
        }
        public string Street
        {
            get => _street;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length > 100)
                    throw new ArgumentException("Street of location cannot be empty and should not exceed 100 characters");
                _street = value;
            }
        }
        public City City { get; set; }
        #endregion

        #region Constructors
        public Location(string number,string street,City city,string companyName = "")
        {
            Number = number;
            Street = street;
            City = city;
            CompanyName = companyName;
        }
        public Location()
        {
        
        }
        #endregion

        #region Methods

        #endregion
    }
}
