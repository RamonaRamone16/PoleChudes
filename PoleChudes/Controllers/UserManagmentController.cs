using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoleChudes.BLL.Services;
using PoleChudes.Models.Models.User;

namespace PoleChudes.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserManagmentController : Controller
    {
        private readonly UserManagmentService _userManagmentService;

        public UserManagmentController(UserManagmentService userManagmentService)
        {
            _userManagmentService = userManagmentService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userManagmentService.GetAllUsers();
            return View("Users", users);
        }

        [HttpGet]
        public IActionResult CreateForAdmin()
        {
            var model = _userManagmentService.GetUserCreateModel();
            return View("Create", model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateForAdmin(UserCreateModel model)
        {
            await _userManagmentService.CreateUserForAdmin(model);
            return RedirectToAction("GetAll");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Create()
        {
            return View("Register");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create(UserCreateModel model)
        {
            await _userManagmentService.CreateUser(model);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> UpdateGet(string id)
        {
            var model = await _userManagmentService.GetUserEditModel(id);
            return View("Update", model);
        }

        public async Task<IActionResult> UpdatePut(UserEditModel model)
        {
            await _userManagmentService.UpdateUser(model);
            return RedirectToAction("GetAll");
        }
    }
}
