using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PoleChudes.DAL;
using PoleChudes.DAL.Entities;
using PoleChudes.Models.Enums;
using PoleChudes.Models.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoleChudes.BLL.Services
{
    public class UserManagmentService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly ApplicationDBContext _context;

        public UserManagmentService(ApplicationDBContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public List<UserGetModel> GetAllUsers()
        {
            var users = _context.Users.Include(x => x.Matches);
            return _mapper.Map<List<UserGetModel>>(users);
        }

        public async Task CreateUserForAdmin(UserCreateModel model)
        {
            User user = _mapper.Map<User>(model);
            await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, model.RoleName);
        }

        public async Task CreateUser(UserCreateModel model)
        {
            User user = _mapper.Map<User>(model);
            await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, Roles.User.ToString());
        }

        public UserCreateModel GetUserCreateModel()
        {
            return new UserCreateModel()
            {
                Roles = GetRolesSelectList()
            };
        }

        public async Task UpdateUser(UserEditModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            _mapper.Map(model, user); 
            await _userManager.UpdateAsync(user);
            
            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles);

            await _userManager.AddToRoleAsync(user, model.RoleName);
        }

        public async Task<UserEditModel> GetUserEditModel(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var model = _mapper.Map<UserEditModel>(user);
            var roles = await _userManager.GetRolesAsync(user);
            model.Roles = GetRolesSelectList();
            model.RoleName = roles[0];
            return model;
        }

        private SelectList GetRolesSelectList()
        {
            return new SelectList(_roleManager.Roles, "Name", "Name");
        }
    }
}
