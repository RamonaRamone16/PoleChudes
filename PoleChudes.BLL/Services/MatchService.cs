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
        private Random random;
        private WordService _wordService;

        public MatchService(ApplicationDBContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, WordService wordService) : base(context, mapper, httpContextAccessor)
        {
            random = new Random();
            _wordService = wordService;
        }

        public List<MatchGetModel> GetAll(string userId)
        {
            var matches = _context.Matches.Where(x => x.UserId == userId);
            return _mapper.Map<List<MatchGetModel>>(matches);
        }

        public async Task Create(string userId)
        {
            var lastWord = _context.Words.First();
            int rnd;

            Word word;

            do
            {
                rnd = random.Next(lastWord.Id + 1);
                word = _context.Words.FirstOrDefault(x => x.Id == rnd);
            } while (word == null);

            var match = new Match()
            {
                UserId = userId,
                WordId = rnd
            };

            SetHiddenWord(rnd, word);

            var entity =  await _context.AddAsync(match);
            MatchId = entity.Entity.Id;
        }

        public async Task Update(bool success)
        {
            var word = _context.Matches.FirstOrDefault(x => x.Id == MatchId);

            word.Successfully = success;
            word.Points = Points;

            await _context.UpdateAsync(word);
        }

        public async Task Delete(int id)
        {
            var word = _context.Matches.FirstOrDefault(x => x.Id == id);

            await _context.RemoveAsync(word);
        }

        public bool CheckSuccessOneSymbol(string inputStr)
        {
            DecrementPoints();
            char input = Char.Parse(inputStr.ToLower());

            var word = Word;
            var hiddenWord = HiddenWord;

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
                HiddenWord = builder.ToString();
                return true;
            }
            return false;
        }

        public async Task<bool> CheckSuccessWholeWord(string input)
        {
            DecrementPoints();

            if (Word.ToLower().Equals(input.ToLower()))
            {
                await Update(true);
                return true;
            }

            await Update(false);
            return false;
        }

        public void DecrementPoints()
        {
            var points = Points - 1;
            Points = points;
        }

        public MatchGetModel GetMatchGetModel()
        {
            return new MatchGetModel()
            {
                Word = new WordGetModel()
                {
                    Question = Question,
                    Answer = Word
                },
                Points = Points,
                HiddenWord = HiddenWord
            };
        }

        public async Task<bool> CheckPoints()
        {
            if (Points == 0)
            {
                await Update(false);
                return false;
            }
            else
                return true;
        }

        public GameOverModel GetModelIfPointsEnded()
        {
            return new GameOverModel()
            {
                Message = "У вас закончились очки!",
                Signal = "Вы проирали!",
                Word = _wordService.GetWordGetModel()
            };
        }

        public GameOverModel GetModelIfNotGuessTheWord()
        {
            return new GameOverModel()
            {
                Message = "Вы не угадали слово!",
                Signal = "Вы проирали!",
                Word = _wordService.GetWordGetModel()
            };
        }


        public GameOverModel GetModelIfGuessTheWord()
        {
            return new GameOverModel()
            {
                Message = "Вы угадали загаданное слово!",
                Signal = "Вы выиграли!",
                Success = true,
                Word = _wordService.GetWordGetModel()
            };
        }

        private void SetHiddenWord(int wordId, Word word)
        {
            _httpContextAccessor.HttpContext.Session.SetInt32("WordId", wordId);
            _httpContextAccessor.HttpContext.Session.SetString("Word", word.Answer.ToLower());
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
    }
}
