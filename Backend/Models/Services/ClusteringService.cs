using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Backend.Models.Database;
using Backend.Models.Repositories;
using Backend.Models.Clustering;
using System.Linq;
namespace Backend.Models.Services
{
    public class ClusteringService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ClusteringService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public async Task<List<CentroidDto>> FindKMeansCluster(int k, int iterations)
        {
            var wordList = await _unitOfWork.Words.GetDistinctWords();
            var blogs = await _unitOfWork.Blogs.GetAllBlogsWithData();
            var centroids = CreateAndPlaceInitialCentroids(k, wordList);

            for (int i = 0; i < iterations; i++)
            {
                centroids.ForEach(c => ClearCentroidAssignments(c));
                blogs.ForEach(b => AssignBlogsToClosestCentroid(b, centroids));
                centroids.ForEach(c => RecalculateCentroidCenter(c));
            }
            return CreateCentroidDtoObject(centroids);
        }
        public async Task<List<CentroidDto>> FindKMeansCluster(int k)
        {
            var wordList = await _unitOfWork.Words.GetDistinctWords();
            var blogs = await _unitOfWork.Blogs.GetAllBlogsWithData();
            var centroids = CreateAndPlaceInitialCentroids(k, wordList);
            var oldCentroids = new List<Centroid>();

            while (true)
            {
                centroids.ForEach(c => ClearCentroidAssignments(c));
                blogs.ForEach(b => AssignBlogsToClosestCentroid(b, centroids));
                centroids.ForEach(c => RecalculateCentroidCenter(c));

                if (centroids == oldCentroids) break;
                else oldCentroids = centroids;
            }
            return CreateCentroidDtoObject(centroids);
        }
        private List<CentroidDto> CreateCentroidDtoObject(List<Centroid> centroids)
        {
            var centroidsDto = new List<CentroidDto>();
            foreach (var c in centroids)
            {
                var tmpC = new CentroidDto();
                tmpC.Assignments = new List<BlogDto>();
                foreach (var blog in c.Assignments)
                {
                    tmpC.Assignments.Add(new BlogDto { BlogTitle = blog.BlogTitle });
                }
                centroidsDto.Add(tmpC);
            }
            return centroidsDto;
        }
        private void AssignBlogsToClosestCentroid(Blog b, List<Centroid> centroids)
        {
            double distance = Double.MaxValue;
            Centroid best = new Centroid();

            foreach (Centroid c in centroids)
            {
                double cDist = PearsonCentroid(c, b);
                if (cDist < distance)
                {
                    best = c;
                    distance = cDist;
                }
            }
            best.Assignments.Add(b);
        }
        private Centroid RecalculateCentroidCenter(Centroid c)
        {
            //Find average count for each word
            foreach (var word in c.Words)
            {
                double average = 0;
                foreach (var b in c.Assignments)
                    average += word.Amount;
                average /= c.Assignments.Count;
                //Update count for the centroid
                word.Amount = average;
            }
            return c;
        }
        private List<Centroid> CreateAndPlaceInitialCentroids(int k, List<Word> wordList)
        {
            List<Centroid> centroids = new List<Centroid>();
            for (int c = 0; c < k; c++)
            {
                var centroid = new Centroid();
                var random = new Random();
                centroid.Words = new List<WordDto>();
                centroid.Assignments = new List<Blog>();
                foreach (var word in wordList)
                {
                    var randomValue = random.Next(word.Min, word.Max);
                    centroid.Words.Add(new WordDto
                    {
                        WordTitle = word.WordTitle,
                        Amount = randomValue
                    });
                }
                centroids.Add(centroid);
            }
            return centroids;
        }
        private void ClearCentroidAssignments(Centroid c) => c.Assignments.Clear();
        private double PearsonCentroid(Centroid A, Blog B)
        {
            double sumA = 0, sumB = 0, sumAsq = 0, sumBsq = 0, pSum = 0;
            int n = 0;  //Counter for amount of total words

            foreach (var word in A.Words)
            {
                var wA = word;
                var wB = B.Words.ElementAt(n); // Order repositories in a way that enables me to use ElementAt O(1) instead of potentually looping all words in O(n).
                sumA += wA.Amount;
                sumB += wB.Amount;
                sumAsq += wA.Amount * wA.Amount;
                sumBsq += wB.Amount * wB.Amount;
                pSum += wA.Amount * wB.Amount;
                n++;
            }
            //Calculate
            double num = pSum - ((sumA * sumB) / n);
            double den = Math.Sqrt((sumAsq - Math.Pow(sumA, 2.0) / n) * (sumBsq - Math.Pow(sumB, 2.0) / n));
            return 1.0 - num / den;
        }
    }
}