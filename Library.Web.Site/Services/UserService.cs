using Library.Web.Site.Models.Interfaces;
using Library.Web.Site.Models.Model;
using Library.Web.Site.Models.ModelDto.UserDto;
using System.Net.Http;

namespace Library.Web.Site.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        public UserService(IHttpClientFactory httpClientFactory, IConfigurationRoot configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(configuration["Uri"]);
        }

        public UserLoginResponse Login(string username, string password)
        {
            try
            {
                UserLoginResponse userLogin = new UserLoginResponse();
                var request = new UserLoginRequest
                {
                    Username = username,
                    Password = password
                };
                var response = _httpClient.PostAsJsonAsync("api/User/Login", request).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var userResponse = response.Content.ReadFromJsonAsync<UserLoginResponse>().Result;
                    userLogin.UserId = userResponse.UserId;
                    userLogin.Username = userResponse.Username;
                    userLogin.Role = userResponse.Role;

                    return userLogin;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Server temporarily unreachable. Please try logging in again later.",ex);
            }

            return null;
        }
    }
}
