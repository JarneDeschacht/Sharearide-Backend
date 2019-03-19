using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sharearide.Models
{
    public class City
    {
        #region Fields
        private string _postalCode;
        private string _name;
        #endregion

        #region Properties
        public string Postalcode
        {
            get => _postalCode;
            set
            {
                Regex postcodeRegex = new Regex(@"^\d{1,10}$");
                if (value == null || !postcodeRegex.IsMatch(value))
                    throw new ArgumentException("Postal code may not contain more than 10 digits");
                _postalCode = value;
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length > 100)
                    throw new ArgumentException("City must have a name, and the name should not exceed 100 characters");
                _name = value;
            }
        }
        public Country Country { get; set; }
        #endregion

        #region Contstructors
        public City(string postalcode, string name,Country country)
        {
            Postalcode = postalcode;
            Name = name;
            Country = country;
        }
        #endregion
    }
}
