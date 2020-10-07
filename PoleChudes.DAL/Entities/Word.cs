using System;
using System.Collections.Generic;
using System.Text;

namespace PoleChudes.DAL.Entities
{
    public class Word : BaseEntity<int>
    {
        public string Qestion { get; set; }
        public string Answer { get; set; }
        public string AdminId { get; set; }

        public User Admin { get; set; }
        public ICollection<Match> Matches { get; set; }
    }
}
