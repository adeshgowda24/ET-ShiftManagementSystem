using AutoMapper;

namespace ET_ShiftManagementSystem.MapperProfiles
{
    public class ProjectProfiles : Profile
    {
        public ProjectProfiles()
        {
            CreateMap<ShiftMgtDbContext.Entities.Project , Models.ProjectDto>().ReverseMap();
        }
    }
}
