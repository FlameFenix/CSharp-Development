using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Data.Common
{
    public static class Constants
    {
        public const int ID_LENGTH = 36;

        public const int USER_USERNAME_MIN_LENGTH = 5;
        public const int USER_USERNAME_MAX_LENGTH = 20;

        public const int USER_EMAIL_MAX_LENGTH = 100;

        public const int USER_PASSWORD_MIN_LENGTH = 6;
        public const int USER_PASSWORD_MAX_LENGTH = 64;

        public const int PRODUCT_NAME_MIN_LENGTH = 4;
        public const int PRODUCT_NAME_MAX_LENGTH = 20;

        public const double PRODUCT_PRICE_MIN_VALUE = 0.05;
        public const double PRODUCT_PRICE_MAX_VALUE = 1000.0;
    }
}
