using Library.Web.Site.Models.ShareModels;

namespace Library.Web.Site.Models
{
    public class LoginModel
    {
        public string Username {  get; set; }
        public string Password { get; set; }
        public string ReturnUrl {  get; set; } = string.Empty;
    }
}
