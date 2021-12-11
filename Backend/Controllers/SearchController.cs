using Microsoft.AspNetCore.Mvc;
using Backend.Models.Repositories;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public SearchController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            return Ok(await _unitOfWork.Pages.GetAllAsync());
        }
    }
}
