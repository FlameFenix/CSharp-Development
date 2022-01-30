using SUHttpServer.Server.HTTP;
using SUHttpServer.Server.Responses;
using System.Text;
using System.Web;

namespace SUHttpServer
{
    public class StartUp
    {
        private const string HtmlForm = @"<form action='/HTML' method='POST'>
   Name: <input type='text' name='Name'/>
   Age: <input type='number' name='Age'/>
<input type='submit' value='Save'/>
</form>";

        private const string DownloadForm = @"<form action='/Content' method='POST'>
   <input type='submit' value ='Download Sites Content' /> 
</form>";

        private const string FileName = "content.txt";

        public static async Task Main()
        {
            await DownloadSitesAsTextFile(StartUp.FileName,
                new string[]
                {
                    "https://judge.softuni.org/",
                    "https://softuni.org"
                });

            var server = new HttpServer(routes => routes
                .MapGet("/", new TextResponse("Hello from the server!"))
                .MapGet("/Redirect", new RedirectResponse("https://softuni.org/"))
                .MapGet("/HTML", new HtmlResponse(StartUp.HtmlForm))
                .MapPost("/HTML", new TextResponse("", StartUp.AddFormDateAction))
                .MapGet("/Content", new HtmlResponse(StartUp.DownloadForm))
                .MapPost("/Content", new TextFileResponse(StartUp.FileName))
                .MapGet("/Cookies", new HtmlResponse("", StartUp.AddCookiesAction)));


            await server.Start();
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

        private static async Task DownloadSitesAsTextFile(string fileName, string[] urls)
        {
            var downloads = new List<Task<string>>();

            foreach (var url in urls)
            {
                downloads.Add(DownloadWebSiteContent(url));
            }

            var responses = await Task.WhenAll(downloads);

            var responsesString = string.Join (Environment.NewLine + new string('-', 100), responses);

            await File.WriteAllTextAsync(fileName, responsesString);
        }

        private static void AddCookiesAction(Request request, Response response)
        {

            var requestHasCookies = request.Cookies.Any(c => c.Name != Session.SessionCookieName);

            var bodyText = "";

            

            if (requestHasCookies)
            {
                var cookieText = new StringBuilder();

                cookieText.Append("<h1>Cookies</h1>")
                          .Append("<table border='1'><tr><th>Name</th><th>Value</th></tr>");

                foreach (var cookie in request.Cookies)
                {
                    cookieText.Append("<tr>")
                              .Append($"<td>{HttpUtility.HtmlEncode(cookie.Name)}</td>")
                              .Append($"<td>{HttpUtility.HtmlEncode(cookie.Value)}</td>")
                              .Append("<tr>");
                }

                cookieText.Append("</table>");

                bodyText = cookieText.ToString();
            }
            else
            {
                bodyText = "<h1> Cookies set!</h1>";
            }

            if (!requestHasCookies)
            {
                response.Cookies.Add("My-Cookie", "My-Value");
                response.Cookies.Add("My-Second-Cookie", "My-Second-Value");
            }

            response.Body = "";
            response.Body = bodyText;
        }
    }
}
