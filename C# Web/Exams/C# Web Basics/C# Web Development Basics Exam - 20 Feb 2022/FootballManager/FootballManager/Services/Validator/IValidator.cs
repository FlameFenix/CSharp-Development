using FootballManager.ViewModels.Players;
using FootballManager.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Services.Validator
{
    public interface IValidator
    {
        public ICollection<string> ValidateRegistration(RegisterViewModel model);

        public ICollection<string> ValidateLogin(LoginViewModel model);

        public ICollection<string> ValidateAddPlayer(AddPlayerViewModel model);
    }
}
