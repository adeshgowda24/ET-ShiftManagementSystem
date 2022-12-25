using AutoMapper;

namespace ET_ShiftManagementSystem.MapperProfiles
{
    public class ShiftProfile : Profile
    {
        public ShiftProfile()
        {
            CreateMap<ShiftMgtDbContext.Entities.Shift , Models.ShiftDTO>().ReverseMap();
        }
    }
}
