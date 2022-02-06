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
                //.MapGet<HomeController>("/Login", new HtmlResponse(StartUp.LoginForm))
                //.MapPost<HomeController>("/Login", new HtmlResponse("", StartUp.LoginAction))
                //.MapGet<HomeController>("/Logout", new HtmlResponse("", StartUp.LogoutAction))
                //.MapGet<HomeController>("/UserProfile", new HtmlResponse("", StartUp.GetUserDataAction))
                ).Start();
        }

        private static void GetUserDataAction(Request request, Response response)
        {
            if (request.Session.ContainsKey(Session.SessionUserKey))
            {
                response.Body = "";
                response.Body += 
                    $"<h3> Currently logged-in user is with username '{Constants.Username}'</h3>";
            }
            else
            {
                response.Body = "";
                response.Body +=
                    "<h3> You should first log in - <a href='/Login'>Login</a></h3>";
            }
        }

        private static void LogoutAction(Request request, Response response)
        {
            var sessionBeforeClear = request.Session;
            request.Session.Clear();
            response.Body = "";
            response.Body += "<h3> Logged out successfully!</h3>";
        }

        private static void LoginAction(Request request, Response response)
        {
            request.Session.Clear();

            var bodyText = "";

            var usernameMatches = request.Form["Username"] == Constants.Username;

            var passwordMatches = request.Form["Password"] == Constants.Password;

            if (usernameMatches && passwordMatches)
            {
                request.Session[Session.SessionUserKey] = "MyUserId";
                response.Cookies.Add(Session.SessionUserKey, request.Session.Id);

                bodyText = "<h3> Logged successfully!</h3>";
            }
            else
            {
                bodyText = Constants.LoginForm;
            }

            response.Body = "";
            response.Body += bodyText;
        }

        private static void AddFormDateAction(Request request, Response response)
        {
            response.Body = "";

            foreach (var (key, value) in request.Form)
            {
                response.Body += $"{key} - {value}";
                response.Body += Environment.NewLine;
            }
        }


    }
}
