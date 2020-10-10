using AutoMapper;
using Microsoft.AspNetCore.Http;
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
        protected readonly IHttpContextAccessor _httpContextAccessor;
        public BaseService(ApplicationDBContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager = null)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public int WordId
        {
            get => _httpContextAccessor.HttpContext.Session.GetInt32("WordId").Value;
            set => _httpContextAccessor.HttpContext.Session.SetInt32("WordId", value);
        }

        public int MatchId
        {
            get => _httpContextAccessor.HttpContext.Session.GetInt32("MatchId").Value;
            set => _httpContextAccessor.HttpContext.Session.SetInt32("MatchId", value);
        }

        public string Word
        {
            get => _httpContextAccessor.HttpContext.Session.GetString("Word");
            set => _httpContextAccessor.HttpContext.Session.SetString("Word", value);
        }

        public string Question
        {
            get => _httpContextAccessor.HttpContext.Session.GetString("Question");
            set => _httpContextAccessor.HttpContext.Session.SetString("Question", value);
        }

        public int Points
        {
            get => _httpContextAccessor.HttpContext.Session.GetInt32("Points").Value;
            set => _httpContextAccessor.HttpContext.Session.SetInt32("Points", value);
        }

        public string HiddenWord
        {
            get => _httpContextAccessor.HttpContext.Session.GetString("HiddenWord");
            set => _httpContextAccessor.HttpContext.Session.SetString("HiddenWord", value);
        }
    }
}
