using BlogApi.Models;
using BlogApi.ViewModels;

namespace BlogApi.Helpers
{
    public class MapperProfile : AutoMapper.Profile
    {
        public MapperProfile()
        {
            CreateMap<Post, PostViewModel>().ReverseMap();
            CreateMap<Term, TermViewModel>().ReverseMap();
            CreateMap<Comment, CommentViewModel>().ReverseMap();
            CreateMap<Notification, NotificationViewModel>().ReverseMap();
        }
    }
}
