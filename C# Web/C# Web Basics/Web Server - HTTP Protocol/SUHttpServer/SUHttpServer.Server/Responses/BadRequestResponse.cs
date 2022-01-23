using SUHttpServer.Server.HTTP;

namespace SUHttpServer.Server.Responses
{
    public class BadRequestResponse : Response
    {
        public BadRequestResponse() 
            : base(StatusCode.BadRequest)
        {
        }
    }
}
