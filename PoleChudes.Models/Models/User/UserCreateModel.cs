namespace PoleChudes.Models.Models.User
{
    public class UserCreateModel : UserEditModel
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
