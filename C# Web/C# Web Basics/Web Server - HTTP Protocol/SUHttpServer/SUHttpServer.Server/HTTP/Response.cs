﻿using System.Text;

namespace SUHttpServer.Server.HTTP
{
    public class Response
    {
        public Response(StatusCode statusCode)
        {
           this.StatusCode = statusCode;
           this.Headers.Add(Header.Server, "My Web Server");
           this.Headers.Add(Header.Date, $"{DateTime.UtcNow:r}");
        }

        public StatusCode StatusCode { get; init; }

        public HeaderCollection Headers { get; } = new HeaderCollection();

        public CookieCollection Cookies { get; } = new CookieCollection();

        public string Body { get; set; }

        public override string ToString()
        {
            var result = new StringBuilder();

            result.AppendLine($"HTTP/1.1 {(int)StatusCode} {StatusCode}");

            foreach (var header in Headers)
            {
                result.AppendLine(header.ToString());
            }

            foreach (var cookie in Cookies)
            {
                result.AppendLine($"{Header.SetCookie}: {cookie}");
            }

            result.AppendLine();

            if(!string.IsNullOrEmpty(Body))
            {
                result.AppendLine(Body);
            }

            return result.ToString();
        }
    }
}
