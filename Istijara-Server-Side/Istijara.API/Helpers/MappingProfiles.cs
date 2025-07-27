using AutoMapper;
using Istijara.API.Dtos;
using Istijara.Core.DTOs.Identity;
using Istijara.Core.Entities;
using Istijara.Repository.Dtos;

namespace Istijara.API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<RegisterModel, ApplicationUser>()
                .ForMember(d => d.UserName, o => o.MapFrom<EmailToUserNameResolver>())
                .ForMember(d => d.Email, o => o.MapFrom(s => s.EmailOrPhone.Contains("@") ? s.EmailOrPhone : null))
                .ForMember(d => d.PhoneNumber, o => o.MapFrom(s => s.EmailOrPhone.Contains("@") ? null : s.EmailOrPhone));


            CreateMap<ItemDto, Item>();
            CreateMap<Item, ItemDto>();


            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryDto>();

        }
    }
}
