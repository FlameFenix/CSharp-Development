using SUHttpServer.Server.Common;
using SUHttpServer.Server.HTTP;
using SUHttpServer.Server.Responses;

namespace SUHttpServer.Server.Routing
{
    public class RoutingTable : IRoutingTable
    {
        private readonly Dictionary<Method, Dictionary<string, Response>> routes;

        public RoutingTable()
            => routes = new()
            {
                [Method.Get] = new(),
                [Method.Post] = new(),
                [Method.Put] = new(),
                [Method.Delete] = new()
            };


        public IRoutingTable Map(string url, Method method, Response response)
            => method switch
            {
                Method.Get => MapGet(url, response),
                Method.Post => MapPost(url, response),
                _ => throw new InvalidOperationException(
                    $"Method '{method}' is not supported.")
            };

        public IRoutingTable MapGet(string url, Response response)
        {
            Guard.AgainstNull(url, nameof(url));
            Guard.AgainstNull(response, nameof(response));

            routes[Method.Get][url] = response;

            return this;
        }

        public IRoutingTable MapPost(string url, Response response)
        {
            Guard.AgainstNull(url, nameof(url));
            Guard.AgainstNull(response, nameof(response));

            routes[Method.Post][url] = response;

            return this;
        }

        public Response MatchRequest(Request request)
        {
            var requestMethod = request.Method;
            var requestUrl = request.Url;

            if(!routes.ContainsKey(requestMethod)
                || !routes[requestMethod].ContainsKey(requestUrl))
            {
                return new NotFoundResponse();
            }

            return routes[requestMethod][requestUrl];
        }
    }
}
