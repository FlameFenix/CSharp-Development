using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedTrip.Common
{
    public class DataConstants
    {
        // User
        public const int USER_USERNAME_MIN_LENGTH = 6;
        public const int USER_USERNAME_MAX_LENGTH = 20;

        public const int USER_PASSWORD_MIN_LENGTH = 6;
        public const int USER_PASSWORD_MAX_LENGTH = 20;
        public const int USER_HASH_PASSWORD_MAX_LENGTH = 64;

        // Trip

        public const int TRIP_SEATS_MIN_VALUE = 2;
        public const int TRIP_SEATS_MAX_VALUE = 6;

        public const int TRIP_DESCRIPTION_MAX_LENGTH = 80;

    }
}
