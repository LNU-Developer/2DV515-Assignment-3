using Microsoft.AspNetCore.Mvc;
using Backend.Models.Repositories;
using Backend.Models.Services;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClusteringController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ClusteringService _clusteringService;
        public ClusteringController(IUnitOfWork unitOfWork, ClusteringService clusteringService)
        {
            _unitOfWork = unitOfWork;
            _clusteringService = clusteringService;
        }

        [HttpGet("kmeans-iterations")]
        public async Task<IActionResult> GetKmeansAsync(int k, int iterations)
        {
            return Ok(await _clusteringService.FindKMeansCluster(k, iterations));
        }

        [HttpGet("kmeans-self")]
        public async Task<IActionResult> GetKmeansAsync(int k)
        {
            return Ok(await _clusteringService.FindKMeansCluster(k));
        }
    }
}
