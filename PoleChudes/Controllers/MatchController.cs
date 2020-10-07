using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PoleChudes.BLL.Services;
using PoleChudes.DAL.Entities;
using System.Threading.Tasks;

namespace PoleChudes.Controllers
{
    public class MatchController : Controller
    {
        private readonly MatchService _matchService;
        private readonly UserManager<User> _userManager;

        public MatchController(MatchService matchService, UserManager<User> userManager)
        {
            _matchService = matchService;
            _userManager = userManager;
        }

        public async Task<IActionResult> GetAll()
        {
            var user = await _userManager.GetUserAsync(User);
            var matches = _matchService.GetAll(user.Id);
            return View("AllMatches", matches);
        }

        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            //var match = _matchService.GetMatch(user.Id);

            //HttpContext.Session.SetString("TempWord", value);

            return View();
        }

        public async Task<IActionResult> GetGame()
        {
            return View();
        }
    }
}
