using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TweetApp.Dtos;
using TweetApp.Models;
using TweetApp.Services;

namespace TweetApp.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/V1.0/tweets")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("Register")]
        public IActionResult Register(RegisterUserDto userDto)
        {
            var response = _authRepository.Register(
                new User
                {
                    UserName = userDto.UserName,
                    Name = userDto.Name,
                    SecurityQuestion = userDto.SecurityQuestion,
                    SecurityAnswer = userDto.SecurityAnswer
                }, userDto.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginUserDto userDto)
        {
            var response = _authRepository.Login(userDto.UserName, userDto.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("Forgot")]
        public IActionResult Forgot(ForgotUserDto userDto)
        {
            var response = _authRepository.ForgotPassword(
                new User
                {
                    UserName = userDto.UserName,
                    SecurityQuestion = userDto.SecurityQuestion,
                    SecurityAnswer = userDto.SecurityAnswer
                }, userDto.Password, userDto.NewPassword
                );
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
