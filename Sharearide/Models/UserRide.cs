using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sharearide.Models
{
    public class UserRide
    {
        #region Properties
        public int UserId { get; set; }
        public int RideId { get; set; }
        #endregion

        #region Navigational Properties
        public User User { get; set; }
        public Ride Ride { get; set; }
        #endregion

        #region Constructors
        protected UserRide()
        {

        }
        public UserRide(User user, Ride ride) : this()
        {
            User = user;
            UserId = user.UserId;

            Ride = ride;
            RideId = ride.RideId;
        }
        #endregion
    }
}
