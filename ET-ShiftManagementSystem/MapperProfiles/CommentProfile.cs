using AutoMapper;

namespace ET_ShiftManagementSystem.MapperProfiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<ShiftMgtDbContext.Entities.Comment, Models.CommentDTO>().ReverseMap();
        }
    }
}
