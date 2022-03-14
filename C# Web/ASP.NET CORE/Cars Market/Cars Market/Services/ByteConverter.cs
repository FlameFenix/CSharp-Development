using Cars_Market.Services.Contracts;

namespace Cars_Market.Services
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
