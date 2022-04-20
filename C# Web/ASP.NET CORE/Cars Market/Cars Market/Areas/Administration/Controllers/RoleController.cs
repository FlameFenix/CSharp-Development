using Cars_Market.Core.Services;
using Cars_Market.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Cars_Market.Areas.Administration.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RoleController : AdministrationController
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SellerService sellerService;
        public RoleController
            (RoleManager<IdentityRole> _roleManager,
            UserManager<IdentityUser> _userManager,
            SellerService _sellerService)
        {
            roleManager = _roleManager;
            userManager = _userManager;
            sellerService = _sellerService;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await roleManager.Roles.ToListAsync();

            return View(roles);
        }

        public async Task<IActionResult> Create() => View(new IdentityRole());

        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            await roleManager.CreateAsync(role);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if(role == null)
            { 
                ViewBag.ErrorTitle = "Error occured while trying to delete role";
                ViewBag.ErrorMessage = "The resource you are looking for (or one of its dependencies) could have been removed," +
                " had its name changed, or is temporarily unavailable." +
                " Please review the following URL and make sure that it is spelled correctly.";
            return View("Error");
            }

            await roleManager.DeleteAsync(role);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddRoleToUser()
        {
            ViewBag.Roles = await roleManager.Roles.OrderBy(x => x.Name).ToListAsync();
            ViewBag.Users = await userManager.Users.ToListAsync();

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddRoleToUser(AddRolesToUserFormModel rolesModel)
        {
            var email = rolesModel.UserEmail;

            var user = await userManager.FindByEmailAsync(email);

            var userRolesActive = await userManager.GetRolesAsync(user);

            await userManager.RemoveFromRolesAsync(user, userRolesActive);

            await userManager.AddToRolesAsync(user, rolesModel.SelectedRoles);

            return RedirectToAction("AddRoleToUser");
        }

    }
}
