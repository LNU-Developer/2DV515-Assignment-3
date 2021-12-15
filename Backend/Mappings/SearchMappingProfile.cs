using AutoMapper;
using Backend.Models.Database;
using Backend.Models.Search;
using Backend.DTOs;
namespace Backend.Mappings
{
    public class SearchMappingProfile : Profile
    {
        public SearchMappingProfile()
        {
            CreateMap<Page, PageDto>().ReverseMap();
            CreateMap<Score, ScoreDto>().ReverseMap();
        }
    }
}