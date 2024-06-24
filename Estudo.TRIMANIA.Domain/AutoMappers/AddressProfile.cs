using AutoMapper;
using Estudo.TRIMANIA.Domain.Commands.Users;
using Estudo.TRIMANIA.Domain.Entities;

namespace Estudo.TRIMANIA.Domain.AutoMappers
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<AddressRequest, Address>()
                ;
        }
    }
}
