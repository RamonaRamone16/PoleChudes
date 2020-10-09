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

        [HttpPost]
        public async Task<IActionResult> GetAll()
        {
            var user = await _userManager.GetUserAsync(User);
            var matches = _matchService.GetAll(user.Id);
            return View("AllMatches", matches);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);

            await _matchService.Create(user.Id);
            var model = _matchService.GetMatchGetModel();

            return View("Game", model);
        }

        [HttpGet]
        public IActionResult GetMatchGetModel()
        {
            var model = _matchService.GetMatchGetModel();

            return PartialView("_Game", model);
        }

        [HttpGet]
        public IActionResult CheckOneSymbol(string input)
        {
            var model = _matchService.GetMatchGetModel();

            if (_matchService.CheckSuccessOneSymbol(input))
                model = _matchService.GetMatchGetModel();

            return PartialView("_Game", model);
        }

        public async Task<IActionResult> CheckWholeWord(string input)
        {
            if (await _matchService.CheckSuccessWholeWord(input))
                return PartialView("_Winner");
            else
                return PartialView("_GameOver");
        }

        public IActionResult DecrementPoints()
        {
            _matchService.DecrementPoints();
            var model = _matchService.GetMatchGetModel();

            return PartialView("_Game", model);
        }
    }
}
