using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PoleChudes.DAL;
using PoleChudes.DAL.Entities;
using PoleChudes.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoleChudes.BLL.Services
{
    public class MatchService : BaseService
    {
        private readonly Random _random;
        private readonly WordService _wordService;

        public MatchService(ApplicationDBContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, WordService wordService) : base(context, mapper, httpContextAccessor)
        {
            _random = new Random();
            _wordService = wordService;
        }

        public async Task<List<MatchGetModel>> GetAllByUserId(string userId)
        {
            var matches = await _context.Matches
                .Where(x => x.UserId == userId)
                .Include(x => x.Word)
                .ToListAsync();

            return _mapper.Map<List<MatchGetModel>>(matches);
        }

        public async Task<List<MatchModel>> GetAll()
        {
            var matches = await _context.Matches
                .Include(x => x.User)
                .Include(x => x.Word)
                .ToListAsync();

            return _mapper.Map<List<MatchModel>>(matches);
        }

        public async Task<bool> Create(string userId)
        {
            var lastWord = _context.Words.ToList().FindLast(x => x.Answer != null && x.Question != null);
            if (lastWord == null)
            {
                return false;
            }

            int rnd;

            Word word;

            do
            {
                rnd = _random.Next(lastWord.Id + 1);
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

            return true;
        }

        public async Task Update(bool success)
        {
            var word = await _context.Matches.FirstOrDefaultAsync(x => x.Id == MatchId);

            word.Successfully = success;
            word.Points = Points;

            await _context.UpdateAsync(word);
        }

        public async Task Delete(int id)
        {
            var word = await _context.Matches.FirstOrDefaultAsync(x => x.Id == id);

            await _context.RemoveAsync(word);
        }

        public bool CheckSuccessOneSymbol(string inputStr)
        {
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
                Points++;
                return true;
            }
            Points--;
            return false;
        }

        public async Task<bool> CheckSuccessWholeWord(string input)
        {
            if (Word.ToLower().Equals(input.ToLower()))
            {
                Points += 3;
                await Update(true);
                return true;
            }

            Points -= 3;
            await Update(false);
            return false;
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
                Points = Points,
                Word = _wordService.GetWordGetModel()
            };
        }

        public GameOverModel GetModelIfNotGuessTheWord()
        {
            return new GameOverModel()
            {
                Message = "Вы не угадали слово!",
                Signal = "Вы проирали!",
                Points = Points,
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
                Points = Points,
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
