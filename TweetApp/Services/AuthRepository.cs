using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TweetApp.Data;
using TweetApp.Models;

namespace TweetApp.Services
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _Context;
        private readonly IConfiguration _Configuration;

        public AuthRepository(DataContext context, IConfiguration configuration)
        {
            _Context = context;
            _Configuration = configuration;
        }

        public ServiceResponse<int> Register(User user, string password)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            if (IsUserExist(user.UserName))
            {
                response.Success = false;
                response.Message = "User already exist";
                return response;
            }
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;
            user.Name = user.Name;
            user.SecurityQuestion = user.SecurityQuestion;
            user.SecurityAnswer = user.SecurityAnswer;
            _Context.User.Add(user);
            _Context.SaveChanges();

            response.Data = user.Id;
            response.Message = "User added successfully!";

            return response;
        }
        public ServiceResponse<string> Login(string username, string password)
        {
            var response = new ServiceResponse<string>();
            var user = _Context.User.FirstOrDefault(u => u.UserName.ToLower().Equals(username.ToLower()));
            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found";
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Worng Password!";
            }
            else
            {
                response.Data = CreateToken(user);
                response.Message = user.Name + " is loggedin Successfully!";
            }
            return response;
        }
        public ServiceResponse<int> ForgotPassword(User user, string password, string newPassword)
        {
            var response = new ServiceResponse<int>();
            var fetchedUser = _Context.User.FirstOrDefault(u => u.UserName.ToLower().Equals(user.UserName));
            if (fetchedUser == null)
            {
                response.Success = false;
                response.Message = "User not found";
            }
            else if (!VerifyPasswordHash(password, fetchedUser.PasswordHash, fetchedUser.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password!";
            }
            else
            {
                if ((!fetchedUser.SecurityQuestion.ToLower().Equals(user.SecurityQuestion.ToLower()))
                    && (!fetchedUser.SecurityAnswer.ToLower().Equals(user.SecurityAnswer.ToLower())))
                {
                    response.Success = false;
                    response.Message = "Wrong security question or answer!";
                    return response;
                }
                CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);
                fetchedUser.PasswordSalt = passwordSalt;
                fetchedUser.PasswordHash = passwordHash;
                _Context.User.Update(fetchedUser);
                _Context.SaveChanges();
                response.Data = fetchedUser.Id;
                response.Message = "Forgotted password reset successfully!";
            }
            return response;
        }

        public bool IsUserExist(string username)
        {
            if (_Context.User.Any(u => u.UserName.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.
                UTF8.GetBytes(_Configuration.GetSection("AppSettings:Token").Value));

            SigningCredentials cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = cred
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
