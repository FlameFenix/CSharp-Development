namespace SUHttpServer.Server.HTTP
{
    public class TextResponse : ContentResponse
    {
        public TextResponse(string text) 
            : base(text, ContentType.PlainText)
        {
        }
    }
}
