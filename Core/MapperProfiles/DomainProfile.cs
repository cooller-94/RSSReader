using AutoMapper;
using Core.Models;
using Core.Models.RSS;
using Infrastructure.Models;
using Image = Core.Models.RSS.Image;

namespace Core.MapperProfiles
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<FeedDTO, Feed>().ReverseMap();
            CreateMap<Models.RSS.Image, ImageDTO>().ReverseMap();

            CreateMap<Item, Post>().ReverseMap();
            CreateMap<Item, PostDTO>().ReverseMap();
            CreateMap<Post, PostDTO>().ReverseMap();
            CreateMap<Channel, FeedDTO>();

            CreateMap<Feed, FeedInformation>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Image == null ? null : src.Image.Url));


            CreateMap<UserFeed, FeedInformation>()
                .ForMember(i => i.Title, opt => opt.MapFrom(src => src.Feed.Title))
                .ForMember(i => i.Description, opt => opt.MapFrom(src => src.Feed.Description))
                .ForMember(i => i.Url, opt => opt.MapFrom(src => src.Feed.Url))
                .ForMember(i => i.Category, opt => opt.MapFrom(src => src.Category == null ? null : src.Category.Name))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Feed.Image == null ? null : src.Feed.Image));

            CreateMap<Category, CategoryDTO>().ReverseMap();

            CreateMap<WebFeedUrl, FeedDTO>().ForMember(i => i.Image, opt => opt.MapFrom(src => src.IconUrl));

            CreateMap<string, ImageDTO>()
                .ForMember(i => i.Url, opt => opt.MapFrom(src => src));
        }
    }
}
