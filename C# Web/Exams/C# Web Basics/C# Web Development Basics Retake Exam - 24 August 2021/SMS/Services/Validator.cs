using SMS.Data.Common;
using SMS.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SMS.Services
{
    public class Validator : IValidator
    {
        public ICollection<string> ValidateRegistration(RegisterViewModel model)
        {
            var errors = new List<string>();

            if (model.Username.Length < Constants.USER_USERNAME_MIN_LENGTH)
            {
                errors.Add($"Username must be in range {Constants.USER_USERNAME_MIN_LENGTH} - {Constants.USER_USERNAME_MAX_LENGTH}");
            }

            if(model.Password.Length < Constants.USER_PASSWORD_MIN_LENGTH)
            {
                errors.Add($"Password must be in range {Constants.USER_PASSWORD_MIN_LENGTH} - {Constants.USER_PASSWORD_MAX_LENGTH}");
            }

            if(model.Password != model.ConfirmPassword)
            {
                errors.Add("Password and confirm password are diffrent");
            }

            if (!Regex.IsMatch(model.Email, @"^[A-z]+\@[A-z]+\.[A-z]+$"))
            {
                errors.Add("Email is invalid");
            }

            return errors;
        }

        public ICollection<string> ValidateLogin(LoginViewModel model)
        {
            var errors = new List<string>();

            if(string.IsNullOrWhiteSpace(model.Username))
            {
                errors.Add("Field 'Username' is required");
            }

            if (string.IsNullOrWhiteSpace(model.Password))
            {
                errors.Add("Field 'Password' is required");
            }

            return errors;
        }

        public ICollection<string> ValidateProduct(ProductViewModel model)
        {
            var errors = new List<string>();

            if(model.Name.Length < Constants.PRODUCT_NAME_MIN_LENGTH
                || model.Name.Length > Constants.PRODUCT_NAME_MAX_LENGTH)
            {
                errors.Add($"Product name must be in range {Constants.PRODUCT_NAME_MIN_LENGTH} - {Constants.PRODUCT_NAME_MAX_LENGTH}");
            }

            var price = decimal.Parse(model.Price, CultureInfo.InvariantCulture);

            if (price < 0.05M ||
                price > 1000M)
            {
                errors.Add($"Product price is invalid or out of range {Constants.PRODUCT_PRICE_MIN_VALUE} - {Constants.PRODUCT_PRICE_MAX_VALUE}");
            }

            return errors;
        }
    }
}
