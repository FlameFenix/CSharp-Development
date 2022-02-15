using SharedTrip.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedTrip.Services.Validator
{
    public interface IValidator
    {
        public List<string> RegisterValidation(RegisterViewModel model);

        public List<string> AddTripValidation(TripViewModel model);
    }
}
