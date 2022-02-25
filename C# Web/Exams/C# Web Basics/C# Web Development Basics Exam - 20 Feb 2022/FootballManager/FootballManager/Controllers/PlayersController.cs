using FootballManager.Data;
using FootballManager.Data.Models;
using FootballManager.Services.Validator;
using FootballManager.ViewModels.Players;
using Microsoft.EntityFrameworkCore;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System.Linq;

namespace FootballManager.Controllers
{
    public class PlayersController : Controller
    {
        private FootballManagerDbContext data;
        private Validator validator;
        public PlayersController(FootballManagerDbContext _data,
            Validator _validator)
        {
            data = _data;
            validator = _validator;
        }

        [Authorize]
        public HttpResponse All()
        {
            var players = data.Players.ToList();

            return View(players);
        }

        [Authorize]
        public HttpResponse Add()
        {
            return View();
        }

        [HttpPost]
        
        public HttpResponse Add(AddPlayerViewModel model)
        {
            var errors = validator.ValidateAddPlayer(model);

            var isPlayerExists = data.Players.FirstOrDefault(x => x.FullName == model.FullName);

            if (isPlayerExists != null)
            {
                errors.Add("This player exists");
            }

            if (errors.Any())
            {
                return Redirect("Add");
            }

            Player player = new Player()
            {
                FullName = model.FullName,
                ImageUrl = model.ImageUrl,
                Position = model.Position,
                Speed = model.Speed,
                Endurance = model.Endurance,
                Description = model.Description,
            };

            data.Players.Add(player);
            data.SaveChanges();

            return View("All", data.Players.ToList());
        }

        [Authorize]
        public HttpResponse Collection()
        {
            var user = data.Users
                           .Include(x => x.UserPlayers)
                           .ThenInclude(x => x.Player)
                           .FirstOrDefault(x => x.Id == this.User.Id);

            var players = user.UserPlayers.Select(x => x.Player).ToList();

            return View(players);
        }

        [Authorize]
        public HttpResponse AddToCollection(string playerId)
        {
            var player = data.Players.FirstOrDefault(x => x.Id == int.Parse(playerId));

            var user = data.Users.Include(x => x.UserPlayers).FirstOrDefault(x => x.Id == this.User.Id);

            var userPlayerExists = user.UserPlayers.FirstOrDefault(x => x.PlayerId == int.Parse(playerId));

            if (userPlayerExists != null && user.UserPlayers.Contains(userPlayerExists))
            {
                return Redirect("All");
            }

            UserPlayer userPlayer = new UserPlayer()
            {
                PlayerId = int.Parse(playerId),
                UserId = this.User.Id
            };

            user.UserPlayers.Add(userPlayer);

            data.SaveChanges();

            return Redirect("All");
        }

        [Authorize]
        public HttpResponse RemoveFromCollection(string playerId)
        {
            var user = data.Users.Include(x => x.UserPlayers).ThenInclude(x => x.Player).FirstOrDefault(x => x.Id == this.User.Id);

            var currentPlayer = user.UserPlayers.Where(x => x.PlayerId == int.Parse(playerId)).FirstOrDefault();

            user.UserPlayers.Remove(currentPlayer);

            data.SaveChanges();

            return Redirect("Collection");
        }
    }
}
