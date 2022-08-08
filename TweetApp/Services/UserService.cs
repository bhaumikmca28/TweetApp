using TweetApp.Data;
using TweetApp.Dtos;
using TweetApp.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace TweetApp.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public ServiceResponse<List<GetUserDto>> GetAllUsers()
        {
            var response = new ServiceResponse<List<GetUserDto>>();
            var dbUser = _context.User.ToList();
            response.Data = dbUser.Select(u => _mapper.Map<GetUserDto>(u)).ToList();
            return response;
        }

        public ServiceResponse<List<GetUserDto>> GetUsersByUserName(string userName)
        {
            var response = new ServiceResponse<List<GetUserDto>>();
            var dbUser = _context.User.Where(x => EF.Functions.Like(x.UserName, $"%{userName}%")).ToList();
            response.Data = dbUser.Select(u => _mapper.Map<GetUserDto>(u)).ToList();
            return response;
        }
    }
}
