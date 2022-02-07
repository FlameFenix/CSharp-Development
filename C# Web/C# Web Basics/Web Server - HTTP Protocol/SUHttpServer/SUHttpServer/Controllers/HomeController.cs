using SUHttpServer.Server.Controllers;
using SUHttpServer.Server.HTTP;
using SUHttpServer.Server.Responses;
using System.Text;
using System.Web;

namespace SUHttpServer.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(Request request)
            : base(request)
        {

        }

        public Response Index() => Text("Hello from the server!");

        public Response Redirect() => new RedirectResponse(Common.Constants.redirectUrl);

        public Response Html() => View(); //new HtmlResponse(Common.Constants.HtmlForm);

        protected Response Html(string text, CookieCollection cookies = null)
        {
            var response = new HtmlResponse(text);

            if (cookies != null)
            {
                foreach (var cookie in cookies)
                {
                    response.Cookies.Add(cookie.Name, cookie.Value);
                }
            }

            return response;
        }

        public Response HtmlFormPost()
        {
            string formData = string.Empty;

            foreach (var (key, value) in Request.Form)
            {
                formData += $"{key} - {value}";
                formData += Environment.NewLine;
            }

            return Text(formData);
        }

        public Response Content() => View(); //new HtmlResponse(Common.Constants.DownloadForm);

        public Response DownloadContent()
        {
              DownloadSitesAsTextFile(Common.Constants.FileName ,
    new string[]
    {
                    "https://judge.softuni.org/",
                    "https://softuni.org"
    }).Wait();
            return File(Common.Constants.FileName);
        }
        public Response Cookies()
        {
            if(this.Request.Cookies.Any(x => x.Name !=
            SUHttpServer.Server.HTTP.Session.SessionCookieName))
            {
                var cookieText = new StringBuilder();

                cookieText.Append("<h1>Cookies</h1>")
                      .Append("<table border='1'><tr><th>Name</th><th>Value</th></tr>");

                foreach (var cookie in Request.Cookies)
                {
                    cookieText.Append("<tr>")
                              .Append($"<td>{HttpUtility.HtmlEncode(cookie.Name)}</td>")
                              .Append($"<td>{HttpUtility.HtmlEncode(cookie.Value)}</td>")
                              .Append("<tr>");
                }

                cookieText.Append("</table>");
                return Html(cookieText.ToString());
            }

            var cookies = new CookieCollection();
            cookies.Add("My-Cookie", "My-Cookie");
            cookies.Add("My-Second-Cookie", "My-Second-Value");

            return Html("<h1> Cookies set!</h1>", cookies);
        }

        private static async Task DownloadSitesAsTextFile(string fileName, string[] urls)
        {
            var downloads = new List<Task<string>>();

            foreach (var url in urls)
            {
                downloads.Add(DownloadWebSiteContent(url));
            }

            var responses = await Task.WhenAll(downloads);

            var responsesString = string.Join(Environment.NewLine + new string('-', 100), responses);

            await System.IO.File.WriteAllTextAsync(fileName, responsesString);
        }

        private static async Task<string> DownloadWebSiteContent(string url)
        {
            var httpClient = new HttpClient();
            using (httpClient)
            {
                var response = await httpClient.GetAsync(url);

                var html = await response.Content.ReadAsStringAsync();

                return html.Substring(0, 2000);
            }
        }

        public Response Session()
        {
            string currentDataKey = "CurrentDate";
            var sessionExists = Request.Session.ContainsKey(currentDataKey);

            if(sessionExists)
            {
                var currentDate = Request.Session[currentDataKey];

                return Text($"Stored date: {currentDate}");
            }

            return Text("Current date stored!");
        }
    }
}
