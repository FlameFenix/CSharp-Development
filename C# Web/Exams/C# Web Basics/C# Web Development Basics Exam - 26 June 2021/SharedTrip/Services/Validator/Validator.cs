using SharedTrip.Common;
using SharedTrip.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SharedTrip.Services.Validator
{
    public class Validator : IValidator
    {
        public List<string> RegisterValidation(RegisterViewModel model)
        {
            var errors = new List<string>();

            if (model.Username.Length < DataConstants.USER_USERNAME_MIN_LENGTH ||
                model.Username.Length > DataConstants.USER_USERNAME_MAX_LENGTH)
            {
                errors.Add($"Username must be {DataConstants.USER_USERNAME_MIN_LENGTH} - {DataConstants.USER_USERNAME_MAX_LENGTH} symbols!");
            }

            if (model.Password.Length < DataConstants.USER_PASSWORD_MIN_LENGTH ||
                model.Password.Length > DataConstants.USER_PASSWORD_MAX_LENGTH)
            {
                errors.Add($"Password must be {DataConstants.USER_PASSWORD_MIN_LENGTH} - {DataConstants.USER_PASSWORD_MAX_LENGTH} symbols!");
            }

            if (model.Password != model.ConfirmPassword)
            {
                errors.Add("Password and ConfirmPassword not match!");
            }

            if (!Regex.IsMatch(model.Email, @"^[A-z]+\@[A-z]+\.[A-z]+$"))
            {
                errors.Add($"Invalid email!");
            }

            return errors;

        }

        public List<string> AddTripValidation(TripViewModel model)
        {
            var errors = new List<string>();

            if(model.Seats < 2 && model.Seats > 6)
            {
                errors.Add($"Seats should be in range {DataConstants.TRIP_SEATS_MIN_VALUE} - {DataConstants.TRIP_SEATS_MAX_VALUE}");
            }

            bool isDateCorrect = DateTime.TryParse(model.DepartureTime, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result);

            if (!isDateCorrect)
            {
                errors.Add("The date should be in following format");
            }

            return errors;
        }
    }
}
