using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UserControl.Dtos;
using UserControl.Models;

namespace UserControl.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<PostUserDto, User>();
            CreateMap<User, IdentityUser<int>>();
        }
    }
}
