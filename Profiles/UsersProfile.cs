using AutoMapper;
using Store.Dtos.Users;
using Store.Models;

namespace Store.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<UserCreateDto, User>();
            CreateMap<UserAuthenticateDto, User>();
            CreateMap<User, UserReadDto>();
            CreateMap<UserCreateDto, UserReadDto>();
        }
        
    }
}