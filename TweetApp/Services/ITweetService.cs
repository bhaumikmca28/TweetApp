using TweetApp.Dtos;
using TweetApp.Models;

namespace TweetApp.Services
{
    public interface ITweetService
    {
        ServiceResponse<List<Tweet>> GetAllTweets();
        ServiceResponse<List<Tweet>> GetAllTweetsByUserName(string UserName);
        ServiceResponse<Tweet> GetTweetsById(int id);
        ServiceResponse<int> AddTweet(AddTweetDto NewTweet);
        ServiceResponse<Tweet> UpdateTweet(Tweet UpdatedTweet);
        ServiceResponse<Tweet> DeleteTweet(int id);
    }
}
