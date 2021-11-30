using Microsoft.AspNetCore.Mvc;
using Backend.Models.Repositories;
using Backend.Models.Services;
using System.Threading.Tasks;
using AutoMapper;
using System.Collections.Generic;
using Backend.DTOs;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClusteringController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly KmeansService _kmeansService;
        private readonly HierarchicalService _hierarchicalService;
        public ClusteringController(IUnitOfWork unitOfWork, KmeansService kmeansService, HierarchicalService hierarchicalService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _kmeansService = kmeansService;
            _hierarchicalService = hierarchicalService;
            _mapper = mapper;
        }

        [HttpGet("kmeans-iterations")]
        public async Task<IActionResult> GetKmeans(int k, int iterations)
        {
            return Ok(_mapper.Map<List<CentroidDto>>(await _kmeansService.FindKMeansCluster(k, iterations)));
        }

        [HttpGet("kmeans-self")]
        public async Task<IActionResult> GetKmeans(int k)
        {
            return Ok(_mapper.Map<List<CentroidDto>>(await _kmeansService.FindKMeansCluster(k)));
        }

        [HttpGet("hierarchical")]
        public Task<IActionResult> GetHierarchical()
        {
            return Task.FromResult(Ok());
        }
    }
}
