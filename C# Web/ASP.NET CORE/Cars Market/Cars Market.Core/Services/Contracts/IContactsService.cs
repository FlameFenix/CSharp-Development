using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars_Market.Core.Services.Contracts
{
    public interface IContactsService
    {
        public Task ContactUs(string messageTitle, string messageText, string sender);
    }
}
