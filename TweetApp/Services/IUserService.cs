using TweetApp.Dtos;
using TweetApp.Models;

namespace TweetApp.Services
{
    public interface IUserService
    {
        ServiceResponse<List<GetUserDto>> GetAllUsers();
        ServiceResponse<List<GetUserDto>> GetUsersByUserName(string userName);
    }
}
