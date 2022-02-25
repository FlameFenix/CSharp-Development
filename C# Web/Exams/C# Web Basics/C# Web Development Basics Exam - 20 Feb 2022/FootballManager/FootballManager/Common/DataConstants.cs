using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Common
{
    public static class DataConstants
    {
        // User 

        public const int USER_USERNAME_MIN_LENGTH = 5;
        public const int USER_USERNAME_MAX_LENGTH = 20;

        public const int USER_EMAIL_MIN_LENGTH = 10;
        public const int USER_EMAIL_MAX_LENGTH = 60;

        public const int USER_PASSWORD_MIN_LENGTH = 5;
        public const int USER_PASSWORD_MAX_LENGTH = 20;
        public const int USER_PASSWORD_HASHED = 64;

        // Player

        public const int PLAYER_FULL_NAME_MIN_LENGTH = 5;
        public const int PLAYER_FULL_NAME_MAX_LENGTH = 80;

        public const int PLAYER_IMAGE_URL_LENGTH = 256;

        public const int PLAYER_POSITION_MIN_LENGTH = 5;
        public const int PLAYER_POSITION_MAX_LENGTH = 20;

        public const int PLAYER_SPEED_MIN_VALUE = 0;
        public const int PLAYER_SPEED_MAX_VALUE = 10;

        public const int PLAYER_ENDURANCE_MIN_VALUE = 0;
        public const int PLAYER_ENDURANCE_MAX_VALUE = 10;

        public const int PLAYER_DESCRIPTION_MAX_LENGTH = 200;
    }
}
