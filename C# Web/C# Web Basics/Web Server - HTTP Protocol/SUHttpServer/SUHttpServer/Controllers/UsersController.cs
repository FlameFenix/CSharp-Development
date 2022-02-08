using SUHttpServer.Server.Controllers;
using SUHttpServer.Server.HTTP;
using SUHttpServer.Server.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUHttpServer.Controllers
{
    public class UsersController : Controller
    {
        public UsersController(Request request)
            : base(request)
        {
        }

        public Response Login() => View();

        public Response LogInUser()
        {
            Request.Session.Clear();

            var bodyText = "";

            var usernameMatches = Request.Form["Username"] == Common.Constants.Username;

            var passwordMatches = Request.Form["Password"] == Common.Constants.Password;

            if (usernameMatches && passwordMatches)
            {
                if (!this.Request.Session.ContainsKey(SUHttpServer.Server.HTTP.Session.SessionUserKey))
                {
                    Request.Session[SUHttpServer.Server.HTTP.Session.SessionUserKey] = "MyUserId";

                    var cookies = new CookieCollection();
                    cookies.Add(SUHttpServer.Server.HTTP.Session.SessionCookieName, Request.Session.Id);

                    return Html("<h3> Logged successfully!</h3>", cookies);
                }
                return Html("<h3> Logged successfully!</h3>");
            }

            return Redirect("/Login");
        }

        protected Response Html(string html, CookieCollection cookies)
        {
            var response = new HtmlResponse(html);

            if (cookies != null)
            {
                foreach (var cookie in cookies)
                {
                    response.Cookies.Add(cookie.Name, cookie.Value);
                }
            }

            return response;
        }

        internal Response GetUserDataAction()
        {
            if (Request.Session.ContainsKey(Session.SessionUserKey))
            {
                return Html($"<h3> Currently logged-in user is with username '{Common.Constants.Username}'</h3>");
            }

            return Html("<h3> You should first log in - <a href='/Login'>Login</a></h3>");
        }

        public Response LogoutAction()
        {
            Request.Session.Clear();
            return Html("<h3> Logged out successfully!</h3>");
        }
    }
}
