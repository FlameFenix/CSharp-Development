using Cars_Market.Core.Services.Contracts;
using Microsoft.AspNetCore.Http;
using System.Drawing;

namespace Cars_Market.Core.Services
{
    public class ByteConverter : IByteConverter
    {
        public byte[] ConvertToByteArray(IFormFile file)
        {
            Image image = Image.FromStream(file.OpenReadStream());
            var newImage = new Bitmap(600, 350);
            using (var g = Graphics.FromImage(newImage))
            {
                g.DrawImage(image, 0, 0, 600, 350);
            }

            ImageConverter converter = new();

            return (byte[]) converter.ConvertTo(newImage, typeof(byte[]));

            //using (var ms = new MemoryStream())
            //{
            //    file.CopyTo(ms);
            //    var fileBytes = ms.ToArray();
            //    return fileBytes;
            //}
        }


    }
}
