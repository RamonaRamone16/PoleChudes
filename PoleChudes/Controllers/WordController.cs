using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PoleChudes.BLL.Services;
using PoleChudes.DAL.Entities;
using PoleChudes.Models.Models;
using System.Threading.Tasks;

namespace PoleChudes.Controllers
{
    public class WordController : Controller
    {
        private readonly WordService _wordService;
        private readonly UserManager<User> _userManager;

        public WordController(WordService wordService, UserManager<User> userManager)
        {
            _wordService = wordService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var words = _wordService.GetAll();
            return View("AllWords", words);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(WordCreateModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            await _wordService.Create(model, user.Id);
            return RedirectToAction("GetAll");
        }

        public async Task<IActionResult> UpdateGet(int id)
        {
            var model = await _wordService.GetWordEditModel(id);
            return View("Update", model);
        }

        public async Task<IActionResult> UpdatePut(WordEditModel model)
        {
            await _wordService.Update(model);
            return RedirectToAction("GetAll");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _wordService.Delete(id);
            return RedirectToAction("GetAll");
        }
    }
}
