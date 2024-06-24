using AutoMapper;
using Estudo.TRIMANIA.Domain.Commands.Admins;
using Estudo.TRIMANIA.Domain.Commands.Users;
using Estudo.TRIMANIA.Domain.Entities;

namespace Estudo.TRIMANIA.Domain.AutoMappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<SignUpRequest, User>()
                .ForMember(user => user.Address, m => m.Ignore())
                .ForMember(user => user.Password, m => m.Ignore())
                ;

            CreateMap<User, GetUserResponse>();
        }
    }
}
