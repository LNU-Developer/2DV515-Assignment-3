using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Backend.Models.Repositories;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClusteringController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ClusteringController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _unitOfWork.Words.GetAllAsync());
        }
    }
}
