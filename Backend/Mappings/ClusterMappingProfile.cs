using AutoMapper;
using Backend.Models.Clustering;
using Backend.Models.Database;
using Backend.DTOs;
namespace Backend.Mappings
{
    public class ClusteringMappingProfile : Profile
    {
        public ClusteringMappingProfile()
        {
            CreateMap<Cluster, ClusterDto>().ReverseMap();
            CreateMap<Blog, BlogDto>().ReverseMap();
        }
    }
}