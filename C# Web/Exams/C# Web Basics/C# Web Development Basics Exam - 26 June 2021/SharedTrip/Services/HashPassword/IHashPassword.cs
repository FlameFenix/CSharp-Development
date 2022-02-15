using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedTrip.Services.HashPassword
{
    public interface IHashPassword
    {
        public string PasswordHasher(string password);
    }
}
