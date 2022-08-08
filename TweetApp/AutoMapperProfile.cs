using AutoMapper;
using TweetApp.Dtos;
using TweetApp.Models;

namespace TweetApp
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, GetUserDto>();
            CreateMap<AddTweetDto,Tweet>();
            CreateMap<UpdateTweetDto,Tweet>();
        }
    }
}
