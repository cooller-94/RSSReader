using AutoMapper;
using Core.Models;
using Core.Models.RSS;
using Infrastructure.Models;

namespace Core.MapperProfiles
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<FeedDTO, Feed>().ReverseMap();

            CreateMap<Item, Post>().ReverseMap();
            CreateMap<Item, PostDTO>().ReverseMap();
            CreateMap<Post, PostDTO>().ReverseMap();

            CreateMap<Feed, FeedInformation>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Image == null ? null : src.Image.Url));
        }
    }
}
