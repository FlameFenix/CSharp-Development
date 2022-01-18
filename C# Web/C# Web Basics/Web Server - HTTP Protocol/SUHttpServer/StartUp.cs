using SUHttpServer.HTTP;
using SUHttpServer.Responses;

namespace SUHttpServer
{
    public class StartUp
    {
        public static void Main()
            => new HttpServer(routes => routes
                 .MapGet("/", new TextResponse("Hello from the server!"))
                 .MapGet("/HTML", new HtmlResponse("<h1>HTML response</h1>"))
                 .MapGet("/Redirect", new RedirectResponse("https://softuni.org/"))).Start();
    }
}
