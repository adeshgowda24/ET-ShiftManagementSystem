using AutoMapper;

namespace ET_ShiftManagementSystem.MapperProfiles
{
    public class ProjectDetails : Profile
    {
        public ProjectDetails() 
        {
            CreateMap<ShiftMgtDbContext.Entities.ProjectDetail, Models.ProjectDetailsDTO>().ReverseMap();
        }
    }
}
