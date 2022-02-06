using SUHttpServer.Common;
using SUHttpServer.Controllers;
using SUHttpServer.Server.HTTP;
using SUHttpServer.Server.Responses;
using SUHttpServer.Server.Routing;
using System.Text;
using System.Web;

namespace SUHttpServer
{
    public class StartUp
    {

        public static async Task Main()
        {

            await new HttpServer(routes => routes
                .MapGet<HomeController>("/", c => c.Index())
                .MapGet<HomeController>("/Redirect", c => c.Redirect())
                .MapGet<HomeController>("/HTML", c => c.Html())
                .MapPost<HomeController>("/HTML", c => c.HtmlFormPost())
                .MapGet<HomeController>("/Content", c => c.Content())
                .MapPost<HomeController>("/Content", c => c.DownloadContent())
                .MapGet<HomeController>("/Cookies", c => c.Cookies())
                .MapGet<HomeController>("/Session", c => c.Session())
                .MapGet<UsersController>("/Login", c => c.Login())
                .MapPost<UsersController>("/Login", c => c.LogInUser())
                .MapGet<UsersController>("/Logout", c => c.LogoutAction())
                .MapGet<UsersController>("/UserProfile",c => c.GetUserDataAction())
                ).Start();
        }
    }
}
