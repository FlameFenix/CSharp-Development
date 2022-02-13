using SMS.Data.Common;
using SMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SMS.Services
{
    public class Validator : IValidator
    {
        public string ValidateRegistration(RegisterViewModel model)
        {
            var errors = new StringBuilder();

            if (model.Username.Length < Constants.USER_USERNAME_MIN_LENGTH)
            {
                errors.AppendLine($"Username must be in range {Constants.USER_USERNAME_MIN_LENGTH} - {Constants.USER_USERNAME_MAX_LENGTH}");
            }

            if(model.Password.Length < Constants.USER_PASSWORD_MIN_LENGTH)
            {
                errors.AppendLine($"Password must be in range {Constants.USER_PASSWORD_MIN_LENGTH} - {Constants.USER_PASSWORD_MAX_LENGTH}");
            }

            if(model.Password != model.ConfirmPassword)
            {
                errors.AppendLine("Password and confirm password are diffrent");
            }

            if (!Regex.IsMatch(model.Email, @"^[A-z]+\@[A-z]+\.[A-z]+$"))
            {
                errors.AppendLine("Email is invalid");
            }

            return errors.ToString();
        }
    }
}
