using System.Threading.Tasks;
using Backend.Models.Repositories;
using Backend.Models.Clustering;
using System.Collections.Generic;
using Backend.Models.Database;
using System;
namespace Backend.Models.Services
{
    public class ClusteringService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ClusteringService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task FindKMeansCluster(int k)
        {
            var wordList = await _unitOfWork.Words.GetAllWordsWithWordReferences();
            var blogs = await _unitOfWork.Blogs.GetAllBlogsWithData();

            var centroids = CreateAndPlaceInitialCentroids(k, wordList);



        }

        public Centroid RecalculateCentroidCenter(Centroid centroid)
        {
            return centroid;

        }

        private List<Centroid> CreateAndPlaceInitialCentroids(int k, List<Word> wordList)
        {
            List<Centroid> centroids = new List<Centroid>();
            for (int c = 0; c < k; c++)
            {
                Centroid centroid = new Centroid();
                Random random = new Random();
                for (int w = 0; w < wordList.Count; w++)
                {
                    Word word = new Word
                    {
                        WordId = wordList[w].WordId,
                        WordTitle = wordList[w].WordTitle,
                        CurrentCount = random.Next(wordList[w].Min, wordList[w].Max)
                    };
                    centroid.Words.Add(word);
                }
                centroids.Add(centroid);
            }
            return centroids;
        }
    }
}