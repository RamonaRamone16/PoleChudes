namespace PoleChudes.Models.Models
{
    public class GameOverModel
    {
        public string Message { get; set; }
        public string Signal { get; set; }
        public bool Success { get; set; }
        public WordGetModel Word { get; set; }
    }
}
