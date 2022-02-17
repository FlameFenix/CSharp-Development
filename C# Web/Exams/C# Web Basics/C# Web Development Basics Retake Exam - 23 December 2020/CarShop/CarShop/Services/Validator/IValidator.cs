using CarShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Services.Validator
{
    internal interface IValidator
    {
        public ICollection<string> ValidateRegistration(RegisterViewModel model);
    }
}
