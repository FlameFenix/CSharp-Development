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

        public const string USER_EMAIL_REGEX = @"^[A-z]+\@[A-z]+\.[A-z]+$";

        public const int USER_PASSWORD_MAX_LENGTH_HASHED = 64;
        public const int USER_PASSWORD_MIN_LENGTH = 5;
        public const int USER_PASSWORD_MAX_LENGTH = 20;

        // Car

        public const int CAR_MODEL_MIN_LENGTH = 5;
        public const int CAR_MODEL_MAX_LENGTH = 20;

        public const int CAR_IMAGEURL_MAX_LENGTH = 256;

        public const string CAR_PLATENUMBER_REGEX = @"^[A-Z]{1,2}[0-9]{4}[A-Z]{2}$";

        // Issue

        public const int ISSUE_DESCRIPTION_MIN_LENGTH = 5;
        public const int ISSUE_DESCRIPTION_MAX_LENGTH = 1000;
    }
}
