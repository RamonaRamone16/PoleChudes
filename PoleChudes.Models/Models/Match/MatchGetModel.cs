using PoleChudes.Models.Models.Word;

namespace PoleChudes.Models.Models
{
    public class MatchGetModel : MatchCreateModel
    {
        public string UserName { get; set; }
        public WordGetModel Word { get; set; }
    }
}
