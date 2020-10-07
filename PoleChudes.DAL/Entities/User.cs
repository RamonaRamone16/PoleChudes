using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace PoleChudes.DAL.Entities
{
    public class User : IdentityUser
    {
        public ICollection<Word> Words { get; set; }
        public ICollection<Match> Matches { get; set; }
    }
}
