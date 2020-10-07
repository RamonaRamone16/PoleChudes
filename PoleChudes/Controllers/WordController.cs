using Microsoft.AspNetCore.Mvc;
using PoleChudes.BLL.Services;

namespace PoleChudes.Controllers
{
    public class WordController : Controller
    {
        private readonly WordService _wordService;

        public WordController(WordService wordService)
        {
            _wordService = wordService;
        }

        public IActionResult GetAll()
        {
            var words = _wordService.GetAll();
            return View("", words);
        }

        public IActionResult Create()
        {
            var words = _wordService.GetAll();
            return View("", words);
        }
    }
}
