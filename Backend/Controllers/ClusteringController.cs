using Microsoft.AspNetCore.Mvc;
using Backend.Models.Repositories;
using System.Linq;
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

        [HttpGet()]
        public IActionResult GetAllUsers()
        {
            var data = _unitOfWork.Blogs.GetAllBlogsWithDataReference();
            return Ok(data.FirstOrDefault());
        }

        [HttpGet("kmeans")]
        public async Task<IActionResult> GetKmeansAsync(int k, int iterations)
        {
            return Ok(await _clusteringService.FindKMeansCluster(k, iterations));
        }
    }
}
