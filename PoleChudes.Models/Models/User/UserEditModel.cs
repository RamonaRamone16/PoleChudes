using Microsoft.AspNetCore.Mvc.Rendering;

namespace PoleChudes.Models.Models.User
{
    public class UserEditModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }

        public SelectList Roles { get; set; }
    }
}
