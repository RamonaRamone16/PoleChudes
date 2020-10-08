namespace PoleChudes.Models.Models
{
    public class MatchGetModel : MatchCreateModel
    {
        public string HiddenWord { get; set; }
        public WordGetModel Word { get; set; }
    }
}
