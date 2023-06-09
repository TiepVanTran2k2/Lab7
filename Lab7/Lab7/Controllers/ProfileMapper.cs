using AutoMapper;

namespace Lab7.Controllers
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<Account, UserDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, ModelProductUpdate>().ReverseMap();
        }
    }
}
