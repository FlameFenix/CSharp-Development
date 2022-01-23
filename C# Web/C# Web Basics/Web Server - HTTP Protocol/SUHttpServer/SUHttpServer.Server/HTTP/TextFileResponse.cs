namespace SUHttpServer.Server.HTTP
{
    public class TextFileResponse : Response
    {
        public string FileName { get; set; }

        public TextFileResponse(string fileName) 
            : base(StatusCode.OK)
        {
            FileName = fileName;
            Headers.Add(Header.ContentType, ContentType.PlainText);
        }

        public override string ToString()
        {
            if (File.Exists(FileName))
            {
                Body = File.ReadAllTextAsync(FileName).Result;
                var fileBytesCount = new FileInfo(FileName).Length;
                Headers.Add(Header.ContentLength, fileBytesCount.ToString());
                Headers.Add(Header.ContentDisposition, $"attachment; filename=\"{FileName}\"");
            }

            return base.ToString();
        }
    }
}
