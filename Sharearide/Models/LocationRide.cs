using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sharearide.Models
{
    public class LocationRide
    {
        #region Properties
        public int LocationId { get; set; }
        public int RideId { get; set; }
        #endregion

        #region Navigational Properties
        public Location Location { get; set; }
        public Ride Ride { get; set; }
        #endregion

        #region Extra
        public int Index { get; set; }
        #endregion

        #region Constructors
        protected LocationRide()
        {

        }
        public LocationRide(Location location,Ride ride,int index) : this()
        {
            Location = location;
            LocationId = location.LocationId;

            Ride = ride;
            RideId = ride.RideId;

            Index = index;
        }
        #endregion
    }
}
