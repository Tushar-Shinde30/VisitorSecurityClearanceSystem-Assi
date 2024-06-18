using AutoMapper;
using WebApplication1.DTO;
using WebApplication1.Entities;

namespace WebApplication1.Common
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<VisitorDTO, Entities.Visitor>().ReverseMap();
            CreateMap<OfficeDTO, Entities.OfficeEntity>().ReverseMap();
            CreateMap<SecurityEntity, SecurityDTO>().ReverseMap();
            CreateMap<ManagerEntity, ManagerDTO>().ReverseMap();
        }
    }
}