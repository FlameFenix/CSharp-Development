using FootballManager.Common;
using FootballManager.Data;
using FootballManager.ViewModels.Users;
using FootballManager.Services.PasswordHash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballManager.ViewModels.Players;

namespace FootballManager.Services.Validator
{
    public class Validator : IValidator
    {
        private FootballManagerDbContext data;
        private PasswordHasher passwordHasher;
        public Validator(FootballManagerDbContext _data,
            PasswordHasher _passwordHasher)
        {
            data = _data;
            passwordHasher = _passwordHasher;
        }
        public ICollection<string> ValidateRegistration(RegisterViewModel model)
        {
            var errors = new List<string>();

            if(model.Username.Length < DataConstants.USER_USERNAME_MIN_LENGTH || 
                model.Username.Length > DataConstants.USER_USERNAME_MAX_LENGTH)
            {
                errors.Add($"Username should be in range {DataConstants.USER_USERNAME_MIN_LENGTH} - {DataConstants.USER_USERNAME_MAX_LENGTH}");
            }

            if(model.Password.Length < DataConstants.USER_PASSWORD_MIN_LENGTH || 
                model.Password.Length > DataConstants.USER_USERNAME_MAX_LENGTH)
            {
                errors.Add($"Password should be in range {DataConstants.USER_PASSWORD_MIN_LENGTH} - {DataConstants.USER_USERNAME_MAX_LENGTH}");
            }

            if(model.Password != model.ConfirmPassword)
            {
                errors.Add("Password and confirm password not match");
            }

            if(model.Email.Length < DataConstants.USER_EMAIL_MIN_LENGTH || 
                model.Email.Length > DataConstants.USER_EMAIL_MAX_LENGTH)
            {
                errors.Add($"Email should be in range {DataConstants.USER_EMAIL_MIN_LENGTH} - {DataConstants.USER_EMAIL_MAX_LENGTH}");
            }

            return errors;
        }

        public ICollection<string> ValidateLogin(LoginViewModel model)
        {
            var errors = new List<string>();

            var user = data.Users.FirstOrDefault(x => x.Username == model.Username);

            if(user == null)
            {
                errors.Add("Username does not exists!");
            }

            if(user.Password != passwordHasher.HashPassword(model.Password))
            {
                errors.Add("Incorrect password!");
            }

            return errors;
        }

        public ICollection<string> ValidateAddPlayer(AddPlayerViewModel model)
        {
            var errors = new List<string>();

            if(model.FullName.Length < DataConstants.PLAYER_FULL_NAME_MIN_LENGTH ||
                model.FullName.Length > DataConstants.PLAYER_FULL_NAME_MAX_LENGTH)
            {
                errors.Add($"Full name should be in range {DataConstants.PLAYER_FULL_NAME_MIN_LENGTH} - {DataConstants.PLAYER_FULL_NAME_MAX_LENGTH}");
            }

            if(model.ImageUrl.Length > DataConstants.PLAYER_IMAGE_URL_LENGTH)
            {
                errors.Add("Too long url");
            }

            if(model.Description.Length > DataConstants.PLAYER_DESCRIPTION_MAX_LENGTH)
            {
                errors.Add($"Description should be maximum {DataConstants.PLAYER_DESCRIPTION_MAX_LENGTH}");
            }

            if(model.Speed < DataConstants.PLAYER_SPEED_MIN_VALUE ||
                model.Speed > DataConstants.PLAYER_SPEED_MAX_VALUE)
            {
                errors.Add($"Speed should be in range {DataConstants.PLAYER_SPEED_MIN_VALUE} - {DataConstants.PLAYER_SPEED_MAX_VALUE}");
            }
           
            if (model.Endurance < DataConstants.PLAYER_ENDURANCE_MIN_VALUE ||
                model.Endurance > DataConstants.PLAYER_ENDURANCE_MAX_VALUE)
            {
                errors.Add($"Speed should be in range {DataConstants.PLAYER_ENDURANCE_MIN_VALUE} - {DataConstants.PLAYER_ENDURANCE_MAX_VALUE}");
            }

            return errors;
        }
    }
}
