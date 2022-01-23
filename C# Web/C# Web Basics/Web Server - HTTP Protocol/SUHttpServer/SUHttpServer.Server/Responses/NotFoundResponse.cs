using SUHttpServer.HTTP;

namespace SUHttpServer.Responses
{
    public class NotFoundResponse : Response
    {
        public NotFoundResponse() 
            : base(StatusCode.NotFound)
        {
        }
    }
}
