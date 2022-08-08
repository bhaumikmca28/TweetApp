using AutoMapper;
using TweetApp.Data;
using TweetApp.Dtos;
using TweetApp.Models;

namespace TweetApp.Services
{
    public class TweetService : ITweetService
    {
        private readonly DataContext _context;

        public IMapper _mapper { get; }

        public TweetService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public ServiceResponse<int> AddTweet(AddTweetDto newTweet)
        {
            var response = new ServiceResponse<int>();
            Tweet tweet = _mapper.Map<Tweet>(newTweet);
            _context.Tweets.Add(tweet);
            _context.SaveChanges();

            response.Data = tweet.Id;
            response.Message = "New tweet added successfully";
            return response;
        }

        public ServiceResponse<Tweet> UpdateTweet(Tweet UpdatedTweet)
        {
            var response = new ServiceResponse<Tweet>();
            var tweet = _context.Tweets.FirstOrDefault(t => t.Id == UpdatedTweet.Id);
            if (tweet == null)
            {
                response.Success = false;
                response.Message = "Tweet not found!";
                return response;
            }
            tweet.TweetMsg = UpdatedTweet.TweetMsg;
            tweet.TweetDate = UpdatedTweet.TweetDate;
            tweet.User.Id = UpdatedTweet.User.Id;
            _context.Tweets.Update(tweet);
            _context.SaveChanges();

            response.Data = tweet;
            response.Message = "Tweet got updated successfully!";
            return response;
        }

        public ServiceResponse<Tweet> DeleteTweet(int id)
        {
            var response = new ServiceResponse<Tweet>();
            var tweet = _context.Tweets.FirstOrDefault(t => t.Id == id);
            if (tweet == null)
            {
                response.Success = false;
                response.Message = "Tweet not found!";
                return response;
            }
            _context.Tweets.Remove(tweet);
            _context.SaveChanges();

            response.Data = tweet;
            response.Message = "Tweet deleted successfully!";
            return response;
        }

        public ServiceResponse<List<Tweet>> GetAllTweets()
        {
            var response = new ServiceResponse<List<Tweet>>();
            response.Data = _context.Tweets.ToList();
            return response;
        }

        public ServiceResponse<List<Tweet>> GetAllTweetsByUserName(string userName)
        {
            var response = new ServiceResponse<List<Tweet>>();
            response.Data = _context.Tweets.Where(t => t.User.UserName == userName).ToList();
            return response;
        }

        public ServiceResponse<Tweet> GetTweetsById(int id)
        {
            var response = new ServiceResponse<Tweet>();
            response.Data = _context.Tweets.FirstOrDefault(t => t.Id == id);
            return response;
        }

    }
}
