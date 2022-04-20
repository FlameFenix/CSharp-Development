using Cars_Market.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cars_Market.Areas.Moderator.Controllers
{
    [Authorize(Roles = "Moderator")]
    [Area("Moderator")]
    public abstract class ModeratorController : BaseController
    {
    }
}
