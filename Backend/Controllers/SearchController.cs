using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Backend.Models.Services;
using AutoMapper;
using System.Collections.Generic;
using Backend.DTOs;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly SearchService _searchService;
        public SearchController(IMapper mapper, SearchService searchService)
        {
            _searchService = searchService;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> Search(string word, int k)
        {
            return Ok(_mapper.Map<List<ScoreDto>>(await _searchService.FindKSearchResult(word, k)));
        }
    }
}
