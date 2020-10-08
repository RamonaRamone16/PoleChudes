using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PoleChudes.BLL.Services;
using PoleChudes.DAL.Entities;
using System;
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
        public IActionResult CheckOneSymbol(string input)
        {
            var word = HttpContext.Session.GetString("Word");
            var hiddenWord = HttpContext.Session.GetString("HiddenWord");
            var model = _matchService.GetMatchGetModel();

            if (_matchService.CheckSuccessOneSymbol(hiddenWord, word, Char.Parse(input)))
                model = _matchService.GetMatchGetModel();

            return PartialView("_Game", model);
        }

        public IActionResult CheckWholeWord(string input)
        {
            var word = HttpContext.Session.GetString("Word");
            if (_matchService.CheckSuccessWholeWord(word, input))
                return View("Winner");
            else
                return View("GameOver");
        }

        public IActionResult DecrementPoints()
        {
            var points = HttpContext.Session.GetInt32("Points").Value - 1;
            HttpContext.Session.SetInt32("Points", points);

            var model = _matchService.GetMatchGetModel();

            return PartialView("_Game", model);
        }
    }
}
