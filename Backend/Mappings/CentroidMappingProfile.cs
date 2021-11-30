using AutoMapper;
using Backend.Models.Clustering;
using Backend.Models.Database;
using Backend.DTOs;
namespace Backend.Mappings
{
    public class CentroidMappingProfile : Profile
    {
        public CentroidMappingProfile()
        {
            CreateMap<Centroid, CentroidDto>().ReverseMap();
            CreateMap<Blog, BlogDto>().ReverseMap();
            CreateMap<Word, WordDto>().ReverseMap();
        }
    }
}