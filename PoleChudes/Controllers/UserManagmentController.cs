using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PoleChudes.BLL.Services;
using PoleChudes.DAL.Entities;
using PoleChudes.Models.Models.User;

namespace PoleChudes.Controllers
{
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
        public IActionResult Create()
        {
            var model = _userManagmentService.GetUserCreateModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateModel model)
        {
            await _userManagmentService.CreateUser(model);
            return RedirectToAction("GetAll");
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
