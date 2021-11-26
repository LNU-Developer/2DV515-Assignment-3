using System.Threading.Tasks;
using Backend.Models.Repositories;
using Backend.Models.Clustering;
using System.Collections.Generic;
using Backend.Models.Database;
using System;
using System.Linq;
namespace Backend.Models.Services
{
    public class ClusteringService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ClusteringService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task FindKMeansCluster(int k, int iterations)
        {
            var wordList = await _unitOfWork.Words.GetAllWordsWithWordReferences();
            var blogs = _unitOfWork.Blogs.GetAllBlogsWithDataReference();

            var centroids = CreateAndPlaceInitialCentroids(k, wordList);
            foreach (var item in centroids)
            {
                var replacedCentroids = RecalculateCentroidCenter(item);
            }



        }

        public Centroid RecalculateCentroidCenter(Centroid c)
        {
            //Find average count for each word
            for (int k = 0; k < c.Words.Count; k++)
            {
                double avg = 0;

                //Iterate over all blogs assigned to this centroid
                WordReference wordReference = null;
                foreach (Blog b in c.Assignments)
                {
                    wordReference = b.WordReferences.FirstOrDefault(i => i.WordId == c.Words[k].WordId);
                    avg += wordReference.Count;
                }
                avg /= c.Assignments.Count;

                //Update word count for the centroid
                wordReference.Count = avg;
            }
            return c;

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
                        WordReferences = wordList[w].WordReferences,
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