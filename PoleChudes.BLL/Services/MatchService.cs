using AutoMapper;
using Microsoft.AspNetCore.Http;
using PoleChudes.DAL;
using PoleChudes.DAL.Entities;
using PoleChudes.Models.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace PoleChudes.BLL.Services
{
    public class MatchService : BaseService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MatchService(ApplicationDBContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public List<MatchGetModel> GetAll(string userId)
        {
            var matches = _context.Matches.Where(x => x.UserId == userId);
            return _mapper.Map<List<MatchGetModel>>(matches);
        }

        //TODO: проверка на полномочия юзера
        public async Task Create(string userId)
        {
            var match = new Match()
            {
                UserId = userId
            };
            await _context.AddAsync(match);
        }

        public async Task Update(IIdentity identity, int id, WordEditModel model)
        {
            var word = await _context.Matches.FirstOrDefaultAsync(x => x.Id == id);

            _mapper.Map(model, word);

            await _context.UpdateAsync(word);
        }

        public async Task Delete(int id)
        {
            var word = await _context.Matches.FirstOrDefaultAsync(x => x.Id == id);

            await _context.RemoveAsync(word);
        }
    }
}
