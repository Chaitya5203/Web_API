using AutoMapper;
using WebAPIDemoRepositorys.Data;
using WebAPIDemoRepositorys.ViewModel;
namespace WebAPIDemo
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Villainfo, VillaDTO>().ReverseMap();
        }
    }
}