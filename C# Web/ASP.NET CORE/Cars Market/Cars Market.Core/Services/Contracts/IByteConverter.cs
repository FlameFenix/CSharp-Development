using Microsoft.AspNetCore.Http;

namespace Cars_Market.Core.Services.Contracts
{
    public interface IByteConverter
    {
        public byte[] ConvertToByteArray(IFormFile file);
    }
}
