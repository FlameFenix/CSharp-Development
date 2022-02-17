using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Common
{
    public static class DataConstants
    {
        // User

            public const int USER_USERNAME_MIN_LENGTH = 4;
            public const int USER_USERNAME_MAX_LENGTH = 20;

            public const int USER_PASSWORD_MAX_LENGTH_HASHED = 64;
            public const int USER_PASSWORD_MIN_LENGTH = 5;
            public const int USER_PASSWORD_MAX_LENGTH = 20;

        // Car

        public const int CAR_MODEL_MIN_LENGTH = 5;
        public const int CAR_MODEL_MAX_LENGTH = 20;

        public const int CAR_IMAGEURL_MAX_LENGTH = 256;

        // Issue

        public const int ISSUE_DESCRIPTION_MIN_LENGTH = 5;
        public const int ISSUE_DESCRIPTION_MAX_LENGTH = 1000;
    }
}
