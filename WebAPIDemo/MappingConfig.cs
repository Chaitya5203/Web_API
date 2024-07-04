using AutoMapper;
using WebAPIDemoRepositorys.Data;
using WebAPIDemoRepositorys.ViewModel;
namespace WebAPIDemo
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<VillaDTO, Villainfo>()
                .ForMember(dest => dest.Createddate, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Modifieddate, opt => opt.MapFrom(e=>DateTime.Now)); 
            CreateMap<Villainfo, VillaDTO>();
        }
    }
}