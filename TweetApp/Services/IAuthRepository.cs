using TweetApp.Models;

namespace TweetApp.Services
{
    public interface IAuthRepository
    {
        ServiceResponse<int> Register(User user, string password);
        ServiceResponse<string> Login(string username, string password);
        ServiceResponse<int> ForgotPassword(User user, string password, string newPassword);
        bool IsUserExist(string username);
    }
}
