using Library.Web.Site.Models.ShareModels;

namespace Library.Web.Site.Models.ModelDto.UserDto
{
    public class UserLoginResponse : UserByUsernameResponse
    {
        public Role Role { get; set; }
    }
}
