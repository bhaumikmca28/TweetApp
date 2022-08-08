using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TweetApp.Dtos;
using TweetApp.Models;
using TweetApp.Services;

namespace TweetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TweetController : ControllerBase
    {
        private readonly ITweetService _tweetService;

        public TweetController(ITweetService tweetService)
        {
            _tweetService = tweetService;
        }

        [HttpGet("All")]
        public IActionResult Get()
        {
            return Ok(_tweetService.GetAllTweets());
        }

        [HttpGet("{userName}")]
        public IActionResult Get(string userName)
        {
            return Ok(_tweetService.GetAllTweetsByUserName(userName));
        }

        [HttpPost]
        public IActionResult Add(AddTweetDto newTweet)
        {
            var response = _tweetService.AddTweet(newTweet);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Update(Tweet updatedtweet)
        {
            var response = _tweetService.UpdateTweet(updatedtweet);
            if (response.Data == null)
                return NotFound(response);

            return Ok(response);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var response = _tweetService.DeleteTweet(id);
            if (response.Data == null)
                return NotFound(response);

            return Ok(response);
        }
    }
}
