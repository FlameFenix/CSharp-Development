namespace SUHttpServer.HTTP
{
    public class TextResponse : ContentResponse
    {
        public TextResponse(string text, Action<Request, Response> preRenderAction = null) 
            : base(text, ContentType.PlainText, preRenderAction)
        {
        }
    }
}
