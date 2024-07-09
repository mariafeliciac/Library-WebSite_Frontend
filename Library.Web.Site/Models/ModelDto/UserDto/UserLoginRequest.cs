namespace Library.Web.Site.Models.ModelDto.UserDto
{
    public class UserLoginRequest : UserByUsernameResponse
    {
        public string Password { get; set; }

    }
}
