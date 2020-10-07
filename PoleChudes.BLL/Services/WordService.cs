using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PoleChudes.DAL;
using PoleChudes.DAL.Entities;
using PoleChudes.Models.Models;
using PoleChudes.Models.Models.Word;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoleChudes.BLL.Services
{
    public class WordService : BaseService
    {
        public WordService(ApplicationDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public List<WordGetModel> GetAll()
        {
            var words = _context.Words;
            return _mapper.Map<List<WordGetModel>>(words);
        }

        //TODO: проверка на полномочия юзера
        public async Task Create(WordCreateModel model, string adminId)
        {
            var word = _mapper.Map<Word>(model);
            word.AdminId = adminId;
            await _context.AddAsync(word);
        }

        public async Task Update(int id, WordEditModel model)
        {
            var word = await _context.Words.FirstOrDefaultAsync(x => x.Id == id);

            _mapper.Map(model, word);

            await _context.UpdateAsync(word);
        }

        public async Task Delete(int id)
        {
            var word = await _context.Words.FirstOrDefaultAsync(x => x.Id == id);

            await _context.RemoveAsync(word);
        }
    }
}
