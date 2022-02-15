namespace SharedTrip
{
    using MyWebServer;
    using MyWebServer.Controllers;
    using MyWebServer.Results.Views;
    using SharedTrip.Data;
    using SharedTrip.Services.HashPassword;
    using SharedTrip.Services.Validator;
    using System.Threading.Tasks;

    public class Startup
    {
        public static async Task Main()
            => await HttpServer
                .WithRoutes(routes => routes
                    .MapStaticFiles()
                    .MapControllers())
                .WithServices(services => services
                    .Add<IViewEngine, CompilationViewEngine>()
                    .Add<IValidator, Validator>()
                    .Add<IHashPassword, HashPassword>()
                    .Add<ApplicationDbContext>())
                .Start();
    }
}
