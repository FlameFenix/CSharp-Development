using Cars_Market.Core.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace Cars_Market.Core.Services
{
    public class ByteConverter : IByteConverter
    {
        public byte[] ConvertToByteArray(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                return fileBytes;
            }
        }
    }
}
