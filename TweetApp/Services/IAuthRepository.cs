using TweetApp.Models;

namespace TweetApp.Services
{
    public interface IAuthRepository
    {
        ServiceResponse<int> Register(User user, string password);
        ServiceResponse<string> Login(string username, string password);
        bool IsUserExist(string username);
    }
}
