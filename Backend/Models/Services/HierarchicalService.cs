using Backend.Models.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models.Clustering;
namespace Backend.Models.Services
{
    public class HierarchicalService
    {
        private readonly IUnitOfWork _unitOfWork;
        public HierarchicalService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public Task<List<Cluster>> FindHierarchicalCluster()
        {

        }
    }
}