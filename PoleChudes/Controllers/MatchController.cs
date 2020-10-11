using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PoleChudes.BLL.Services;
using PoleChudes.DAL.Entities;
using System.Threading.Tasks;

namespace PoleChudes.Controllers
{
    [Authorize]
    public class MatchController : Controller
    {
        private readonly MatchService _matchService;
        private readonly UserManager<User> _userManager;

        public MatchController(MatchService matchService, UserManager<User> userManager)
        {
            _matchService = matchService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByUserId()
        {
            var user = await _userManager.GetUserAsync(User);
            var matches = await _matchService.GetAllByUserId(user.Id);
            return View("AllMatchesForUser", matches);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var matches = await _matchService.GetAll();
            return View("AllMatchesForAdmin", matches);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);

            if (await _matchService.Create(user.Id))
            {
                var model = _matchService.GetMatchGetModel();
                return View("Game", model);
            }
            else
                return View("NoGame");
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

        [HttpPost]
        public async Task<IActionResult> CheckWholeWord(string input)
        {
            if (await _matchService.CheckSuccessWholeWord(input))
                return PartialView("_GameOver", _matchService.GetModelIfGuessTheWord());
            else
                return PartialView("_GameOver", _matchService.GetModelIfNotGuessTheWord());
        }

        [HttpGet]
        public IActionResult DecrementPoints()
        {
            _matchService.Points--;
            var model = _matchService.GetMatchGetModel();

            return PartialView("_Game", model);
        }

        [HttpGet]
        public async Task<IActionResult> CheckPoints()
        {
            if (await _matchService.CheckPoints())
                return NoContent();
            else
                return PartialView("_GameOver", _matchService.GetModelIfPointsEnded());
        }


        [HttpGet]
        public IActionResult CheckHiddenWord()
        {
            if (_matchService.HiddenWord.ToLower().Equals(_matchService.Word.ToLower()))
                return PartialView("_GameOver", _matchService.GetModelIfGuessTheWord());
            else
                return NoContent();
        }
    }
}
