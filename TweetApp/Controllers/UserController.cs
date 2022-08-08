using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TweetApp.Services;

namespace TweetApp.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/V1.0/tweets/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            return Ok(_userService.GetAllUsers());
        }

        [HttpGet("search/{userName}")]
        public IActionResult Get(string userName)
        {
            return Ok(_userService.GetUsersByUserName(userName));
        }
    }
}
