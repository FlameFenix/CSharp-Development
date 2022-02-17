using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Services.HashPassword
{
    public interface IHashPassword 
    {
       public string PasswordHasher(string password);
    }
}
