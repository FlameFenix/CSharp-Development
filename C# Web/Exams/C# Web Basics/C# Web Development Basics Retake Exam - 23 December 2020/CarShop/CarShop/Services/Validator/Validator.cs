using CarShop.Common;
using CarShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Services.Validator
{
    public class Validator : IValidator
    {
        public ICollection<string> ValidateRegistration(RegisterViewModel model)
        {
            var errors = new List<string>();

            if(model.Username.Length < DataConstants.USER_USERNAME_MIN_LENGTH ||
                model.Username.Length > DataConstants.USER_USERNAME_MAX_LENGTH)
            {
                errors.Add($"Username should be in range {DataConstants.USER_USERNAME_MIN_LENGTH} - {DataConstants.USER_USERNAME_MAX_LENGTH}");
            }

            if(model.Password.Length < DataConstants.USER_PASSWORD_MIN_LENGTH ||
                model.Password.Length > DataConstants.USER_PASSWORD_MAX_LENGTH)
            {
                errors.Add($"Password should be in range {DataConstants.USER_PASSWORD_MIN_LENGTH} - {DataConstants.USER_PASSWORD_MAX_LENGTH}");
            }

            if(model.Password != model.ConfirmPassword)
            {
                errors.Add($"Password and confirm password doesnt match");
            }

            return errors;
        }
    }
}
