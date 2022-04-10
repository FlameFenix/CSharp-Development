using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars_Market.Infrastructure.Data.Seed.PictureConverter
{
    public static class Converter
    {
        public static byte[] ConvertPicture(string path)
        {
            FileStream stream = new(path, FileMode.Open, FileAccess.Read);

            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

            return bytes;
        }
    }
}
