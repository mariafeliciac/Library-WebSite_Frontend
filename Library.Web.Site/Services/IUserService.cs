using Library.Web.Site.Models.ModelDto.UserDto;

namespace Library.Web.Site.Services
{
    public interface IUserService
    {
        UserLoginResponse Login(string username, string password);
    }
}