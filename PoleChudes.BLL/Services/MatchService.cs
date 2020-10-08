using AutoMapper;
using Microsoft.AspNetCore.Http;
using PoleChudes.DAL;
using PoleChudes.DAL.Entities;
using PoleChudes.Models.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoleChudes.BLL.Services
{
    public class MatchService : BaseService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private Random  random;
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
            var lastWord = _context.Words.First();
            random = new Random();
            int rnd;

            Word word;

            do
            {
                rnd = random.Next(lastWord.Id+1);
                word = _context.Words.FirstOrDefault(x => x.Id == rnd);
            } while (word == null);

            var match = new Match()
            {
                UserId = userId,
                WordId = rnd
            };

            SetHiddenWord(rnd, word);

            await _context.AddAsync(match);
        }

        public async Task Update(int id, WordEditModel model)
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

        public bool ChangeHiddenWord(string input)
        {
            var word = _httpContextAccessor.HttpContext.Session.GetString("Word");
            var hiddenWord = _httpContextAccessor.HttpContext.Session.GetString("HiddenWord");

            if (CheckSuccessWholeWord(word, input))
                return true;
            else if (CheckSuccessOneSymbol(hiddenWord, word, Char.Parse(input)))
                return true;

            return false;
        }

        public MatchGetModel GetMatchGetModel()
        {
            return new MatchGetModel()
            {
                Word = new WordGetModel()
                {
                    Question = _httpContextAccessor.HttpContext.Session.GetString("Question"),
                    Answer = _httpContextAccessor.HttpContext.Session.GetString("Word")
                },
                Points = _httpContextAccessor.HttpContext.Session.GetInt32("Points").Value,
                HiddenWord = _httpContextAccessor.HttpContext.Session.GetString("HiddenWord")
            };
        }

        private void SetHiddenWord(int wordId, Word word)
        {
            _httpContextAccessor.HttpContext.Session.SetInt32("WordId", wordId);
            _httpContextAccessor.HttpContext.Session.SetString("Word", word.Answer);
            _httpContextAccessor.HttpContext.Session.SetString("Question", word.Question);
            _httpContextAccessor.HttpContext.Session.SetInt32("Points", word.Answer.Length - 2);
            _httpContextAccessor.HttpContext.Session.SetString("HiddenWord", SetHiddenWord(word.Answer));
        }

        private string SetHiddenWord(string word)
        {
            string hid = String.Empty;
            for (int i = 0; i< word.Length; i++)
                hid += '*';
            return hid;
        }

        public bool CheckSuccessWholeWord(string word, string input)
        {
            if (word.Equals(input))
                return true;
            return false;
        }

        public bool CheckSuccessOneSymbol(string hiddenWord, string word, char input)
        {
            StringBuilder builder = new StringBuilder(hiddenWord);
            if (!hiddenWord.Contains(input) && word.Contains(input))
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] == input)
                    {
                        builder[i] = input;
                    }
                }
                _httpContextAccessor.HttpContext.Session.SetString("HiddenWord", builder.ToString());
                return true;
            }
            return false;
        }
    }
}
