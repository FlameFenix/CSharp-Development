using SUHttpServer.Server.HTTP;

namespace SUHttpServer.Server.Responses
{
    public class NotFoundResponse : Response
    {
        public NotFoundResponse() 
            : base(StatusCode.NotFound)
        {
        }
    }
}
