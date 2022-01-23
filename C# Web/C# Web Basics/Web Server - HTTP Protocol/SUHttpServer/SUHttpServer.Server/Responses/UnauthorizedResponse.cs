using SUHttpServer.Server.HTTP;

namespace SUHttpServer.Server.Responses
{
    public class UnauthorizedResponse : Response
    {
        public UnauthorizedResponse() 
            : base(StatusCode.Unauthorized)
        {
        }
    }
}
