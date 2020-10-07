using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PoleChudes.DAL;
using PoleChudes.DAL.Entities;
using System.Security.Principal;
using System.Threading.Tasks;

namespace PoleChudes.BLL.Services
{
    public class BaseService
    {
        protected readonly ApplicationDBContext _context;
        protected readonly IMapper _mapper;
        protected readonly UserManager<User> _userManager;
        public BaseService(ApplicationDBContext context, IMapper mapper, UserManager<User> userManager = null)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task IsAdmin(IIdentity identity)
        {
            var user = await _userManager.FindByNameAsync(identity.Name);
        }
    }
}
