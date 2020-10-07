namespace PoleChudes.DAL.Entities
{
    public class Match : BaseEntity<int>
    {
        public bool Successfully { get; set; }
        public int Points { get; set; }
        public string UserId { get; set; }
        public int WordId { get; set; }

        public User User { get; set; }
        public Word Word { get; set; }
    }
}
