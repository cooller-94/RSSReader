using AutoMapper;
using Core.Models;
using Core.Models.RSS;
using Web.Models;

namespace Web.MappingProfiles
{
    public class ViewModelProfile : Profile
    {
        public ViewModelProfile()
        {
            CreateMap<CategoryDTO, string>().ConvertUsing(src => src?.Name);
            CreateMap<string, CategoryDTO>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src));

            CreateMap<FeedModel, FeedDTO>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.CategoryTitle))
                .ReverseMap()
                .ForMember(dest => dest.CategoryTitle, opt => opt.MapFrom(src => src.Category));

            CreateMap<PostDTO, PostModel>();
            CreateMap<SyncFeedResultModel, SyncFeedResult>();
            CreateMap<FeedInformation, FeedInformationModel>();
        }
    }
}
