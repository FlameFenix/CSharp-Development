using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUHttpServer.HTTP
{
    public class HeaderCollection
    {
        public readonly Dictionary<string, Header> headers;

        public HeaderCollection() 
            => headers = new Dictionary<string, Header>();

        public int Count => headers.Count;

        public void Add(string name, string value)
        {
            var header = new Header(name, value);
            headers.Add(name, header);
        }
    }
}
