using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using lesavrilshop_be.Core.DTOs.Users;
using lesavrilshop_be.Core.Entities.Users;

namespace lesavrilshop_be.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Address, AddressDto>();
            CreateMap<UserAddress, UserAddressDto>()
                .ForMember(dest => dest.Address,
                    opt => opt.MapFrom(src => src.Address));
            CreateMap<UpdateUserAddressDto, UserAddress>();
        }
    }
}