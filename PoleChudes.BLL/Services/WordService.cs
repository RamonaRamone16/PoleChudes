using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PoleChudes.DAL;
using PoleChudes.DAL.Entities;
using PoleChudes.Models.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PoleChudes.BLL.Services
{
    public class WordService : BaseService
    {
        public WordService(ApplicationDBContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor)
        {
        }

        public List<WordModel> GetAll()
        {
            var words = _context.Words
                .ToList();

            return _mapper.Map<List<WordModel>>(words);
        }

        public async Task Create(WordCreateModel model, string adminId)
        {
            var word = _mapper.Map<Word>(model);
            word.AdminId = adminId;
            await _context.AddAsync(word);
        }

        public async Task Update(WordEditModel model)
        {
            var word = _context.Words.FirstOrDefault(x => x.Id == model.Id);

            _mapper.Map(model, word);

            await _context.UpdateAsync(word);
        }

        public async Task<WordEditModel> GetWordEditModel(int id)
        {
            var word = _context.Words.FirstOrDefault(x => x.Id == id);
            return _mapper.Map<WordEditModel>(word);
        }

        public async Task Delete(int id)
        {
            var word = _context.Words.FirstOrDefault(x => x.Id == id);

            await _context.RemoveAsync(word);
        }

        public WordGetModel GetWordGetModel()
        {
            return new WordGetModel()
            {
                Question = Question,
                Answer = Word
            };
        }
    }
}
